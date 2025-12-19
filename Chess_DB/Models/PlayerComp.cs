using System.ComponentModel.DataAnnotations.Schema;

public class PlayerCompetition
{

    [ForeignKey("Player")]
    public int PlayerId { get; set; }

    [ForeignKey("Competition")]
    public int CompId { get; set; }

}
