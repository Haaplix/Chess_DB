using Chess_DB.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;

namespace Chess_DB.Messages;


public class WindowEditMatchMessage : AsyncRequestMessage<WindowEditMatchViewModel?>
{
    public Match? MatchToEdit { get; }
    public Player player1;
    public Player player2;
    public Player Winner;
    public Competition competition;
    public List<string> listplay;

    public WindowEditMatchMessage(Match match, Player p1, Player p2, Player winner, Competition comp, List<string> playedPiece)
    {
        MatchToEdit = match;
        player1 = p1;
        player2 = p2;
        Winner = winner;
        competition = comp;
        listplay = playedPiece;
    }

}