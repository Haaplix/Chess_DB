
using System;
using System.Data.Common;
using System.IO;
using Chess_DB.Data_Base_Services;

static class TestProgram
{
    public static void TestDatabase()
    {
        string dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Chess_DB\Data_Base_Services\U&P.db"));
        Data_managment dataManagement = new Data_managment($@"Data Source={dbPath}");
        // Query the actual table created by your SQL script
        DbDataReader reader = dataManagement.ExecuteReader(@"SELECT * FROM Admins;");
        while (reader.Read())
        {

            // Match the column names from sql.sql: username, password
            Console.WriteLine($"Username: {reader["user"]}, Password: {reader["password_hash"]}, Email: {reader["email"]}");
        }
    }
}


