using AvansDevOps.Domain.Models.Users;

namespace AvansDevOps.Domain.Models;

public class BacklogItem
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int? StoryPoints { get; set; }
    public List<Activity>? Activities { get; set; }
    public IUser? AssignedUser { get; set; }
}