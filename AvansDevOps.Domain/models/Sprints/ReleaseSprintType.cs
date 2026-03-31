using AvansDevOps.Domain.Models.Sprints.States;

namespace AvansDevOps.Domain.Models.Sprints;
public class ReleaseSprint : ISprintType
{
    public void HandleFinish(Sprint sprint)
    {
        sprint.State = new SprintReleasedState();
    }
}