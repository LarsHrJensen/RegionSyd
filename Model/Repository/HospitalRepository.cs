using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

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

            using (_connection = new(_connectionString))
            {
                SqlCommand command = new(cmdText, _connection);
                
                command.Parameters.AddWithValue("@Id", id);

                _connection.Open();
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
            throw new NotImplementedException();
        }
    }
}
