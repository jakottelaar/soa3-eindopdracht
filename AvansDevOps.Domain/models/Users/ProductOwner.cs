namespace AvansDevOps.Domain.Models.Users;

public class ProductOwner : IUser
{
    public string Name { get; set; }
    public string Email { get; set; }
}