namespace MyTelegram.Core;

public interface IMtpHelper
{
    AesKeyData CalcTempAesKeyData(byte[] newNonce,
        byte[] serverNonce);

    long ComputeSalt(byte[] newNonce,
        byte[] serverNonce);
}