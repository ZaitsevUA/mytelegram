﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The current account's <a href="https://corefork.telegram.org/api/stars">Telegram Stars balance »</a> has changed.
/// See <a href="https://corefork.telegram.org/constructor/updateStarsBalance" />
///</summary>
[TlObject(0x4e80a379)]
public sealed class TUpdateStarsBalance : IUpdate
{
    public uint ConstructorId => 0x4e80a379;
    ///<summary>
    /// New balance.
    /// See <a href="https://corefork.telegram.org/type/StarsAmount" />
    ///</summary>
    public MyTelegram.Schema.IStarsAmount Balance { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Balance);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Balance = reader.Read<MyTelegram.Schema.IStarsAmount>();
    }
}