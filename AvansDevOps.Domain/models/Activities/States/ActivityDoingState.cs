namespace AvansDevOps.Domain.Models.Activities.States;

public class ActivityDoingState : IActivityState
{
    public void Start(Activity activity)
    {
        throw new InvalidOperationException("Activity is already being worked on.");
    }

    public void Complete(Activity activity)
    {
        Console.WriteLine($"    • Activity '{activity.Title}' completed. Moving to Done state.");
        activity.SetState(new ActivityDoneState());
    }
}