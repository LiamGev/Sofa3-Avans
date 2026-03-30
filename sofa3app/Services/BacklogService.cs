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
            var project = GetProject(projectId);

            IBacklogItemFactory factory = new Domain.Factories.StoryFactory();
            var item = factory.Create(title, description);

            project.AddBacklogItem(item);
            _projectRepository.Update(project);

            return item;
        }

        public BacklogItem CreateBug(Guid projectId, string title, string description)
        {
            var project = GetProject(projectId);

            IBacklogItemFactory factory = new Domain.Factories.BugFactory();
            var item = factory.Create(title, description);

            project.AddBacklogItem(item);
            _projectRepository.Update(project);

            return item;
        }

        public void AddActivityToBacklogItem(Guid projectId, Guid backlogItemId, string activityTitle)
        {
            var project = GetProject(projectId);
            var item = GetBacklogItem(project, backlogItemId);

            item.AddActivity(activityTitle);
            _projectRepository.Update(project);
        }

        public void AddMessageToBacklogItem(Guid projectId, Guid backlogItemId, string author, string content)
        {
            var project = GetProject(projectId);
            var item = GetBacklogItem(project, backlogItemId);

            item.AddMessage(author, content);
            _projectRepository.Update(project);
        }

        public void StartWork(Guid projectId, Guid backlogItemId)
        {
            var project = GetProject(projectId);
            var item = GetBacklogItem(project, backlogItemId);

            item.StartWork();
            _projectRepository.Update(project);
        }

        public void MoveToTesting(Guid projectId, Guid backlogItemId)
        {
            var project = GetProject(projectId);
            var item = GetBacklogItem(project, backlogItemId);

            item.MoveToTesting();
            _projectRepository.Update(project);
        }

        public void Complete(Guid projectId, Guid backlogItemId)
        {
            var project = GetProject(projectId);
            var item = GetBacklogItem(project, backlogItemId);

            item.Complete();
            _projectRepository.Update(project);
        }

        private Project GetProject(Guid projectId)
        {
            return _projectRepository.GetById(projectId)
                   ?? throw new InvalidOperationException("Project not found.");
        }

        private BacklogItem GetBacklogItem(Project project, Guid backlogItemId)
        {
            return project.GetBacklogItemById(backlogItemId)
                   ?? throw new InvalidOperationException("Backlog item not found.");
        }
    }
}