namespace Core.Domain.Entities.DataModels;

public class BaseCreatedDataModel : IdentifiableEntity, ICreatedDataModel
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
}