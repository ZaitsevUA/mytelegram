// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Delete messages in a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 MESSAGE_DELETE_FORBIDDEN You can't delete one of the messages you tried to delete, most likely because it is a service message.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.deleteMessages" />
///</summary>
internal sealed class DeleteMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteMessages, MyTelegram.Schema.Messages.IAffectedMessages>,
    Channels.IDeleteMessagesHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPtsHelper _ptsHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IChannelAdminRightsChecker _channelAdminRightsChecker;
    private readonly IQueryProcessor _queryProcessor;
    public DeleteMessagesHandler(ICommandBus commandBus,
        IPtsHelper ptsHelper,
        IAccessHashHelper accessHashHelper, IQueryProcessor queryProcessor, IChannelAdminRightsChecker channelAdminRightsChecker)
    {
        _commandBus = commandBus;
        _ptsHelper = ptsHelper;
        _accessHashHelper = accessHashHelper;
        _queryProcessor = queryProcessor;
        _channelAdminRightsChecker = channelAdminRightsChecker;
    }

    protected override async Task<IAffectedMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteMessages obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            if (obj.Id.Count > 0)
            {
                var ids = obj.Id.ToList();

                if (!await _channelAdminRightsChecker.HasChatAdminRightAsync(inputChannel.ChannelId, input.UserId,
                        p => p.AdminRights.DeleteMessages))
                {
                    var firstInboxMessageId =
                        await _queryProcessor.ProcessAsync(
                            new GetFirstInboxMessageIdByMessageIdListQuery(inputChannel.ChannelId, ids));
                    if (firstInboxMessageId > 0)
                    {
                        RpcErrors.RpcErrors403.MessageDeleteForbidden.ThrowRpcError();
                    }
                }

                // Delete channel post message: delete all repies
                // Delete forwarded post message: update post message channelId to 777
                var channelReadModel =
                    await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(inputChannel.ChannelId));
                IReadOnlyCollection<int>? repliesMessageIds = null;
                long? discussionGroupChannelId = null;
                var newTopMessageId =
                    await _queryProcessor.ProcessAsync(new GetTopMessageIdQuery(inputChannel.ChannelId, ids));
                int? newTopMessageIdForDiscussionGroup = null;

                if (channelReadModel!.Broadcast && channelReadModel.LinkedChatId.HasValue)
                {
                    discussionGroupChannelId = channelReadModel.LinkedChatId;
                    repliesMessageIds =
                      await _queryProcessor.ProcessAsync(
                          new GetCommentsMessageIdListQuery(channelReadModel.ChannelId, ids));

                    if (repliesMessageIds.Any())
                    {
                        newTopMessageIdForDiscussionGroup =
                          await _queryProcessor.ProcessAsync(new GetTopMessageIdQuery(channelReadModel.LinkedChatId.Value,
                              repliesMessageIds.ToList()));
                    }
                }


                var command =
                    new StartDeleteChannelMessagesCommand(TempId.New, input.ToRequestInfo(), inputChannel.ChannelId,
                        ids,
                        newTopMessageId,
                        newTopMessageIdForDiscussionGroup,
                        discussionGroupChannelId,
                        repliesMessageIds
                        );
                await _commandBus.PublishAsync(command);

                return null!;
            }

            var pts = _ptsHelper.GetCachedPts(input.UserId);

            return new TAffectedMessages { Pts = pts, PtsCount = 0 };
        }

        throw new NotImplementedException();
    }
}
