using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Player
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int playerID { get; set; }
    public required string Firstname { get; set; }

    public required string Lastname { get; set; }
    public int ELO { get; set; }

    public List<Competition> Competitions { get; set; } = new();

    public List<Match> AsPlayer1 { get; set; } = new();
    public List<Match> AsPlayer2 { get; set; } = new();
    public List<Match> Winner { get; set; } = new();
}

