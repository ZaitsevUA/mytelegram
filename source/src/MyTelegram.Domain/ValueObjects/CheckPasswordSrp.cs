namespace MyTelegram.Domain.ValueObjects;

public class CheckPasswordSrp(
    long srpId,
    byte[] a,
    byte[] m1) : ValueObject
{
    public byte[] A { get; init; } = a;
    public byte[] M1 { get; init; } = m1;

    public long SrpId { get; init; } = srpId;
}
