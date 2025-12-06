using System.IO;
using System;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;



public static class Connexion
{

    public static async Task<List<Player>> FindPlayerAsync(string? firstname, string? lastname, string? id)
    {
        using (var _context = new PlayerDbcontext())
        {
            IQueryable<Player> query = _context.Players;

            if (!string.IsNullOrWhiteSpace(firstname))
                query = query.Where(p => p.Firstname.Contains(firstname));

            if (!string.IsNullOrWhiteSpace(lastname))
                query = query.Where(p => p.Lastname.Contains(lastname));

            if (!string.IsNullOrWhiteSpace(id))
                query = query.Where(p => p.playerID.ToString().Contains(id));

            return await query.ToListAsync();
        }

    }

    public static async Task<List<Competition>> FindCompAsync(string? name, string? country, string? city, string? date, string? id)
    {
        using (var _context = new CompetitionDbcontext())
        {
            IQueryable<Competition> query = _context.Competitions;

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.CompName.Contains(name));

            if (!string.IsNullOrWhiteSpace(country))
                query = query.Where(p => p.country.Contains(country));

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(p => p.city.Contains(city));

            if (!string.IsNullOrWhiteSpace(date))
                query = query.Where(p => p.date.Contains(date));

            if (!string.IsNullOrWhiteSpace(id))
                query = query.Where(p => p.CompId.ToString().Contains(id));

            return await query.ToListAsync();
        }
    }


}