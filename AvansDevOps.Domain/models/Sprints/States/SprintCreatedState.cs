namespace AvansDevOps.Domain.Models.Sprints.States;

public class SprintCreatedState : ISprintState
{
    public void Next(Sprint context)
    {
        context.State = new SprintActiveState();
    }

    public void Previous(Sprint context)
    {
        throw new InvalidOperationException("Sprint is already in the first state.");
    }
}