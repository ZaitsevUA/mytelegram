﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Secure <a href="https://corefork.telegram.org/passport">telegram passport</a> values were received
/// See <a href="https://corefork.telegram.org/constructor/messageActionSecureValuesSentMe" />
///</summary>
[TlObject(0x1b287353)]
public sealed class TMessageActionSecureValuesSentMe : IMessageAction
{
    public uint ConstructorId => 0x1b287353;
    ///<summary>
    /// Vector with information about documents and other Telegram Passport elements that were shared with the bot
    ///</summary>
    public TVector<MyTelegram.Schema.ISecureValue> Values { get; set; }

    ///<summary>
    /// Encrypted credentials required to decrypt the data
    /// See <a href="https://corefork.telegram.org/type/SecureCredentialsEncrypted" />
    ///</summary>
    public MyTelegram.Schema.ISecureCredentialsEncrypted Credentials { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Values);
        writer.Write(Credentials);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Values = reader.Read<TVector<MyTelegram.Schema.ISecureValue>>();
        Credentials = reader.Read<MyTelegram.Schema.ISecureCredentialsEncrypted>();
    }
}