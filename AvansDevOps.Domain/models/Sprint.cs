using AvansDevOps.Domain.Models.Users;
using AvansDevOps.Domain.Models.BacklogItems;

namespace AvansDevOps.Domain.Models;

public class Sprint
{
    public required string Name { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public List<BacklogItem>? BacklogItems { get; set; }
    public required ScrumMaster ScrumMaster { get; set; }
}