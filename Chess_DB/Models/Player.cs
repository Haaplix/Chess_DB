using System.IO;
using System;

using Microsoft.EntityFrameworkCore;
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
}

