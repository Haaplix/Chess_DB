using System.Linq.Expressions;
using Avalonia.Dialogs.Internal;
using Microsoft.EntityFrameworkCore;

public class Admin: Person
{
    private string Password;
    public Admin(string FirstName, string LastName, string Password): base(FirstName, LastName)
    {
        this.Password = Password;
    }

    public bool CheckPassword(string Password)
    {
        return this.Password == Password;
    }

    
}