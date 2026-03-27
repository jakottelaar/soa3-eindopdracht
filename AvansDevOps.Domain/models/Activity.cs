using AvansDevOps.Domain.Models.Users;

namespace AvansDevOps.Domain.Models;

public class Activity
{
    public required string Title { get; set; }
    public IUser? AssignedUser { get; set; }
}