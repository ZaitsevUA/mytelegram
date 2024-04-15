namespace MyTelegram.Domain.Aggregates.ChatInvite;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ChatInviteId>))]
public class ChatInviteId : Identity<ChatInviteId>
{
    public ChatInviteId(string value) : base(value)
    {
    }

    public static ChatInviteId Create(long channelId, long inviteId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"chatinviteid-{channelId}-{inviteId}");
    }
}