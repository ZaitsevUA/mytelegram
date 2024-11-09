using System.Buffers;

namespace MyTelegram.Services.Services;

public class MtpHelper(
    IHashHelper hashHelper,
    IAesHelper aesHelper)
    : IMtpHelper, ITransientDependency
{
    public AesKeyData CalcTempAesKeyData(byte[] newNonce,
        byte[] serverNonce)
    {
        // serverNonce.Length=16 newNonce.Length=32
        // tmp_aes_key := SHA1(new_nonce + server_nonce) + substr (SHA1(server_nonce + new_nonce), 0, 12);
        // tmp_aes_iv := substr (SHA1(server_nonce + new_nonce), 12, 8) + SHA1(new_nonce + new_nonce) + substr (new_nonce, 0, 4);
        // encrypted_answer := AES256_ige_encrypt (answer_with_hash, tmp_aes_key, tmp_aes_iv); here, tmp_aes_key is a 256-bit key, and tmp_aes_iv is a 256-bit initialization vector. The same as in all the other instances that use AES encryption, the encrypted data is padded with random bytes to a length divisible by 16 immediately prior to encryption.

        // https://corefork.telegram.org/mtproto/auth_key
        // newNonce is int256,serverNonce is int128
        var length = newNonce.Length + serverNonce.Length;
        Span<byte> ns = stackalloc byte[length];
        Span<byte> sn = stackalloc byte[length];
        Span<byte> nn = stackalloc byte[newNonce.Length + newNonce.Length];

        newNonce.CopyTo(ns);
        serverNonce.CopyTo(ns[newNonce.Length..]);

        serverNonce.CopyTo(sn);
        newNonce.CopyTo(sn[serverNonce.Length..]);

        newNonce.CopyTo(nn);
        newNonce.CopyTo(nn[newNonce.Length..]);

        var nsHash = hashHelper.Sha1(ns);
        var snHash = hashHelper.Sha1(sn);
        var nnHash = hashHelper.Sha1(nn);

        var tempAesKey = new byte[32];
        var tempAesIv = new byte[32];

        var tempAesKeySpan = tempAesKey.AsSpan();
        var tempAesIvSpan = tempAesIv.AsSpan();
        nsHash.CopyTo(tempAesKeySpan);
        snHash.AsSpan(0, 12).CopyTo(tempAesKeySpan[nsHash.Length..]);

        snHash.AsSpan(12, 8).CopyTo(tempAesIvSpan);
        nnHash.CopyTo(tempAesIvSpan[8..]);
        newNonce.AsSpan(0, 4).CopyTo(tempAesIvSpan[(8 + nnHash.Length)..]);

        return new AesKeyData(tempAesKey, tempAesIv);
    }

    public long ComputeSalt(byte[] newNonce,
        byte[] serverNonce)
    {
        Span<byte> bytes = stackalloc byte[8];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = (byte)(newNonce[i] ^ serverNonce[i]);
        }

        return BitConverter.ToInt64(bytes);
    }

    public void Encrypt(long authKeyId, byte[] authKeyData, ReadOnlySpan<byte> data, Span<byte> outputBuffer)
    {
        throw new NotImplementedException();
    }

    private AesKeyData CalcKey(byte[] authKey,
        //byte[] msgKey,
        ReadOnlySpan<byte> msgKey,
        bool toServer)
    {
        if (msgKey.Length != 16)
        {
            throw new ArgumentException($"MsgKey length:{msgKey.Length} should be 16.");
        }

        var x = toServer ? 0 : 8;
        // Length=52
        Span<byte> aSource = stackalloc byte[msgKey.Length + 36];
        msgKey.CopyTo(aSource);
        authKey.AsSpan(x, 36).CopyTo(aSource[msgKey.Length..]);
        Span<byte> bSource = stackalloc byte[36 + msgKey.Length];
        authKey.AsSpan(40 + x, 36).CopyTo(bSource);
        msgKey.CopyTo(bSource[36..]);
        var sha256A = hashHelper.Sha256(aSource).AsSpan();
        var sha256B = hashHelper.Sha256(bSource).AsSpan();
        var aesKey = new byte[32];
        var aesIv = new byte[32];
        var aesKeySpan = aesKey.AsSpan();
        var aesIvSpan = aesIv.AsSpan();
        sha256A[..8].CopyTo(aesKeySpan);
        sha256B.Slice(8, 16).CopyTo(aesKeySpan[8..]);
        sha256A.Slice(24, 8).CopyTo(aesKeySpan[24..]);

        sha256B[..8].CopyTo(aesIvSpan);
        sha256A.Slice(8, 16).CopyTo(aesIvSpan[8..]);
        sha256B.Slice(24, 8).CopyTo(aesIvSpan[24..]);

        return new AesKeyData(aesKey, aesIv);
    }

    private ReadOnlySpan<byte> CalcMsgKey(byte[] authKey,
        //byte[] data,
        ReadOnlySpan<byte> data,
        bool toServer)
    {
        var x = toServer ? 8 : 0;
        var buffer = ArrayPool<byte>.Shared.Rent(32 + data.Length);
        var span = buffer.AsSpan(0, 32 + data.Length);
        authKey.AsSpan(88 + x, 32).CopyTo(span);
        data.CopyTo(span.Slice(32));

        var msgKeyLarge = hashHelper.Sha256(span);
        ArrayPool<byte>.Shared.Return(buffer);

        return msgKeyLarge.AsSpan(8, 16);
    }

    private long Mod(long n, int m)
    {
        return ((n % m) + m) % m;
    }
}
