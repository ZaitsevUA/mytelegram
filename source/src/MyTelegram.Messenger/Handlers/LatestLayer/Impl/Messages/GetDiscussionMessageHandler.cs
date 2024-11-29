// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/threads">discussion message</a> from the <a href="https://corefork.telegram.org/api/discussion">associated discussion group</a> of a channel to show it on top of the comment section, without actually joining the group
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TOPIC_ID_INVALID The specified topic ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getDiscussionMessage" />
///</summary>
internal sealed class GetDiscussionMessageHandler(
    IPeerHelper peerHelper,
    IQueryProcessor queryProcessor,
    IChannelAppService channelAppService,
    ILayeredService<IChatConverter> layeredChatService,
    ILayeredService<IMessageConverter> layeredMessageService,
    IAccessHashHelper accessHashHelper,
    IPhotoAppService photoAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDiscussionMessage,
            MyTelegram.Schema.Messages.IDiscussionMessage>,
        Messages.IGetDiscussionMessageHandler
{
    protected override async Task<IDiscussionMessage> HandleCoreAsync(IRequestInput input,
        RequestGetDiscussionMessage obj)
    {
        await accessHashHelper.CheckAccessHashAsync(obj.Peer);
        // peer is the channel peer
        var peer = peerHelper.GetPeer(obj.Peer);
        var channelReadModel = await channelAppService.GetAsync(peer.PeerId);
        if (channelReadModel == null)
        {
            RpcErrors.RpcErrors400.ChatIdInvalid.ThrowRpcError();
        }
        var query = new GetDiscussionMessageQuery(channelReadModel!.Broadcast, peer.PeerId, obj.MsgId);

        var messageReadModel = await queryProcessor
            .ProcessAsync(query);

        if (messageReadModel == null)
        {
            return new TDiscussionMessage
            {
                Chats = new TVector<IChat>(),
                Messages = new TVector<IMessage>(),
                Users = new TVector<IUser>(),
            };
        }

        var dialogReadModel =
            await queryProcessor.ProcessAsync(new GetDialogByIdQuery(DialogId.Create(input.UserId, PeerType.Channel, messageReadModel.ToPeerId)), default);
        var channelReadModels = await queryProcessor
            .ProcessAsync(new GetChannelByChannelIdListQuery(new long[] { peer.PeerId, messageReadModel.ToPeerId }), default)
     ;

        var readMaxId = 0;
        if (dialogReadModel != null)
        {
            readMaxId = Math.Max(dialogReadModel.ReadInboxMaxId, dialogReadModel.ReadOutboxMaxId);
        }

        //if (reply?.MaxId > 0 && readMaxId > reply.MaxId)
        //{
        //    readMaxId = reply.MaxId;
        //}

        var message = layeredMessageService.GetConverter(input.Layer).ToDiscussionMessage(input.UserId, messageReadModel);

        var photoReadModels = await photoAppService.GetPhotosAsync(channelReadModels);

        var chats = layeredChatService.GetConverter(input.Layer).ToChannelList(
            input.UserId,
            channelReadModels,
            photoReadModels,
            new List<long>(),
            new List<IChannelMemberReadModel>(),
             true);

        return new TDiscussionMessage
        {
            Chats = new TVector<IChat>(chats),
            Messages = new TVector<IMessage>(message),
            Users = new TVector<IUser>(),
            MaxId = readMaxId,
            ReadInboxMaxId = dialogReadModel?.ReadInboxMaxId,
            ReadOutboxMaxId = dialogReadModel?.ReadOutboxMaxId
        };
    }
}
