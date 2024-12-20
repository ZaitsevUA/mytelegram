﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Official clients only, invoke with Google Play Integrity token.
/// See <a href="https://corefork.telegram.org/method/invokeWithGooglePlayIntegrity" />
///</summary>
[TlObject(0x1df92984)]
public sealed class RequestInvokeWithGooglePlayIntegrity : IRequest<IObject>, IHasSubQuery
{
    public uint ConstructorId => 0x1df92984;
    ///<summary>
    /// Nonce.
    ///</summary>
    public string Nonce { get; set; }

    ///<summary>
    /// Token.
    ///</summary>
    public string Token { get; set; }

    ///<summary>
    /// Query.
    ///</summary>
    public IObject Query { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Nonce);
        writer.Write(Token);
        writer.Write(Query);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Nonce = reader.ReadString();
        Token = reader.ReadString();
        Query = reader.Read<IObject>();
    }
}
