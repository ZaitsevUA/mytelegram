using System.Collections.Concurrent;
using System.Threading.Channels;

namespace MyTelegram.Services.Services;

public class MessageQueueProcessor<TData>(
    IDataProcessor<TData> dataProcessor,
    ILogger<MessageQueueProcessor<TData>> logger) : IMessageQueueProcessor<TData>
{
    private readonly TimeSpan _idleTimeSpan = TimeSpan.FromMinutes(5);
    private readonly ConcurrentDictionary<long, Channel<TData>> _queues = [];
    private readonly ConcurrentDictionary<long, CancellationTokenSource> _timeoutTokens = [];

    public void Enqueue(TData data, long key)
    {
        if (_timeoutTokens.TryGetValue(key, out var cts))
        {
            cts.Cancel();
        }

        cts = new CancellationTokenSource();
        _timeoutTokens.TryAdd(key, cts);

        if (_queues.TryGetValue(key, out var channel))
        {
            channel.Writer.TryWrite(data);
            _ = ProcessTimeoutTaskAsync(key, cts.Token);
        }
        else
        {
            channel = Channel.CreateUnbounded<TData>();
            _queues.TryAdd(key, channel);
            channel.Writer.TryWrite(data);

            var task = ProcessAsync(channel);
            var releaseQueueTask = ProcessTimeoutTaskAsync(key, cts.Token);
            Task.WhenAll(task, releaseQueueTask);
        }
    }

    public Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
        //throw new NotImplementedException();
    }

    private async Task ProcessTimeoutTaskAsync(long key, CancellationToken cancellationToken)
    {
        await Task.Delay(_idleTimeSpan, cancellationToken)
            .ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
        if (!cancellationToken.IsCancellationRequested)
        {
            ReleaseQueue(key);
        }
    }

    private void ReleaseQueue(long key)
    {
        if (_queues.TryRemove(key, out var queue))
        {
            queue.Writer.Complete();

            _timeoutTokens.TryRemove(key, out var cts);
            cts?.Cancel();
        }
    }

    private async Task ProcessAsync(Channel<TData> channel)
    {
        while (await channel.Reader.WaitToReadAsync())
        {
            await foreach (var data in channel.Reader.ReadAllAsync())
            {
                try
                {
                    await dataProcessor.ProcessAsync(data);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Process {MessageType} queue failed", typeof(TData));
                }
            }
        }
    }
}
