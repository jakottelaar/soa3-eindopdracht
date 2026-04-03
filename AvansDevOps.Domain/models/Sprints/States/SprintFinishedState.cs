namespace AvansDevOps.Domain.Models.Sprints.States;

public class SprintFinishedState : ISprintState
{
    public void Start(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot start a sprint that is already finished.");
    }

    public void Finish(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' is already finished.");
    }

    public void StartRelease(Sprint sprint)
    {
        Console.WriteLine($"Sprint '{sprint.Name}' starting release process. Transitioning to Released state.");
        sprint.SetState(new SprintReleasedState());
    }

    public void ReleaseSucceeded(Sprint sprint)
    {
        throw new InvalidOperationException("Sprint is already finished.");
    }

    public void ReleaseFailed(Sprint sprint)
    {
        throw new InvalidOperationException("Sprint is already finished.");
    }

    public void Cancel(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot cancel a sprint that is already finished.");
    }
}