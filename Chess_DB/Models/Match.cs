

using System.ComponentModel.DataAnnotations;

public class Match
{
    [Key]
    public int MatchId { get; set; }
    public int CompetitionId { get; set; }

    public Competition Competition { get; set; }

    public int Player1Id { get; set; }
    public Player Player1 { get; set; }

    public int Player2Id { get; set; }
    public Player Player2 { get; set; }

    public int WinnerId { get; set; }
    public Player Winner { get; set; }
    public string PlayedPieces { get; set; } = string.Empty;
}