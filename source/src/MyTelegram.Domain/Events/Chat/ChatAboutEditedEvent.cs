namespace MyTelegram.Domain.Events.Chat;

public class ChatAboutEditedEvent(
    RequestInfo requestInfo,
    string? about) : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    public string? About { get; } = about;
}