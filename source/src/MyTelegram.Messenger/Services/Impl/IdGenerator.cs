using MyTelegram.Services.Services.IdGenerator;

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

    private async Task<int> GetMaxMessageIdAsync(long ownerUserId)
    {
        int? maxId = await queryProcessor.ProcessAsync(new GetMaxMessageIdByUserIdQuery(ownerUserId));

        return maxId ?? 0;
    }

    private async Task<int> GetChannelMaxMessageIdAsync(long channelId)
    {
        var channelReadModel = await queryProcessor.ProcessAsync(new GetChannelByIdQuery(channelId));
        return channelReadModel!.TopMessageId;
    }

    public async Task<long> NextLongIdAsync(IdType idType,
        long id = 0,
        int step = 1,
        CancellationToken cancellationToken = default)
    {
        //var state = _cache.GetOrAdd(idType, id);
        var sw = Stopwatch.StartNew();
        HiLoValueGeneratorState state;
        if (idType == IdType.MessageId)
        {

            state = await cache.GetOrAddAsync(idType, id, async () =>
            {
                int maxId;
                if (peerHelper.IsChannelPeer(id))
                {
                    maxId = await GetChannelMaxMessageIdAsync(id);
                }
                else
                {
                    maxId = await GetMaxMessageIdAsync(id);
                }
                var blockSize = stateBlockSizeHelper.GetBlockSize(idType);
                //var low = maxId % blockSize;
                var high = maxId / blockSize;

                // Console.WriteLine($"[{idType}]get previous id from db:maxId={maxId} high={high} ");
                return new HiLoValueGeneratorState(blockSize, maxId, (high + 1) * blockSize + 1);
            });
        }

        //else if (idType == IdType.Pts)
        //{
        //    state = await cache.GetOrAddAsync(idType, id, async () =>
        //    {
        //        var blockSize = stateBlockSizeHelper.GetBlockSize(idType);
        //        var ptsReadModel = await queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(id), cancellationToken);
        //        if (ptsReadModel != null)
        //        {
        //            var high = ptsReadModel.Pts / blockSize;
        //            var low = ptsReadModel.Pts % blockSize;
        //            if (low != 0)
        //            {
        //                high++;
        //            }

        //            return new HiLoValueGeneratorState(blockSize, ptsReadModel.Pts - GetInitId(idType), high * blockSize + 1);
        //        }

        //        return new HiLoValueGeneratorState(blockSize);
        //    });


        //}
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

        // Console.WriteLine($"[{idType}]generate new id,initId={GetInitId(idType)},nextId={nextId} ,nextId + GetInitId(idType)={nextId + GetInitId(idType)}");
        // state.Test();

        return nextId + GetInitId(idType);
    }

    private static long GetInitId(IdType idType)
    {
        return idType switch
        {
            IdType.ChannelId => MyTelegramServerDomainConsts.ChannelInitId,
            IdType.UserId => MyTelegramServerDomainConsts.UserIdInitId + 10000, // First 10000 for testing
            IdType.BotUserId => MyTelegramServerDomainConsts.BotUserInitId,
            IdType.ChatId => MyTelegramServerDomainConsts.ChatIdInitId,
            IdType.Pts => MyTelegramServerDomainConsts.PtsInitId,
            _ => 0
        };
    }
}