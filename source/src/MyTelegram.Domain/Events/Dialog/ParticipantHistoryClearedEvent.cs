namespace MyTelegram.Domain.Events.Dialog;

public class ParticipantHistoryClearedEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public ParticipantHistoryClearedEvent(
        RequestInfo requestInfo,
        long ownerPeerId,
        int historyMinId):base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        HistoryMinId = historyMinId;

    }

    public int HistoryMinId { get; }

    public long OwnerPeerId { get; }

}
