using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProjectRepository
    {
        void Add(Project project);
        Project? GetById(Guid id);
        void Update(Project project);
        List<Project> GetAll();
    }
}