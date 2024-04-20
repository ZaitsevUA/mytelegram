namespace MyTelegram.Domain.Commands.Dialog;

public class UpdateDialogCommand(DialogId aggregateId, long ownerUserId, Peer toPeer, int topMessageId, int pts)
    : Command<DialogAggregate, DialogId, IExecutionResult>(aggregateId)
{
    public long OwnerUserId { get; } = ownerUserId;
    public Peer ToPeer { get; } = toPeer;
    public int TopMessageId { get; } = topMessageId;
    public int Pts { get; } = pts;
}