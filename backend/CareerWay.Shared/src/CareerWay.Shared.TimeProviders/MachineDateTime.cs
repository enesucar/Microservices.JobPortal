namespace CareerWay.Shared.TimeProviders;

public class MachineDateTime : DateTimeBase
{
    public override DateTime Now => DateTime.Now;

    public override TimeZoneInfo TimeZoneInfo => TimeZoneInfo.Local;
}
