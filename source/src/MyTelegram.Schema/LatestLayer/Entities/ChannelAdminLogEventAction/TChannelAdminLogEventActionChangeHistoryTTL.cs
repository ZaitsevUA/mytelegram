﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The Time-To-Live of messages in this chat was changed
/// See <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangeHistoryTTL" />
///</summary>
[TlObject(0x6e941a38)]
public sealed class TChannelAdminLogEventActionChangeHistoryTTL : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0x6e941a38;
    ///<summary>
    /// Previous value
    ///</summary>
    public int PrevValue { get; set; }

    ///<summary>
    /// New value
    ///</summary>
    public int NewValue { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(PrevValue);
        writer.Write(NewValue);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        PrevValue = reader.ReadInt32();
        NewValue = reader.ReadInt32();
    }
}