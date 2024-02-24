﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Reply to a story.
/// See <a href="https://corefork.telegram.org/constructor/inputReplyToStory" />
///</summary>
[TlObject(0x5881323a)]
public sealed class TInputReplyToStory : IInputReplyTo
{
    public uint ConstructorId => 0x5881323a;
    public MyTelegram.Schema.IInputPeer Peer { get; set; }

    ///<summary>
    /// ID of the story to reply to.
    ///</summary>
    public int StoryId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Peer);
        writer.Write(StoryId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Peer = reader.Read<MyTelegram.Schema.IInputPeer>();
        StoryId = reader.ReadInt32();
    }
}