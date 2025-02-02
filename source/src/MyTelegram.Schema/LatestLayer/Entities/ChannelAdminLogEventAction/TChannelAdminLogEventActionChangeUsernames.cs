﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The list of usernames associated with the channel was changed
/// See <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangeUsernames" />
///</summary>
[TlObject(0xf04fb3a9)]
public sealed class TChannelAdminLogEventActionChangeUsernames : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0xf04fb3a9;
    ///<summary>
    /// Previous set of usernames
    ///</summary>
    public TVector<string> PrevValue { get; set; }

    ///<summary>
    /// New set of usernames
    ///</summary>
    public TVector<string> NewValue { get; set; }

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
        PrevValue = reader.Read<TVector<string>>();
        NewValue = reader.Read<TVector<string>>();
    }
}