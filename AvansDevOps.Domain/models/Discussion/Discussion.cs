using AvansDevOps.Domain.Models.BacklogItems;
using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.Discussion.Composite;
using AvansDevOps.Domain.Models.BacklogItems.States;
using AvansDevOps.Domain.Models.Notifications;

namespace AvansDevOps.Domain.Models.Discussion;

public class Discussion
{
    private readonly DiscussionThread root;
    private readonly BacklogItem backlogItem;
    private readonly List<IObserver> observers = [];

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
        
        // Notify team members that a new post was added
        NotifyObservers($"💬 NOTIFICATION: New Reply in Discussion\nBacklog Item: '{backlogItem.Title}'\nAuthor: {post.Author.Name}\nMessage: {post.Message}");
    }

    public void Subscribe(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Unsubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(string message)
    {
        foreach (var observer in observers)
        {
            observer.Update(message);
        }
    }

    public void Accept(IDiscussionVisitor visitor) => root.Accept(visitor);

    public bool IsLocked() => backlogItem.State is BacklogItemDoneState;

    private void CheckNotLocked()
    {
        if (IsLocked())
            throw new InvalidOperationException("Discussion is locked: backlog item is done.");
    }
}