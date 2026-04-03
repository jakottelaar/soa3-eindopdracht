using AvansDevOps.Domain.Models.Users;

using AvansDevOps.Domain.Models.Discussion;

namespace AvansDevOps.Domain.Models.Discussion.Composite;

public class DiscussionThread : IDiscussionComponent
{
    private readonly List<IDiscussionComponent> _children = new();

    public string Message { get; }
    public IUser Author { get; }
    public IReadOnlyList<IDiscussionComponent> Children => _children.AsReadOnly();

    public DiscussionThread(string message, IUser author)
    {
        Message = message;
        Author = author;
    }

    public void Add(IDiscussionComponent component) => _children.Add(component);
    public void Remove(IDiscussionComponent component) => _children.Remove(component);

    public void Accept(IDiscussionVisitor visitor)
    {
        visitor.VisitThread(this); // Visitor pattern
        foreach (var child in _children)
            child.Accept(visitor); // recursief door de boom
    }
}