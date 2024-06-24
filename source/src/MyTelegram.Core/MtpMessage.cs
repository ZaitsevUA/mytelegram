namespace MyTelegram.Core;

public record MtpMessage(
    //byte[] ServerSalt,
    long ServerSalt,
    //ReadOnlyMemory<byte> ServerSalt,
    long SessionId,
    long MessageId,
    int SeqNumber,
    int MessageDataLength,
    ReadOnlyMemory<byte> MessageData);
