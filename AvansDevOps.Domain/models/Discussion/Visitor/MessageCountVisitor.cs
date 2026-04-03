using AvansDevOps.Domain.Models.Discussion.Composite;

namespace AvansDevOps.Domain.Models.Discussion.Visitor;

public class MessageCountVisitor : IDiscussionVisitor
{
    private int count;

    public void VisitPost(DiscussionPost post) => count++;
    public void VisitThread(DiscussionThread thread) => count++;

    public int GetCount() => count;
}