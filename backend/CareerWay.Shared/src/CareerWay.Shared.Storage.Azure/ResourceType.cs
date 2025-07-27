using System.ComponentModel;

namespace CareerWay.Shared.Storage.Azure;

public enum ResourceType : byte
{
    [Description("b")]
    Blob,

    [Description("c")]
    Container
}
