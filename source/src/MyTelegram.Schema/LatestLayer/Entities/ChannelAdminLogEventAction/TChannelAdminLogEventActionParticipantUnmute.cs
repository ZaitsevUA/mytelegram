﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A group call participant was unmuted
/// See <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionParticipantUnmute" />
///</summary>
[TlObject(0xe64429c0)]
public sealed class TChannelAdminLogEventActionParticipantUnmute : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0xe64429c0;
    ///<summary>
    /// The participant that was unmuted
    /// See <a href="https://corefork.telegram.org/type/GroupCallParticipant" />
    ///</summary>
    public MyTelegram.Schema.IGroupCallParticipant Participant { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Participant);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Participant = reader.Read<MyTelegram.Schema.IGroupCallParticipant>();
    }
}