﻿using Core.Configurations;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Task.Manager.Service.Domain.DataConnections;

namespace Task.Manager.Service.Infrastructure.Data;

internal sealed class TaskManagerContext(DbContextOptions<TaskManagerContext> options) : DbContext(options), ITaskManagerContext
{
    public DbSet<Domain.DataModels.TaskDataModel> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.ApplyTrackingChanges();

        return await base.SaveChangesAsync(cancellationToken);
    }
}