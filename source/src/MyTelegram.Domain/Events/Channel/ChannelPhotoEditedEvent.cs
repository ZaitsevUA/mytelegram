namespace MyTelegram.Domain.Events.Channel;

public class ChannelPhotoEditedEvent(
    RequestInfo requestInfo,
    long channelId,
    long? photoId,
    string messageActionData,
    long randomId)
    : RequestAggregateEvent2<ChannelAggregate, ChannelId>(requestInfo)
{
    //byte[] photo,
    //Photo = photo;

    public long ChannelId { get; } = channelId;
    public long? PhotoId { get; } = photoId;

    public string MessageActionData { get; } = messageActionData;

    //public byte[] Photo { get; }
    public long RandomId { get; } = randomId;
}
