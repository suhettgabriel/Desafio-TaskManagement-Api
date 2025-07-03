using Moq;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Tests.Application
{
    public class TaskServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _taskService = new TaskService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetTaskByIdAsync_ShouldReturnTaskDto_WhenTaskExists()
        {
            // Arrange
            var taskId = 1;
            var taskItem = new TaskItem { Id = taskId, Title = "Test Task", Status = TaskItemStatus.Pendente };

            _mockUnitOfWork.Setup(uow => uow.Tasks.GetByIdAsync(taskId)).ReturnsAsync(taskItem);

            // Act
            var result = await _taskService.GetTaskByIdAsync(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskId, result.Id);
            Assert.Equal(taskItem.Title, result.Title);
        }

        [Fact]
        public async Task GetTaskByIdAsync_ShouldReturnNull_WhenTaskDoesNotExist()
        {
            // Arrange
            var taskId = 99;
            _mockUnitOfWork.Setup(uow => uow.Tasks.GetByIdAsync(taskId)).ReturnsAsync((TaskItem)null);

            // Act
            var result = await _taskService.GetTaskByIdAsync(taskId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateTaskAsync_ShouldCallAddAndComplete_AndReturnTaskDto()
        {
            // Arrange
            var createTaskDto = new CreateTaskDto { Title = "New Task", Description = "Description", DueDate = DateTime.Now };
            var taskItem = new TaskItem();

            _mockUnitOfWork.Setup(uow => uow.Tasks.AddAsync(It.IsAny<TaskItem>())).Callback<TaskItem>(t => taskItem = t);
            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _taskService.CreateTaskAsync(createTaskDto);

            // Assert

            _mockUnitOfWork.Verify(uow => uow.Tasks.AddAsync(It.IsAny<TaskItem>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(createTaskDto.Title, result.Title);
            Assert.Equal(TaskItemStatus.Pendente.ToString(), result.Status);
        }
    }
}