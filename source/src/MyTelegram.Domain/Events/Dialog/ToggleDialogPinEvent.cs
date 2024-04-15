namespace MyTelegram.Domain.Events.Dialog;

public class ToggleDialogPinEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public ToggleDialogPinEvent(RequestInfo requestInfo,
        bool pinned) : base(requestInfo)
    {
        Pinned = pinned;
    }

    public bool Pinned { get; }
}