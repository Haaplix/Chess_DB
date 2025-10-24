public class Admin: Person
{
    private string Password;
    public Admin(string Password): base("FirstName", "Lastname")
    {
        this.Password = Password;
    }

    public bool CheckPassword(string Password)
    {
        return this.Password == Password;
    }
}