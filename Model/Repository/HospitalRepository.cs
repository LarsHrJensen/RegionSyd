using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using RegionSyd.Model.Store;

namespace RegionSyd.Model.Repository
{
    internal class HospitalRepository : IRepository<Hospital>
    {
        SqlConnection _connection;
        string _connectionString;
        string _tableName;

        // I f*ing hate dependency injection, give me lethal injection instead
        public HospitalRepository(IConfiguration config, string tablename="Hospitals")
        {
            _connectionString = config.GetConnectionString("SQLAuth");
            _tableName = tablename;
        }

        public void Delete(Hospital entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hospital> GetAll()
        {
            string commandText = $"SELECT * FROM {_tableName}";

            List<Hospital> result = new();

            using (SqlConnection connection = new(_connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = commandText;

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Hospital hopsital = new Hospital(
                            (string)reader["Name"], (string)reader["Address"],
                            (string)reader["City"], (string)reader["Region"],
                            (int)reader["Id"]
                        );
                        result.Add(hopsital);
                    }
                }
            }

            return result;
        }

        public Hospital GetById(int id)
        {
            Hospital hospital;

            string cmdText = $"SELECT * FROM {_tableName} WHERE Id = @Id";

            using (SqlConnection connection = new(_connectionString))
            {
                SqlCommand command = new(cmdText, connection);
                
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader()) 
                {
                    // If no Hospital exists with given id
                    // return null 
                    if (!reader.Read()) return null;
                    hospital = new Hospital(
                        (string)reader["Name"], (string)reader["Address"],
                        (string)reader["Region"], (string)reader["City"], (int)reader["Id"]
                    );

                    return hospital;
                }
            }
        }

        public void Insert(Hospital entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Hospital entity)
        {
            string commandText = $"UPDATE {_tableName} SET [Name] = @Name, [Address] = @Address, [Region] = @Region, [City] = @City WHERE [Id] = @Id";
            using (SqlConnection connection = new(_connectionString))
            {
                
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar) { Value = entity.Name});
                command.Parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar) { Value = entity.Address});
                command.Parameters.Add(new SqlParameter("@Region", SqlDbType.VarChar) { Value = entity.Region});
                command.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar) { Value = entity.City});
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id });

                connection.Open();

                int result = command.ExecuteNonQuery();
                if(result == 0)
                {
                    throw new Exception("Could not update");
                } if(result == 1)
                {
                    MessageStore.Message = $"Successfully updated {entity.Name}";
                }
                else
                {
                    throw new Exception("How did we get here?");
                }

            }
        }
    }
}
