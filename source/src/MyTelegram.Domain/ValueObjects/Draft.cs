namespace MyTelegram.Domain.ValueObjects;

public class Draft(
    bool noWebpage,
    bool invertMedia,
    int? replyToMsgId,
    string message,
    int date,
    byte[]? entities = null,
    byte[]? media = null,
    int? topMsgId = null,
    long? effect = null
    )
    : ValueObject
{
    //bool? invertMedia,

    public string Message { get; init; } = message;
    public bool NoWebpage { get; init; } = noWebpage;
    public bool InvertMedia { get; } = invertMedia;
    public int? ReplyToMsgId { get; init; } = replyToMsgId;
    public int Date { get; init; } = date;
    public byte[]? Entities { get; init; } = entities;
    public byte[]? Media { get; } = media;
    public int? TopMsgId { get; init; } = topMsgId;
    public long? Effect { get; } = effect;
}
