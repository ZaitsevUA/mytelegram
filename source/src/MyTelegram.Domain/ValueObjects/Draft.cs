namespace MyTelegram.Domain.ValueObjects;

public class Draft(
    string message,
    bool noWebpage,
    int? replyToMsgId,
    int date,
    byte[]? entities = null,
    int? topMsgId = null)
    : ValueObject
{
    public string Message { get; init; } = message;
    public bool NoWebpage { get; init; } = noWebpage;
    public int? ReplyToMsgId { get; init; } = replyToMsgId;
    public int Date { get; init; } = date;
    public byte[]? Entities { get; init; } = entities;
    public int? TopMsgId { get; init; } = topMsgId;
}
