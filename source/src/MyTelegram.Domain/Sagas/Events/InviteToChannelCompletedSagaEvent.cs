namespace MyTelegram.Domain.Sagas.Events;

public class InviteToChannelCompletedSagaEvent(
    RequestInfo requestInfo,
    long channelId,
    long inviterId,
    bool broadcast,
    IReadOnlyList<long> memberUidList,
    IReadOnlyList<long>? privacyRestrictedUserId,
    bool hasLink
    )
    : RequestAggregateEvent2<InviteToChannelSaga, InviteToChannelSagaId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public long InviterId { get; } = inviterId;
    public bool Broadcast { get; } = broadcast;
    public IReadOnlyList<long> MemberUidList { get; } = memberUidList;
    public IReadOnlyList<long>? PrivacyRestrictedUserId { get; } = privacyRestrictedUserId;
    public bool HasLink { get; } = hasLink;
}
