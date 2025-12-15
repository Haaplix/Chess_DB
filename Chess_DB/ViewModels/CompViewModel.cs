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

public partial class CompViewModel : ViewModelBase
{
    [ObservableProperty]
    public int _compId;
    [ObservableProperty]
    public string _compName;
    [ObservableProperty]
    public DateTime? _date;
    [ObservableProperty]
    public string _city;

    [ObservableProperty]
    public string _country;
    public Competition _currentComp;

    public CompViewModel(Competition comp)
    {
        CompId = comp.CompId;
        CompName = comp.CompName;
        Date = comp.Date.HasValue ? comp.Date.Value.ToDateTime(TimeOnly.MinValue) : null;
        City = comp.City;
        Country = comp.Country;
        _currentComp = comp;
        LoadPlayer();
    }

    [RelayCommand]
    private async Task OpendCompAsync()
    {
        var compusercontrol = WeakReferenceMessenger.Default.Send(new CompMessage(this));
    }

    [RelayCommand]
    private async Task OpendWindowEditCompAsync()
    {
        var editcompwindow = await WeakReferenceMessenger.Default.Send(new WindowEditCompMessage(_currentComp));
    }

    [RelayCommand]
    private async Task OpendWindowAdddMatchAsync()
    {
        var editcompwindow = await WeakReferenceMessenger.Default.Send(new WindowAddPtoCMessage(_currentComp));
    }


    [RelayCommand]
    private async Task Editcomp()
    {
        using (var context = new AppDbContext())
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            var editComp = new Competition
            {
                CompId = CompId,
                CompName = CompName,
                City = City,
                Country = Country,
                Date = Date.HasValue ? DateOnly.FromDateTime(Date.Value) : null,
            };
            context.Competitions.Update(editComp);
            await context.SaveChangesAsync();

            Console.WriteLine($"Competition modified: {CompName} {Country} {City} {Date} (ID generated: {CompId})");

        }
    }


    [ObservableProperty]
    private ObservableCollection<PlayerViewModel> playerList = new();

    [RelayCommand]
    public void LoadPlayer()
    {
        using (var context = new AppDbContext())
        {
            context.Database.EnsureCreated();
            var players = context.Players.ToListAsync().Result;
            PlayerList.Clear();

            foreach (var player in players)
            {
                PlayerList.Add(new PlayerViewModel(player));
            }
        }
    }


    [ObservableProperty]
    private string firstName_search;
    [ObservableProperty]
    private string lastName_search;
    [ObservableProperty]
    private string id_search;


    public static async Task<List<Player>> FindPlayerAsync(string? firstname, string? lastname, string? id)
    {
        using (var _context = new AppDbContext())
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



    [RelayCommand]
    private async Task SearchPlayers()
    {
        var result = await FindPlayerAsync(FirstName_search, LastName_search, Id_search);

        PlayerList.Clear();

        foreach (var p in result)
        {
            PlayerList.Add(new PlayerViewModel(p)
            {
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                ELO = p.ELO,
                PlayerID = p.playerID
            });
        }
    }
#pragma warning restore CS8601 // Possible null reference assignment.


}
