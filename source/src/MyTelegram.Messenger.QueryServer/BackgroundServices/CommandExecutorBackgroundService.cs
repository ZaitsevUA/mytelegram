using EventFlow.Aggregates.ExecutionResults;
using Microsoft.Extensions.Hosting;
using MyTelegram.Messenger.QueryServer.Services;

namespace MyTelegram.Messenger.QueryServer.BackgroundServices;

public class CommandExecutorBackgroundService(
    ICommandExecutor<PtsAggregate, PtsId, IExecutionResult> ptsCommandExecutor)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return ptsCommandExecutor.ProcessCommandAsync();
    }
}