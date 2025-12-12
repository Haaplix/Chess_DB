using System;
using System.Data.Common;
using System.Threading.Tasks;
using Avalonia.Animation.Easings;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SQLitePCL;

namespace Chess_DB.ViewModels;

public partial class PlayerViewModel : ViewModelBase
{
    [ObservableProperty]
    public string _firstname;
    [ObservableProperty]
    public string _lastname;
    [ObservableProperty]
    public int _eLO;
    [ObservableProperty]
    public int _playerID;

    public Player _currentPlayer;

    public PlayerViewModel(Player player)
    {
        Firstname = player.Firstname;
        Lastname = player.Lastname;
        ELO = player.ELO;
        PlayerID = player.playerID;
        _currentPlayer = player;
    }

    [RelayCommand]
    private async Task OpendPlayerAsync()
    {
        var playerusercontrol = WeakReferenceMessenger.Default.Send(new PlayerMessage(this));
    }


    [RelayCommand]
    private async Task OpendWindowEditPlayerAsync()
    {
        var editplayerwindow = await WeakReferenceMessenger.Default.Send(new WindowEditPlayerMessage(_currentPlayer));
    }


    [RelayCommand]
    private async Task Editplayer()
    {
        using (var context = new AppDbContext())
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            var editPlayer = new Player
            {
                Firstname = Firstname,
                Lastname = Lastname,
                ELO = ELO,
                playerID = PlayerID
            };
            context.Players.Update(editPlayer);
            await context.SaveChangesAsync();

            Console.WriteLine($"Player modified: {Firstname} {Lastname} (ID generated: {PlayerID})");

        }
    }



}
