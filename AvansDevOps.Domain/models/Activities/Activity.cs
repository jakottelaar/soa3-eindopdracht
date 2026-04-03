using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.Activities.States;
using AvansDevOps.Domain.Models.Notifications;

namespace AvansDevOps.Domain.Models.Activities;

public class Activity : IObservable
{
    public required string Title { get; set; }
    public IUser? AssignedUser { get; set; }
    public IActivityState State { get; set; } = new ActivityTodoState();
    
    private readonly List<IObserver> observers = [];

    public void SetState(IActivityState state)
    {
        State = state;
    }

    public IActivityState GetState()
    {
        return State;
    }

    public void Start() =>
        State.Start(this);

    public void Complete() =>
        State.Complete(this);

    public string GetCurrentStateName()
    {
        return State.GetType().Name.Replace("Activity", "").Replace("State", "");
    }

    public void DisplayStatus()
    {
        string assignee = AssignedUser?.Name ?? "Unassigned";
        Console.WriteLine($"      [{GetCurrentStateName()}] {Title} | Assigned: {assignee}");
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
}