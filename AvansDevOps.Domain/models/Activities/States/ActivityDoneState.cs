namespace AvansDevOps.Domain.Models.Activities.States;

public class ActivityDoneState : IActivityState
{
    public void Start(Activity activity)
    {
        throw new InvalidOperationException("Cannot start an activity that is already done.");
    }

    public void Complete(Activity activity)
    {
        Console.WriteLine($"Activity '{activity.Title}' is already completed.");
    }
}