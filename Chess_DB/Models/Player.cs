using System.IO;
using System;

using Microsoft.EntityFrameworkCore;

public class Player
{
    public string Firstname {get; set;}

    public string Lastname {get; set;}
    public int ELO{get; set;}
    public int playerID {get; set;}
    //private string picture {get; set;}
     
    
    // public Player(string Firstname, string Lastname, int ELO, int playerID, string picture):
    // {
    //     this.ELO = ELO;
    //     this.playerID = playerID;
    //     this.picture = picture;
    // }

}

public class PlayerDbcontext : DbContext
{
    public DbSet<Player> Players { get; set; }

    string dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Chess_DB\Data_Base_Services\User.db"));

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }


}
