using Domain.Entities;
using Domain.Interfaces;

namespace Infra.Repos
{
    // Fake repository:
    // De opdracht staat een eenvoudige repository-implementatie in geheugen toe
    // om de application core te kunnen testen zonder echte database.
    public class InMemoryProjectRepository : IProjectRepository
    {
        private readonly List<Project> _projects = new();

        public void Add(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            _projects.Add(project);
        }

        public Project? GetById(Guid id)
        {
            return _projects.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Project project)
        {
            // Niet nodig voor in-memory opslag,
            // omdat objectreferenties al worden bijgehouden.
        }

        public List<Project> GetAll()
        {
            return _projects;
        }
    }
}