using Chess_DB.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;


namespace Chess_DB.Messages;

public class MatchMessage : ValueChangedMessage<MatchViewModel>
{
    public MatchMessage(MatchViewModel m) : base(m)
    {

    }
}