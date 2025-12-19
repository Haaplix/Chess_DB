using Chess_DB.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;


namespace Chess_DB.Messages;

public class PlayerMessage : ValueChangedMessage<PlayerViewModel>
{
    public PlayerMessage(PlayerViewModel p) : base(p)
    {

    }
}