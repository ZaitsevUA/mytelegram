namespace MyTelegram.QueryHandlers.MongoDB.Channel;

public class GetSendAsPeerIdQueryHandler : IQueryHandler<GetSendAsPeerIdQuery, long?>
{
    private readonly IQueryOnlyReadModelStore<ChannelReadModel> _store;

    public GetSendAsPeerIdQueryHandler(IQueryOnlyReadModelStore<ChannelReadModel> store)
    {
        _store = store;
    }

    public async Task<long?> ExecuteQueryAsync(GetSendAsPeerIdQuery query, CancellationToken cancellationToken)
    {
        return await _store.
            FirstOrDefaultAsync(p => (p.LinkedChatId == query.LinkedChannelId || p.ChannelId == query.LinkedChannelId) && p.CreatorId == query.CreatorUserId, p => p.ChannelId, cancellationToken: cancellationToken);
    }
}