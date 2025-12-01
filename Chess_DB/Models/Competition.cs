using System.IO;
using System;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
public class Competition
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int CompId { get; set; }
    public required string CompName { get; set; }
    public required string date { get; set; }
    public required string city { get; set; }
    public required string country { get; set; }

    // public Competition(string date, string city,string country)
    // {
    //     this.date = date;
    //     this.city = city;
    //     this.country = country;
    // }
}


public class CompetitionDbcontext : DbContext
{
    public DbSet<Competition> Competitions { get; set; }

    string dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Chess_DB\Data_Base_Services\Competitions.db"));

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }


}
