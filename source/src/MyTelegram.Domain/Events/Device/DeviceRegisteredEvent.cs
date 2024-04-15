namespace MyTelegram.Domain.Events.Device;

public class DeviceRegisteredEvent : RequestAggregateEvent2<DeviceAggregate, DeviceId>
{
    public DeviceRegisteredEvent(RequestInfo requestInfo,
        int tokenType,
        string token) : base(requestInfo)
    {
        TokenType = tokenType;
        Token = token;
    }

    public string Token { get; }
    public int TokenType { get; }
}
