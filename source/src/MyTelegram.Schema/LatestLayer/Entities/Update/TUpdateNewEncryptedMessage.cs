﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// New encrypted message.
/// See <a href="https://corefork.telegram.org/constructor/updateNewEncryptedMessage" />
///</summary>
[TlObject(0x12bcbd9a)]
public sealed class TUpdateNewEncryptedMessage : IUpdate
{
    public uint ConstructorId => 0x12bcbd9a;
    ///<summary>
    /// Message
    /// See <a href="https://corefork.telegram.org/type/EncryptedMessage" />
    ///</summary>
    public MyTelegram.Schema.IEncryptedMessage Message { get; set; }

    ///<summary>
    /// New <strong>qts</strong> value, see <a href="https://corefork.telegram.org/api/updates">updates »</a> for more info.
    ///</summary>
    public int Qts { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Message);
        writer.Write(Qts);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Message = reader.Read<MyTelegram.Schema.IEncryptedMessage>();
        Qts = reader.ReadInt32();
    }
}