using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskDbContext _context;
        public ITaskRepository Tasks { get; private set; }

        public UnitOfWork(TaskDbContext context)
        {
            _context = context;
            Tasks = new TaskRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}