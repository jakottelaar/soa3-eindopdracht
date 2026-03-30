namespace AvansDevOps.Domain.Models.Activities.States;

public class ActivityDoingState : IActivityState
{
    public void Next(Activity context)
    {
        context.State = new ActivityDoneState();
    }

    public void Previous(Activity context)
    {
        context.State = new ActivityTodoState();
    }
}