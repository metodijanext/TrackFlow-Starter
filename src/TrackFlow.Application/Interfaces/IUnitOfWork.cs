using TrackFlow.Domain.Entities;

namespace TrackFlow.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Project> Projects { get; }
    IRepository<TimeEntry> TimeEntries { get; }
    IRepository<ProjectUser> ProjectUsers { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
