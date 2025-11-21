using System;
using Chess_DB.ViewModels;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Chess_DB.Messages;

public class WindowCompetitionMessage : AsyncRequestMessage<WindowCompetitionViewModel?>;
