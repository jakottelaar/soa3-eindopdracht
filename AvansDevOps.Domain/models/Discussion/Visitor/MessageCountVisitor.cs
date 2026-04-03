using AvansDevOps.Domain.Models.Discussion.Composite;

using AvansDevOps.Domain.Models.Discussion;

namespace AvansDevOps.Domain.Models.Discussion.Visitor;

public class MessageCountVisitor : IDiscussionVisitor
{
    private int _count;

    public void VisitPost(DiscussionPost post) => _count++;
    public void VisitThread(DiscussionThread thread) => _count++;

    public int GetCount() => _count;
}