﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A user joined the chat via an invite link
/// See <a href="https://corefork.telegram.org/constructor/messageActionChatJoinedByLink" />
///</summary>
[TlObject(0x31224c3)]
public sealed class TMessageActionChatJoinedByLink : IMessageAction
{
    public uint ConstructorId => 0x31224c3;
    ///<summary>
    /// ID of the user that created the invite link
    ///</summary>
    public long InviterId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(InviterId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        InviterId = reader.ReadInt64();
    }
}