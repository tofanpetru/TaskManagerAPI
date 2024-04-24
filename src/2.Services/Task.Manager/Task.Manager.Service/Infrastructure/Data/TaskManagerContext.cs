using Core.Configurations;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Task.Manager.Service.Domain.DataConnections;

namespace Task.Manager.Service.Infrastructure.Data;

internal sealed class TaskManagerContext : DbContext, ITaskManagerContext
{
    public DbSet<Contracts.Commons.Entities.Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseDataModelConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.ApplyTrackingChanges();

        return await base.SaveChangesAsync(cancellationToken);
    }
}