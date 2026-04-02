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
            Assert.Equal("To Do", item.CurrentState.Name);
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
            Assert.Equal("To Do", item.CurrentState.Name);
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
        public void CreateTask_Adds_Task_To_Project()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var item = service.CreateTask(project.Id, "New Task", "Task Description");

            Assert.Single(project.BacklogItems);
            Assert.Equal("Task", item.Type);
            Assert.Equal("New Task", item.Title);
            Assert.Equal("To Do", item.CurrentState.Name);
        }

        [Fact]
        public void CreateSpike_Adds_Spike_To_Project()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var item = service.CreateSpike(project.Id, "New Spike", "Spike Description");

            Assert.Single(project.BacklogItems);
            Assert.Equal("Spike", item.Type);
            Assert.Equal("New Spike", item.Title);
            Assert.Equal("To Do", item.CurrentState.Name);
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
        public void StartWork_Changes_BacklogItem_State_To_Doing()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");

            service.StartWork(project.Id, item.Id);

            Assert.Equal("Doing", item.CurrentState.Name);
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
        public void MoveToReadyForTesting_Changes_BacklogItem_State()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");
            service.StartWork(project.Id, item.Id);

            service.MoveToReadyForTesting(project.Id, item.Id);

            Assert.Equal("Ready For Testing", item.CurrentState.Name);
        }

        [Fact]
        public void MoveToReadyForTesting_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            Assert.Throws<InvalidOperationException>(() =>
                service.MoveToReadyForTesting(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public void MoveToReadyForTesting_WithUnknownItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            Assert.Throws<InvalidOperationException>(() =>
                service.MoveToReadyForTesting(project.Id, Guid.NewGuid()));
        }

        [Fact]
        public void StartTesting_Changes_BacklogItem_State_To_Testing()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");
            service.StartWork(project.Id, item.Id);
            service.MoveToReadyForTesting(project.Id, item.Id);

            service.StartTesting(project.Id, item.Id);

            Assert.Equal("Testing", item.CurrentState.Name);
        }

        [Fact]
        public void StartTesting_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            Assert.Throws<InvalidOperationException>(() =>
                service.StartTesting(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public void StartTesting_WithUnknownItem_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            Assert.Throws<InvalidOperationException>(() =>
                service.StartTesting(project.Id, Guid.NewGuid()));
        }

        [Fact]
        public void ApproveTesting_Changes_BacklogItem_State_To_Tested()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");
            service.StartWork(project.Id, item.Id);
            service.MoveToReadyForTesting(project.Id, item.Id);
            service.StartTesting(project.Id, item.Id);

            service.ApproveTesting(project.Id, item.Id);

            Assert.Equal("Tested", item.CurrentState.Name);
        }

        [Fact]
        public void RejectTesting_Changes_BacklogItem_State_Back_To_ToDo()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");
            service.StartWork(project.Id, item.Id);
            service.MoveToReadyForTesting(project.Id, item.Id);
            service.StartTesting(project.Id, item.Id);

            service.RejectTesting(project.Id, item.Id);

            Assert.Equal("To Do", item.CurrentState.Name);
        }

        [Fact]
        public void ApproveDone_Changes_BacklogItem_State_To_Done_When_All_Activities_Are_Completed()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");

            service.AddActivityToBacklogItem(project.Id, item.Id, "Implement feature");
            var activity = item.Activities.First();
            item.CompleteActivity(activity.Id);

            service.StartWork(project.Id, item.Id);
            service.MoveToReadyForTesting(project.Id, item.Id);
            service.StartTesting(project.Id, item.Id);
            service.ApproveTesting(project.Id, item.Id);

            service.ApproveDone(project.Id, item.Id);

            Assert.Equal("Done", item.CurrentState.Name);
        }

        [Fact]
        public void ApproveDone_Without_Completed_Activities_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");

            service.AddActivityToBacklogItem(project.Id, item.Id, "Implement feature");

            service.StartWork(project.Id, item.Id);
            service.MoveToReadyForTesting(project.Id, item.Id);
            service.StartTesting(project.Id, item.Id);
            service.ApproveTesting(project.Id, item.Id);

            Assert.Throws<InvalidOperationException>(() =>
                service.ApproveDone(project.Id, item.Id));
        }

        [Fact]
        public void RejectDone_Changes_BacklogItem_State_Back_To_ReadyForTesting()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");

            service.StartWork(project.Id, item.Id);
            service.MoveToReadyForTesting(project.Id, item.Id);
            service.StartTesting(project.Id, item.Id);
            service.ApproveTesting(project.Id, item.Id);

            service.RejectDone(project.Id, item.Id);

            Assert.Equal("Ready For Testing", item.CurrentState.Name);
        }
    }
}