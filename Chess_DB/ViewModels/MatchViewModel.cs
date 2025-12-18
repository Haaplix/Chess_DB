using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;


namespace Chess_DB.ViewModels;

public partial class MatchViewModel : ViewModelBase
{
    [ObservableProperty]
    public int _matchId;
    [ObservableProperty]
    public int _competitionId;
    [ObservableProperty]
    public Competition _competition;
    [ObservableProperty]
    public int _player1Id;

    [ObservableProperty]
    public Player _player1;
    [ObservableProperty]
    public int _player2Id;
    [ObservableProperty]
    public Player _player2;
    [ObservableProperty]
    public int _winnerId;
    [ObservableProperty]
    public Player _winner;
    [ObservableProperty]
    public Competition _comp;
    [ObservableProperty]
    public List<string> _playedPieces = new();
    public Match Match;

    public MatchViewModel(Match match, Player p1, Player p2, Player winner, Competition comp, List<string> playedPiece)
    {
        MatchId = match.MatchId;
        CompetitionId = match.CompetitionId;
        Competition = match.Competition;
        Player1Id = match.Player1Id;
        Player1 = p1;
        Player2Id = match.Player2Id;
        Player2 = p2;
        WinnerId = match.WinnerId;
        Winner = winner;
        Comp = comp;
        PlayedPieces = playedPiece;
        Match = match;
    }

    [RelayCommand]
    private async Task OpenMatchAsync()
    {
        var playerusercontrol = WeakReferenceMessenger.Default.Send(new MatchMessage(this));
    }
    [RelayCommand]
    private async Task OpendWindowEditMatchAsync()
    {
        var editcompwindow = await WeakReferenceMessenger.Default.Send(new WindowEditMatchMessage(Match, Player1, Player2, Winner, Comp, PlayedPieces));
    }

    [RelayCommand]
    private async Task EditMatch()
    {
        using (var context = new AppDbContext())
        {
            // #pragma warning disable CS8601 // Possible null reference assignment.
            var editMatch = new Match
            {
                MatchId = MatchId,
                CompetitionId = CompetitionId,
                Player1Id = Player1Id,
                Player2Id = Player2Id,
                WinnerId = WinnerId,
                PlayedPieces = PlayedPieces,
            };
            context.Match.Update(editMatch);
            await context.SaveChangesAsync();

            WeakReferenceMessenger.Default.Send(new MatchMessage(this));
        }
    }

}
