
using System.IO;
using System;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Competition> Competitions { get; set; }

    string dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Chess_DB\Data_Base_Services\AppDatabase.db"));

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //relationship many to many
        modelBuilder.Entity<Player>()
            .HasMany(p => p.Competitions)
            .WithMany(c => c.Players)
            .UsingEntity("PlayerCompetition");

        //relationship one to many
        modelBuilder.Entity<Match>()
            .HasOne(m => m.Competition)
            .WithMany(c => c.Matches)
            .HasForeignKey(m => m.CompetitionId);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.Player1)
            .WithMany(p => p.AsPlayer1)
            .HasForeignKey(m => m.Player1Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.Player2)
            .WithMany(p => p.AsPlayer2)
            .HasForeignKey(m => m.Player2Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.Winner)
            .WithMany(p => p.Winner)
            .HasForeignKey(m => m.WinnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}