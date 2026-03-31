using AvansDevOps.Domain.Models.Sprints.States;

namespace AvansDevOps.Domain.Models.Sprints;

public class ReviewSprint : ISprintType
{
    public void HandleFinish(Sprint sprint)
    {
        sprint.State = new SprintFinishedState();
    }
}
