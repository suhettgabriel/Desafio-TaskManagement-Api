using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync(TaskItemStatus? status, DateTime? dueDate)
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync(status, dueDate);

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                DueDate = t.DueDate
            });
        }

        public async Task<TaskDto?> GetTaskByIdAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                DueDate = task.DueDate
            };
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto taskDto)
        {
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                Status = TaskItemStatus.Pendente
            };

            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.CompleteAsync();

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                DueDate = task.DueDate
            };
        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto taskDto)
        {
            var existingTask = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (existingTask == null)
            {
                return false;
            }

            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.Status = taskDto.Status;
            existingTask.DueDate = taskDto.DueDate;

            _unitOfWork.Tasks.Update(existingTask);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var taskToDelete = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (taskToDelete == null)
            {
                return false;
            }

            _unitOfWork.Tasks.Remove(taskToDelete);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}