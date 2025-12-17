using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;


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
        LoadPinComp();
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
            // #pragma warning disable CS8601 // Possible null reference assignment.
            var editPlayer = new Player
            {
                Firstname = Firstname,
                Lastname = Lastname,
                ELO = ELO,
                playerID = PlayerID
            };
            context.Players.Update(editPlayer);
            await context.SaveChangesAsync();

            WeakReferenceMessenger.Default.Send(new PlayerMessage(this));
        }
    }

    [ObservableProperty]
    private ObservableCollection<LightCompViewModel> compList = new();

    [RelayCommand]
    public void LoadPinComp()
    {
        using (var context = new AppDbContext())
        {
            context.Database.EnsureCreated();
            var playersInComp = context.PlayerCompetition
                .Where(pc => pc.PlayerId == PlayerID)
                .ToListAsync().Result;
            var competitions = context.Competitions.ToListAsync().Result;
            CompList.Clear();

            foreach (var pc in playersInComp)
            {
                var comp = competitions.FirstOrDefault(c => c.CompId == pc.CompId);
                if (comp != null)
                {
                    CompList.Add(new LightCompViewModel(comp));
                }
            }
        }
    }

    

}
