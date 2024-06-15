namespace MyTelegram.Domain.Events.User;

public class UserProfilePhotoUploadedEvent(
    RequestInfo requestInfo,
    long photoId,
    bool fallback,
    IVideoSize? videoEmojiMarkup)
    : RequestAggregateEvent2<UserAggregate, UserId>(requestInfo)
{
    //public bool HasVideo { get; }
    //public double VideoStartTs { get; }

    //long userId,
    //UserItem userItem,
    /*, bool hasVideo, double videoStartTs*/
    //UserId = userId;
    //UserItem = userItem;
    //HasVideo = hasVideo;
    //VideoStartTs = videoStartTs;

    public long PhotoId { get; } = photoId;

    public bool Fallback { get; } = fallback;

    //public long UserId { get; }
    //public UserItem UserItem { get; }
    public IVideoSize? VideoEmojiMarkup { get; } = videoEmojiMarkup;
}