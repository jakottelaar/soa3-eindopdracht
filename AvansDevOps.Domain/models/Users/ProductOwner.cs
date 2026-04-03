namespace AvansDevOps.Domain.Models.Users;

public class ProductOwner : IUser
{
    public string Name { get; set; }
    public string Email { get; set; }

    public ProductOwner(string name, string email)
    {
        Name = name;
        Email = email;
    }
}