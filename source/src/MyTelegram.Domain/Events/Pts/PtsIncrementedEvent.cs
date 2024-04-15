namespace MyTelegram.Domain.Events.Pts;

public class PtsIncrementedEvent : RequestAggregateEvent2< PtsAggregate, PtsId> 
{
    public PtsIncrementedEvent(
        RequestInfo requestInfo,
        long peerId,
        int pts,
        PtsChangeReason reason,
        string messageBoxId ):base(requestInfo)
    {
        PeerId = peerId;
        Pts = pts;
        Reason = reason;
        MessageBoxId = messageBoxId;
        
    }

    public string MessageBoxId { get; }

    public long PeerId { get; }

    /// <summary>
    ///     the new pts
    /// </summary>
    public int Pts { get; }

    public PtsChangeReason Reason { get; }
    
}
