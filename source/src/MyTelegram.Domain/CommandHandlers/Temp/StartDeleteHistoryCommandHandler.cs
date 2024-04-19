using MyTelegram.Domain.Aggregates.Temp;

namespace MyTelegram.Domain.CommandHandlers.Temp;

public class StartDeleteHistoryCommandHandler : CommandHandler<TempAggregate, TempId, StartDeleteHistoryCommand>
{
    public override Task ExecuteAsync(TempAggregate aggregate, StartDeleteHistoryCommand command, CancellationToken cancellationToken)
    {
        aggregate.StartDeleteHistory(command.RequestInfo, command.MessageItems, command.Revoke, command.DeleteGroupMessagesForEveryone);

        return Task.CompletedTask;
    }
}