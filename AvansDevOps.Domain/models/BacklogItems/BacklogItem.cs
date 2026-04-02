using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.Activities;
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
}