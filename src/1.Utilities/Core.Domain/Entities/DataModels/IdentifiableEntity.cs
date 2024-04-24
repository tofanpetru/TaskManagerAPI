namespace Core.Domain.Entities.DataModels;

public class IdentifiableEntity : IIdentifiableEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
}