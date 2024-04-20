namespace MyTelegram.Domain.Events.Chat;

public class NewChatMsgIdPinnedEvent(int pinnedMsgId) : AggregateEvent<ChatAggregate, ChatId>
{
    public int PinnedMsgId { get; } = pinnedMsgId;
}
