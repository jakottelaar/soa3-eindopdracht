namespace AvansDevOps.Domain.Models.Sprints;

public interface ISprintState
{
    void Start(Sprint sprint);
    void Finish(Sprint sprint);
    void StartRelease(Sprint sprint);
    void ReleaseSucceeded(Sprint sprint);
    void ReleaseFailed(Sprint sprint);
    void Cancel(Sprint sprint);
}