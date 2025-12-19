using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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
    [ObservableProperty]
    private ObservableCollection<LightCompViewModel> compList = new();
    [ObservableProperty]
    private ObservableCollection<MatchViewModel> pinMatchList = new();

    public PlayerViewModel(Player player)
    {
        Firstname = player.Firstname;
        Lastname = player.Lastname;
        ELO = player.ELO;
        PlayerID = player.playerID;
        _currentPlayer = player;
        LoadPinComp();
        using var _ = LoadPinMatch();
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

    public async Task LoadPinMatch()
    {
        using (var context = new AppDbContext())
        {
            var matches = await context.Match
                        .Where(m => m.Player1Id == PlayerID || m.Player2Id == PlayerID)
                        .OrderByDescending(m => m.MatchId) // or MatchDate
                        .ToListAsync();

            foreach (var match in matches)
            {
                var player1 = await context.Players.FindAsync(match.Player1Id);
                var player2 = await context.Players.FindAsync(match.Player2Id);
                var winner = await context.Players.FindAsync(match.WinnerId);
                var compet = await context.Competitions.FindAsync(match.CompetitionId);
                PinMatchList.Add(new MatchViewModel(match, player1, player2, winner, compet, match.PlayedPieces));
            }
        }
    }

}
