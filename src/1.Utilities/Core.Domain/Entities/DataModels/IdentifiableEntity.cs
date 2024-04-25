namespace Core.Domain.Entities.DataModels;

public class IdentifiableEntity<T> : IIdentifiableEntity<T> where T : struct
{
    public T Id { get; set; }
}