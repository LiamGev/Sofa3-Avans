using sofa3Domain.Entities;

namespace sofa3Domain.Interfaces
{
    public interface IBacklogItemFactory
    {
        BacklogItem Create(string title, string description);
    }
}