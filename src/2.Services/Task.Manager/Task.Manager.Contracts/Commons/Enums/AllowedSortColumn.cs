using NetEscapades.EnumGenerators;

namespace Task.Manager.Contracts.Commons.Enums;

[EnumExtensions]
internal enum AllowedSortColumn
{
    Unknown,
    Priority,
    Status,
    CreatedOn,
    ModifiedOn
}