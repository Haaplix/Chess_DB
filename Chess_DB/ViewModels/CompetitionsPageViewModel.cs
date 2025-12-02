
using System;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Data;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Chess_DB.ViewModels;

public partial class CompetitionsPageViewModel : ViewModelBase
{

    private int? _competitionId;
    [ObservableProperty]
    [Required(ErrorMessage = "*")]
    private string? _competitionName;
    [ObservableProperty]
    [Required(ErrorMessage = "*")]
    private string? _date;
    [ObservableProperty]
    [Required(ErrorMessage = "*")]
    private string? _city;
    [ObservableProperty]
    [Required(ErrorMessage = "*")]
    private string? _country;


    public CompetitionsPageViewModel()
    {
        LoadComp();
    }


    [RelayCommand]
    private async Task AddCompetition()
    {
        ValidateAllProperties();
        if (HasErrors) return;

        using (var context = new CompetitionDbcontext())
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            context.Database.EnsureCreated();

            var newCompetition = new Competition
            {

                CompName = CompetitionName,
                date = Date,
                city = City,
                country = Country,
            };
#pragma warning restore CS8601 // Possible null reference assignment.
            context.Competitions.Add(newCompetition);
            await context.SaveChangesAsync();



            // Clear the input fields after adding the competition
            CompetitionName = Date = City = Country = string.Empty;
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
        using (var context = new CompetitionDbcontext())
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

    [RelayCommand]
    private void SearchCompetitions()
    {
        DataTable result = Connexion.FindComp(Name_search, Country_search, City_search, Date_search, Id_search);
        Console.WriteLine(Name_search);
        CompList.Clear();

        foreach (DataRow row in result.Rows)
        {
            CompList.Add(new Competition
            {
                CompName = row["CompName"].ToString(),
                country = row["country"].ToString(),
                city = row["city"].ToString(),
                date = row["date"].ToString(),
                CompId = Convert.ToInt32(row["CompId"])
            });
            Console.WriteLine(row["Firstname"].ToString());
        }
    }

}

