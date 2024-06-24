namespace MyTelegram.Domain.Aggregates.UserName;

public class UserNameAggregate : SnapshotAggregateRoot<UserNameAggregate, UserNameId, UserNameSnapshot>
{
    private readonly UserNameState _state = new();
    public UserNameAggregate(UserNameId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void Delete()
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UserNameDeletedEvent());
    }

    public void Create(long userId, string userName)
    {
        var lowerUserName = userName.ToLower();
        if (lowerUserName.Length > MyTelegramServerDomainConsts.UsernameMaxLength || lowerUserName.Length < MyTelegramServerDomainConsts.UsernameMinLength)
        {
            RpcErrors.RpcErrors400.UsernameInvalid.ThrowRpcError();
        }

        if (IsNew)
        {
            Emit(new UserNameCreatedEvent(userId, lowerUserName));
        }
        else
        {
            if (_state.IsDeleted)
            {
                Emit(new UserNameCreatedEvent(userId, lowerUserName));
            }
            else
            {
                RpcErrors.RpcErrors400.UsernameOccupied.ThrowRpcError();
            }
        }
    }

    public void SetUserName(RequestInfo requestInfo,
        long selfUserId,
        PeerType peerType,
        long peerId,
        string userName)
    {
        var lowerUserName = userName.ToLower();
        if (lowerUserName.Length > MyTelegramServerDomainConsts.UsernameMaxLength || lowerUserName.Length < MyTelegramServerDomainConsts.UsernameMinLength)
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.UserNameInvalid);
            RpcErrors.RpcErrors400.UsernameInvalid.ThrowRpcError();
        }

        if (IsNew)
        {
            Emit(new SetUserNameSuccessEvent(requestInfo,
                selfUserId,
                lowerUserName,
                peerType,
                peerId));
        }
        else
        {
            if (_state.IsDeleted)
            {
                Emit(new SetUserNameSuccessEvent(requestInfo,
                    selfUserId,
                    lowerUserName,
                    peerType,
                    peerId));
            }
            else
            {
                RpcErrors.RpcErrors400.UsernameOccupied.ThrowRpcError();
            }
        }
    }

    protected override Task<UserNameSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new UserNameSnapshot(_state.UserName, _state.IsDeleted));
    }
    protected override Task LoadSnapshotAsync(UserNameSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }
}
