namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemTodoState : IBacklogItemState
{
    public void Start(BacklogItem item)
    {
        Console.WriteLine($"  ✓ BacklogItem '{item.Title}' started. Moving to Doing state.");
        item.SetState(new BacklogItemDoingState());
    }

    public void MarkReadyForTesting(BacklogItem item)
    {
        throw new InvalidOperationException("Cannot mark as ready for testing. Item needs to be started first.");
    }

    public void StartTesting(BacklogItem item)
    {
        throw new InvalidOperationException("Cannot start testing. Item needs to be in Doing state first.");
    }

    public void Approve(BacklogItem item)
    {
        throw new InvalidOperationException("Cannot approve. Item needs to progress through workflow states first.");
    }

    public void Reject(BacklogItem item)
    {
        throw new InvalidOperationException("Cannot reject. Item is not yet in testing state.");
    }

}