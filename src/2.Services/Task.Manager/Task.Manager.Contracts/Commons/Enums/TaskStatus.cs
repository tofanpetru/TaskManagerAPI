using NetEscapades.EnumGenerators;

namespace Task.Manager.Contracts.Commons.Enums;

[EnumExtensions]
public enum TaskStatus
{
    Unknown,
    New,
    InProgress,
    Completed
}