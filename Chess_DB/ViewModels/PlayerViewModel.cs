
using Microsoft.EntityFrameworkCore;

namespace Chess_DB.ViewModels;

public partial class PlayerViewModel : ViewModelBase
{
}

public class AddPlayer
{
    public void AddNewPlayer()
    {
        using (var context = new PlayerDbcontext())
        {
            
            context.Database.EnsureCreated();
            var player = new Player {Firstname = "Adam", Lastname = "JSP", ELO = 1223, playerID = 1333};

            context.Players.Add(player);
            context.SaveChanges();
        }
    }
}
