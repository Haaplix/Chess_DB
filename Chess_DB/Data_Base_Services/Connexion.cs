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


    /// <summary>
    /// exécute une commmande SQL non_query!
    /// </summary>
    /// <param name="query">string représentant la commande SQL a exécuter! Attention que pour les Query type!</param>
    public static void ExecuteNonQuery(string query)
    {
        using (var conn = connection())
        {
            conn.Open();
            var cmd = new SQLiteCommand(query, conn);
            cmd.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// retourne une Table avec la data demandée par la query
    /// </summary>
    /// <param name="query">string représentant la commade SQL a exécuter! Attention que pour les Query type!</param>
    /// <returns>retourne un objet de type DataTable</returns>
    public static DataTable ExecuteQuery(string query)
    {
        using (var conn = connection())
        {
            conn.Open();
            var cmd = new SQLiteCommand(query, conn);
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();


            var result = new DataTable();
            result.Load(reader);
            return result;
        }
    }
    public static DataTable FindPlayer(long? ID, string? name)
    {
        using (var conn = connection())
        {
            var cmd = new SQLiteCommand(conn);
            string query = "Select * from Players where 1=1";

            if (ID != null)
            {
                query += " and ID=@ID";
                cmd.Parameters.AddWithValue("@ID", ID.ToString());
            }
            if (name != "")
            {
                query += " and Nom=@name";
                cmd.Parameters.AddWithValue("@name", name);
            }

            cmd.CommandText = query;

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
            string query = "Select * from Players where 1=1";

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