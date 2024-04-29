// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

/// <summary>
///     Search for messages and peers globally
///     <para>Possible errors</para>
///     Code Type Description
///     400 FOLDER_ID_INVALID Invalid folder ID.
///     400 SEARCH_QUERY_EMPTY The search query is empty.
///     See <a href="https://corefork.telegram.org/method/messages.searchGlobal" />
/// </summary>
internal sealed class SearchGlobalHandler(
    IMessageAppService messageAppService,
    ILayeredService<IRpcResultProcessor> layeredService)
    :
        RpcResultObjectHandler<Schema.Messages.RequestSearchGlobal, Schema.Messages.IMessages>,
        Messages.ISearchGlobalHandler
{
    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestSearchGlobal obj)
    {
        var userId = input.UserId;

        var r = await messageAppService.SearchGlobalAsync(new SearchGlobalInput
        {
            OwnerPeerId = userId,
            SelfUserId = userId,
            Limit = obj.Limit,
            Q = obj.Q,
            FolderId = obj.FolderId,
            OffsetId = obj.OffsetId
        });

        return layeredService.GetConverter(input.Layer).ToMessages(r, input.Layer);
    }
}