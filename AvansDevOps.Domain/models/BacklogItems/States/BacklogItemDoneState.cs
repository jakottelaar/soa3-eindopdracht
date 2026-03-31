namespace AvansDevOps.Domain.Models.BacklogItems.States;

public class BacklogItemDoneState : IBacklogItemState
{
    public void Next(BacklogItem context)
    {
        throw new InvalidOperationException("BacklogItem is already done.");
    }

    public void Previous(BacklogItem context)
    {
        throw new InvalidOperationException("Cannot reopen a done BacklogItem.");
    }
}