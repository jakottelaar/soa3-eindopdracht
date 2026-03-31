namespace AvansDevOps.Domain.Models.Activities.States;

public class ActivityDoneState : IActivityState
{
    public void Next(Activity context)
    {
        throw new InvalidOperationException("Activity is already in the last state.");
    }

    public void Previous(Activity context)
    {
        context.State = new ActivityDoingState();
    }
}