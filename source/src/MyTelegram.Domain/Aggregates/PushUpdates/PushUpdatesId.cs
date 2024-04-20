namespace MyTelegram.Domain.Aggregates.PushUpdates;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<PushUpdatesId>))]
public class PushUpdatesId(string value) : Identity<PushUpdatesId>(value)
{
    public static PushUpdatesId Create(long peerId,
        long excludeAuthKeyId,
        long excludeUid,
        long onlySendToThisAuthKeyId,
        int pts)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"push_id_pts_{peerId}_{excludeAuthKeyId}_{excludeUid}_{onlySendToThisAuthKeyId}_{pts}");
    }

    public static PushUpdatesId CreateEncryptedPushId(long peerId,
        long permAuthKeyId,
        int qts)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"push_id_qts_{peerId}_{permAuthKeyId}_{qts}");
    }
}
