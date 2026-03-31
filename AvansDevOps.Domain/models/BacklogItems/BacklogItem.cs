using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.Activities;
using AvansDevOps.Domain.Models.BacklogItems.States;

namespace AvansDevOps.Domain.Models.BacklogItems;

public class BacklogItem
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int? StoryPoints { get; set; }
    public List<Activity>? Activities { get; set; }
    public IUser? AssignedUser { get; set; }
    public IBacklogItemState State { get; set; } = new BacklogItemTodoState();

    public void NextState() =>
        State.Next(this);

    public void PreviousState() =>
        State.Previous(this);
}