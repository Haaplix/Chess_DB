public class Player :Person
{
    private int ELO;
    private int playerID;
    private string picture;
    public Player(string Firstname, string Lastname, int ELO, int playerID, string picture):base(Firstname,Lastname)
    {
        this.ELO = ELO;
        this.playerID = playerID;
        this.picture = picture;
    }

}