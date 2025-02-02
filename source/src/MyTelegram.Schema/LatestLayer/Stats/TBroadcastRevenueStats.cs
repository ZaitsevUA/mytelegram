﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Stats;

///<summary>
/// <a href="https://corefork.telegram.org/api/revenue">Channel revenue ad statistics, see here »</a> for more info.Note that all balances and currency amounts and graph values are in the smallest unit of the chosen cryptocurrency (currently nanotons for TONs, so to obtain a value in USD divide the chosen amount by <code>10^9</code>, and then divide by <code>usd_rate</code>).
/// See <a href="https://corefork.telegram.org/constructor/stats.broadcastRevenueStats" />
///</summary>
[TlObject(0x5407e297)]
public sealed class TBroadcastRevenueStats : IBroadcastRevenueStats
{
    public uint ConstructorId => 0x5407e297;
    ///<summary>
    /// Ad impressions graph
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    public MyTelegram.Schema.IStatsGraph TopHoursGraph { get; set; }

    ///<summary>
    /// Ad revenue graph (in the smallest unit of the cryptocurrency in which revenue is calculated)
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    public MyTelegram.Schema.IStatsGraph RevenueGraph { get; set; }

    ///<summary>
    /// Current balance, current withdrawable balance and overall revenue
    /// See <a href="https://corefork.telegram.org/type/BroadcastRevenueBalances" />
    ///</summary>
    public MyTelegram.Schema.IBroadcastRevenueBalances Balances { get; set; }

    ///<summary>
    /// Current conversion rate of the cryptocurrency (<strong>not</strong> in the smallest unit) in which revenue is calculated to USD
    ///</summary>
    public double UsdRate { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(TopHoursGraph);
        writer.Write(RevenueGraph);
        writer.Write(Balances);
        writer.Write(UsdRate);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        TopHoursGraph = reader.Read<MyTelegram.Schema.IStatsGraph>();
        RevenueGraph = reader.Read<MyTelegram.Schema.IStatsGraph>();
        Balances = reader.Read<MyTelegram.Schema.IBroadcastRevenueBalances>();
        UsdRate = reader.ReadDouble();
    }
}