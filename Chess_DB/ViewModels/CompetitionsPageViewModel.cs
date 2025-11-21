using CommunityToolkit.Mvvm.Input;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Threading.Tasks;

namespace Chess_DB.ViewModels;

public partial class CompetitionsPageViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task OpendWindowAddCompetitionAsync()
    {
        // Send the message to the previously registered handler and await the selected album
        var competitionwindow = await WeakReferenceMessenger.Default.Send(new WindowCompetitionMessage());
    }
}