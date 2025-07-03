using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync(TaskItemStatus? status, DateTime? dueDate);
        Task<TaskDto?> GetTaskByIdAsync(int id);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto taskDto);
        Task<bool> UpdateTaskAsync(int id, UpdateTaskDto taskDto);
        Task<bool> DeleteTaskAsync(int id);
    }
}