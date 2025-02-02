﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A user left the channel/supergroup (in the case of big groups, info of the user that has joined isn't shown)
/// See <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionParticipantLeave" />
///</summary>
[TlObject(0xf89777f2)]
public sealed class TChannelAdminLogEventActionParticipantLeave : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0xf89777f2;


    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {

    }
}