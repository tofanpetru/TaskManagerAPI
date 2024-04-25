using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task.Manager.Service.Domain.DataModels;

namespace Task.Manager.Service.Infrastructure.Data.DataModelConfigurations;

internal sealed class TaskConfigurations : IEntityTypeConfiguration<TaskDataModel>
{
    public void Configure(EntityTypeBuilder<TaskDataModel> builder)
    {
        builder.Property(p => p.Title)
               .IsRequired();

        builder.ToTable("Tasks", "tasks");
    }
}