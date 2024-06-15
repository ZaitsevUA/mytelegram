namespace MyTelegram.ReadModel.ReadModelLocators;

public class PtsReadModelLocator : IPtsReadModelLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var ownerPeerId = 0L;
        var aggregateEvent = domainEvent.GetAggregateEvent();
        switch (aggregateEvent)
        {
            case TempPtsIncrementedEvent tempPtsIncrementedEvent:
                ownerPeerId = tempPtsIncrementedEvent.OwnerPeerId;
                break;

            case PtsGlobalSeqNoUpdatedEvent ptsGlobalSeqNoUpdatedEvent:
                ownerPeerId = ptsGlobalSeqNoUpdatedEvent.PeerId;
                break;

            case UpdatesCreatedEvent updateCreatedEvent:
                ownerPeerId = updateCreatedEvent.OwnerPeerId;
                break;

            case EncryptedPushUpdatesCreatedEvent encryptedPushUpdatesCreatedEvent:
                ownerPeerId = encryptedPushUpdatesCreatedEvent.InboxOwnerPeerId;
                break;

            case PtsUpdatedEvent ptsUpdatedEvent:
                ownerPeerId = ptsUpdatedEvent.PeerId;
                break;
            case InboxMessageEditCompletedEvent inboxEditCompletedEvent:
                ownerPeerId = inboxEditCompletedEvent.OwnerPeerId;
                break;
            case OutboxMessageEditCompletedEvent outboxEditCompletedEvent:
                ownerPeerId = outboxEditCompletedEvent.OwnerPeerId;
                break;
            case ReadHistoryPtsIncrementEvent readHistoryPtsIncrementEvent:
                ownerPeerId = readHistoryPtsIncrementEvent.PeerId;
                break;
            case DeleteMessagePtsIncrementedEvent4 deleteMessagePtsIncrementedEvent:
                ownerPeerId = deleteMessagePtsIncrementedEvent.UserId;
                break;
            case ClearSingleUserHistoryCompletedEvent clearSingleUserHistoryCompletedEvent:
                ownerPeerId = clearSingleUserHistoryCompletedEvent.DeletedBoxItem.OwnerPeerId;
                break;
            case UserCreatedEvent userCreatedEvent:
                ownerPeerId = userCreatedEvent.UserId;
                break;
            case UpdatePinnedBoxPtsCompletedEvent updatePinnedBoxPtsCompletedEvent:
                ownerPeerId = updatePinnedBoxPtsCompletedEvent.PeerId;
                break;
            case ChannelCreatedEvent channelCreatedEvent:
                ownerPeerId = channelCreatedEvent.ChannelId;
                break;
            case UpdateOutboxPinnedCompletedEvent updateOutboxPinnedCompletedEvent:
                ownerPeerId = updateOutboxPinnedCompletedEvent.OwnerPeerId;
                break;
        }

        if (ownerPeerId != 0)
        {
            yield return PtsId.Create(ownerPeerId).Value;
        }
    }
}