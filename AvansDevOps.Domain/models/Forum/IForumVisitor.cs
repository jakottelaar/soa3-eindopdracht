namespace AvansDevOps.Domain.Models.Forum;

    public interface IForumVisitor
    {
        void VisitThread(DiscussionThread thread);
        void VisitMessage(Message message);
    }