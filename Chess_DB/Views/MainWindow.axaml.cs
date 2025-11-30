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

        // Whenever 'Send(new PurchaseAlbumMessage())' is called, invoke this callback on the MainWindow instance:
        WeakReferenceMessenger.Default.Register<MainWindow, WindowPlayerMessage>(this, static (w, m) =>
        {
            // Create an instance of MusicStoreWindow and set MusicStoreViewModel as its DataContext.
            var dialog = new AddPlayerWindow
            {
                DataContext = new PlayersPageViewModel()
            };
            // Show dialog window and reply with returned AlbumViewModel or null when the dialog is closed.
            m.Reply(dialog.ShowDialog<WindowPlayerViewModel?>(w));
        });

        // Whenever 'Send(new PurchaseAlbumMessage())' is called, invoke this callback on the MainWindow instance:
        WeakReferenceMessenger.Default.Register<MainWindow, WindowCompetitionMessage>(this, static (w, m) =>
        {
            // Create an instance of MusicStoreWindow and set MusicStoreViewModel as its DataContext.
            var dialog = new AddCompetitionWindow
            {
                DataContext = new CompetitionsPageViewModel()
            };
            // Show dialog window and reply with returned AlbumViewModel or null when the dialog is closed.
            m.Reply(dialog.ShowDialog<WindowCompetitionViewModel?>(w));
        });
    }
}

