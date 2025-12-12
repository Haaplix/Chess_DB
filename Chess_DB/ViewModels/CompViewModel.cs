using System;
using System.Threading.Tasks;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;


namespace Chess_DB.ViewModels;

public partial class CompViewModel : ViewModelBase
{
    [ObservableProperty]
    public int _compId;
    [ObservableProperty]
    public string _compName;
    [ObservableProperty]
    public DateOnly? _date;
    [ObservableProperty]
    public string _city;

    [ObservableProperty]
    public string _country;
    public Competition _currentComp;

    public CompViewModel(Competition comp)
    {
        CompId = comp.CompId;
        CompName = comp.CompName;
        Date = comp.Date;
        City = comp.City;
        Country = comp.Country;
        _currentComp = comp;
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
    private async Task Editcomp()
    {
        using (var context = new AppDbContext())
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            var editComp = new Competition
            {
                CompName = CompName,
                City = City,
                Country = Country,
            };
            context.Competitions.Update(editComp);
            await context.SaveChangesAsync();

            Console.WriteLine($"Competition modified: {CompName} {Country} {City} {Date} (ID generated: {CompId})");

        }
    }
}
