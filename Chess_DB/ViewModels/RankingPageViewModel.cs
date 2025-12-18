using System;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Chess_DB.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using SQLitePCL;
using System.Diagnostics.CodeAnalysis;


namespace Chess_DB.ViewModels;

public partial class RankingPageViewModel : PlayersPageViewModel
{
    public RankingPageViewModel()
    {
        _ = RankingElo();
    }

    [ObservableProperty]
    private ObservableCollection<LightPlayerViewModel> playerEloRankList = new();
    private async Task RankingElo()
    {
        using (var context = new AppDbContext())
        {
            var players = await context.Players.OrderByDescending(p => p.ELO).ToListAsync();
            PlayerEloRankList.Clear();
            foreach (var player in players)
            {
                PlayerEloRankList.Add(new LightPlayerViewModel(player));
            }
        }
    }

}


