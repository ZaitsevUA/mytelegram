namespace MyTelegram.Domain.Events.Chat;

public class ChatPhotoEditedEvent(
    RequestInfo requestInfo,
    long chatId,
    long photoId,
    string messageActionData,
    long randomId)
    : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    //byte[] photo,
    //Photo = photo;

    public long ChatId { get; } = chatId;
    public long PhotoId { get; } = photoId;

    public string MessageActionData { get; } = messageActionData;

    //public byte[] Photo { get; }
    public long RandomId { get; } = randomId;
}