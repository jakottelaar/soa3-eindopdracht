namespace AvansDevOps.Domain.Models.Sprints.States;

public class SprintActiveState : ISprintState
{
    public void Start(Sprint sprint)
    {
        throw new InvalidOperationException("Sprint is already active.");
    }

    public void Finish(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' finished. Transitioning to Finished state.");
        sprint.SetState(new SprintFinishedState());
    }

    public void StartRelease(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot start release while sprint is still active. Finish the sprint first.");
    }

    public void ReleaseSucceeded(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot mark release as succeeded while sprint is still active. Use StartRelease first.");
    }

    public void ReleaseFailed(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot mark release as failed while sprint is still active. Use StartRelease first.");
    }

    public void Cancel(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' cancelled while active. Transitioning to Cancelled state.");
        sprint.SetState(new SprintCancelledState());
    }
}