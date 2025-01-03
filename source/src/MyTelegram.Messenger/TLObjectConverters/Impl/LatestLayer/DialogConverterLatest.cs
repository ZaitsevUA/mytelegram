using MyTelegram.Schema.Updates;

namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class DialogConverterLatest(
    ILayeredService<IChatConverter> layeredChatService,
    ILayeredService<IUserConverter> layeredUserService,
    ILayeredService<IMessageConverter> layeredMessageService,
    IObjectMapper objectMapper,
    ILayeredService<IPeerNotifySettingsConverter> layeredPeerNotifySettingsService)
    : LayeredConverterBase, IDialogConverterLatest
{
    private IChatConverter? _chatConverter;
    private IMessageConverter? _messageConverter;
    private IUserConverter? _userConverter;
    protected virtual IObjectMapper ObjectMapper { get; } = objectMapper;

    public override int Layer => Layers.LayerLatest;

    public virtual IDialogs ToDialogs(GetDialogOutput output)
    {
        var dialogs = new List<IDialog>();
        var messages = output.MessageList.Where(p => p.ToPeerType == PeerType.Channel)
            .ToDictionary(k => k.Id, v => v);
        //var peerNotifySettingsDict = output.PeerNotifySettingList.ToDictionary(k => k.PeerId, v => v);
        foreach (var dialog in output.DialogList)
        {
            var channelReadModel = dialog.ToPeerType == PeerType.Channel
                ? output.ChannelList.FirstOrDefault(p => p.ChannelId == dialog.ToPeerId)
                : null;
            var tDialog = CreateDialog(dialog, messages, channelReadModel);
            if (dialog.ToPeerType == PeerType.Chat)
            {
            }


            dialogs.Add(tDialog);
        }

        var userList = GetUserConverter().ToUserList(output.SelfUserId, output.UserList, output.PhotoList,
            output.ContactList, output.PrivacyList);
        var chatList = GetChatConverter().ToChatList(output.SelfUserId, output.ChatList, output.PhotoList);

        var channelList = GetChatConverter().ToChannelList(
            output.SelfUserId,
            output.ChannelList,
            output.PhotoList,
            output.ChannelList.Select(p => p.ChannelId).ToList(),
            output.ChannelMemberList);
        foreach (var chat in channelList)
        {
            chatList.Add(chat);
        }

        var allMessages = output.MessageList.ToList();

        foreach (var chat in channelList)
        {
            if (chat is TChannelForbidden channelForbidden)
            {
                allMessages.RemoveAll(p => p.ToPeerId == channelForbidden.Id);
            }
        }

        var messageList = GetMessageConverter()
            .ToMessages(allMessages, output.PollList, output.ChosenPollOptions, output.SelfUserId);

        if (dialogs.Count == output.Limit)
        {
            return new TDialogsSlice
            {
                Chats = new TVector<IChat>(chatList),
                Dialogs = new TVector<IDialog>(dialogs),
                Messages = new TVector<IMessage>(messageList),
                Users = new TVector<IUser>(userList),
                Count = output.Limit
            };
        }

        return new TDialogs
        {
            Chats = new TVector<IChat>(chatList),
            Dialogs = new TVector<IDialog>(dialogs),
            Messages = new TVector<IMessage>(messageList),
            Users = new TVector<IUser>(userList)
        };
    }

    public IPeerDialogs ToPeerDialogs(GetDialogOutput output)
    {
        var dialogs = ToDialogs(output);

        var pts = output.PtsReadModel?.Pts ?? 0;
        if (output.CachedPts > pts)
        {
            pts = output.CachedPts;
        }

        return dialogs switch
        {
            TDialogs dialogs1 => new TPeerDialogs
            {
                Chats = dialogs1.Chats,
                Dialogs = dialogs1.Dialogs,
                Messages = dialogs1.Messages,
                Users = dialogs1.Users,
                State = new TState
                {
                    Pts = pts,
                    Date = DateTime.UtcNow.ToTimestamp(),
                    Qts = output.PtsReadModel?.Qts ?? 0,
                    UnreadCount = output.PtsReadModel?.UnreadCount ?? 0
                }
            },
            TDialogsSlice dialogsSlice => new TPeerDialogs
            {
                Chats = dialogsSlice.Chats,
                Dialogs = dialogsSlice.Dialogs,
                Messages = dialogsSlice.Messages,
                Users = dialogsSlice.Users,
                State = new TState
                {
                    Pts = pts,
                    Date = DateTime.UtcNow.ToTimestamp(),
                    Qts = output.PtsReadModel?.Qts ?? 0,
                    UnreadCount = output.PtsReadModel?.UnreadCount ?? 0
                }
            },
            _ => throw new ArgumentOutOfRangeException(nameof(output))
        };
    }

    protected virtual IDialog CreateDialog(IDialogReadModel dialogReadModel,
        Dictionary<string, IMessageReadModel> messages, IChannelReadModel? channelReadModel)
    {
        var tDialog = ObjectMapper.Map<IDialogReadModel, TDialog>(dialogReadModel);
        tDialog.NotifySettings = GetPeerNotifySettings(dialogReadModel.NotifySettings);
        if (dialogReadModel.ReadOutboxMaxId == 0 && dialogReadModel.ReadInboxMaxId != 0)
        {
            tDialog.ReadInboxMaxId = dialogReadModel.ReadInboxMaxId;
        }

        if (dialogReadModel.ToPeerType == PeerType.Channel)
        {
            if (channelReadModel != null)
            {
                var maxId = new[]
                {
                    dialogReadModel.MaxSendOutMessageId, dialogReadModel.ReadOutboxMaxId,
                    dialogReadModel.ReadInboxMaxId,
                    dialogReadModel.ChannelHistoryMinId
                }.Max();
                tDialog.TopMessage = channelReadModel.TopMessageId;
                tDialog.Pts = channelReadModel.Pts;
                tDialog.UnreadCount = channelReadModel.TopMessageId - maxId;
                if (tDialog.UnreadCount < 0)
                {
                    tDialog.UnreadCount = 0;
                }
            }
        }
        else
        {
            tDialog.Pts = null;
        }

        return tDialog;
    }

    protected virtual IChatConverter GetChatConverter()
    {
        return _chatConverter ??= layeredChatService.GetConverter(GetLayer());
    }

    protected virtual IMessageConverter GetMessageConverter()
    {
        return _messageConverter ??= layeredMessageService.GetConverter(GetLayer());
    }

    protected virtual IPeerNotifySettings? GetPeerNotifySettings(PeerNotifySettings? peerNotifySettings)
    {
        var layer = GetLayer();
        var converter = layeredPeerNotifySettingsService.GetConverter(layer);
        var settings = converter.ToPeerNotifySettings(peerNotifySettings);

        return layeredPeerNotifySettingsService.GetConverter(GetLayer()).ToPeerNotifySettings(peerNotifySettings);
    }

    protected virtual IUserConverter GetUserConverter()
    {
        return _userConverter ??= layeredUserService.GetConverter(GetLayer());
    }
}