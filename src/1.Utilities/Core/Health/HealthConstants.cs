using System.Collections.Immutable;

namespace Core.Health;

public static class HealthConstants
{
    public static readonly ImmutableList<string> ReadinessTags = ["ready"];
}