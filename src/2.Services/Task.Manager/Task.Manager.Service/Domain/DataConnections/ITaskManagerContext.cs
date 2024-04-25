using Microsoft.EntityFrameworkCore;

namespace Task.Manager.Service.Domain.DataConnections;

internal interface ITaskManagerContext
{
    DbSet<DataModels.TaskDataModel> Tasks { get; set; }
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}