﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates we <a href="https://corefork.telegram.org/api/stories#reactions">reacted to a story »</a>.
/// See <a href="https://corefork.telegram.org/constructor/updateSentStoryReaction" />
///</summary>
[TlObject(0x7d627683)]
public sealed class TUpdateSentStoryReaction : IUpdate
{
    public uint ConstructorId => 0x7d627683;
    ///<summary>
    /// The peer that sent the story
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    public MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// ID of the story we reacted to
    ///</summary>
    public int StoryId { get; set; }

    ///<summary>
    /// The reaction that was sent
    /// See <a href="https://corefork.telegram.org/type/Reaction" />
    ///</summary>
    public MyTelegram.Schema.IReaction Reaction { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Peer);
        writer.Write(StoryId);
        writer.Write(Reaction);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Peer = reader.Read<MyTelegram.Schema.IPeer>();
        StoryId = reader.ReadInt32();
        Reaction = reader.Read<MyTelegram.Schema.IReaction>();
    }
}