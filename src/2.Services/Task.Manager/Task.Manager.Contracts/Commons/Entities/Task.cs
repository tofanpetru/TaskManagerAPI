﻿using Core.Domain.Entities.DataModels;
using TaskStatus = Task.Manager.Contracts.Commons.Enums.TaskStatus;

namespace Task.Manager.Contracts.Commons.Entities;

public sealed class Task : BaseMixedDataModel<Guid>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int Priority { get; set; }
    public TaskStatus Status { get; set; }
}