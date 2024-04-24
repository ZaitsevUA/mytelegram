namespace MyTelegram.GatewayServer.Services;

public interface IClientDataSender
{
    Task SendAsync(ReadOnlyMemory<byte> data,
        ClientData clientData);

    Task SendAsync(MTProto.UnencryptedMessageResponse data);
    Task SendAsync(MTProto.EncryptedMessageResponse data);
    int EncodeData(MTProto.EncryptedMessageResponse data, ClientData d, byte[] encodedBytes);
    int GetEncodedDataMaxLength(int messageDataLength);
}
