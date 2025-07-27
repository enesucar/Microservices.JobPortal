namespace CareerWay.Shared.TimeProviders;

public abstract class DateTimeBase : IDateTime
{
    public abstract DateTime Now { get; }

    public abstract TimeZoneInfo TimeZoneInfo { get; }

    public DateTime ConvertToSource(DateTime dateTime, TimeZoneInfo sourceTimeZoneInfo)
    {
        DateTime unspecifiedDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
        return TimeZoneInfo.ConvertTime(unspecifiedDateTime, TimeZoneInfo, sourceTimeZoneInfo);
    }

    public DateTime ConvertFromSource(DateTime dateTime, TimeZoneInfo sourceTimeZoneInfo)
    {
        DateTime unspecifiedDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
        return TimeZoneInfo.ConvertTime(unspecifiedDateTime, sourceTimeZoneInfo, TimeZoneInfo);
    }
}
