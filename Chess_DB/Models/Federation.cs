using System.Collections.Generic;

public class Federation
{
    private List<Player> players; //changer avec la base de données
    private List<Competition> competitions; //changer avec la base de données
    public Federation(List<Player> players, List<Competition> competitions)
    {
        this.players = players;
        this.competitions = competitions;
    }
    public void AddPlayer()
    {
        //ajouter le player à la base de donées
    }

    public void AddCompetition()
    {
        //ajouter la competition à la base de donées
    }
}