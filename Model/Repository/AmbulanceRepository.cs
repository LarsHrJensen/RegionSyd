using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using RegionSyd.Model.Store;


namespace RegionSyd.Model.Repository.Repository
{
    public class AmbulanceRepository : IRepository<Ambulance>
    {
        SqlConnection _connection;
        string _connectionString;
        string _tableName;

        IConfiguration _configuration;

        // I f*ing hate dependency injection, give me lethal injection instead
        public AmbulanceRepository(IConfiguration config, string ambulanceTableName="Ambulances")
        {

            _connectionString = config.GetConnectionString("SQLAuth");
            _tableName = ambulanceTableName;
            _configuration = config;

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
                        HospitalRepository hospitalRepo = new HospitalRepository(_configuration);

                        Hospital hospital = hospitalRepo.GetById((int)reader["Station"]);


                        Ambulance amb = new Ambulance(
                            (string)reader["Name"], hospital,
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
            string commandText = $"INSERT INTO {_tableName}([Name], [Station], [Status]) VALUES (@Name, @Station, @Status)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar) { Value = entity.Name});
                command.Parameters.Add(new SqlParameter("@Station", SqlDbType.Int) { Value = entity.Hospital.Id});
                command.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar) { Value = entity.Status });

                connection.Open();

                int result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    Store.MessageStore.Message = "Error : Ambulance not added";
                }
                else
                {
                    Store.MessageStore.Message = "Success! Ambulance added.";
                }
            }
        }

        public void Update(Ambulance entity)
        {
            string commandText = $"UPDATE {_tableName} SET [Name] = @Name, [Station] = @Station, [Status] = @Status WHERE [Id] = @Id";

            using (SqlConnection connection = new(_connectionString))
            {
                

                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar) { Value = entity.Name });
                command.Parameters.Add(new SqlParameter("@Station", SqlDbType.Int) { Value = entity.Hospital.Id });
                command.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar) { Value = entity.Status });
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id });

                connection.Open();

                int result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("Could not update");
                } else if (result == 1)
                {
                    MessageStore.Message = $"Succesfully updated {entity.Name}";
                }
                else
                {
                    throw new Exception("Wtf happend here");
                }
            }

        }
    }
}
