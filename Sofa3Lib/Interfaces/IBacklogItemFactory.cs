using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBacklogItemFactory
    {
        BacklogItem Create(string title, string description);
    }
}