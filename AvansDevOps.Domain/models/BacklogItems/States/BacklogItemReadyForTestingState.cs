namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemReadyForTestingState : IBacklogItemState
{
    public void Start(BacklogItem item)
    {
        throw new InvalidOperationException("Item is already in testing workflow.");
    }

    public void MarkReadyForTesting(BacklogItem item)
    {
        throw new InvalidOperationException("Item is already marked as ready for testing.");
    }

    public void StartTesting(BacklogItem item)
    {
        Console.WriteLine($"  ✓ BacklogItem '{item.Title}' testing started. Moving to Testing state.");
        item.SetState(new BacklogItemTestingState());
    }

    public void Approve(BacklogItem item)
    {
        throw new InvalidOperationException("Item must be in Testing state to approve.");
    }

    public void Reject(BacklogItem item)
    {
        throw new InvalidOperationException("Item must be in Testing state to reject.");
    }
}