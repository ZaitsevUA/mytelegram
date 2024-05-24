namespace MyTelegram;

public class UserFriendlyException : Exception
{
    public UserFriendlyException()
    {
    }

    public UserFriendlyException(string message, int errorCode = 400) : base(message)
    {
        ErrorCode = errorCode;
    }

    public UserFriendlyException(string message, Exception inner) : base(message, inner)
    {
    }

    public int ErrorCode { get; }
}