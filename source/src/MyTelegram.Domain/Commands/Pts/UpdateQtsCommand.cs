namespace MyTelegram.Domain.Commands.Pts;

public class UpdateQtsCommand(
    PtsId aggregateId,
    long peerId,
    int newQts) : Command<PtsAggregate, PtsId, IExecutionResult>(aggregateId)
{
    public int NewQts { get; } = newQts;

    public long PeerId { get; } = peerId;
}
