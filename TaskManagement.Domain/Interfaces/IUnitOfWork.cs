namespace TaskManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository Tasks { get; }
        Task<int> CompleteAsync();
    }
}