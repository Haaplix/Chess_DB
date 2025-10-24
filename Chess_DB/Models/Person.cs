public class Person
{
    private string Firstname, Lastname;
    public Person(string Firstname, string Lastname)
    {
        this.Firstname = Firstname;
        this.Lastname = Lastname;
    }
    public string Displayname()
    {
        return Firstname + " " + Lastname;
    }
}