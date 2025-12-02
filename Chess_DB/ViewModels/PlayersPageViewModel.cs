
using System;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Collections.ObjectModel;


namespace Chess_DB.ViewModels;

public partial class PlayersPageViewModel : ViewModelBase
{
    [ObservableProperty]
    [Required(ErrorMessage = "*")]
    private string? _firstN;
    [ObservableProperty]
    [Required(ErrorMessage = "*.")]
    private string? _lastN;
    [ObservableProperty]
    private int? _elo = 1400;
    [ObservableProperty]
    // [Required(ErrorMessage = "Id is required.")]

    private int? _id;

    // [ObservableProperty]
    // private string? searchText;

    // [ObservableProperty]
    // private bool isBusy;

    public PlayersPageViewModel()
    {
        LoadPlayer();
    }

    [RelayCommand]
    private async Task Addplayer()
    {

        ValidateAllProperties();
        if (HasErrors) return;

        using (var context = new PlayerDbcontext())
        {
#pragma warning disable CS8601 // Possible null reference assignment.

            context.Database.EnsureCreated();
            var newPlayer = new Player
            {
                Firstname = FirstN,
                Lastname = LastN,
                ELO = Elo.Value
            };
#pragma warning restore CS8601 // Possible null reference assignment.

            context.Players.Add(newPlayer);
            await context.SaveChangesAsync();

            Console.WriteLine($"Player added: {LastN} {FirstN} (ID generated: {newPlayer.playerID})");

        }

        // OPTIONAL: Clear inputs after adding
        FirstN = LastN = string.Empty;
        Elo = Id = null;


        await Task.Delay(50);
        LoadPlayer();
    }


    [RelayCommand]
    private async Task OpendWindowAddPlayerAsync()
    {
        // Send the message to the previously registered handler and await the selected album
        var playerwindow = await WeakReferenceMessenger.Default.Send(new WindowPlayerMessage());
    }


    [ObservableProperty]
    private ObservableCollection<Player> playerList = new();

    [RelayCommand]
    public void LoadPlayer()
    {

        using (var context = new PlayerDbcontext())
        {
            context.Database.EnsureCreated();
            var competitions = context.Players.ToListAsync().Result;
            PlayerList.Clear();

            foreach (var comp in competitions)
            {
                PlayerList.Add(comp);
            }
        }
    }

    [ObservableProperty]
    private string firstName_search;
    [ObservableProperty]
    private string lastName_search;
    [ObservableProperty]
    private string id_search;

    [RelayCommand]
    private void SearchPlayers()
    {
        DataTable result = Connexion.FindPlayer(FirstName_search, LastName_search, Id_search);
        Console.WriteLine(FirstName_search);
        PlayerList.Clear();

        foreach (DataRow row in result.Rows)
        {
            PlayerList.Add(new Player
            {
                Firstname = row["Firstname"].ToString(),
                Lastname = row["Lastname"].ToString(),
                ELO = Convert.ToInt32(row["ELO"]),
                playerID = Convert.ToInt32(row["playerID"])
            });
            Console.WriteLine(row["Firstname"].ToString());
        }
    }

}


