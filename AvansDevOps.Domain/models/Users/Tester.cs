namespace AvansDevOps.Domain.Models.Users;

public class Tester : IUser
{
    public string Name { get; set; }
    public string Email { get; set; }

    public Tester(string name, string email)
    {
        Name = name;
        Email = email;
    }
}