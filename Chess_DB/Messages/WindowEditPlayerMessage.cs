using Chess_DB.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;

namespace Chess_DB.Messages;


public class WindowEditPlayerMessage : AsyncRequestMessage<WindowEditPlayerViewModel?>
{
    public Player? PlayerToEdit { get; }

    public WindowEditPlayerMessage(Player player)
    {
        PlayerToEdit = player;
    }

}
