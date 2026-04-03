namespace AvansDevOps.Domain.Models.Forum;

    public class DiscussionThread : IForumComponent
    {
        public string Title { get; set; }

        private readonly List<IForumComponent> messages = [];

        public DiscussionThread(string title)
        {
            Title = title;
        }

        public void Add(IForumComponent component)
        {
            messages.Add(component);
        }

        public void Remove(IForumComponent component)
        {
            messages.Remove(component);
        }

        public void Accept(IForumVisitor visitor)
        {
            visitor.VisitThread(this);

            foreach (var message in messages)
            {
                message.Accept(visitor);
            }
        }
    }