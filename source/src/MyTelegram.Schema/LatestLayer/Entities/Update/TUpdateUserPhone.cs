﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A user's phone number was changed
/// See <a href="https://corefork.telegram.org/constructor/updateUserPhone" />
///</summary>
[TlObject(0x5492a13)]
public sealed class TUpdateUserPhone : IUpdate
{
    public uint ConstructorId => 0x5492a13;
    ///<summary>
    /// User ID
    ///</summary>
    public long UserId { get; set; }

    ///<summary>
    /// New phone number
    ///</summary>
    public string Phone { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(UserId);
        writer.Write(Phone);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        UserId = reader.ReadInt64();
        Phone = reader.ReadString();
    }
}