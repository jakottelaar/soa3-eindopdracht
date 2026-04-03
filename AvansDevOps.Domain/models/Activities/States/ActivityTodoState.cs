namespace AvansDevOps.Domain.Models.Activities.States;

public class ActivityTodoState : IActivityState
{
    public void Start(Activity activity)
    {
        Console.WriteLine($"    • Activity '{activity.Title}' started. Moving to Doing state.");
        activity.SetState(new ActivityDoingState());
    }

    public void Complete(Activity activity)
    {
        throw new InvalidOperationException("Cannot complete an activity that hasn't been started yet.");
    }
}