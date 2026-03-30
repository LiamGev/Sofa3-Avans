using Domain.Entities;
using Domain.Interfaces;

namespace App.Services
{
    public class BacklogService
    {
        private readonly IProjectRepository _projectRepository;

        public BacklogService(IProjectRepository projectRepository)
        {
            ArgumentNullException.ThrowIfNull(projectRepository);
            _projectRepository = projectRepository;
        }

        public BacklogItem CreateStory(Guid projectId, string title, string description)
        {
            var project = _projectRepository.GetById(projectId)
                          ?? throw new InvalidOperationException("Project not found.");

            IBacklogItemFactory factory = new Domain.Factories.StoryFactory();
            var item = factory.Create(title, description);

            project.AddBacklogItem(item);
            _projectRepository.Update(project);

            return item;
        }

        public BacklogItem CreateBug(Guid projectId, string title, string description)
        {
            var project = _projectRepository.GetById(projectId)
                          ?? throw new InvalidOperationException("Project not found.");

            IBacklogItemFactory factory = new Domain.Factories.BugFactory();
            var item = factory.Create(title, description);

            project.AddBacklogItem(item);
            _projectRepository.Update(project);

            return item;
        }

        public void AddActivityToBacklogItem(Guid projectId, Guid backlogItemId, string activityTitle)
        {
            var item = GetBacklogItem(projectId, backlogItemId);
            item.AddActivity(activityTitle);
            _projectRepository.Update(GetProject(projectId));
        }

        public void AddMessageToBacklogItem(Guid projectId, Guid backlogItemId, string author, string content)
        {
            var item = GetBacklogItem(projectId, backlogItemId);
            item.AddMessage(author, content);
            _projectRepository.Update(GetProject(projectId));
        }

        public void StartWork(Guid projectId, Guid backlogItemId)
        {
            var item = GetBacklogItem(projectId, backlogItemId);
            item.StartWork();
            _projectRepository.Update(GetProject(projectId));
        }

        public void MoveToTesting(Guid projectId, Guid backlogItemId)
        {
            var item = GetBacklogItem(projectId, backlogItemId);
            item.MoveToTesting();
            _projectRepository.Update(GetProject(projectId));
        }

        public void Complete(Guid projectId, Guid backlogItemId)
        {
            var item = GetBacklogItem(projectId, backlogItemId);
            item.Complete();
            _projectRepository.Update(GetProject(projectId));
        }

        private Project GetProject(Guid projectId)
        {
            return _projectRepository.GetById(projectId)
                   ?? throw new InvalidOperationException("Project not found.");
        }

        private BacklogItem GetBacklogItem(Guid projectId, Guid backlogItemId)
        {
            var project = GetProject(projectId);

            return project.GetBacklogItemById(backlogItemId)
                   ?? throw new InvalidOperationException("Backlog item not found.");
        }
    }
}