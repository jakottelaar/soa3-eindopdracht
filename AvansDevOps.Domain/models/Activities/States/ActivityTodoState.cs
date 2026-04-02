namespace AvansDevOps.Domain.Models.Activities.States;

public class ActivityTodoState : IActivityState
{
    public void Start(Activity activity)
    {
        activity.SetState(new ActivityDoingState());
    }

    public void Complete(Activity activity)
    {
        throw new InvalidOperationException();
    }
}