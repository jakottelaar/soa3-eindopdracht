using AvansDevOps.Domain.Models.Activities.States;

namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemTestedState : IBacklogItemState
{
    public void Next(BacklogItem context)
    {
        if (context.Activities != null && context.Activities.Any(a => a.State is not ActivityDoneState))
        {
            throw new InvalidOperationException(
                "Cannot mark BacklogItem as done: not all activities are done.");
        }

        context.State = new BacklogItemDoneState();
    }

    public void Previous(BacklogItem context)
    {
        context.State = new BacklogItemReadyForTestingState();
    }
}