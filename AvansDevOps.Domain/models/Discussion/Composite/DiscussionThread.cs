using AvansDevOps.Domain.Models.Users;

namespace AvansDevOps.Domain.Models.Discussion.Composite;

public class DiscussionThread : IDiscussionComponent
{
    private readonly List<IDiscussionComponent> children = [];

    public string Message { get; }
    public IUser Author { get; }
    public IReadOnlyList<IDiscussionComponent> Children => children.AsReadOnly();

    public DiscussionThread(string message, IUser author)
    {
        Message = message;
        Author = author;
    }

    public void Add(IDiscussionComponent component) => children.Add(component);
    public void Remove(IDiscussionComponent component) => children.Remove(component);

    public void Accept(IDiscussionVisitor visitor)
    {
        visitor.VisitThread(this); // Visitor pattern
        foreach (var child in children)
            child.Accept(visitor); // recursief door de boom
    }
}