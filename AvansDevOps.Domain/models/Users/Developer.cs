namespace AvansDevOps.Domain.Models.Users;

public class Developer : IUser
{
    public string Name { get; set; }
    public string Email { get; set; }

    public Developer(string name, string email)
    {
        Name = name;
        Email = email;
    }
}