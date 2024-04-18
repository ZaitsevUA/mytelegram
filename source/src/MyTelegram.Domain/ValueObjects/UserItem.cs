namespace MyTelegram.Domain.ValueObjects;

public class UserItem(
    long userId,
    long accessHash,
    string phone,
    string firstName,
    string? lastName,
    string? userName)
    : ValueObject
{
    //// required for native aot serialization
    //public UserItem()
    //{

    //}

    //[Newtonsoft.Json.JsonConstructor]
    //[JsonConstructor]
    //,
    //byte[]? profilePhoto
    //ProfilePhoto = profilePhoto;

    public long AccessHash { get; init; } = accessHash;
    public string FirstName { get; init; } = firstName;
    public string? LastName { get; init; } = lastName;

    public string Phone { get; init; } = phone;
    public byte[]? ProfilePhoto { get; init; }

    public long UserId { get; init; } = userId;
    public string? UserName { get; init; } = userName;
}