namespace AvansDevOps.Domain.Models.Sprints.States;

public class SprintFinishedState : ISprintState
{
    public void Start(Sprint sprint)
    {
        throw new NotImplementedException();
    }

    public void Finish(Sprint sprint)
    {
        throw new NotImplementedException();
    }

    public void StartRelease(Sprint sprint)
    {
        throw new NotImplementedException();
    }

    public void ReleaseSucceeded(Sprint sprint)
    {
        throw new NotImplementedException();
    }

    public void ReleaseFailed(Sprint sprint)
    {
        throw new NotImplementedException();
    }

    public void Cancel(Sprint sprint)
    {
        throw new NotImplementedException();
    }
}