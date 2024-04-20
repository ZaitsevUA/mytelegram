namespace MyTelegram.Domain.CommandHandlers.Message;

public class
    CheckMessageViewLogCommandHandler : CommandHandler<MessageViewLogAggregate, MessageViewLogId,
        CheckMessageViewLogCommand>
{
    public override Task ExecuteAsync(MessageViewLogAggregate aggregate,
        CheckMessageViewLogCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CheckMessageViewLog(command.RequestInfo, command.MessageId);
        return Task.CompletedTask;
    }
}
