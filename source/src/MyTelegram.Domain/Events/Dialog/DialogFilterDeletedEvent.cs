namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogFilterDeletedEvent(
    RequestInfo requestInfo,
    int filterId) : RequestAggregateEvent2<DialogFilterAggregate, DialogFilterId>(requestInfo)
{
    public int FilterId { get; } = filterId;
}
