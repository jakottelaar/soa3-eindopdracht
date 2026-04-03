using AvansDevOps.Domain.Models.Users;

namespace AvansDevOps.Domain.Models.Discussion.Composite;

public class DiscussionPost : IDiscussionComponent
{
    public string Message { get; }
    public IUser Author { get; }
    private readonly List<DiscussionPost> replies = [];

    public IReadOnlyList<DiscussionPost> Replies => replies.AsReadOnly();

    public DiscussionPost(string message, IUser author)
    {
        Message = message;
        Author = author;
    }

    public void AddReply(DiscussionPost reply)
    {
        replies.Add(reply);
    }

    public void Accept(IDiscussionVisitor visitor)
    {
        visitor.VisitPost(this); // Visitor pattern
        foreach (var reply in replies)
        {
            reply.Accept(visitor); // Recursively visit replies
        }
    }
}