// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public interface ISendMessageDataConverter :
    ILayeredDataConverter<SendOutboxMessageCompletedSagaEvent, Schema.IUpdates>,
    ILayeredDataConverter<ReceiveInboxMessageCompletedSagaEvent, Schema.IUpdates>
{
    IUpdates ToSelfOtherDeviceUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent);
}

internal sealed class SendMessageDataConverter(
    ILayeredService<IMessageConverter> messageLayeredService,
    IObjectMapper objectMapper)
    : ISendMessageDataConverter
{
    public int Layer => Layers.LayerLatest;

    public string Name => "MyTelegram.Handlers.Messages.ISendMessageHandler";

    public IUpdates Convert(ReceiveInboxMessageCompletedSagaEvent data)
    {
        var item = data.MessageItem;
        if (ShouldCreateFullMessageUpdates(data))
        {
            var updateNewMessages = data.MessageItems.Select(p => new TUpdateNewMessage
            {
                Pts = p.Pts,
                PtsCount = 1,
                Message = messageLayeredService.Converter.ToMessage(p)
            });

            var updates = new TUpdates
            {
                Chats = [],
                Date = item.Date,
                Users = [],
                Seq = 0,
                Updates = new TVector<IUpdate>(updateNewMessages)
            };

            if (data.MessageItem.MessageActionType == MessageActionType.SetMessagesTtl &&
                data.MessageItem.MessageAction is TMessageActionSetMessagesTTL messageActionSetMessagesTtl)
            {
                updates.Updates.Insert(0, new TUpdatePeerHistoryTTL
                {
                    Peer = data.MessageItem.ToPeer.ToPeer(),
                    TtlPeriod = messageActionSetMessagesTtl.Period == 0 ? null : messageActionSetMessagesTtl.Period
                });
            }

            return updates;
        }

        // TODO:
        /*if (item.MessageSubType == MessageSubType.DeleteChatUser)
           {
               var action = (TMessageActionChatDeleteUser)messageAction;
               if (action.UserId == item.OwnerPeer.PeerId)
               {
                   r.Chats = new TVector<IChat>(new TChatForbidden
                   {
                       Id = item.ToPeer.PeerId,
                       Title = aggregateEvent.ChatTitle
                   });
               }
           }*/

        switch (item.ToPeer.PeerType)
        {
            case PeerType.Self:
            case PeerType.User:
                {
                    var updates = new TUpdateShortMessage
                    {
                        Out = false,
                        Message = item.Message,
                        Date = item.Date,
                        Entities = item.Entities,
                        Id = item.MessageId,
                        UserId = item.SenderPeer.PeerId,
                        Pts = item.Pts,
                        PtsCount = 1,
                        Mentioned = true,
                        ReplyTo = item.InputReplyTo.ToMessageReplyHeader(),
                        Silent = item.Silent,
                        TtlPeriod = item.TtlPeriod
                    };
                    return updates;
                }
            case PeerType.Chat:
                {
                    var updates = new TUpdateShortChatMessage
                    {
                        Out = false,
                        Message = item.Message,
                        Date = item.Date,
                        Entities = item.Entities,
                        Id = item.MessageId,
                        //UserId = aggregateEvent.SenderPeerId,
                        FromId = item.SenderPeer.PeerId,
                        ChatId = item.ToPeer.PeerId,
                        Pts = item.Pts,
                        PtsCount = 1,
                        ReplyTo = item.InputReplyTo.ToMessageReplyHeader(),
                        Silent = item.Silent,
                        TtlPeriod = item.TtlPeriod
                    };
                    return updates;
                }
            default:
                throw new NotImplementedException();
        }
    }
    public IUpdates Convert(SendOutboxMessageCompletedSagaEvent data)
    {
        if (data.MessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            if (data.IsSendGroupedMessages)
            {
                return CreateSendGroupedChannelMessageUpdates(data);
            }
            else
            {
                return CreateSendChannelMessageUpdates(data);
            }
        }
        else
        {
            if (ShouldCreateFullMessageUpdates(data))
            {
                return CreateFullMessageUpdates(data);
            }
            else
            {
                var item = data.MessageItem;
                if (item.MessageSubType == MessageSubType.ClearHistory)
                {
                    return CreateClearHistoryUpdates(data);
                }

                return CreateUpdateShortSentMessageUpdates(data);
            }
        }
    }

    public IUpdates ToSelfOtherDeviceUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;

        switch (item.ToPeer.PeerType)
        {
            case PeerType.Self:
            case PeerType.User:
                if (ShouldCreateFullMessageUpdates(aggregateEvent))
                {
                    return CreateFullMessageUpdates(aggregateEvent);
                }

                if (item.MessageSubType == MessageSubType.ClearHistory)
                {
                    return CreateClearHistoryUpdates(aggregateEvent);
                }

                return new TUpdateShortMessage
                {
                    Out = true,
                    Id = item.MessageId,
                    UserId = item.ToPeer.PeerId,
                    Message = item.Message,
                    Pts = item.Pts,
                    PtsCount = 1,
                    Date = item.Date,
                    FwdFrom =
                        item.FwdHeader == null
                            ? null
                            : objectMapper.Map<MessageFwdHeader, TMessageFwdHeader>(item.FwdHeader),
                    ReplyTo = item.InputReplyTo.ToMessageReplyHeader(),
                    Entities = item.Entities,
                    Silent = item.Silent,
                    TtlPeriod = item.TtlPeriod
                };

            case PeerType.Chat:
                return new TUpdateShortChatMessage
                {
                    Out = true,
                    Id = item.MessageId,
                    FromId = item.SenderPeer.PeerId,
                    ChatId = item.ToPeer.PeerId,
                    Message = item.Message,
                    Pts = item.Pts,
                    PtsCount = 1,
                    Date = item.Date,
                    Entities = item.Entities,
                    ReplyTo = item.InputReplyTo.ToMessageReplyHeader(),
                    FwdFrom =
                        item.FwdHeader == null
                            ? null
                            : objectMapper.Map<MessageFwdHeader, TMessageFwdHeader>(item.FwdHeader),
                    Silent = item.Silent,
                    TtlPeriod = item.TtlPeriod
                };

            case PeerType.Channel:
                {
                    var updateReadChannelInbox = new TUpdateReadChannelInbox
                    {
                        ChannelId = item.ToPeer.PeerId,
                        MaxId = item.MessageId,
                        // FolderId = 0,
                        Pts = item.Pts,
                        StillUnreadCount = 0
                    };
                    IUpdate updateNewChannelMessage = new TUpdateNewChannelMessage
                    {
                        Pts = item.Pts,
                        PtsCount = 1,
                        Message = messageLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                            .ToMessage(item, aggregateEvent.RequestInfo.UserId)
                    };
                    if (item.MessageSubType == MessageSubType.ClearHistory)
                    {
                        updateNewChannelMessage = new TUpdateEditChannelMessage
                        {
                            Pts = item.Pts,
                            PtsCount = 1,
                            Message = messageLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer).ToMessage(item)
                        };
                    }

                    var updates = new TUpdates
                    {
                        Updates = new TVector<IUpdate>(updateReadChannelInbox, updateNewChannelMessage),
                        Users = [],
                        Chats = [],
                        Date = item.Date,
                        Seq = 0
                    };
                    ProcessScheduleMessage(aggregateEvent, updates);

                    return updates;
                }
        }

        throw new NotImplementedException();
    }

    private IUpdates CreateClearHistoryUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        return new TUpdates
        {
            Chats = [],
            Users = [],
            Date = DateTime.UtcNow.ToTimestamp(),
            Updates = new TVector<IUpdate>(new TUpdateMessageID
            {
                Id = item.MessageId,
                RandomId = item.RandomId
            },
                new TUpdateEditMessage
                {
                    Message = messageLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                        .ToMessage(item, aggregateEvent.RequestInfo.UserId),
                    Pts = item.Pts,
                    PtsCount = 1
                }
            )
        };
    }

    private IUpdates CreateFullMessageUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent)
    {
        List<IUpdate> updateList = aggregateEvent.MessageItems.Select(p => (IUpdate)new TUpdateMessageID
        {
            Id = p.MessageId,
            RandomId = p.RandomId
        })
                .ToList()
            ;

        List<IUpdate> updateNewMessages = aggregateEvent.MessageItems.Select(p => (IUpdate)new TUpdateNewMessage
        {
            Message = messageLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToMessage(p, aggregateEvent.RequestInfo.UserId),
            Pts = p.Pts,
            PtsCount = 1
        }).ToList();

        updateList.AddRange(updateNewMessages);

        var updates = new TUpdates
        {
            Users = [],
            Chats = [],
            Date = DateTime.UtcNow.ToTimestamp(),
            Updates = new TVector<IUpdate>(updateList)
        };
        ProcessScheduleMessage(aggregateEvent, updates);
        ProcessSetHistoryTtl(aggregateEvent, updates);

        return updates;
    }

    private IUpdates CreateSendChannelMessageUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        var updateMessageId = new TUpdateMessageID { Id = item.MessageId, RandomId = item.RandomId };
        var updateReadChannelInbox = new TUpdateReadChannelInbox
        {
            ChannelId = item.ToPeer.PeerId,
            MaxId = item.MessageId,
            // FolderId = 0,
            Pts = item.Pts,
            StillUnreadCount = 0
        };
        IUpdate updateNewChannelMessage = new TUpdateNewChannelMessage
        {
            Pts = item.Pts,
            PtsCount = 1,
            Message = messageLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToMessage(item, aggregateEvent.RequestInfo.UserId)
        };

        if (item.MessageSubType == MessageSubType.ClearHistory)
        {
            updateNewChannelMessage = new TUpdateEditChannelMessage
            {
                Pts = item.Pts,
                PtsCount = 1,
                Message = messageLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer).ToMessage(item)
            };
        }

        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateMessageId, updateReadChannelInbox, updateNewChannelMessage),
            Users = [],
            Chats = [],
            Date = item.Date,
            Seq = 0
        };

        ProcessScheduleMessage(aggregateEvent, updates);
        ProcessSetHistoryTtl(aggregateEvent, updates);

        return updates;
    }

    private IUpdates CreateSendGroupedChannelMessageUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent)
    {
        List<IUpdate> updateList = aggregateEvent.MessageItems.Select(p => (IUpdate)new TUpdateMessageID
        {
            Id = p.MessageId,
            RandomId = p.RandomId
        }).ToList();
        var item = aggregateEvent.MessageItem;
        // selfUser==-1 means the updates is for channel member except sender
        const int selfUserId = -1;
        foreach (var m in aggregateEvent.MessageItems)
        {
            var updateReadChannelInbox = new TUpdateReadChannelInbox
            {
                ChannelId = m.ToPeer.PeerId,
                MaxId = m.MessageId,
                Pts = m.Pts
            };
            var updateNewChannelMessage = new TUpdateNewChannelMessage
            {
                Message = messageLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                    .ToMessage(m, selfUserId),
                Pts = m.Pts,
                PtsCount = 1
            };

            updateList.Add(updateReadChannelInbox);
            updateList.Add(updateNewChannelMessage);
        }

        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateList),
            Chats = [],
            Date = DateTime.UtcNow.ToTimestamp(),
            Users = []
        };

        ProcessScheduleMessage(aggregateEvent, updates);
        ProcessSetHistoryTtl(aggregateEvent, updates);

        return updates;
    }

    private void ProcessSetHistoryTtl(SendOutboxMessageCompletedSagaEvent aggregateEvent, TUpdates updates)
    {
        if (aggregateEvent.MessageItem.MessageAction is TMessageActionSetMessagesTTL messageActionSetMessagesTtl)
        {
            int? period = messageActionSetMessagesTtl.Period;
            if (period == 0)
            {
                period = null;
            }
            updates.Updates.Insert(1, new TUpdatePeerHistoryTTL
            {
                Peer = aggregateEvent.MessageItem.ToPeer.ToPeer(),
                TtlPeriod = period
            });
        }
    }

    private void ProcessScheduleMessage(SendOutboxMessageCompletedSagaEvent aggregateEvent, TUpdates updates)
    {
        var item = aggregateEvent.MessageItem;
        if (item.ScheduleDate.HasValue)
        {
            updates.Updates.Add(new TUpdateDeleteScheduledMessages
            {
                Messages = new TVector<int>(aggregateEvent.MessageItems.Select(p => p.ScheduleMessageId ?? 0)),
                Peer = item.ToPeer.ToPeer()
            });
        }
    }

    private IUpdates CreateUpdateShortSentMessageUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        return new TUpdateShortSentMessage
        {
            Date = item.Date,
            Entities = item.Entities,
            Id = item.MessageId,
            Out = item.IsOut,
            Pts = item.Pts,
            PtsCount = 1,
            TtlPeriod = item.TtlPeriod
        };
    }

    private bool ShouldCreateFullMessageUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        if (aggregateEvent.IsSendGroupedMessages ||
            aggregateEvent.IsSendQuickReplyMessages ||
            ShouldCreateFullMessageUpdates(item) ||
            item.ScheduleDate.HasValue
           )
        {
            return true;
        }

        return false;
    }

    private bool ShouldCreateFullMessageUpdates(ReceiveInboxMessageCompletedSagaEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        if (aggregateEvent.IsSendGroupedMessages ||
            aggregateEvent.IsSendQuickReplyMessages ||
            ShouldCreateFullMessageUpdates(item)
           )
        {
            return true;
        }

        return false;
    }

    private bool ShouldCreateFullMessageUpdates(MessageItem item)
    {
        if (
            item.Media != null ||
            item.MessageSubType == MessageSubType.ForwardMessage ||
            item.SendMessageType == SendMessageType.MessageService ||
            item.Effect.HasValue ||
            item.ReplyMarkup != null
        )
        {
            return true;
        }

        return false;
    }
}