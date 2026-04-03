namespace AvansDevOps.Domain.Models.Users;

public class Developer : IUser
{
    public string Name { get; set; }
    public string Email { get; set; }
}