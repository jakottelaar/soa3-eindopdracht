using AvansDevOps.Domain.Models.Users;

using AvansDevOps.Domain.Models.Discussion;

namespace AvansDevOps.Domain.Models.Discussion.Composite;

public class DiscussionPost : IDiscussionComponent
{
    public string Message { get; }
    public IUser Author { get; }
    private readonly List<DiscussionPost> _replies = new();

    public IReadOnlyList<DiscussionPost> Replies => _replies.AsReadOnly();

    public DiscussionPost(string message, IUser author)
    {
        Message = message;
        Author = author;
    }

    public void AddReply(DiscussionPost reply)
    {
        _replies.Add(reply);
    }

    public void Accept(IDiscussionVisitor visitor)
    {
        visitor.VisitPost(this); // Visitor pattern
        foreach (var reply in _replies)
        {
            reply.Accept(visitor); // Recursively visit replies
        }
    }
}