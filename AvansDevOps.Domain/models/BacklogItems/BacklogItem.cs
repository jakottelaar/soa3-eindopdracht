using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.Activities;
using AvansDevOps.Domain.Models.Activities.States;
using AvansDevOps.Domain.Models.BacklogItems.States;
using AvansDevOps.Domain.Models.Notifications;

namespace AvansDevOps.Domain.Models.BacklogItems;

public class BacklogItem : IObservable
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int? StoryPoints { get; set; }
    public List<Activity>? Activities { get; set; }
    public IUser? AssignedUser { get; set; }
    public IBacklogItemState State { get; set; } = new BacklogItemTodoState();
    private readonly List<IObserver> observers = [];
    private readonly Dictionary<Activity, IObserver> activityObservers = [];

    public void Start() => 
        State.Start(this);

    public void MarkReadyForTesting() => 
        State.MarkReadyForTesting(this);

    public void StartTesting() => 
        State.StartTesting(this);

    public void Approve() => 
        State.Approve(this);

    public void Reject() => 
        State.Reject(this);

    public void ResetToTodoAndNotifyScrumMaster()
    {
        if (State is not BacklogItemTestedState)
        {
            throw new InvalidOperationException("Item can only be reset to todo from Tested state.");
        }

        SetState(new BacklogItemTodoState());
        NotifyObservers($"NOTIFICATION: Scrum Master Attention\nBacklog item '{Title}' was moved from Tested to Todo for rework.");
    }
        
    public void SetState(IBacklogItemState state)
    {
        State = state;
    }

    public IBacklogItemState GetState()
    {
        return State;
    }
    
    public void NotifyObservers(string message)
    {
        foreach (var observer in observers)        
        {
            observer.Update(message);
        }
    }

    public void Subscribe(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Unsubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }

    public string GetCurrentStateName()
    {
        return State.GetType().Name.Replace("BacklogItem", "").Replace("State", "");
    }

    public void DisplayStatus()
    {
        string assignee = AssignedUser?.Name ?? "Unassigned";
        string points = StoryPoints.HasValue ? StoryPoints.ToString() : "N/A";
        Console.WriteLine($"    [{GetCurrentStateName()}] {Title} | Assigned: {assignee} | Points: {points}");
        
        // Display activities if any exist
        if (Activities != null && Activities.Count > 0)
        {
            Console.WriteLine($"      Activities ({Activities.Count}):");
            foreach (var activity in Activities)
            {
                activity.DisplayStatus();
            }
        }
    }

    public void AddActivity(Activity activity)
    {
        Activities ??= new List<Activity>();
        Activities.Add(activity);

        var observer = new ActivityStateObserver(this);
        activity.Subscribe(observer);
        activityObservers[activity] = observer;

        HandleActivityStateChanged();
        Console.WriteLine($"  ✓ Added activity '{activity.Title}' to backlog item '{Title}'");
    }

    public void RemoveActivity(Activity activity)
    {
        if (activityObservers.TryGetValue(activity, out var observer))
        {
            activity.Unsubscribe(observer);
            activityObservers.Remove(activity);
        }

        if (Activities != null && Activities.Remove(activity))
        {
            Console.WriteLine($"  ✓ Removed activity '{activity.Title}' from backlog item '{Title}'");
        }
        else
        {
            Console.WriteLine($"  ✗ Activity '{activity.Title}' not found in backlog item '{Title}'");
        }
    }

    public List<Activity> GetActivities()
    {
        return Activities ?? new List<Activity>();
    }

    public bool AreAllActivitiesDone()
    {
        if (Activities == null || Activities.Count == 0)
            return true; // No activities means all are "done"

        return Activities.All(a => a.GetState() is ActivityDoneState);
    }

    private void HandleActivityStateChanged()
    {
        if (State is BacklogItemDoneState && !AreAllActivitiesDone())
        {
            SetState(new BacklogItemTestedState());
            NotifyObservers($"NOTIFICATION: Backlog Item Reopened\nBacklog item '{Title}' moved from Done to Tested because an activity regressed.");
        }
    }

    private sealed class ActivityStateObserver(BacklogItem backlogItem) : IObserver
    {
        public void Update(string message)
        {
            _ = message;
            backlogItem.HandleActivityStateChanged();
        }
    }
}