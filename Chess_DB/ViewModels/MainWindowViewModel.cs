using CommunityToolkit.Mvvm.ComponentModel;
using Chess_DB.Data_Base_Services;
using System.IO;
using System;

namespace Chess_DB.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    public MainWindowViewModel()
    {
        Console.WriteLine("Starting database test...");

        TestProgram.TestDatabase();
    }
}
