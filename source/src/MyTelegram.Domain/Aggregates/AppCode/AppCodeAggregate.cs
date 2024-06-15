namespace MyTelegram.Domain.Aggregates.AppCode;

public class AppCodeAggregate : SnapshotAggregateRoot<AppCodeAggregate, AppCodeId, AppCodeSnapshot>
{
    private readonly int _expireMinutes = 10;
    private readonly int _maxAllowedSendCountPerDay = 100;
    private readonly int _maxFailedCount = 5;
    private readonly AppCodeState _state = new();

    public AppCodeAggregate(AppCodeId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void CancelCode(RequestInfo requestInfo,
        string phoneNumber,
        string phoneCodeHash)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new AppCodeCanceledEvent(requestInfo, phoneNumber, phoneCodeHash));
    }

    public void CheckCode(RequestInfo requestInfo, string code)
    {
        var isValidCode = CheckCodeCore(code, _maxFailedCount);
        Emit(new CheckAppCodeCompletedEvent(requestInfo, _state.Email, isValidCode));
    }

    public void CheckPasswordConfirmEmailCode(RequestInfo requestInfo, string code)
    {
        var isValidCode = CheckCodeCore(code, _maxFailedCount + 1);
        Emit(new CheckPasswordConfirmEmailCodeCompletedEvent(requestInfo, _state.Email, isValidCode));
    }

    public void CheckRecoverPasswordCode(RequestInfo requestInfo, string code)
    {
        var isValidCode = CheckCodeCore(code, _maxFailedCount + 1);
        Emit(new CheckRecoverPasswordCodeCompletedEvent(requestInfo, _state.Email, isValidCode));
    }

    public void CheckSignInCode(RequestInfo requestInfo,
        string code,
        long userId)
    {
        var isCodeValid = CheckCodeCore(code,
            _maxFailedCount,
            RpcErrors.RpcErrors400.PhoneCodeInvalid,
            RpcErrors.RpcErrors400.PhoneCodeExpired);

        Emit(new CheckSignInCodeCompletedEvent(requestInfo,
            isCodeValid,
            userId));
    }

    public void CheckSignUpCode(RequestInfo requestInfo,
        long userId,
        string phoneCodeHash,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        Emit(new CheckSignUpCodeCompletedEvent(requestInfo,
            true,
            userId,
            accessHash,
            phoneNumber,
            firstName,
            lastName
        ));
    }

    public void Create(RequestInfo requestInfo,
        long userId,
        string phoneNumber,
        string code,
        string phoneCodeHash,
        long creationTime)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        var expire = GetExpire();

        Emit(new AppCodeCreatedEvent(requestInfo,
            userId,
            phoneNumber,
            code,
            expire,
            phoneCodeHash,
            creationTime));
    }

    public void CreateEmailCode(RequestInfo requestInfo, long userId, string email, string code)
    {
        if (_state.LastEmailCodeSendDate.Date == DateTime.UtcNow.Date)
        {
            var sentCount = _state.TodaySentCount;
            if (sentCount >= _maxAllowedSendCountPerDay)
            {
                RpcErrors.RpcErrors400.PhoneNumberFlood.ThrowRpcError();
            }
        }

        var expire = GetExpire();
        Emit(new EmailCodeCreatedEvent(requestInfo, userId, email, code, expire));
    }

    public void ResetEmailCode(RequestInfo requestInfo, string code)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (string.IsNullOrEmpty(_state.Email))
        {
            RpcErrors.RpcErrors400.EmailHashExpired.ThrowRpcError();
        }

        if (_state.TodaySentCount > _maxAllowedSendCountPerDay)
        {
            RpcErrors.RpcErrors400.PhoneNumberFlood.ThrowRpcError();
        }

        var expire = DateTime.UtcNow.AddMinutes(_expireMinutes).ToTimestamp();
        Emit(new EmailCodeCreatedEvent(requestInfo, requestInfo.UserId, _state.Email!, code, expire));
    }

    protected override Task<AppCodeSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new AppCodeSnapshot(_state.Expire, _state.FailedCount, _state.PhoneCodeHash, _state.Code,
            _state.Email, _state.LastSmsCodeSendDate, _state.LastEmailCodeSendDate, _state.TotalSentCount,
            _state.TodaySentCount));
    }

    protected override Task LoadSnapshotAsync(AppCodeSnapshot snapshot, ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);

        return Task.CompletedTask;
    }

    private bool CheckCodeCore(string code, int maxFailedCount,
                RpcError? invalidCodeError = null,
        RpcError? codeExpiredError = null
    )
    {
        if (code.IsNullOrEmpty())
        {
            RpcErrors.RpcErrors400.PhoneCodeEmpty.ThrowRpcError();
        }

        if (_state.FailedCount > maxFailedCount)
        {
            //(invalidCodeError ?? RpcErrors.RpcErrors400.PhoneCodeInvalid).ThrowRpcError();
            (invalidCodeError ?? RpcErrors.RpcErrors400.CodeInvalid).ThrowRpcError();
        }

        var now = DateTime.UtcNow.ToTimestamp();
        if (now > _state.Expire || _state.Canceled)
        {
            //(codeExpiredError ?? RpcErrors.RpcErrors400.PhoneCodeExpired).ThrowRpcError();
            (codeExpiredError ?? RpcErrors.RpcErrors400.PasswordRecoveryExpired).ThrowRpcError();
        }

        // The validation failed count should be saved,so not throw exception
        return string.Equals(_state.Code, code, StringComparison.OrdinalIgnoreCase);
    }

    private int GetExpire()
    {
        return DateTime.UtcNow.AddMinutes(_expireMinutes).ToTimestamp();
    }
}