using AvansDevOps.Domain.Models.Discussion.Composite;

namespace AvansDevOps.Domain.Models.Discussion;

public interface IDiscussionVisitor
{
    void VisitPost(DiscussionPost post);
    void VisitThread(DiscussionThread thread);
}