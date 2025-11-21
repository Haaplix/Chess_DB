
using System;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Chess_DB.ViewModels;

public partial class PlayersPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private string? _firstN;
    [ObservableProperty]
    private string? _lastN;
    [ObservableProperty]
    private int? _elo;
    [ObservableProperty]
    private int? _id;


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
    private async Task Addplayer()
    {
        if (string.IsNullOrWhiteSpace(FirstN) || string.IsNullOrWhiteSpace(LastN) || Elo == null || Id == null)
        {
            Console.WriteLine("Please fill all fields.");
            return;
        }

        using (var context = new PlayerDbcontext())
        {
            var newPlayer = new Player
            {
                Firstname = FirstN,
                Lastname = LastN,
                ELO = Elo.Value,
                playerID = Id.Value
            };

            context.Players.Add(newPlayer);
            await context.SaveChangesAsync();

            Console.WriteLine($"Player added: {LastN} {FirstN} (ID: {Id})");
        }

        // OPTIONAL: Clear inputs after adding
        FirstN = LastN = string.Empty;
        Elo = Id = null;
    }


    [RelayCommand]
    private async Task OpendWindowAddPlayerAsync()
    {
        // Send the message to the previously registered handler and await the selected album
        var playerwindow = await WeakReferenceMessenger.Default.Send(new WindowPlayerMessage());
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