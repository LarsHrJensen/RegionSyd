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

        IConfiguration _configuration;

        string _tableName;
        // I f*ing hate dependency injection, give me lethal injection instead
        public TransportRepository(IConfiguration config, string tableName="Transports")
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("SQLAuth");
            _tableName = tableName;
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

                        if(fromHospital is null || toHospital is null)
                        {
                            throw new Exception("Something fucky wucky happend.");
                        }

                        Predicate<Patient> findPatient = (patient) =>
                        {
                            return (patient.Id == (int)reader["Id"]);
                        };

                        Patient patient = patients.Find(findPatient);

                        Debug.WriteLine("");
                        Debug.WriteLine("");
                        Debug.WriteLine("");
                        Debug.WriteLine(reader["Arrival"]);
                        Debug.WriteLine("");
                        Debug.WriteLine("");
                        Debug.WriteLine("");

                        Transport transport = new(
                            fromHospital, toHospital, (DateTime)reader["Arrival"], patient
                            );


                        result.Add(transport);
                    }
                }
            }

            return result;
        }

        public Transport GetById(int id)
        {
            throw new NotImplementedException();
        }
        
        public void Insert(Transport entity)
        {
            string commandText = $"INSERT INTO {_tableName}([From], [To], [Patient], [Arrival]) VALUES (@From, @To, @Patient, @Arrival)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add(new SqlParameter("@From", SqlDbType.Int) { Value = entity.StartHospital.Id });
                command.Parameters.Add(new SqlParameter("@To", SqlDbType.Int) { Value = entity.DestinationHospital.Id });
                command.Parameters.Add(new SqlParameter("@Patient", SqlDbType.Int) { Value = entity.Patient.Id });
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

        public void Update(Transport entity)
        {
            string commandText = $"UPDATE {_tableName} SET [From] = @From, [To] = @To, [Patient] = @Patient WHERE [Id] = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add(new SqlParameter("@From", SqlDbType.Int) { Value = entity.StartHospital.Id });
                command.Parameters.Add(new SqlParameter("@To", SqlDbType.Int) { Value = entity.DestinationHospital.Id });
                command.Parameters.Add(new SqlParameter("@Patient", SqlDbType.Int) { Value = entity.Patient.Id });
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id });

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

                        Transport transport = new(
                            fromHospital, toHospital, (DateTime)reader["Arrival"], patient
                            );


                        result.Add(transport);
                    }

                }

                return result;
            }



        }
    }
}
