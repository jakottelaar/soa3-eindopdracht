namespace AvansDevOps.Domain.Models.Forum;

    public class DiscussionThread : IForumComponent
    {
        public string Title { get; set; }

        private readonly List<IForumComponent> _messages = new();

        public DiscussionThread(string title)
        {
            Title = title;
        }

        public void Add(IForumComponent component)
        {
            _messages.Add(component);
        }

        public void Remove(IForumComponent component)
        {
            _messages.Remove(component);
        }

        public void Accept(IForumVisitor visitor)
        {
            visitor.VisitThread(this);

            foreach (var message in _messages)
            {
                message.Accept(visitor);
            }
        }
    }