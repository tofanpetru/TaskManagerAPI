namespace Core.Domain.Entities.DataModels;

public interface ICreatedDataModel
{
    DateTime CreatedOn { get; set; }
    DateTime ModifiedOn { get; set; }
}