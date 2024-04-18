namespace MyTelegram.Domain.Commands.User;

public class UploadProfilePhotoCommand(
    UserId aggregateId,
    RequestInfo requestInfo,
    long photoId,
    bool fallback,
    byte[]? photo,
    VideoSizeEmojiMarkup? videoEmojiMarkup = null)
    : RequestCommand2<UserAggregate, UserId, IExecutionResult>(aggregateId, requestInfo)
{
    //public byte[]? Photo { get; }
    //public VideoSizeEmojiMarkup? VideoEmojiMarkup { get; }

    //long fileId,

    public long PhotoId { get; } = photoId;
    public bool Fallback { get; } = fallback;
    public byte[]? Photo { get; } = photo;
    public VideoSizeEmojiMarkup? VideoEmojiMarkup { get; } = videoEmojiMarkup;
}