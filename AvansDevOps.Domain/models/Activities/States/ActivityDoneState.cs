namespace AvansDevOps.Domain.Models.Activities.States;

public class ActivityDoneState : IActivityState
{
    public void Start(Activity activity)
    {
        throw new InvalidOperationException();
    }

    public void Complete(Activity activity)
    {
        throw new InvalidOperationException();
    }
}