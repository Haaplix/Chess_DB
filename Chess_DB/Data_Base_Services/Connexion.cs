using System.IO;
using System;
using System.Data;
using System.Data.SQLite;



public static class Connexion
{
    static string dbPathPlayer = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Chess_DB\Data_Base_Services\Player.db"));
    private static readonly string key = $@"Data Source={dbPathPlayer}";
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

    public static DataTable FindComp(string? name, string? country, string? city, string? date, string? id)
    {
        using (var conn = connection())
        {
            var cmd = new SQLiteCommand(conn);
            string query = "Select * from Competitions where 1=1";

            if (name != "" && name != null)
            {
                query += " and FirstName like @firstname";
            }
            if (country != "" && country != null)
            {
                query += " and LastName like @lastname";
            }
            if (city != "" && city != null)
            {
                query += " and LastName like @lastname";
            }
            if (date != "" && date != null)
            {
                query += " and LastName like @lastname";
            }
            if (id != "" && id != null)
            {
                query += " and playerID like @id";
            }

            cmd.CommandText = query;

            if (name != null && name != "")
            {
                cmd.Parameters.AddWithValue("@firstname", $"%{name}%");
            }
            if (country != null && country != "")
            {
                cmd.Parameters.AddWithValue("@lastname", $"%{country}%");
            }
            if (city != null && city != "")
            {
                cmd.Parameters.AddWithValue("@lastname", $"%{city}%");
            }
            if (date != null && date != "")
            {
                cmd.Parameters.AddWithValue("@lastname", $"%{date}%");
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

}