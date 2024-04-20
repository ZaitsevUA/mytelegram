namespace MyTelegram.Domain.Sagas.States;

public class SignInSagaSnapshot(
    long reqMsgId,
    long tempAuthKeyId,
    long permAuthKeyId)
    : ISnapshot
{
    public long PermAuthKeyId { get; } = permAuthKeyId;

    public long ReqMsgId { get; } = reqMsgId;
    public long TempAuthKeyId { get; } = tempAuthKeyId;
}
