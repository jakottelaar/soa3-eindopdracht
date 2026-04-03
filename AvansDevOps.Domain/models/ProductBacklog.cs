using AvansDevOps.Domain.Models.BacklogItems;

namespace AvansDevOps.Domain.Models;

public class ProductBacklog
{
    public List<BacklogItem>? Items { get; set; }

    public ProductBacklog()
    {
        Items = [];
    }

    public void AddItem(BacklogItem item)
    {
        Items?.Add(item);
    }

    public void RemoveItem(BacklogItem item)
    {
        Items?.Remove(item);
    }

    public List<BacklogItem>? GetItems()
    {
        return Items;
    }
}