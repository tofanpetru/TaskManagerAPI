using Core.Domain.Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Core.Configurations;

public sealed class BaseDataModelConfiguration : IEntityTypeConfiguration<IdentifiableEntity>
{
    public void Configure(EntityTypeBuilder<IdentifiableEntity> builder)
    {
        builder.HasKey(b => b.Id);
    }
}