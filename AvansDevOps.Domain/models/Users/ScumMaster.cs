namespace AvansDevOps.Domain.Models.Users;

public class ScrumMaster : IUser
{
    public string Name { get; set; }
    public string Email { get; set; }

    public ScrumMaster(string name, string email)
    {
        Name = name;
        Email = email;
    }
}