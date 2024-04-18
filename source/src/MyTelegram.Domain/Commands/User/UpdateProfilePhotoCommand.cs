namespace MyTelegram.Domain.Commands.User;

public class UpdateProfilePhotoCommand(
    UserId aggregateId,
    RequestInfo requestInfo,
    long photoId,
    bool fallback)
    : RequestCommand2<UserAggregate, UserId, IExecutionResult>(aggregateId, requestInfo)
{
    //long fileId,
    //,
    //byte[]? photo,
    //VideoSizeEmojiMarkup? videoEmojiMarkup = null
    //Photo = photo;
    //VideoEmojiMarkup = videoEmojiMarkup;

    public long PhotoId { get; } = photoId;
    public bool Fallback { get; } = fallback;
}