using Chess_DB.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;


namespace Chess_DB.Messages;

public class CompMessage : ValueChangedMessage<CompViewModel>
{
    public CompMessage(CompViewModel c) : base(c)
    {

    }
}