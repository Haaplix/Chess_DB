using Microsoft.EntityFrameworkCore;

public class UserDbcontext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=D:\\Ecole\\Ecam\\3Ba\\Object oriented programmig\\Chess_DB\\Chess_DB\\Data_Base_Services\\User.db");
    }


}

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
}