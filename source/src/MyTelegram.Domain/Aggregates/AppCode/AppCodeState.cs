namespace MyTelegram.Domain.Aggregates.AppCode;

public class AppCodeState : AggregateState<AppCodeAggregate, AppCodeId, AppCodeState>,
    IApply<AppCodeCreatedEvent>,
    IApply<AppCodeCanceledEvent>,
    IApply<SignUpRequiredEvent>,
    IApply<AppCodeCheckFailedEvent>,
    IApply<CheckSignUpCodeCompletedEvent>,
    IApply<CheckSignInCodeCompletedEvent>,
    IApply<EmailCodeCreatedEvent>,
    IApply<CheckAppCodeCompletedEvent>,
    IApply<CheckPasswordConfirmEmailCodeCompletedEvent>,
    IApply<CheckRecoverPasswordCodeCompletedEvent>

{
    public bool Canceled { get; private set; }
    public string Code { get; private set; } = default!;
    public string? Email { get; private set; }
    public int Expire { get; private set; }
    public int FailedCount { get; private set; }
    public DateTime LastEmailCodeSendDate { get; private set; }
    public DateTime LastSmsCodeSendDate { get; private set; }
    public string PhoneCodeHash { get; private set; } = default!;
    public int TodaySentCount { get; private set; }
    public int TotalSentCount { get; private set; }
    public void Apply(AppCodeCanceledEvent aggregateEvent)
    {
        Canceled = true;
    }

    public void Apply(AppCodeCheckFailedEvent aggregateEvent)
    {
        FailedCount++;
    }

    public void Apply(AppCodeCreatedEvent aggregateEvent)
    {
        PhoneCodeHash = aggregateEvent.PhoneCodeHash;
        Code = aggregateEvent.Code;
        Expire = aggregateEvent.Expire;
        FailedCount = 0;
    }

    public void Apply(CheckSignInCodeCompletedEvent aggregateEvent)
    {
        if (!aggregateEvent.IsCodeValid)
        {
            FailedCount++;
        }
    }

    public void Apply(CheckSignUpCodeCompletedEvent aggregateEvent)
    {
        if (!aggregateEvent.IsCodeValid)
        {
            FailedCount++;
        }
    }

    public void Apply(SignUpRequiredEvent aggregateEvent)
    {
    }

    public void Apply(EmailCodeCreatedEvent aggregateEvent)
    {
        Code = aggregateEvent.Code;
        Email = aggregateEvent.Email;
        Expire = aggregateEvent.Expire;
        LastEmailCodeSendDate = DateTime.UtcNow;
        FailedCount = 0;
        if (LastEmailCodeSendDate.Date == DateTime.UtcNow.Date)
        {
            TodaySentCount++;
        }
        else
        {
            TodaySentCount = 1;
            TotalSentCount++;
        }
    }

    public void Apply(CheckAppCodeCompletedEvent aggregateEvent)
    {
        if (!aggregateEvent.IsValidCode)
        {
            FailedCount++;
        }
    }

    public void Apply(CheckPasswordConfirmEmailCodeCompletedEvent aggregateEvent)
    {
        if (!aggregateEvent.IsValidCode)
        {
            FailedCount++;
        }
    }

    public void Apply(CheckRecoverPasswordCodeCompletedEvent aggregateEvent)
    {
        if (!aggregateEvent.IsValidCode)
        {
            FailedCount++;
        }
    }

    public void LoadSnapshot(AppCodeSnapshot snapshot)
    {
        Expire= snapshot.Expire;
        FailedCount= snapshot.FailedCount;
        PhoneCodeHash= snapshot.PhoneCodeHash;
        Code = snapshot.Code;
        Email= snapshot.Email;
        LastSmsCodeSendDate= snapshot.LastSmsCodeSendDate;
        LastEmailCodeSendDate= snapshot.LastEmailCodeSendDate;
        TotalSentCount= snapshot.TotalSentCount;
        TodaySentCount= snapshot.TodaySentCount;
    }
}
