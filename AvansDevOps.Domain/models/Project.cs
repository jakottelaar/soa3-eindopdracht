using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.Sprints;

namespace AvansDevOps.Domain.Models;

public class Project(string name, string description, ProductOwner productOwner)
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public ProductOwner ProductOwner { get; set; } = productOwner;
    public List<Sprint> Sprints { get; set; } = new List<Sprint>();
    public ProductBacklog ProductBacklog { get; set; } = new ProductBacklog();
}