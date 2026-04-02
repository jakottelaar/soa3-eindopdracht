using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.Activities.States;

namespace AvansDevOps.Domain.Models.Activities;

public class Activity
{
    public required string Title { get; set; }
    public IUser? AssignedUser { get; set; }
    public IActivityState State { get; set; } = new ActivityTodoState();

    public void SetState(IActivityState state)
    {
        State = state;
    }

    public IActivityState GetState()
    {
        return State;
    }
}