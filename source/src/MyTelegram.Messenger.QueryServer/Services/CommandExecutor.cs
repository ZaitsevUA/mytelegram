using System.Threading.Channels;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace MyTelegram.Messenger.QueryServer.Services;

public class CommandExecutor<TAggregate, TIdentity, TExecutionResult>(ICommandBus commandBus)
    : ICommandExecutor<TAggregate, TIdentity, TExecutionResult>
    where TAggregate : IAggregateRoot<TIdentity>
    where TIdentity : IIdentity
    where TExecutionResult : IExecutionResult
{
    private readonly Channel<ICommand<TAggregate, TIdentity, TExecutionResult>> _commands = Channel.CreateUnbounded<ICommand<TAggregate, TIdentity, TExecutionResult>>();

    public Task ProcessCommandAsync()
    {
        Task.Run(async () =>
        {
            while (await _commands.Reader.WaitToReadAsync())
            {
                while (_commands.Reader.TryRead(out var command))
                {
                    await commandBus.PublishAsync(command, default);
                }
            }
        });

        return Task.CompletedTask;
    }

    public void Enqueue(
        ICommand<TAggregate, TIdentity, TExecutionResult> command)
    {
        _commands.Writer.TryWrite(command);
    }
}