namespace MyTelegram.AuthServer.Handlers;

public class MsgsAckHandler(ILogger<MsgsAckHandler> logger)
    : BaseObjectHandler<TMsgsAck, IObject>, IMsgsAckHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        TMsgsAck obj)
    {
        logger.LogTrace("Receive acks from authKeyId={AuthKeyId:x2} msgIds={@MsgIds}", input.AuthKeyId, obj.MsgIds);

        return Task.FromResult<IObject>(null!);
    }
}