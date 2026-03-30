namespace AvansDevOps.Domain.Models.Sprint.States;

public class SprintCancelledState : ISprintState
{
    public void Next(Sprint context)
    {
        throw new InvalidOperationException("Activity is already in the last state.");
    }

    public void Previous(Sprint context)
    {
        context.State = new SPrintReleasedState();
    }
}