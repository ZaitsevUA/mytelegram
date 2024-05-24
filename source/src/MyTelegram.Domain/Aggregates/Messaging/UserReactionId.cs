namespace MyTelegram.Domain.Aggregates.Messaging;

public class UserReactionId(string value) : Identity<UserReactionId>(value)
{
    public static UserReactionId Create(long reactionSenderPeerId,
        long toPeerId,
        int messageId,
        long reactionId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"userreactionid-{reactionSenderPeerId}-{toPeerId}-{messageId}-{reactionId}");
    }
}