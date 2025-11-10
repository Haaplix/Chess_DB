using Microsoft.Data.Sqlite;




namespace Chess_DB.Data_Base_Services
{
    public class Data_managment
    {
        private readonly string _connectionString; // Need to set this to your database path just hardcoded it

        public Data_managment(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteQuery(string query)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }
        }

        public SqliteDataReader ExecuteReader(string query)
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = query;
            return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

    }
}