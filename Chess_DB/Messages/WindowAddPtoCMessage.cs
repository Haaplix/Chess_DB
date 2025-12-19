using Chess_DB.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Chess_DB.Messages;

public class WindowAddPtoCMessage : AsyncRequestMessage<WindowAddPtoCViewModel?>
{
    public Competition? Comp { get; }

    public WindowAddPtoCMessage(Competition comp)
    {
        Comp = comp;
    }

}