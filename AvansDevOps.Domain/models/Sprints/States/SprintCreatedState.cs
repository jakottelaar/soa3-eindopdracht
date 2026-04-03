namespace AvansDevOps.Domain.Models.Sprints.States;

public class SprintCreatedState : ISprintState
{
    public void Start(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' started. Transitioning to Active state.");
        sprint.SetState(new SprintActiveState());
    }

    public void Finish(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot finish a sprint that hasn't been started yet.");
    }

    public void StartRelease(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot start release on a sprint that hasn't been started yet.");
    }

    public void ReleaseSucceeded(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot mark release as succeeded on a sprint that hasn't been started yet.");
    }

    public void ReleaseFailed(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot mark release as failed on a sprint that hasn't been started yet.");
    }

    public void Cancel(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' cancelled. Transitioning to Cancelled state.");
        sprint.SetState(new SprintCancelledState());
    }
}