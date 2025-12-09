using System.IO;
using System;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
public class Competition
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int CompId { get; set; }
    public required string CompName { get; set; }
    public DateOnly? date { get; set; }
    public required string city { get; set; }
    public required string country { get; set; }

    public List<Player> Players { get; set; } = new();

}