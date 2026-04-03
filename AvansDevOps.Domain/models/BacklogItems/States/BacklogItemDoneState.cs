namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemDoneState : IBacklogItemState
{
    public void Start(BacklogItem item)
    {
        throw new InvalidOperationException("BacklogItem is already done and cannot be worked on again.");
    }

    public void MarkReadyForTesting(BacklogItem item)
    {
        throw new InvalidOperationException("BacklogItem is already done.");
    }

    public void StartTesting(BacklogItem item)
    {
        throw new InvalidOperationException("BacklogItem is already done.");
    }

    public void Approve(BacklogItem item)
    {
        throw new InvalidOperationException("BacklogItem is already done.");
    }

    public void Reject(BacklogItem item)
    {
        throw new InvalidOperationException("Cannot reject a completed backlog item.");
    }
}