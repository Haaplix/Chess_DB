using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls.Primitives;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;


namespace Chess_DB.ViewModels;

public partial class LightPlayerViewModel : ViewModelBase
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

    public LightPlayerViewModel(Player player)
    {
        Firstname = player.Firstname;
        Lastname = player.Lastname;
        ELO = player.ELO;
        PlayerID = player.playerID;
        _currentPlayer = player;

    }
}

