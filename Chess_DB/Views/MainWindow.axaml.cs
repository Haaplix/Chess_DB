using System;
using Avalonia.Controls;
using Chess_DB.Messages;
using Chess_DB.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Chess_DB.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        if (Design.IsDesignMode)
            return;


        WeakReferenceMessenger.Default.Register<MainWindow, WindowPlayerMessage>(this, static (w, m) =>
        {
            var dialog = new AddPlayerWindow
            {
                DataContext = new PlayersPageViewModel()
            };
            m.Reply(dialog.ShowDialog<WindowPlayerViewModel?>(w));
        });



        WeakReferenceMessenger.Default.Register<MainWindow, WindowCompetitionMessage>(this, static (w, m) =>
        {
            var dialog = new AddCompetitionWindow
            {
                DataContext = new CompetitionsPageViewModel()
            };
            m.Reply(dialog.ShowDialog<WindowCompetitionViewModel?>(w));
        });



        WeakReferenceMessenger.Default.Register<MainWindow, WindowEditPlayerMessage>(this, static (w, m) =>
        {
            var dialog = new EditPlayerView
            {
                DataContext = new PlayerViewModel(m.PlayerToEdit)
            };
            m.Reply(dialog.ShowDialog<WindowEditPlayerViewModel?>(w));
        });

        WeakReferenceMessenger.Default.Register<MainWindow, WindowEditCompMessage>(this, static (w, m) =>
        {
            var dialog = new EditCompView
            {
                DataContext = new CompViewModel(m.CompToEdit)
            };
            m.Reply(dialog.ShowDialog<WindowEditCompViewModel?>(w));
        });
    }

}




