using System;
using System.Data.Common;
using System.IO;
using Avalonia.Controls.Converters;
using Microsoft.EntityFrameworkCore;

public class UserDbcontext : DbContext
{
    public DbSet<User> Users { get; set; }

    string dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Chess_DB\Data_Base_Services\User.db"));

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }


}

public class AdminDbcontext : DbContext
{
    public DbSet<Admins> Admins { get; set; }

    string dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Chess_DB\Data_Base_Services\Admin.db"));

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    
}
public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class Admins
{
    public int Id {get; set;}
    public string Name {get; set;}

    public string LName {get; set;}

    public string Pwd {get; set;}
}