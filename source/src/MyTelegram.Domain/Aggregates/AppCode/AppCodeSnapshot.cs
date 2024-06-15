namespace MyTelegram.Domain.Aggregates.AppCode;

public record AppCodeSnapshot(
    int Expire,
    int FailedCount,
    string PhoneCodeHash,
    string Code,
    string? Email,
    DateTime LastSmsCodeSendDate,
    DateTime LastEmailCodeSendDate,
    int TotalSentCount,
    int TodaySentCount)
    : ISnapshot;