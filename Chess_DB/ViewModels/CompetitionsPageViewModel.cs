
using System;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Data;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;

namespace Chess_DB.ViewModels;

public partial class CompetitionsPageViewModel : ViewModelBase
{

    private int? _competitionId;
    [ObservableProperty]
    private string? _competitionName;
    [ObservableProperty]
    private string? _date;
    [ObservableProperty]
    private string? _city;
    [ObservableProperty]
    private string? _country;

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
}