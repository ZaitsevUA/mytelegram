namespace MyTelegram.Domain.ValueObjects;

public class BotCommand(
    string command,
    string description) : ValueObject
{
    public string Command { get; init; } = command;
    public string Description { get; init; } = description;
}
