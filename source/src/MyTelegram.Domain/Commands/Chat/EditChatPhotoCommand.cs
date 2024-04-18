namespace MyTelegram.Domain.Commands.Chat;

public class EditChatPhotoCommand(
    ChatId aggregateId,
    RequestInfo requestInfo,
    long fileId,
    long photoId,
    string messageActionData,
    long randomId)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo)
{
    //byte[] photo,
    //Photo = photo;

    public long FileId { get; } = fileId;
    public long PhotoId { get; } = photoId;

    public string MessageActionData { get; } = messageActionData;

    //public byte[] Photo { get; }
    public long RandomId { get; } = randomId;
}
