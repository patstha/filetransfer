using Dapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;

namespace FileTransfer.Persistence
{
    public class DataService : IDataService, System.IDisposable
    {
        private readonly ILogger<DataService> _log;
        private readonly string _connectionString;

        public DataService(
            ILogger<DataService> log,
            string connectionString)
        {
            _log = log;
            _connectionString = connectionString;
        }

        public void Connect()
        {
            _log.LogDebug($"TODO: Write code to {nameof(Connect)} to Connection String {_connectionString}");
        }

        public List<Person> GetPersons()
        {
            List<Person> persons = new();
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from person";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 2)
                            {
                                Person person = new()
                                {
                                    Id = (ulong)reader[0],
                                    LegalName = (string)reader[1],
                                    DateOfBirth = (System.DateTime)reader[2]
                                };
                                persons.Add(person);
                            }
                        }
                    }
                }
            }
            return persons;
        }

        public List<Person> GetPersonsDapper()
        {
            List<Person> persons = new();
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                persons = connection.Query<Person>("select * from person").AsList();
            }
            return persons;
        }

        public void CreateStateLookupTable()
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                string query = @"create table if not exists lookupstate 
                                (
                                    id serial primary key, 
                                    name nvarchar(255) not null, 
                                    capital nvarchar(255) not null
                                )
                                ;";
                connection.Query(query);
            }
        }

        public void DeleteStateLookupTable()
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                string query = @"drop table if exists lookupstate;";
                connection.Query(query);
            }
        }

        public void InsertStateLookupTable()
        {
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                string query = @"insert into lookupstate
                                (name, capital)
                                values
                                (""Alaska"", ""Anchorage""),
                                (""Arkansas"", ""Little Rock"");
                ; ";
                connection.Query(query);
            }
        }

        public List<LookupState> GetStateLookupTable()
        {
            List<LookupState> states = new();
            using (var connection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                string query = @"select * from lookupstate;";
                states = connection.Query<LookupState>(query).AsList();
            }
            return states;
        }

        public void Disconnect() => _log.LogDebug($"TODO: Write code to {nameof(Disconnect)} from Connection String {_connectionString} here.");

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            // Cleanup
            Disconnect();
        }
    }
}
