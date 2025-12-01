using System.IO;
using System;
using System.Data;
using System.Data.SQLite;



public static class Connexion
{
    static string dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Chess_DB\Data_Base_Services\Player.db"));
    private static readonly string key = $@"Data Source={dbPath}";
    /// <summary>
    /// Connexion a la datatbase 
    /// </summary>
    /// <returns>Retourne un objet de type SQLiteConnection</returns>
    public static SQLiteConnection connection()
    {
        return new SQLiteConnection(key);
    }


    public static DataTable FindPlayer(string? firstname, string? lastname, string? id)
    {
        using (var conn = connection())
        {
            var cmd = new SQLiteCommand(conn);
            string query = "Select * from Players where 1=1";

            if (firstname != "" && firstname != null)
            {
                query += " and FirstName like @firstname";
            }
            if (lastname != "" && lastname != null)
            {
                query += " and LastName like @lastname";
            }
            if (id != "" && id != null)
            {
                query += " and playerID like @id";
            }

            cmd.CommandText = query;

            if (firstname != null && firstname != "")
            {
                cmd.Parameters.AddWithValue("@firstname", $"%{firstname}%");
            }
            if (lastname != null && lastname != "")
            {
                cmd.Parameters.AddWithValue("@lastname", $"%{lastname}%");
            }
            if (id != null && id != "")
            {
                cmd.Parameters.AddWithValue("@id", $"%{id}%");
            }

            conn.Open();
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();

            var result = new DataTable();
            result.Load(reader);
            return result;
        }
    }

    public static DataTable PlayerTable()
    {
        using (var conn = connection())
        {
            var cmd = new SQLiteCommand(conn);
            string query = "Select * from Players";

            cmd.CommandText = query;

            conn.Open();
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();

            var result = new DataTable();
            result.Load(reader);
            return result;
        }
    }

}