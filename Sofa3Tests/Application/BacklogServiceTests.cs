using App.Services;
using Domain.Entities;
using Infra.Repos;
using System;
using System.Linq;
using Xunit;

namespace Tests.Application
{
    public class BacklogServiceTests
    {
        [Fact]
        public void Constructor_WithNullRepository_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new BacklogService(null!));
        }

        [Fact]
        public void CreateStory_Adds_Item_To_Project()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var item = service.CreateStory(project.Id, "New Story", "Description");

            Assert.Single(project.BacklogItems);
            Assert.Equal("Story", item.Type);
            Assert.Equal("New Story", item.Title);
            Assert.Equal("Description", item.Description);
        }

        [Fact]
        public void CreateStory_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.CreateStory(Guid.NewGuid(), "New Story", "Description"));

            Assert.Equal("Project not found.", ex.Message);
        }

        [Fact]
        public void CreateBug_Adds_Bug_To_Project()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var item = service.CreateBug(project.Id, "New Bug", "Bug Description");

            Assert.Single(project.BacklogItems);
            Assert.Equal("Bug", item.Type);
            Assert.Equal("New Bug", item.Title);
            Assert.Equal("Bug Description", item.Description);
        }

        [Fact]
        public void CreateBug_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.CreateBug(Guid.NewGuid(), "New Bug", "Bug Description"));

            Assert.Equal("Project not found.", ex.Message);
        }

        [Fact]
        public void AddActivityToBacklogItem_Adds_Activity_To_Item()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");

            service.AddActivityToBacklogItem(project.Id, item.Id, "Implement login");

            Assert.Single(item.Activities);
            Assert.Equal("Implement login", item.Activities.First().Title);
        }

        [Fact]
        public void AddActivityToBacklogItem_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.AddActivityToBacklogItem(Guid.NewGuid(), Guid.NewGuid(), "Implement login"));

            Assert.Equal("Project not found.", ex.Message);
        }

        [Fact]
        public void AddActivityToBacklogItem_WithUnknownBacklogItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.AddActivityToBacklogItem(project.Id, Guid.NewGuid(), "Implement login"));

            Assert.Equal("Backlog item not found.", ex.Message);
        }

        [Fact]
        public void AddMessageToBacklogItem_Adds_Message_To_Item()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");

            service.AddMessageToBacklogItem(project.Id, item.Id, "Liam", "Please review this.");

            Assert.Single(item.Messages);
            Assert.Equal("Liam", item.Messages.First().Author);
            Assert.Equal("Please review this.", item.Messages.First().Content);
        }

        [Fact]
        public void AddMessageToBacklogItem_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.AddMessageToBacklogItem(Guid.NewGuid(), Guid.NewGuid(), "Liam", "Message"));

            Assert.Equal("Project not found.", ex.Message);
        }

        [Fact]
        public void AddMessageToBacklogItem_WithUnknownBacklogItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.AddMessageToBacklogItem(project.Id, Guid.NewGuid(), "Liam", "Message"));

            Assert.Equal("Backlog item not found.", ex.Message);
        }

        [Fact]
        public void StartWork_Changes_BacklogItem_State()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var item = service.CreateStory(project.Id, "New Story", "Description");

            service.StartWork(project.Id, item.Id);

            Assert.Equal("In Progress", item.CurrentState.Name);
        }

        [Fact]
        public void StartWork_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.StartWork(Guid.NewGuid(), Guid.NewGuid()));

            Assert.Equal("Project not found.", ex.Message);
        }

        [Fact]
        public void StartWork_WithUnknownBacklogItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.StartWork(project.Id, Guid.NewGuid()));

            Assert.Equal("Backlog item not found.", ex.Message);
        }

        [Fact]
        public void MoveToTesting_Changes_BacklogItem_State()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var item = service.CreateStory(project.Id, "New Story", "Description");
            service.StartWork(project.Id, item.Id);

            service.MoveToTesting(project.Id, item.Id);

            Assert.Equal("Testing", item.CurrentState.Name);
        }

        [Fact]
        public void MoveToTesting_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.MoveToTesting(Guid.NewGuid(), Guid.NewGuid()));

            Assert.Equal("Project not found.", ex.Message);
        }

        [Fact]
        public void MoveToTesting_WithUnknownBacklogItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.MoveToTesting(project.Id, Guid.NewGuid()));

            Assert.Equal("Backlog item not found.", ex.Message);
        }

        [Fact]
        public void Complete_Changes_BacklogItem_State_To_Done()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var item = service.CreateStory(project.Id, "New Story", "Description");
            service.StartWork(project.Id, item.Id);
            service.MoveToTesting(project.Id, item.Id);

            service.Complete(project.Id, item.Id);

            Assert.Equal("Done", item.CurrentState.Name);
        }

        [Fact]
        public void Complete_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.Complete(Guid.NewGuid(), Guid.NewGuid()));

            Assert.Equal("Project not found.", ex.Message);
        }

        [Fact]
        public void Complete_WithUnknownBacklogItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.Complete(project.Id, Guid.NewGuid()));

            Assert.Equal("Backlog item not found.", ex.Message);
        }
    }
}