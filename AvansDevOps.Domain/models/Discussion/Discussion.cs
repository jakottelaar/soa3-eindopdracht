using AvansDevOps.Domain.Models.BacklogItems;

using AvansDevOps.Domain.Models.Users;

using AvansDevOps.Domain.Models.Discussion.Composite;

using AvansDevOps.Domain.Models.BacklogItems.States;

namespace AvansDevOps.Domain.Models.Discussion;

public class Discussion
{
    private readonly DiscussionThread root;
    private readonly BacklogItem backlogItem;

    public DiscussionThread Root => root;

    public Discussion(BacklogItem backlogItem, IUser rootAuthor)
    {
        this.backlogItem = backlogItem;
        this.root = new DiscussionThread($"Root thread: {backlogItem.Title}", rootAuthor);
    }

    public void AddThread(DiscussionThread thread)
    {
        CheckNotLocked();
        root.Add(thread);
    }

    public void AddPost(DiscussionPost post)
    {
        CheckNotLocked();
        root.Add(post);
    }

    public void Accept(IDiscussionVisitor visitor) => root.Accept(visitor);

    public bool IsLocked() => backlogItem.State is BacklogItemDoneState;

    private void CheckNotLocked()
    {
        if (IsLocked())
            throw new InvalidOperationException("Discussion is locked: backlog item is done.");
    }
}