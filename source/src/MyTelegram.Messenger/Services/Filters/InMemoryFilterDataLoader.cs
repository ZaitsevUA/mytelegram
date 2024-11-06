namespace MyTelegram.Messenger.Services.Filters;

public class InMemoryFilterDataLoader(
    ICuckooFilter cuckooFilter,
    IQueryProcessor queryProcessor,
    ILogger<InMemoryFilterDataLoader> logger)
    : IInMemoryFilterDataLoader, ITransientDependency
{
    //private readonly IBloomFilter _bloomFilter;
    private readonly int _pageSize = 1000;
    //IBloomFilter bloomFilter,
    //_bloomFilter = bloomFilter;

    public async Task LoadAllFilterDataAsync()
    {
        // UserName data
        var hasMoreData = true;
        var skip = 0;
        var count = 0;

        while (hasMoreData)
        {
            var userNameList = await queryProcessor.ProcessAsync(new GetAllUserNameQuery(skip, _pageSize));
            hasMoreData = userNameList.Count == _pageSize;
            count += userNameList.Count;
            foreach (var userName in userNameList)
                await cuckooFilter.AddAsync(
                    Encoding.UTF8.GetBytes($"{MyTelegramServerDomainConsts.UserNameCuckooFilterKey}_{userName}"));
            skip += _pageSize;
        }

        logger.LogInformation("Load userName list ok,count={Count}", count);
    }
}