using AvansDevOps.Domain.Models.Activities.States;

namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemTestedState : IBacklogItemState
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
        throw new InvalidOperationException();
    }

     public void Approve(BacklogItem item)
    {
        if (item.Activities == null || item.Activities.All(a => a.GetState() is ActivityDoneState))
        {
            item.SetState(new BacklogItemDoneState());
        }
        else
        {
            throw new InvalidOperationException("Activities not finished");
        }
    }

    public void Reject(BacklogItem item)
    {
        item.SetState(new BacklogItemReadyForTestingState());
    }
}