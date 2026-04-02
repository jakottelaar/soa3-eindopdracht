namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemReadyForTestingState : IBacklogItemState
{
    public void Start(BacklogItem item)
    {
        throw new InvalidOperationException();
    }

    public void MarkReadyForTesting(BacklogItem item)
    {
        throw new InvalidOperationException();
    }

     public void StartTesting(BacklogItem item)
    {
        item.SetState(new BacklogItemTestingState());
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