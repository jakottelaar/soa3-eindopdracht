namespace AvansDevOps.Domain.Models.Sprints.States;

public class SprintCancelledState : ISprintState
{
    public void Next(Sprint context)
    {
        throw new InvalidOperationException("Activity is already in the last state.");
    }

    public void Previous(Sprint context)
    {
        context.State = new SprintReleasedState();
    }
}