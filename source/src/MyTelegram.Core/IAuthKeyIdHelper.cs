namespace MyTelegram.Core;

public interface IAuthKeyIdHelper
{
    long GetAuthKeyId(byte[] authKey);
}