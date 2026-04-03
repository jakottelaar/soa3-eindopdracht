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
        // Only release sprints can start a release process
        if (!(sprint.SprintStrategy is ReleaseSprintStrategy))
        {
            throw new InvalidOperationException("Only release sprints can initiate a release process.");
        }

        Console.WriteLine($"Sprint '{sprint.Name}' starting release pipeline. Transitioning to Released state.");
        sprint.SetState(new SprintReleasedState());
    }

    public void ReleaseSucceeded(Sprint sprint)
    {
        throw new InvalidOperationException("Use StartRelease() to initiate the release process first.");
    }

    public void ReleaseFailed(Sprint sprint)
    {
        throw new InvalidOperationException("Use StartRelease() to initiate the release process first.");
    }

    public void Cancel(Sprint sprint)
    {
        throw new InvalidOperationException("Cannot cancel a sprint that is already finished.");
    }
}