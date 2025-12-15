

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PlayerCompetition
{
    [Key]
    public int PlayerCompId { get; set; }

    [ForeignKey("Player")]
    public int PlayersplayerId { get; set; }

    [ForeignKey("Competition")]
    public int CompetitionsCompId { get; set; }

}
