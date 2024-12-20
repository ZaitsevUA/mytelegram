﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Payment was not successful, additional verification is needed
/// See <a href="https://corefork.telegram.org/constructor/payments.paymentVerificationNeeded" />
///</summary>
[TlObject(0xd8411139)]
public sealed class TPaymentVerificationNeeded : IPaymentResult
{
    public uint ConstructorId => 0xd8411139;
    ///<summary>
    /// URL for additional payment credentials verification
    ///</summary>
    public string Url { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Url);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Url = reader.ReadString();
    }
}