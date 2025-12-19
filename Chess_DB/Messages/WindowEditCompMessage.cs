using Chess_DB.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;


namespace Chess_DB.Messages;


public class WindowEditCompMessage : AsyncRequestMessage<WindowEditCompViewModel?>
{
    public Competition? CompToEdit { get; }

    public WindowEditCompMessage(Competition comp)
    {
        CompToEdit = comp;
    }

}
