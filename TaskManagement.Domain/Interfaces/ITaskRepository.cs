using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetAllAsync(TaskItemStatus? status, DateTime? dueDate);
        Task AddAsync(TaskItem task);
        void Update(TaskItem task);
        void Remove(TaskItem task);
    }
}