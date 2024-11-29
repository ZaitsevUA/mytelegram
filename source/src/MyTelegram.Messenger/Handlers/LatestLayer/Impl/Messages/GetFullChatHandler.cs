// ReSharper disable All

using IChatFull = MyTelegram.Schema.Messages.IChatFull;

namespace MyTelegram.Handlers.Messages;

/// <summary>
///     Get full info about a <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group</a>.
///     <para>Possible errors</para>
///     Code Type Description
///     400 CHAT_ID_INVALID The provided chat id is invalid.
///     400 PEER_ID_INVALID The provided peer id is invalid.
///     See <a href="https://corefork.telegram.org/method/messages.getFullChat" />
/// </summary>
internal sealed class GetFullChatHandler(
    IQueryProcessor queryProcessor,
    IPeerHelper peerHelper,
    ILayeredService<IChatConverter> layeredChatService,
    ILayeredService<IPhotoConverter> layeredPhotoService,
    ILayeredService<IUserConverter> layeredUserService,
    IUserAppService userAppService,
    IChannelAppService channelAppService,
    IPhotoAppService photoAppService,
    IPrivacyAppService privacyAppService)
    : RpcResultObjectHandler<Schema.Messages.RequestGetFullChat, IChatFull>,
        Messages.IGetFullChatHandler
{
    protected override async Task<IChatFull> HandleCoreAsync(IRequestInput input,
        RequestGetFullChat obj)
    {
        var peerType = peerHelper.GetPeerType(obj.ChatId);
        switch (peerType)
        {
            case PeerType.Channel:
                {
                    var channel = await channelAppService.GetAsync(obj.ChatId);
                    var channelFull = await queryProcessor.ProcessAsync(new GetChannelFullByIdQuery(obj.ChatId),
                        CancellationToken.None);
                    var migratedFromChatReadModel = channelFull!.MigratedFromChatId == null
                        ? null
                        : await queryProcessor.ProcessAsync(new GetChatByChatIdQuery(channelFull.MigratedFromChatId.Value));

                    var channelMember = await queryProcessor
                            .ProcessAsync(new GetChannelMemberByUserIdQuery(obj.ChatId, input.UserId), default)
                        ;
                    var peerNotifySettings = await queryProcessor
                        .ProcessAsync(
                            new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                                PeerType.Channel,
                                obj.ChatId)),
                            CancellationToken.None);
                    var photoReadModel = await photoAppService.GetAsync(channel.PhotoId);
                    return layeredChatService.GetConverter(input.Layer).ToChatFull(
                        input.UserId,
                        channel,
                        photoReadModel,
                        channelFull!,
                        channelMember,
                        peerNotifySettings,
                        migratedFromChatReadModel
                    );
                }
            case PeerType.Chat:
                {
                    var chat = await queryProcessor
                            .ProcessAsync(new GetChatByChatIdQuery(obj.ChatId), CancellationToken.None)
                        ;

                    if (chat == null)
                    {
                        RpcErrors.RpcErrors400.ChatIdInvalid.ThrowRpcError();
                    }

                    var migrateToChannelReadModel = chat!.MigrateToChannelId.HasValue
                        ? await queryProcessor.ProcessAsync(new GetChannelByIdQuery(chat.MigrateToChannelId.Value))
                        : null;

                    var userList = chat!.IsDeleted
                        ? Array.Empty<IUserReadModel>()
                        : await userAppService.GetListAsync(chat!.ChatMembers.Select(p => p.UserId).ToList());
                    var contactReadModels = chat.IsDeleted
                            ? Array.Empty<IContactReadModel>()
                            : await queryProcessor
                                .ProcessAsync(
                                    new GetContactListQuery(input.UserId, userList.Select(p => p.UserId).ToList()), default)
                        ;

                    var peerNotifySettings = await queryProcessor
                        .ProcessAsync(
                            new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                                PeerType.Chat,
                                obj.ChatId)),
                            CancellationToken.None);

                    var privacyList = await privacyAppService.GetPrivacyListAsync(userList.Select(p => p.UserId).ToList());

                    var photoIds = new List<long>();
                    photoIds.AddRange(userList.Select(p => p.ProfilePhotoId ?? 0));
                    photoIds.AddRange(userList.Select(p => p.FallbackPhotoId ?? 0));
                    photoIds.AddRange(contactReadModels.Select(p => p.PhotoId ?? 0));
                    photoIds.Add(chat.PhotoId ?? 0);
                    photoIds.Add(migrateToChannelReadModel?.PhotoId ?? 0);
                    photoIds.RemoveAll(p => p == 0);

                    var photos = await photoAppService.GetListAsync(photoIds);

                    var users = layeredUserService.GetConverter(input.Layer)
                        .ToUserList(input.UserId, userList, photos, contactReadModels, privacyList);

                    var photoReadModel = chat.MigrateToChannelId.HasValue
                        ? photos.FirstOrDefault(p => p.PhotoId == migrateToChannelReadModel?.PhotoId)
                        : photos.FirstOrDefault(p => p.PhotoId == chat.PhotoId);

                    return layeredChatService.GetConverter(input.Layer).ToChatFull(
                        input.UserId,
                        chat,
                        photoReadModel,
                        users,
                        peerNotifySettings,
                        migrateToChannelReadModel
                    );
                }
        }

        throw new NotImplementedException($"Not supported peer type {peerType},chatId={obj.ChatId}");
    }
}