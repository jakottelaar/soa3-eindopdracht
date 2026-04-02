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

    public void Start() =>
        State.Start(this);

    public void Complete() =>
        State.Complete(this);

    public string GetCurrentStateName()
    {
        return State.GetType().Name.Replace("Activity", "").Replace("State", "");
    }

    public void DisplayStatus()
    {
        string assignee = AssignedUser?.Name ?? "Unassigned";
        Console.WriteLine($"      [{GetCurrentStateName()}] {Title} | Assigned: {assignee}");
    }
}