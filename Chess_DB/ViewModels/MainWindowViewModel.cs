using CommunityToolkit.Mvvm.ComponentModel;
using Chess_DB.Data_Base_Services;
using System.IO;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chess_DB.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    public MainWindowViewModel()
    {
        Console.WriteLine("Starting database test...");

        using (var context = new UserDbcontext())
        {
            context.Database.EnsureCreated();

            // Add a new user
            var user = new User() { Id = 2, Name = "Alice" };
            var user1 = new User() { Id = 1, Name = "Admin" };
            //context.Users.Add(user);
            context.Users.Remove(user);
            context.SaveChanges();

            var users = context.Users.ToListAsync().Result;
            Console.WriteLine("All users in the database:");
            foreach (var u in users)
            {
                Console.WriteLine($"- {u.Name} (ID: {u.Id})");
            }
        }
    }
}
