﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Forwards were enabled or disabled
/// See <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionToggleNoForwards" />
///</summary>
[TlObject(0xcb2ac766)]
public sealed class TChannelAdminLogEventActionToggleNoForwards : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0xcb2ac766;
    ///<summary>
    /// Old value
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    public bool NewValue { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(NewValue);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        NewValue = reader.Read();
    }
}