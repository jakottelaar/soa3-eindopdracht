namespace AvansDevOps.Domain.Models.Forum
{
    public class Message : IForumComponent
    {
        public string Content { get; set; }
        public List<Message> Replies { get; set; } = new();

        public Message(string content)
        {
            Content = content;
        }

        public void AddReply(Message message)
        {
            Replies.Add(message);
        }

        public void Accept(IForumVisitor visitor)
        {
            visitor.VisitMessage(this);

            foreach (var reply in Replies)
            {
                reply.Accept(visitor);
            }
        }
    }
}