using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Task.Manager.Service.Infrastructure.Data.DataModelConfigurations;

internal sealed class TaskConfigurations : IEntityTypeConfiguration<Contracts.Commons.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Contracts.Commons.Entities.Task> builder)
    {
        builder.Property(p => p.Title)
               .IsRequired();
    }
}