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

    public CompViewModel(Competition comp)
    {
        CompId = comp.CompId;
        CompName = comp.CompName;
        Date = comp.Date;
        City = comp.City;
        Country = comp.Country;

    }

    [RelayCommand]
    private async Task OpendCompAsync()
    {
        var compusercontrol = WeakReferenceMessenger.Default.Send(new CompMessage(this));
    }
}
