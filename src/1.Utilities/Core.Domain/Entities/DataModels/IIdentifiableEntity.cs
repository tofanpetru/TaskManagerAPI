namespace Core.Domain.Entities.DataModels;

public interface IIdentifiableEntity<TIdType> where TIdType : struct
{
    TIdType Id { get; set; }
}