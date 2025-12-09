using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System;

using CommunityToolkit.Mvvm.Input;
using System.Reflection.Emit;
using System.Collections.ObjectModel;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Threading.Tasks;

namespace Chess_DB.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IRecipient<PlayerMessage>, IRecipient<CompMessage>
{

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
        WeakReferenceMessenger.Default.Register<PlayerMessage>(this);
        WeakReferenceMessenger.Default.Register<CompMessage>(this);
    }

    public void Receive(PlayerMessage message)
    {
        CurrentPage = message.Value;
    }

    public void Receive(CompMessage message)
    {
        CurrentPage = message.Value;
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



