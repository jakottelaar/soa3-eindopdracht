namespace AvansDevOps.Domain.Models.Activities.States;

public class ActivityTodoState : IActivityState
{
    public void Next(Activity context)
    {
        context.State = new ActivityDoingState();
    }

    public void Previous(Activity context)
    {
        throw new InvalidOperationException("Activity is already in the first state.");
    }
}