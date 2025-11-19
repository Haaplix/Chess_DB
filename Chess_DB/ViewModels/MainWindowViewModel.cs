using CommunityToolkit.Mvvm.ComponentModel;
using Chess_DB.Data_Base_Services;
using System.IO;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CommunityToolkit.Mvvm.Input;

namespace Chess_DB.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    [ObservableProperty]
    private bool _isPaneOpen = true;
    [ObservableProperty]
    private ViewModelBase _currentPage = new HomePageViewModel();
    [RelayCommand]
    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    public MainWindowViewModel()
    {
        Console.WriteLine("Starting database test...");


    }
}
