using Chess_DB.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Chess_DB.Messages;

public class WindowAddMtoCMessage : AsyncRequestMessage<WindowAddMtoCViewModel?>
{
    public Competition? Comp { get; }

    public WindowAddMtoCMessage(Competition comp)
    {
        Comp = comp;
    }

}