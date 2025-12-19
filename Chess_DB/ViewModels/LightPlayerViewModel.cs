using System.Threading.Tasks;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;



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
    [ObservableProperty]
    public Player _currentPlayer;

    public LightPlayerViewModel(Player player)
    {
        Firstname = player.Firstname;
        Lastname = player.Lastname;
        ELO = player.ELO;
        PlayerID = player.playerID;
        CurrentPlayer = player;

    }

    [RelayCommand]
    private async Task OpendPlayerAsync()
    {
        var playerusercontrol = WeakReferenceMessenger.Default.Send(new PlayerMessage(new PlayerViewModel(CurrentPlayer)));
    }
}

