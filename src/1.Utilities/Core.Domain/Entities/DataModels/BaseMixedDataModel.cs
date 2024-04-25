namespace Core.Domain.Entities.DataModels;

public class BaseMixedDataModel<T> : IdentifiableEntity<T>, ICreatedDataModel where T : struct
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
}