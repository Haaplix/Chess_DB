using CommunityToolkit.Mvvm.ComponentModel;
using Chess_DB.Data_Base_Services;
using System.IO;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Chess_DB.ViewModels;

public partial class PlayersPageViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task PrintDB()
    {
        using (var context = new UserDbcontext())
        {
            var users = await context.Users.ToListAsync();
            foreach (var u in users)
            {
                Console.WriteLine($"-{u.Name} (ID: {u.Id})");
            }
        }
    }
}


// public class AddPlayer
// {
//     public void AddNewPlayer()
//     {
//         using (var context = new PlayerDbcontext())
//         {

//             context.Database.EnsureCreated();
//             var player = new Player { Firstname = "Adam", Lastname = "JSP", ELO = 1223, playerID = 1333 };

//             context.Players.Add(player);
//             context.SaveChanges();
//         }
//     }
// }