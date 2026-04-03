using AvansDevOps.Domain.Models.BacklogItems;

using AvansDevOps.Domain.Models.Users;

using AvansDevOps.Domain.Models.Discussion.Composite;

using AvansDevOps.Domain.Models.BacklogItems.States;

namespace AvansDevOps.Domain.Models.Discussion;

public class Discussion
{
    private readonly DiscussionThread _root;
    private readonly BacklogItem _backlogItem;

    public DiscussionThread Root => _root;

    public Discussion(BacklogItem backlogItem, IUser rootAuthor)
    {
        _backlogItem = backlogItem;
        _root = new DiscussionThread($"Root thread: {backlogItem.Title}", rootAuthor);
    }

    public void AddThread(DiscussionThread thread)
    {
        CheckNotLocked();
        _root.Add(thread);
    }

    public void AddPost(DiscussionPost post)
    {
        CheckNotLocked();
        _root.Add(post);
    }

    public void Accept(IDiscussionVisitor visitor) => _root.Accept(visitor);

    // Locked wanneer backlog item 'done' is (casus requirement)
    public bool IsLocked() => _backlogItem.State is BacklogItemDoneState;

    private void CheckNotLocked()
    {
        if (IsLocked())
            throw new InvalidOperationException("Discussion is locked: backlog item is done.");
    }
}