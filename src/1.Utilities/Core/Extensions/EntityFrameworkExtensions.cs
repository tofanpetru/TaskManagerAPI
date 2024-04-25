using Core.Domain.Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Core.Extensions;

public static class EntityFrameworkExtensions
{
    public static void ApplyTrackingChanges(this DbContext context)
    {
        var entries = context.ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            switch (entry.Entity)
            {
                case BaseMixedDataModel<Guid> trackable:
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            trackable.CreatedOn = DateTime.UtcNow;
                            trackable.ModifiedOn = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            trackable.ModifiedOn = DateTime.UtcNow;
                            entry.Property(nameof(ICreatedDataModel.CreatedOn)).IsModified = false;
                            break;
                    }
                    break;
            }
        }
    }
}