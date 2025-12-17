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
    public string _playedPieces = string.Empty;

    public MatchViewModel(Match match)
    {
        MatchId = match.MatchId;
        CompetitionId = match.CompetitionId;
        Player1Id = match.Player1Id;
        Player2Id = match.Player2Id;
        WinnerId = match.WinnerId;
    }
}
