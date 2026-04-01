using App.Services;
using Domain.Entities;
using Infra.Repos;
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
        }

        [Fact]
        public void CreateStory_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            Assert.Throws<InvalidOperationException>(() =>
                service.CreateStory(Guid.NewGuid(), "Story", "Description"));
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
        }

        [Fact]
        public void CreateBug_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            Assert.Throws<InvalidOperationException>(() =>
                service.CreateBug(Guid.NewGuid(), "Bug", "Description"));
        }

        [Fact]
        public void AddActivityToBacklogItem_Adds_Activity()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "Story", "Description");

            service.AddActivityToBacklogItem(project.Id, item.Id, "Implement login");

            Assert.Single(item.Activities);
            Assert.Equal("Implement login", item.Activities.First().Title);
        }

        [Fact]
        public void AddActivityToBacklogItem_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            Assert.Throws<InvalidOperationException>(() =>
                service.AddActivityToBacklogItem(Guid.NewGuid(), Guid.NewGuid(), "Activity"));
        }

        [Fact]
        public void AddActivityToBacklogItem_WithUnknownItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            Assert.Throws<InvalidOperationException>(() =>
                service.AddActivityToBacklogItem(project.Id, Guid.NewGuid(), "Activity"));
        }

        [Fact]
        public void AddMessageToBacklogItem_Adds_Message()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "Story", "Description");

            service.AddMessageToBacklogItem(project.Id, item.Id, "Alice", "Looks good");

            Assert.Single(item.Messages);
            Assert.Equal("Alice", item.Messages.First().Author);
            Assert.Equal("Looks good", item.Messages.First().Content);
        }

        [Fact]
        public void AddMessageToBacklogItem_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            Assert.Throws<InvalidOperationException>(() =>
                service.AddMessageToBacklogItem(Guid.NewGuid(), Guid.NewGuid(), "Alice", "Message"));
        }

        [Fact]
        public void AddMessageToBacklogItem_WithUnknownItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            Assert.Throws<InvalidOperationException>(() =>
                service.AddMessageToBacklogItem(project.Id, Guid.NewGuid(), "Alice", "Message"));
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

            Assert.Throws<InvalidOperationException>(() =>
                service.StartWork(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public void StartWork_WithUnknownItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            Assert.Throws<InvalidOperationException>(() =>
                service.StartWork(project.Id, Guid.NewGuid()));
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

            Assert.Throws<InvalidOperationException>(() =>
                service.MoveToTesting(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public void MoveToTesting_WithUnknownItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            Assert.Throws<InvalidOperationException>(() =>
                service.MoveToTesting(project.Id, Guid.NewGuid()));
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

            Assert.Throws<InvalidOperationException>(() =>
                service.Complete(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public void Complete_WithUnknownItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            Assert.Throws<InvalidOperationException>(() =>
                service.Complete(project.Id, Guid.NewGuid()));
        }
    }
}