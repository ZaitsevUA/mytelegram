// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get recently used <a href="https://corefork.telegram.org/api/emoji-status">emoji statuses</a>
/// See <a href="https://corefork.telegram.org/method/account.getRecentEmojiStatuses" />
///</summary>
internal sealed class GetRecentEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetRecentEmojiStatuses, MyTelegram.Schema.Account.IEmojiStatuses>,
    Account.IGetRecentEmojiStatusesHandler
{
    private readonly IQueryProcessor _queryProcessor;

    public GetRecentEmojiStatusesHandler(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    protected override async Task<MyTelegram.Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetRecentEmojiStatuses obj)
    {
        if (input.UserId == 0)
        {
            return new TEmojiStatuses
            {
                Statuses = new()
            };
        }

        var user = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(input.UserId), default);
        if (user == null)
        {
            RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
        }

        if (user!.RecentEmojiStatuses?.Count > 0)
        {
            return new TEmojiStatuses
            {
                Statuses = new TVector<IEmojiStatus>(user!.RecentEmojiStatuses.Select(p => new TEmojiStatus()
                {
                    DocumentId = p
                }))
            };
        }

        return new TEmojiStatuses
        {
            Statuses = new()
        };
    }
}
