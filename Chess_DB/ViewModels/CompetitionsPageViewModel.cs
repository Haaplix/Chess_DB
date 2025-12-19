
using System;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;


namespace Chess_DB.ViewModels;

public partial class CompetitionsPageViewModel : ViewModelBase
{

    [ObservableProperty]
    [Required(ErrorMessage = "*")]
    private string? _competitionName;
    [ObservableProperty]
    public DateTime? _date;
    [ObservableProperty]
    [Required(ErrorMessage = "*")]
    private string? _city;
    [ObservableProperty]
    [Required(ErrorMessage = "*")]
    private string? _country;
    [ObservableProperty]
    private ObservableCollection<CompViewModel> compList = new();
    [ObservableProperty]
    private string name_search;
    [ObservableProperty]
    private string country_search;
    [ObservableProperty]
    private string city_search;
    [ObservableProperty]
    private string date_search;
    [ObservableProperty]
    private string id_search;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public CompetitionsPageViewModel()
    {
        LoadComp();
    }


    [RelayCommand]
    private async Task AddCompetition()
    {
        ValidateAllProperties();
        if (HasErrors) return;

        using (var context = new AppDbContext())
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            context.Database.EnsureCreated();
            var newCompetition = new Competition
            {
                CompName = CompetitionName,
                Date = Date.HasValue ? DateOnly.FromDateTime(Date.Value) : null,
                City = City,
                Country = Country,
            };
#pragma warning restore CS8601 // Possible null reference assignment.
            context.Competitions.Add(newCompetition);
            await context.SaveChangesAsync();

            CompList.Add(new CompViewModel(newCompetition));

            // Clear the input fields after adding the competition
            CompetitionName = City = Country = string.Empty;
            Date = null;
        }
    }

    [RelayCommand]
    private async Task OpendWindowAddCompetitionAsync()
    {
        // Send the message to the previously registered handler and await the selected album
        var competitionwindow = await WeakReferenceMessenger.Default.Send(new WindowCompetitionMessage());
    }


    [RelayCommand]
    public void LoadComp()
    {
        using (var context = new AppDbContext())
        {
            context.Database.EnsureCreated();
            var competitions = context.Competitions.ToListAsync().Result;
            CompList.Clear();

            foreach (var comp in competitions)
            {
                CompList.Add(new CompViewModel(comp));
            }
        }
    }


    public static async Task<List<Competition>> FindCompAsync(string? name, string? country, string? city, string? date, string? id)
    {
        using (var _context = new AppDbContext())
        {
            IQueryable<Competition> query = _context.Competitions;

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.CompName.Contains(name));

            if (!string.IsNullOrWhiteSpace(country))
                query = query.Where(c => c.Country.Contains(country));

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(c => c.City.Contains(city));

            if (!string.IsNullOrWhiteSpace(date))
            {
                if (DateTime.TryParse(date, out var parsedDate))
                {
                    var parsedDateOnly = DateOnly.FromDateTime(parsedDate);

                    query = query.Where(c => c.Date.HasValue && c.Date.Value == parsedDateOnly);
                }
            }

            if (!string.IsNullOrWhiteSpace(id))
                query = query.Where(c => c.CompId.ToString().Contains(id));

            return await query.ToListAsync();
        }
    }

    [RelayCommand]
    private async Task SearchCompetitions()
    {
        var results = await FindCompAsync(Name_search, Country_search, City_search, Date_search, Id_search);
        CompList.Clear();
#pragma warning disable CS8601 // Possible null reference assignment.
        foreach (var c in results)
        {
            CompList.Add(new CompViewModel(c)
            {
                CompId = c.CompId,
                CompName = c.CompName,
                Date = c.Date.HasValue ? c.Date.Value.ToDateTime(TimeOnly.MinValue) : null,
                City = c.City,
                Country = c.Country
            });

        }
#pragma warning restore CS8601 // Possible null reference assignment.
    }

}

