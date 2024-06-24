namespace MyTelegram.Core;

public interface IAesHelper
{
    //Memory<byte> Ctr128Encrypt(ReadOnlyMemory<byte> span,
    //    ReadOnlySpan<byte> key,
    //    CtrState ctrState);

    byte[] DecryptIge(ReadOnlySpan<byte> encryptedSpan,
        ReadOnlySpan<byte> key,
        ReadOnlySpan<byte> iv);

    byte[] EncryptIge(ReadOnlySpan<byte> plainSpan,
        ReadOnlySpan<byte> key,
        ReadOnlySpan<byte> iv);

    void EncryptIge(ReadOnlySpan<byte> source, Span<byte> destination, byte[] key, byte[] iv);
    void EncryptIge(ReadOnlySpan<byte> source, byte[] destination, byte[] key, byte[] iv);
    void DecryptIge(ReadOnlySpan<byte> source, Span<byte> destination, byte[] key, byte[] iv);
}