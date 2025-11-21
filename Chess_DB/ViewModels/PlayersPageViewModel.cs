
using System;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Chess_DB.Messages;

namespace Chess_DB.ViewModels;

public partial class PlayersPageViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task PrintDB()
    {
        using (var context = new PlayerDbcontext())
        {
            context.Database.EnsureCreated();
            // var player = new Player { Firstname = "Adam", Lastname = "JSP", ELO = 1223, playerID = 1333 };

            // context.Players.Add(player);
            // context.SaveChanges();

            var players = await context.Players.ToListAsync();
            foreach (var p in players)
            {
                Console.WriteLine($"-{p.Lastname} {p.Firstname} (ID: {p.playerID})");
            }
        }
    }

    [RelayCommand]
    private async Task AddPlayerAsync()
    {
        // Send the message to the previously registered handler and await the selected album
        var jsp = await WeakReferenceMessenger.Default.Send(new JspMessage());
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