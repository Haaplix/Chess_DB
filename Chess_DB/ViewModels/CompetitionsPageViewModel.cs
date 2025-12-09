
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
                date = Date.HasValue ? DateOnly.FromDateTime(Date.Value) : null,
                city = City,
                country = Country,
            };
#pragma warning restore CS8601 // Possible null reference assignment.
            context.Competitions.Add(newCompetition);
            await context.SaveChangesAsync();



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


    [ObservableProperty]
    private ObservableCollection<Competition> compList = new();

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
                CompList.Add(comp);
            }
        }
    }

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


    public static async Task<List<Competition>> FindCompAsync(string? name, string? country, string? city, string? date, string? id)
    {
        using (var _context = new AppDbContext())
        {
            IQueryable<Competition> query = _context.Competitions;

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.CompName.Contains(name));

            if (!string.IsNullOrWhiteSpace(country))
                query = query.Where(p => p.country.Contains(country));

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(p => p.city.Contains(city));

            if (!string.IsNullOrWhiteSpace(date))
            {
                if (DateTime.TryParse(date, out var parsedDate))
                {
                    var parsedDateOnly = DateOnly.FromDateTime(parsedDate);

                    query = query.Where(p => p.date.HasValue && p.date.Value == parsedDateOnly);
                }
            }

            if (!string.IsNullOrWhiteSpace(id))
                query = query.Where(p => p.CompId.ToString().Contains(id));

            return await query.ToListAsync();
        }
    }



    [RelayCommand]
    private async Task SearchCompetitions()
    {
        var results = await FindCompAsync(Name_search, Country_search, City_search, Date_search, Id_search);
        CompList.Clear();
#pragma warning disable CS8601 // Possible null reference assignment.
        foreach (var p in results)
        {
            CompList.Add(new Competition
            {
                CompId = p.CompId,
                CompName = p.CompName,
                date = p.date,
                city = p.city,
                country = p.country
            });

        }
#pragma warning restore CS8601 // Possible null reference assignment.
    }

}

