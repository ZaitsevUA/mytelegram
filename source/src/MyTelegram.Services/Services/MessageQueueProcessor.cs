using System.Threading.Channels;

namespace MyTelegram.Services.Services;

public class MessageQueueProcessor<TData> : IMessageQueueProcessor<TData>
{
    private const int MaxQueueCount = 100;

    private readonly IDataProcessor<TData> _dataProcessor;
    private readonly ILogger<MessageQueueProcessor<TData>> _logger;
    private readonly Channel<TData>[] _queues = new Channel<TData>[MaxQueueCount];

    public MessageQueueProcessor(ILogger<MessageQueueProcessor<TData>> logger,
        IDataProcessor<TData> dataProcessor)
    {
        _logger = logger;
        _dataProcessor = dataProcessor;
        CreateQueues();
    }

    protected virtual bool RunInTask { get; set; } = true;

    public void Enqueue(TData data, long key = 0)
    {
        var index = key % MaxQueueCount;
        if (index < 0)
        {
            index += MaxQueueCount;
        }

        var queue = _queues[index];
        queue.Writer.TryWrite(data);
    }

    public Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        Task.Factory.StartNew(async () =>
        {
            var tasks = new List<Task>(MaxQueueCount);
            for (int i = 0; i < MaxQueueCount; i++)
            {
                var queue = _queues[i];

                if (RunInTask)
                {
                    var task = Task.Run(() => ProcessQueueAsync(queue, cancellationToken), cancellationToken);
                    tasks.Add(task);
                }
                else
                {
                    var task = ProcessQueueAsync(queue,
                        cancellationToken);
                    tasks.Add(task);
                }
            }

            await Task.WhenAll(tasks);
        }, TaskCreationOptions.LongRunning);

        return Task.CompletedTask;
    }

    private void CreateQueues()
    {
        for (int i = 0; i < MaxQueueCount; i++)
        {
            var queue = Channel.CreateUnbounded<TData>();
            _queues[i] = queue;
        }
        _logger.LogInformation("Created {MaxQueueCount} queues to process {MessageType} messages", MaxQueueCount, typeof(TData).Name);
    }

    private async Task ProcessQueueAsync(Channel<TData> queue, CancellationToken cancellationToken)
    {
        while (await queue.Reader.WaitToReadAsync(cancellationToken))
        {
            while (queue.Reader.TryRead(out var item))
            {
                try
                {
                    await _dataProcessor.ProcessAsync(item);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Process {MessageType} queue failed", typeof(TData));
                }
            }
        }
    }
}