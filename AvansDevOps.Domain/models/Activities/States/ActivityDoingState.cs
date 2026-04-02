namespace AvansDevOps.Domain.Models.Activities.States;

public class ActivityDoingState : IActivityState
{
    public void Start(Activity activity)
    {
        throw new InvalidOperationException();
    }

    public void Complete(Activity activity)
    {
        activity.SetState(new ActivityDoneState());
    }
}