namespace AvansDevOps.Domain.Models.Sprints.States;

public class SprintCancelledState : ISprintState
{
    public void Start(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot start a cancelled sprint.");
    }

    public void Finish(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot finish a cancelled sprint.");
    }

    public void StartRelease(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot start release on a cancelled sprint.");
    }

    public void ReleaseSucceeded(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot mark release as succeeded on a cancelled sprint.");
    }

    public void ReleaseFailed(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot mark release as failed on a cancelled sprint.");
    }

    public void Cancel(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' is already cancelled.");
    }
}