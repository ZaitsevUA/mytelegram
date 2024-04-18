namespace MyTelegram.Domain.Events.User;

public class UserProfilePhotoChangedEvent(
    RequestInfo requestInfo,
    long userId,
    long photoId,
    bool fallback)
    : RequestAggregateEvent2<UserAggregate, UserId>(requestInfo)
{
    //public bool HasVideo { get; }
    //public double VideoStartTs { get; }

    //,
    //UserItem userItem//,
    //VideoSizeEmojiMarkup? videoEmojiMarkup
    /*, bool hasVideo, double videoStartTs*/
    //UserItem = userItem;
    //VideoEmojiMarkup = videoEmojiMarkup;
    //HasVideo = hasVideo;
    //VideoStartTs = videoStartTs;

    public long UserId { get; } = userId;
    public long PhotoId { get; } = photoId;

    public bool Fallback { get; } = fallback;
    //public UserItem UserItem { get; }
    //public VideoSizeEmojiMarkup? VideoEmojiMarkup { get; }
}

//[EventVersion("UserCreatedEvent", 1)]
//[EventVersion("UpdateProfile", 1)]

//[EventVersion("UpdateStatus", 1)]