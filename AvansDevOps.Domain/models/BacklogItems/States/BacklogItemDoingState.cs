namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemDoingState : IBacklogItemState
{
    public void Start(BacklogItem item)
    {
        throw new InvalidOperationException();
    }

    public void MarkReadyForTesting(BacklogItem item)
    {
        item.SetState(new BacklogItemReadyForTestingState());
        item.NotifyObservers($"BacklogItem '{item.Title}' is now ready for testing.");
    }

    public void StartTesting(BacklogItem item)
    {
        throw new InvalidOperationException();
    }

    public void Approve(BacklogItem item)
    {
        throw new InvalidOperationException();
    }

    public void Reject(BacklogItem item)
    {
        throw new InvalidOperationException();
    }
}