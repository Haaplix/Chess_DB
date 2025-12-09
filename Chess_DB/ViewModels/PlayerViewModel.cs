using System;
using System.Data.Common;
using System.Threading.Tasks;
using Avalonia.Animation.Easings;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

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

    public PlayerViewModel(Player player)
    {
        Firstname = player.Firstname;
        Lastname = player.Lastname;
        ELO = player.ELO;
        PlayerID = player.playerID;

    }

    [RelayCommand]
    private async Task OpendPlayerAsync()
    {
        var playerusercontrol = WeakReferenceMessenger.Default.Send(new PlayerMessage(this));
    }
}
