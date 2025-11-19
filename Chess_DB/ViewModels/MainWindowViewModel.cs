using CommunityToolkit.Mvvm.ComponentModel;
using Chess_DB.Data_Base_Services;
using System.IO;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CommunityToolkit.Mvvm.Input;
using System.Reflection.Emit;
using System.Collections.ObjectModel;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls;

namespace Chess_DB.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    [ObservableProperty]
    private bool _isPaneOpen = true;
    [ObservableProperty]
    private ViewModelBase _currentPage = new CompetitionsPageViewModel();
    [ObservableProperty]
    private ListItemTemplate? _selectedListItem;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;
        var instance = Activator.CreateInstance(value.ModelType);
        if (instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }

    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(CompetitionsPageViewModel),"organization_regular"),
        new ListItemTemplate(typeof(PlayersPageViewModel),"PersonRegular"),
    };


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

public class ListItemTemplate
{
    public Type ModelType { get; }
    public string Label { get; }
    public StreamGeometry ListItemIcon { get; }
    public ListItemTemplate(Type type, string iconKey)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");

        Application.Current!.TryFindResource(iconKey, out var res);
        ListItemIcon = (StreamGeometry)res!;
    }
}