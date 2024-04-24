namespace Core.GeneralUtilities.DateTimeProvider;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentTime() => DateTime.UtcNow;
}