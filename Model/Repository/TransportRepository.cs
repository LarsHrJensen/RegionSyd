using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RegionSyd.Model.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;
using System.Reflection.Metadata;
using RegionSyd.Model.Store;

namespace RegionSyd.Model.Repository
{
    internal class TransportRepository : IRepository<Transport>
    {
        SqlConnection _connection;
        string _connectionString;

        string _hasTransportTable;

        IConfiguration _configuration;

        string _tableName;
        // I f*ing hate dependency injection, give me lethal injection instead
        public TransportRepository(IConfiguration config, string tableName = "Transports", string hasTransportTableName = "AssignedAmbulances")
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("SQLAuth");
            _tableName = tableName;
            _hasTransportTable = hasTransportTableName;
        }

        public void Delete(Transport entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transport> GetAll()
        {
            string commandText = $"SELECT * FROM {_tableName}";

            List<Transport> result = new();

            // Outer new = New List<Hospital>
            // Inner new = GetAll() from HospitalRepository
            List<Hospital> hospitals = new(new HospitalRepository(_configuration).GetAll());
            List<Patient> patients = new(new PatientRepository(_configuration).GetAll());
            // rewrite upper into GetById();

            using (SqlConnection connection = new(_connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = commandText;

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Predicate<Hospital> findFromHospital = (hospital) =>
                        {
                            return (hospital.Id == (int)reader["From"]);
                        };

                        Hospital fromHospital = hospitals.Find(findFromHospital);

                        Predicate<Hospital> findToHospital = (hospital) =>
                        {
                            return (hospital.Id == (int)reader["To"]);
                        };

                        Hospital toHospital = hospitals.Find(findToHospital);

                        if (fromHospital is null || toHospital is null)
                        {
                            throw new Exception("Something fucky wucky happend.");
                        }

                        Predicate<Patient> findPatient = (patient) =>
                        {
                            return (patient.Id == (int)reader["Id"]);
                        };

                        Patient patient = patients.Find(findPatient);

                        int id = reader.GetInt32("Id");

                        Transport transport = new(
                            fromHospital, toHospital, (DateTime)reader["Arrival"], patient, 0, id
                            );


                        result.Add(transport);
                    }
                }
            }

            return result;
        }

        public Transport GetById(int id)
        {
            string commandText = $"SELECT * FROM {_tableName} WHERE [Id]=@Id";


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                connection.Open();

                int transportId;
                Hospital startHospital, destinationHospital;
                Patient patient;
                DateTime arrivalTime;

                Transport transport;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new Exception("No results found");
                    }

                    transportId = reader.GetInt32("Id");

                    HospitalRepository hospitalRepo = new(_configuration);
                    PatientRepository patientRepo = new(_configuration);

                    int startId, destinationId, patientId;

                    Debug.WriteLine("gets here");

                    startId = reader.GetInt32("From");
                    destinationId = reader.GetInt32("To");
                    patientId = reader.GetInt32("Patient");

                    startHospital = hospitalRepo.GetById(startId);
                    destinationHospital = hospitalRepo.GetById(destinationId);
                    patient = patientRepo.GetById(patientId);

                    arrivalTime = reader.GetDateTime("Arrival");

                    transport = new(startHospital, destinationHospital, arrivalTime, patient, 0, transportId);


                    if (reader.Read())
                    {
                        throw new Exception("Too many results found");
                    }

                    return transport;
                }


            }
        }

        public IEnumerable<Transport> GetByAmbulance(Ambulance ambulance)
        {
            string commandText = $"SELECT * FROM {_hasTransportTable} WHERE [Ambulance]=@Ambulance";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add(new SqlParameter("@Ambulance", SqlDbType.Int) { Value = ambulance.Id });
                connection.Open();
                List<Transport> result = new List<Transport>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int transportId = (int)reader["Transport"];
                        result.Add(GetById(transportId));
                    }
                }
                return result;
            }
        }

        public void Insert(Transport entity)
        {
            string commandText = $"INSERT INTO {_tableName}([From], [To], [Patient], [Arrival]) VALUES (@From, @To, @Patient, @Arrival)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add(new SqlParameter("@From", SqlDbType.Int)              { Value = entity.StartHospital.Id });
                command.Parameters.Add(new SqlParameter("@To", SqlDbType.Int)                { Value = entity.DestinationHospital.Id });
                command.Parameters.Add(new SqlParameter("@Patient", SqlDbType.Int)           { Value = entity.Patient.Id });
                command.Parameters.Add(new SqlParameter("@Arrival", SqlDbType.SmallDateTime) { Value = entity.ArrivalTime });

                connection.Open();

                Debug.WriteLine(entity.Patient);

                int result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    Store.MessageStore.Message = "Error : Transport not added";
                }
                else
                {
                    Store.MessageStore.Message = "Success! Transport added.";
                }
            }
        }

        public void AssignAmbulanceToTransport(Ambulance ambulance, Transport transport)
        {
            string commandText = $"INSERT INTO {_hasTransportTable}([Ambulance], [Transport]) VALUES (@Ambulance, @Transport)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add(new SqlParameter("@Ambulance", SqlDbType.Int) { Value = ambulance.Id });
                command.Parameters.Add(new SqlParameter("@Transport", SqlDbType.Int) { Value = transport.Id });

                Debug.WriteLine(ambulance.Id);
                Debug.WriteLine(transport.Id);


                connection.Open();

                int result = command.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Could not assign ambulance");
                }
                else if (result == 1)
                {
                    MessageStore.Message = $"Successfully assigned {ambulance.Name} to transport id : {transport.Id}";
                }
                else
                {
                    throw new Exception("Not even sure what happend here");
                }
            }
        }

        public bool IsAssigned(Transport entity)
        {
            string commandText = $"SELECT COUNT(*) FROM {_hasTransportTable} WHERE [Transport]=@Transport";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add(new SqlParameter("@Transport", SqlDbType.Int) { Value = entity.Id });

                connection.Open();

                int result = (int)command.ExecuteScalar();

                return result != 0;
            }
        }

        public void Update(Transport entity)
        {
            string commandText = $"UPDATE {_tableName} SET [From] = @From, [To] = @To, [Patient] = @Patient WHERE [Id] = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add(new SqlParameter("@From", SqlDbType.Int)     { Value = entity.StartHospital.Id });
                command.Parameters.Add(new SqlParameter("@To", SqlDbType.Int)       { Value = entity.DestinationHospital.Id });
                command.Parameters.Add(new SqlParameter("@Patient", SqlDbType.Int)  { Value = entity.Patient.Id });
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)       { Value = entity.Id });

                connection.Open();

                int result = command.ExecuteNonQuery();
                
                if(result == 0)
                {
                    throw new Exception("Could not update");
                } else if (result == 1)
                {
                    MessageStore.Message = $"Successfully updated {entity.StartHospital.Name} -> {entity.DestinationHospital.Name}";
                }
                else
                {
                    throw new Exception("How did we get here?");
                }
            }
        }


        public IEnumerable<Transport> Find(Dictionary<string, object?> args)
        {
            StringBuilder commandText = new StringBuilder($"SELECT * FROM {_tableName}");

            // Check if any args are set
            bool set = false;
            foreach(KeyValuePair<string, object> arg in args)
            {
                if (arg.Value != null)
                {
                    set = true;
                }
            }
            if (set) commandText.Append(" WHERE");





            if (args.ContainsKey("FromHospital") && args["FromHospital"] != null) commandText.Append(
                " [From]=@From "
                );
            if (args.ContainsKey("ToHospital") && args["ToHospital"] != null) commandText.Append(
            " [To]=@To "
            );
            if (args.ContainsKey("Patient") && args["Patient"] != null) commandText.Append(
            " [Patient]=@Patient "
            );


            commandText.Replace("  ", " AND ");

            

            using (SqlConnection connection = new(_connectionString))
            {
                

                SqlCommand command = new(commandText.ToString(), connection);

                if (set)
                {
                    if (args.ContainsKey("FromHospital")) Debug.WriteLine("Shit fuck");

                    if (args.ContainsKey("FromHospital") && args["FromHospital"] != null) command.Parameters.Add(new SqlParameter("@From", SqlDbType.Int) { Value = ((Hospital)args["FromHospital"]).Id });
                    if (args.ContainsKey("ToHospital") && args["ToHospital"] != null) command.Parameters.Add(new SqlParameter("@To", SqlDbType.Int) { Value = ((Hospital)args["ToHospital"]).Id });
                    if (args.ContainsKey("Patient") && args["Patient"] != null) command.Parameters.Add(new SqlParameter("@Patient", SqlDbType.Int) { Value = ((Patient)args["Patient"]).Id });
                }

                string query = command.CommandText;

                foreach (SqlParameter p in command.Parameters)
                {
                    query = query.Replace(p.ParameterName, p.Value.ToString());
                }

                Debug.WriteLine(command.CommandText);

                List<Transport> result = new List<Transport>();


                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Hospital> hospitals = new();
                    List<Patient> patients = new();

                    HospitalRepository hospitalRepository = new(_configuration);
                    PatientRepository patientRepository = new(_configuration);

                    hospitals = new(hospitalRepository.GetAll());
                    patients = new(patientRepository.GetAll());

                    while (reader.Read())
                    {
                        Predicate<Hospital> findFromHospital = (hospital) =>
                        {
                            return (hospital.Id == (int)reader["From"]);
                        };

                        Hospital fromHospital = hospitals.Find(findFromHospital);

                        Predicate<Hospital> findToHospital = (hospital) =>
                        {
                            return (hospital.Id == (int)reader["To"]);
                        };

                        Hospital toHospital = hospitals.Find(findToHospital);

                        if (fromHospital is null || toHospital is null)
                        {
                            throw new Exception("Something fucky wucky happend.");
                        }

                        Predicate<Patient> findPatient = (patient) =>
                        {
                            return (patient.Id == (int)reader["Id"]);
                        };

                        Patient patient = patients.Find(findPatient);

                        int id = reader.GetInt32("Id");

                        Transport transport = new(
                            fromHospital, toHospital, (DateTime)reader["Arrival"], patient, 0, id
                            );


                        result.Add(transport);
                    }

                }

                return result;
            }



        }
    }
}
