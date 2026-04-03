using AvansDevOps.Domain.Models.Users;

namespace AvansDevOps.Domain.Models.Discussion;

public interface IDiscussionComponent
{
    string Message { get; }
    IUser Author { get; }
    void Accept(IDiscussionVisitor visitor); // Visitor pattern hook
}