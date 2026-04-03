using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.Sprints;

namespace AvansDevOps.Domain.Models;

public class Project(string name, string description, ProductOwner productOwner)
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public IUser ProductOwner { get; set; } = productOwner;
    public List<Sprint> Sprints { get; set; } = [];
    public ProductBacklog ProductBacklog { get; set; } = new ProductBacklog();
    public List<IUser> TeamMembers { get; set; } = [];

    public void AddTeamMember(IUser user)
    {
        if (!TeamMembers.Contains(user))
        {
            TeamMembers.Add(user);
            Console.WriteLine($"User '{user.Name}' added to project '{Name}'.");
        }
        else
        {
            Console.WriteLine($"User '{user.Name}' is already a member of project '{Name}'.");
        }
    }

    public void RemoveTeamMember(IUser user)
    {
        if (TeamMembers.Contains(user))
        {
            TeamMembers.Remove(user);
            Console.WriteLine($"User '{user.Name}' removed from project '{Name}'.");
        }
        else
        {
            Console.WriteLine($"User '{user.Name}' is not a member of project '{Name}'.");
        }
    }

    public List<IUser> GetTeamMembers()
    {
        return TeamMembers;
    }

    public void AddSprint(Sprint sprint)
    {
        Sprints.Add(sprint);
        Console.WriteLine($"Sprint '{sprint.Name}' added to project '{Name}'.");
    }

    public List<Sprint> GetSprints()
    {
        return Sprints;
    }
}