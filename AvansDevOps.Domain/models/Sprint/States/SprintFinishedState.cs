namespace AvansDevOps.Domain.Models.Sprint.States;

public class SprintFinishedState : ISprintState
{
    public void Next(Sprint context)
    {
        context.State = new SprintReleasedState();
    }

    public void Previous(Sprint context)
    {
        context.State = new SprintActiveState();
    }
}