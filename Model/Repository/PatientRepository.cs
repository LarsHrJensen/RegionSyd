using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RegionSyd.Model.Store;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model.Repository
{
    internal class PatientRepository : IRepository<Patient>
    {

        SqlConnection _connection;
        string _connectionString;
        string _tableName;
        // I f*ing hate dependency injection, give me lethal injection instead
        public PatientRepository(IConfiguration config, string tableName="Patients")
        {
            _connectionString = config.GetConnectionString("SQLAuth");
            _tableName = tableName;
        }

        public void Delete(Patient entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetAll()
        {
            string commandText = $"SELECT * FROM {_tableName}";

            List<Patient> result = new();

            using (SqlConnection connection = new(_connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = commandText;

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // TODO : Patient cpr unnecessary for project removal inbound
                        Patient patient = new Patient(
                            "0000000000", (string)reader["Name"], (string)reader["Status"], (int)reader["Id"]
                            );
                        result.Add(patient);
                    }
                }
            }

            return result;
        }

        public Patient GetById(int id)
        {
            string commandText = $"SELECT * FROM {_tableName} WHERE Id=@Id";

            using (SqlConnection connection = new(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new Exception($"No results found for id:{id}");
                    }

                    Patient patient;

                    string fullName = reader.GetString("Name");
                    string status = reader.GetString("Status");
                    int patientId = reader.GetInt32("Id");
                    string cpr = "0000000000";

                    patient = new(cpr, fullName, status, patientId);


                    if (reader.Read())
                    {
                        throw (new Exception("Too many results"));
                    }

                    return patient;
                }

            }
        }

        public void Insert(Patient entity)
        {
            string commandText = $"INSERT INTO {_tableName}([Name], [Status]) VALUES (@Name, @Status)";

            using (SqlConnection conn = new(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, conn);
                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar) { Value = entity.FullName });
                command.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar) { Value = entity.Status });

                conn.Open();
                int reuslt = command.ExecuteNonQuery();
                if (reuslt == 0)
                {
                    throw new Exception("Damn");
                }
                else
                {
                    MessageStore.Message = "Successfully added patient.";
                }
            }

        }

        public void Update(Patient entity)
        {
            string commandText = $"UPDATE {_tableName} SET [Name] = @Name, [Status] = @Status WHERE [Id] = @Id";
            using (SqlConnection connection = new(_connectionString)) 
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar) { Value = entity.FullName });
                command.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar) { Value = entity.Status });
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id });

                connection.Open();

                int result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("Could not update");
                } else if (result == 1)
                {
                    MessageStore.Message = $"Sucessfully updated {entity.FullName}";
                }
                else
                {
                    throw new Exception("Unreachable code reached");
                }
            }

        }
    }
}
