namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemReadyForTestingState : IBacklogItemState
{
    public void Next(BacklogItem context)
    {
        context.State = new BacklogItemTestingState();
    }

    public void Previous(BacklogItem context)
    {
        context.State = new BacklogItemDoingState();
    }
}