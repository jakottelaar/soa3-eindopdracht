namespace AvansDevOps.Domain.Models.Forum;

public interface IForumComponent
{
    void Accept(IForumVisitor visitor);
}