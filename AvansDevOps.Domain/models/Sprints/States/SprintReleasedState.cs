namespace  AvansDevOps.Domain.Models.Sprints.States;

public class SprintReleasedState : ISprintState
{
    public void Start(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot start a sprint that is already in released state.");
    }

    public void Finish(Sprint sprint)
    {
        throw new InvalidOperationException("Use ReleaseSucceeded() or ReleaseFailed() to complete a release.");
    }

    public void StartRelease(Sprint sprint)
    {
        throw new InvalidOperationException("Sprint is already in released state.");
    }

    // volgens mij klopt dit niet want als een sprint gereleased is hoeft hij niet terug naar finished state
    public void ReleaseSucceeded(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' release succeeded! Transitioning to Finished state.");
        sprint.SetState(new SprintFinishedState());
    }

    public void ReleaseFailed(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' release failed. Returning to Finished state for retry.");
        sprint.SetState(new SprintFinishedState());
    }

    public void Cancel(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' cancelled during release. Transitioning to Cancelled state.");
        sprint.SetState(new SprintCancelledState());
    }
}