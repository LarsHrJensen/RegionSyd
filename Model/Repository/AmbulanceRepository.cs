using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;


namespace RegionSyd.Model.Repository.Repository
{
    public class AmbulanceRepository : IRepository<Ambulance>
    {
        SqlConnection _connection;
        string _connectionString;
        string _tableName;

        // I f*ing hate dependency injection, give me lethal injection instead
        public AmbulanceRepository(IConfiguration config, string ambulanceTableName="Ambulances")
        {
            _connectionString = config.GetConnectionString("SQLAuth");
            _tableName = ambulanceTableName;
        }

        public void Delete(Ambulance entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ambulance> GetAll()
        {
            string commandText = $"SELECT * FROM {_tableName}";

            List<Ambulance> result = new();

            using(SqlConnection connection = new(_connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = commandText;

                connection.Open();

                Debug.WriteLine(cmd.CommandText);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ambulance amb = new Ambulance(
                            (string)reader["Name"], (string)reader["Station"],
                            (string)reader["Status"], (int)reader["Id"]
                        );
                        result.Add( amb );
                    }
                }
            }

            return result;
        }

        public Ambulance GetById(int id)
        {
            // select * from ambulances where id = id
            throw new NotImplementedException();
        }

        public void Insert(Ambulance entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Ambulance entity)
        {
            throw new NotImplementedException();
        }
    }
}
