using Core.Domain.Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Core.Configurations;

public sealed class BaseDataModelConfiguration<T> : IEntityTypeConfiguration<IdentifiableEntity<T>> where T : struct
{
    public void Configure(EntityTypeBuilder<IdentifiableEntity<T>> builder)
    {
        builder.HasKey(b => b.Id);
    }
}