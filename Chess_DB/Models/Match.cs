

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Match
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MatchId { get; set; }
    public int CompetitionId { get; set; }

    public Competition Competition { get; set; }

    public int Player1Id { get; set; }
    public Player Player1 { get; set; }

    public int Player2Id { get; set; }
    public Player Player2 { get; set; }

    public int WinnerId { get; set; }
    public Player Winner { get; set; }
    public List<string> PlayedPieces { get; set; }
}