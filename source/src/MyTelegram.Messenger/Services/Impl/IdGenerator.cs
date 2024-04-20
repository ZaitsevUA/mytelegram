namespace MyTelegram.Messenger.Services.Impl;

public class IdGenerator(
    IHiLoValueGeneratorCache cache,
    IHiLoValueGeneratorFactory factory,
    IPeerHelper peerHelper,
    IQueryProcessor queryProcessor,
    IHiLoStateBlockSizeHelper stateBlockSizeHelper,
    ILogger<IdGenerator> logger)
    : IIdGenerator
{
    public async Task<int> NextIdAsync(IdType idType,
        long id,
        int step = 1,
        CancellationToken cancellationToken = default)
    {
        return (int)await NextLongIdAsync(idType, id, step, cancellationToken);
    }

    public async Task<long> NextLongIdAsync(IdType idType,
        long id = 0,
        int step = 1,
        CancellationToken cancellationToken = default)
    {
        //var state = _cache.GetOrAdd(idType, id);
        var sw = Stopwatch.StartNew();
        HiLoValueGeneratorState state;
        if (peerHelper.IsChannelPeer(id) && idType == IdType.MessageId)
        //if (idType == IdType.ChannelId)
        {
            state = await cache.GetOrAddAsync(idType, id, async () =>
            {
                var blockSize = stateBlockSizeHelper.GetBlockSize(idType);

                var channelReadModel = await queryProcessor.ProcessAsync(new GetChannelByIdQuery(id), cancellationToken);
                var channelMaxMessageId = channelReadModel!.TopMessageId;
                var high = channelMaxMessageId / blockSize;
                var low = channelMaxMessageId % blockSize;
                if (low != 0)
                {
                    high++;
                }

                return new HiLoValueGeneratorState(blockSize, channelMaxMessageId, high * blockSize + 1);

            });
        }
        else
        {
            state = cache.GetOrAdd(idType, id);
        }

        var generator = factory.Create(state);
        var nextId = await generator.NextAsync(idType, id, cancellationToken);
        sw.Stop();

        if (sw.Elapsed.TotalMilliseconds > 100)
        {
            logger.LogWarning("[{Timespan}]Generate id too slow,idType={IdType},id={Id}", sw.Elapsed, idType, id);
        }

        return nextId + GetInitId(idType);
    }

    private static long GetInitId(IdType idType)
    {
        return idType switch
        {
            IdType.ChannelId => MyTelegramServerDomainConsts.ChannelInitId,
            IdType.UserId => MyTelegramServerDomainConsts.UserIdInitId + 10000, // Reserve the first 10,000 users for testing
            IdType.BotUserId => MyTelegramServerDomainConsts.BotUserInitId,
            IdType.ChatId => MyTelegramServerDomainConsts.ChatIdInitId,
            IdType.Pts => MyTelegramServerDomainConsts.PtsInitId,
            _ => 0
        };
    }
}