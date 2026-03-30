namespace AvansDevOps.Domain.Models.Sprint.States;

public class SprintActiveState : ISprintState
{
    public void Next(Sprint context)
    {
        context.State = new SprintFinishedState();
    }

    public void Previous(Sprint context)
    {
        context.State = new SprintCreatedState();
    }
}