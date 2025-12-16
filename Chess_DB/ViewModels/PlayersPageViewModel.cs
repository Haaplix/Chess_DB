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
using System.Collections.Generic;
using System.Linq;

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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public PlayersPageViewModel()
    {
        LoadPlayer();
    }

    [RelayCommand]
    private async Task Addplayer()
    {

        ValidateAllProperties();
        if (HasErrors) return;

        using (var context = new AppDbContext())
        {

#pragma warning disable CS8601 // Possible null reference assignment.
            context.Database.EnsureCreated();
            var newPlayer = new Player
            {
                Firstname = FirstN,
                Lastname = LastN,
                ELO = Elo.Value
            };
            context.Players.Add(newPlayer);
            await context.SaveChangesAsync();

            Console.WriteLine($"Player added: {LastN} {FirstN} (ID generated: {newPlayer.playerID})");
            PlayerList.Add(new PlayerViewModel(newPlayer));
        }

        //Clear inputs after adding
        FirstN = LastN = string.Empty;
        Elo = 1400;
        Id = null;



    }


    [RelayCommand]
    private async Task OpendWindowAddPlayerAsync()
    {
        var playerwindow = await WeakReferenceMessenger.Default.Send(new WindowPlayerMessage());
    }



    [ObservableProperty]
    private ObservableCollection<PlayerViewModel> playerList = new();

    [RelayCommand]
    public void LoadPlayer()
    {
        using (var context = new AppDbContext())
        {
            context.Database.EnsureCreated();
            var players = context.Players.ToListAsync().Result;
            PlayerList.Clear();

            foreach (var player in players)
            {
                PlayerList.Add(new PlayerViewModel(player));
            }
        }
    }

    [ObservableProperty]
    private string firstName_search;
    [ObservableProperty]
    private string lastName_search;
    [ObservableProperty]
    private string id_search;


    public static async Task<List<Player>> FindPlayerAsync(string? firstname, string? lastname, string? id)
    {
        using (var _context = new AppDbContext())
        {
            IQueryable<Player> query = _context.Players;

            if (!string.IsNullOrWhiteSpace(firstname))
                query = query.Where(p => p.Firstname.Contains(firstname));

            if (!string.IsNullOrWhiteSpace(lastname))
                query = query.Where(p => p.Lastname.Contains(lastname));

            if (!string.IsNullOrWhiteSpace(id))
                query = query.Where(p => p.playerID.ToString().Contains(id));

            return await query.ToListAsync();
        }

    }



    [RelayCommand]
    private async Task SearchPlayers()
    {
        var result = await FindPlayerAsync(FirstName_search, LastName_search, Id_search);

        PlayerList.Clear();

        foreach (var p in result)
        {
            PlayerList.Add(new PlayerViewModel(p)
            {
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                ELO = p.ELO,
                PlayerID = p.playerID
            });
        }
    }
#pragma warning restore CS8601 // Possible null reference assignment.

}


