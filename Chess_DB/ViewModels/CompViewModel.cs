using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Controls.Converters;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;


namespace Chess_DB.ViewModels;

public partial class CompViewModel : ViewModelBase
{
    [ObservableProperty]
    public int _compId;
    [ObservableProperty]
    public string _compName;
    [ObservableProperty]
    public DateTime? _date;
    [ObservableProperty]
    public string _city;

    [ObservableProperty]
    public string _country;
    public Competition _currentComp;

    public CompViewModel(Competition comp)
    {
        CompId = comp.CompId;
        CompName = comp.CompName;
        Date = comp.Date.HasValue ? comp.Date.Value.ToDateTime(TimeOnly.MinValue) : null;
        City = comp.City;
        Country = comp.Country;
        _currentComp = comp;
        LoadPlayerNotInComp();
        LoadPlayerInComp();
        LoadMatchs();
    }

    [RelayCommand]
    private async Task OpendCompAsync()
    {
        var compusercontrol = WeakReferenceMessenger.Default.Send(new CompMessage(this));
    }

    [RelayCommand]
    private async Task OpendWindowEditCompAsync()
    {
        var editcompwindow = await WeakReferenceMessenger.Default.Send(new WindowEditCompMessage(_currentComp));
    }

    [RelayCommand]
    private async Task OpendWindowAddPtoCAsync()
    {
        var editcompwindow = await WeakReferenceMessenger.Default.Send(new WindowAddPtoCMessage(_currentComp));
    }

    [RelayCommand]
    private async Task OpendWindowAddMtoCAsync()
    {
        var editcompwindow = await WeakReferenceMessenger.Default.Send(new WindowAddMtoCMessage(_currentComp));
    }


    [RelayCommand]
    private async Task Editcomp()
    {
        using (var context = new AppDbContext())
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            var editComp = new Competition
            {
                CompId = CompId,
                CompName = CompName,
                City = City,
                Country = Country,
                Date = Date.HasValue ? DateOnly.FromDateTime(Date.Value) : null,
            };
            context.Competitions.Update(editComp);
            await context.SaveChangesAsync();



        }
        WeakReferenceMessenger.Default.Send(new CompMessage(this));

    }


    [ObservableProperty]
    private ObservableCollection<LightPlayerViewModel> playerNotIncompList = new();
    [ObservableProperty]
    private ObservableCollection<LightPlayerViewModel> playersInCompList = new();


    public void LoadPlayerNotInComp()
    {
        using (var context = new AppDbContext())
        {
            context.Database.EnsureCreated();
            var playersNotInComp = context.Players
                .Where(p => !context.PlayerCompetition
                .Any(pc => pc.CompId == CompId && pc.PlayerId == p.playerID))
            .ToList();

            PlayerNotIncompList.Clear();

            foreach (var player in playersNotInComp)
            {
                PlayerNotIncompList.Add(new LightPlayerViewModel(player));
            }
        }
    }
    [RelayCommand]
    public void LoadPlayerInComp()
    {
        using (var context = new AppDbContext())
        {
            var playersInComp = context.PlayerCompetition
                .Where(pc => pc.CompId == CompId)
                .ToListAsync().Result;


            PlayersInCompList.Clear();

            foreach (var pc in playersInComp)
            {
                var LightPlayer = context.Players.Find(pc.PlayerId);
                if (LightPlayer != null)
                {
                    PlayersInCompList.Add(new LightPlayerViewModel(LightPlayer));
                }
            }
        }
        P1 = PlayersInCompList.FirstOrDefault();
        P2 = PlayersInCompList.FirstOrDefault();
    }


    [ObservableProperty]
    private string? firstName_search;
    [ObservableProperty]
    private string? lastName_search;
    [ObservableProperty]
    private string? id_search;


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

        PlayerNotIncompList.Clear();

        foreach (var p in result)
        {
            PlayerNotIncompList.Add(new LightPlayerViewModel(p)
            {
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                ELO = p.ELO,
                PlayerID = p.playerID
            });
        }
    }
#pragma warning restore CS8601 // Possible null reference assignment.



    [RelayCommand]
    private async Task AddPlayerToComp(LightPlayerViewModel player)
    {
        using (var context = new AppDbContext())
        {
            var playerComp = new PlayerCompetition
            {
                CompId = CompId,
                PlayerId = player.PlayerID/*get player id from somewhere*/
            };
            context.PlayerCompetition.Add(playerComp);
            await context.SaveChangesAsync();

        }
        WeakReferenceMessenger.Default.Send(new CompMessage(this));
        PlayersInCompList.Add(player);
        PlayerNotIncompList.Remove(player);
    }

    [RelayCommand]
    private async Task UpdateComp()
    {
        LoadPlayerNotInComp();
        LoadPlayerInComp();
    }

    [ObservableProperty]
    private LightPlayerViewModel _p1;
    [ObservableProperty]
    private LightPlayerViewModel _p2;

    private int WinnerId;

    [ObservableProperty]
    public List<string> _piecesPlayed = new();



    [ObservableProperty]
    private bool _blackIsCheck;

    [RelayCommand]
    private async Task AddMatch()
    {
        WinnerId = BlackIsCheck ? P1.PlayerID : P2.PlayerID;

        using (var context = new AppDbContext())
        {
            var match = new Match
            {
                Player1Id = P1.PlayerID,
                Player2Id = P2.PlayerID,
                WinnerId = WinnerId,
                CompetitionId = CompId,
                PlayedPieces = PiecesPlayed,
            };
            context.Match.Add(match);
            await context.SaveChangesAsync();
        }
    }


    [ObservableProperty]
    private ObservableCollection<MatchViewModel> matchList = new();

    [RelayCommand]
    public void LoadMatchs()
    {
        using (var context = new AppDbContext())
        {
            context.Database.EnsureCreated();
            var matchs = context.Match.ToListAsync().Result;
            MatchList.Clear();

            foreach (var match in matchs)
            {
                MatchList.Add(new MatchViewModel(match));
            }
        }
    }
    [ObservableProperty]
    private string _blackPMBefore = string.Empty;
    [ObservableProperty]
    private string _blackPMAfter = string.Empty;
    [ObservableProperty]
    private string _whitePMBefore = string.Empty;
    [ObservableProperty]
    private string _whitePMAfter = string.Empty;

    [RelayCommand]
    private void AddPiecePlayed()
    {
        PiecesPlayed.Add(WhitePMBefore);
        PiecesPlayed.Add("->");
        PiecesPlayed.Add(WhitePMAfter);
        PiecesPlayed.Add("|");
        PiecesPlayed.Add(BlackPMBefore);
        PiecesPlayed.Add("->");
        PiecesPlayed.Add(BlackPMAfter);
        PiecesPlayed.Add("|");

        BlackPMBefore = string.Empty;
        BlackPMAfter = string.Empty;
        WhitePMBefore = string.Empty;
        WhitePMAfter = string.Empty;

    }
}
