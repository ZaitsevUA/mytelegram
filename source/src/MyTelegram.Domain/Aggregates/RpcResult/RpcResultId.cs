namespace MyTelegram.Domain.Aggregates.RpcResult;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<RpcResultId>))]
public class RpcResultId : Identity<RpcResultId>
{
    public RpcResultId(string value) : base(value)
    {
    }

    public static RpcResultId Create(long userId,long reqMsgId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"{userId}-{reqMsgId}");
    }
}
