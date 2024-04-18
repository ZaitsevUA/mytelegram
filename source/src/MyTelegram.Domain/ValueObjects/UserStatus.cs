using System.Text;

namespace MyTelegram.Domain.ValueObjects;

public class UserStatus(
    long userId,
    bool online)
{
    public DateTime LastUpdateDate { get; internal set; } = DateTime.UtcNow;
    public bool Online { get; internal set; } = online;

    public long UserId { get; internal set; } = userId;

    public void UpdateStatus(bool online)
    {
        Online = online;
        LastUpdateDate = DateTime.UtcNow;
    }
}

public class Reaction(
    long userId,
    string? emoticon,
    long? customEmojiDocumentId,
    int? date = 0)
    : ValueObject
{
    //public Reaction()
    //{
    //}

    public long UserId { get; set; } = userId;
    public string? Emoticon { get; set; } = emoticon;
    public long? CustomEmojiDocumentId { get; set; } = customEmojiDocumentId;

    public int? Date { get; set; } = date;
    //public int? ChosenOrder { get; set; }

    //public int GetReactionCount() => _count;

    public long GetReactionId()
    {
        if (CustomEmojiDocumentId.HasValue)
        {
            return CustomEmojiDocumentId.Value;
        }

        if (string.IsNullOrEmpty(Emoticon))
        {
            throw new InvalidOperationException("Emotion and CustomEmojiDocumentId is null");
        }
        var bytes = Encoding.UTF8.GetBytes(Emoticon);
        if (bytes.Length >= 8)
        {
            return BitConverter.ToInt64(bytes);
        }

        var newBytes = new byte[8];
        Buffer.BlockCopy(bytes, 0, newBytes, 0, bytes.Length);

        return BitConverter.ToInt64(newBytes);
    }
}

public class ReactionCount(
    string? emoticon,
    long? customEmojiDocumentId,
    int count)
    : ValueObject
{
    public string? Emoticon { get; internal set; } = emoticon;
    public long? CustomEmojiDocumentId { get; internal set; } = customEmojiDocumentId;
    public int Count { get; internal set; } = count;

    public int? ChosenOrder { get; set; }
    //public bool? CanSeeList { get; internal set; }

    //public ReactionCount()
    //{
    //}

    public void IncrementCount()
    {
        Count++;
    }

    public void DecrementCount()
    {
        Count--;
    }

    public long GetReactionId()
    {
        if (CustomEmojiDocumentId.HasValue)
        {
            return CustomEmojiDocumentId.Value;
        }

        if (string.IsNullOrEmpty(Emoticon))
        {
            throw new InvalidOperationException("Emotion and CustomEmojiDocumentId is null");
        }
        var bytes = Encoding.UTF8.GetBytes(Emoticon);
        if (bytes.Length >= 8)
        {
            return BitConverter.ToInt64(bytes);
        }

        var newBytes = new byte[8];
        Buffer.BlockCopy(bytes, 0, newBytes, 0, bytes.Length);

        return BitConverter.ToInt64(newBytes);
    }
}