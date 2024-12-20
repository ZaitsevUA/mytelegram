﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Allowed options for the number of giveaway winners.
/// See <a href="https://corefork.telegram.org/constructor/starsGiveawayWinnersOption" />
///</summary>
[TlObject(0x54236209)]
public sealed class TStarsGiveawayWinnersOption : IStarsGiveawayWinnersOption
{
    public uint ConstructorId => 0x54236209;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// If set, this option must be pre-selected by default in the option list.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Default { get; set; }

    ///<summary>
    /// The number of users that will be randomly chosen as winners.
    ///</summary>
    public int Users { get; set; }

    ///<summary>
    /// The number of <a href="https://corefork.telegram.org/api/stars">Telegram Stars</a> each winner will receive.
    ///</summary>
    public long PerUserStars { get; set; }

    public void ComputeFlag()
    {
        if (Default) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Users);
        writer.Write(PerUserStars);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Default = true; }
        Users = reader.ReadInt32();
        PerUserStars = reader.ReadInt64();
    }
}