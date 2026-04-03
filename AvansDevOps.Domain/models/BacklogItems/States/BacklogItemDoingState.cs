namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemDoingState : IBacklogItemState
{
    public void Start(BacklogItem item)
    {
        throw new InvalidOperationException("Item is already being worked on.");
    }

    public void MarkReadyForTesting(BacklogItem item)
    {
        Console.WriteLine($"  ✓ BacklogItem '{item.Title}' marked ready for testing. Moving to ReadyForTesting state.");
        item.SetState(new BacklogItemReadyForTestingState());
        item.NotifyObservers($"BacklogItem '{item.Title}' is now ready for testing.");
    }

    public void StartTesting(BacklogItem item)
    {
        throw new InvalidOperationException("Item must be marked as ready for testing first.");
    }

    public void Approve(BacklogItem item)
    {
        throw new InvalidOperationException("Item cannot be approved while in Doing state. Complete work first.");
    }

    public void Reject(BacklogItem item)
    {
        throw new InvalidOperationException("Cannot reject. Item is not yet in testing state.");
    }
}