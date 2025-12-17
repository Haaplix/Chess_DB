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

public partial class LightCompViewModel : ViewModelBase
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
    [ObservableProperty]
    public Competition _currentComp;

    public LightCompViewModel(Competition comp)
    {
        CompId = comp.CompId;
        CompName = comp.CompName;
        Date = comp.Date.HasValue ? comp.Date.Value.ToDateTime(TimeOnly.MinValue) : null;
        City = comp.City;
        Country = comp.Country;
        CurrentComp = comp;
    }

    [RelayCommand]
    private async Task OpendCompAsync()
    {
        var playerusercontrol = WeakReferenceMessenger.Default.Send(new CompMessage(new CompViewModel(CurrentComp)));
    }
}
