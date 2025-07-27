namespace CareerWay.Shared.TimeProviders;

public interface IDateTime
{
    DateTime Now { get; }

    TimeZoneInfo TimeZoneInfo { get; }

    DateTime ConvertToSource(DateTime dateTime, TimeZoneInfo sourceTimeZoneInfo);

    DateTime ConvertFromSource(DateTime dateTime, TimeZoneInfo sourceTimeZoneInfo);
}
