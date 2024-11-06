namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IUpdatesConverter : ILayeredConverter
{
    IUpdates ToMigrateChatUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent, IChannelReadModel channelReadModel,
        IChatReadModel chatReadModel);

    IUpdates ToMigrateChatUpdates(ReceiveInboxMessageCompletedSagaEvent aggregateEvent, long channelId);

    IUpdates ToChannelMessageUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent, bool mentioned = false);

    IUpdates ToCreateChannelUpdates(ChannelCreatedEvent channelCreatedEvent,
        SendOutboxMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToCreateChatUpdates(ChatCreatedEvent chatCreatedAggregateEvent,
        SendOutboxMessageCompletedSagaEvent aggregateEvent, IChatReadModel chatReadModel);

    IUpdates ToCreateChatUpdates(ChatCreatedEvent eventData,
        ReceiveInboxMessageCompletedSagaEvent aggregateEvent, IChatReadModel chatReadModel);

    IUpdates ToDeleteMessagesUpdates(PeerType toPeerType,
        DeletedBoxItem item,
        int date);

    IUpdates ToEditUpdates(OutboxMessageEditCompletedSagaEvent aggregateEvent,
        long selfUserId);

    IUpdates ToEditUpdates(InboxMessageEditCompletedSagaEvent aggregateEvent);

    IUpdates ToInboxForwardMessageUpdates(ReceiveInboxMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToInviteToChannelUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent,
        StartInviteToChannelEvent startInviteToChannelEvent,
        //IChat channel,
        IChannelReadModel channelReadModel,
        bool createUpdatesForSelf);

    IUpdates ToInviteToChannelUpdates(IChat channel,
        IUserReadModel senderUserReadModel,
        int date);

    IUpdates ToReadHistoryUpdates(ReadHistoryCompletedSagaEvent eventData);
    IUpdates ToReadHistoryUpdates(UpdateOutboxMaxIdCompletedSagaEvent eventData);
    IUpdates ToSelfOtherDeviceUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToSelfUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedSagaEvent aggregateEvent);
    IUpdates ToSelfUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent);
    IUpdates ToUpdatePinnedMessageServiceUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToUpdatePinnedMessageUpdates(UpdatePinnedMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToUpdatePinnedMessageUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToUpdatePinnedMessageUpdates(ReceiveInboxMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToUpdates(ReceiveInboxMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToDraftsUpdates(IReadOnlyCollection<IDraftReadModel> draftReadModels);

    IUpdates ToChannelUpdates(long selfUserId, IChannelReadModel channelReadModel, IPhotoReadModel? photoReadModel);
}
