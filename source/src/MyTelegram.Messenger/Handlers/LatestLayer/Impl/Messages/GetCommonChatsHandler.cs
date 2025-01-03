// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

/// <summary>
///     Get chats in common with a user
///     <para>Possible errors</para>
///     Code Type Description
///     400 MSG_ID_INVALID Invalid message ID provided.
///     400 USER_ID_INVALID The provided user ID is invalid.
///     See <a href="https://corefork.telegram.org/method/messages.getCommonChats" />
/// </summary>
internal sealed class GetCommonChatsHandler(
    IQueryProcessor queryProcessor,
    ILayeredService<IChatConverter> chatLayeredService,
    IPhotoAppService photoAppService,
    IAccessHashHelper accessHashHelper,
    IPeerHelper peerHelper) : RpcResultObjectHandler<Schema.Messages.RequestGetCommonChats, Schema.Messages.IChats>,
    Messages.IGetCommonChatsHandler
{
    protected override async Task<Schema.Messages.IChats> HandleCoreAsync(IRequestInput input,
        Schema.Messages.RequestGetCommonChats obj)
    {
        await accessHashHelper.CheckAccessHashAsync(obj.UserId);
        var peer = peerHelper.GetPeer(obj.UserId);
        var limit = obj.Limit;
        if (limit < 0)
        {
            limit = 40;
        }

        var commonChannelIds =
            await queryProcessor.ProcessAsync(new GetCommonChatChannelIdsQuery(input.UserId, peer.PeerId, obj.MaxId,
                limit));

        if (commonChannelIds.Count > 0)
        {
            var channelReadModels =
                await queryProcessor.ProcessAsync(new GetChannelByChannelIdListQuery(commonChannelIds.ToList()));
            var photoReadModels = await photoAppService.GetPhotosAsync(channelReadModels);
            var chats = chatLayeredService.GetConverter(input.Layer)
                .ToChannelList(input.UserId, channelReadModels, photoReadModels, commonChannelIds, []);

            return new TChats
            {
                Chats = [.. chats]
            };
        }

        return new TChats
        {
            Chats = []
        };
    }
}