﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Stats;

///<summary>
/// Contains <a href="https://corefork.telegram.org/api/stats">statistics</a> about a <a href="https://corefork.telegram.org/api/stories">story</a>.
/// See <a href="https://corefork.telegram.org/constructor/stats.storyStats" />
///</summary>
[TlObject(0x50cd067c)]
public sealed class TStoryStats : IStoryStats
{
    public uint ConstructorId => 0x50cd067c;
    ///<summary>
    /// A graph containing the number of story views and shares
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    public MyTelegram.Schema.IStatsGraph ViewsGraph { get; set; }

    ///<summary>
    /// A bar graph containing the number of story reactions categorized by "emotion" (i.e. Positive, Negative, Other, etc...)
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    public MyTelegram.Schema.IStatsGraph ReactionsByEmotionGraph { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(ViewsGraph);
        writer.Write(ReactionsByEmotionGraph);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        ViewsGraph = reader.Read<MyTelegram.Schema.IStatsGraph>();
        ReactionsByEmotionGraph = reader.Read<MyTelegram.Schema.IStatsGraph>();
    }
}