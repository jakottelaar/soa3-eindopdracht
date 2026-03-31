namespace  AvansDevOps.Domain.Models.Sprints.States;

public class SprintReleasedState : ISprintState
{
    public void Next(Sprint context)
    {
        context.State = new SprintCancelledState();
    }

    public void Previous(Sprint context)
    {
        context.State = new SprintFinishedState();
    }
}