using Microsoft.EntityFrameworkCore;

namespace Task.Manager.Service.Domain.DataConnections;

internal interface ITaskManagerContext
{
    DbSet<Contracts.Commons.Entities.Task> Tasks { get; set; }
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}