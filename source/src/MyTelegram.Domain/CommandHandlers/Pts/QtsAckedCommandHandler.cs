namespace MyTelegram.Domain.CommandHandlers.Pts;

public class QtsAckedCommandHandler : CommandHandler<PtsAggregate, PtsId, QtsAckedCommand>
{
    public override Task ExecuteAsync(PtsAggregate aggregate,
        QtsAckedCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.QtsAcked(command.PeerId,
            command.PermAuthKeyId,
            command.MsgId,
            command.Qts,
            command.GlobalSeqNo,
            command.ToPeer);
        return Task.CompletedTask;
    }
}