namespace MyTelegram.Domain.Sagas.Events;

public class InviteToChannelCompletedEvent(
    RequestInfo requestInfo,
    long channelId,
    long inviterId,
    bool broadcast,
    IReadOnlyList<long> memberUidList,
    IReadOnlyList<long>? privacyRestrictedUserId)
    : RequestAggregateEvent2<InviteToChannelSaga, InviteToChannelSagaId>(requestInfo),
        IHasCorrelationId
{
    public long ChannelId { get; } = channelId;
    public long InviterId { get; } = inviterId;
    public bool Broadcast { get; } = broadcast;
    public IReadOnlyList<long> MemberUidList { get; } = memberUidList;
    public IReadOnlyList<long>? PrivacyRestrictedUserId { get; } = privacyRestrictedUserId;
}
