using Core.Domain.Entities.DataModels;
using TaskStatus = Task.Manager.Contracts.Commons.Enums.TaskStatus;

namespace Task.Manager.Contracts.Commons.Entities;

public sealed class Task : BaseCreatedDataModel
{
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Priority { get; set; }
    public TaskStatus Status { get; set; }
}