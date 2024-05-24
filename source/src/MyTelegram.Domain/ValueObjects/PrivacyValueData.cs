namespace MyTelegram.Domain.ValueObjects;

public class PrivacyValueData(
    PrivacyValueType privacyValueType,
    string? jsonData = null)
    : ValueObject
{
    public string? JsonData { get; init; } = jsonData;

    public PrivacyValueType PrivacyValueType { get; init; } = privacyValueType;
}
