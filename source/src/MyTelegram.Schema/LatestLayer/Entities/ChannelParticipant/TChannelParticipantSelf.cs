﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Myself
/// See <a href="https://corefork.telegram.org/constructor/channelParticipantSelf" />
///</summary>
[TlObject(0x4f607bef)]
public sealed class TChannelParticipantSelf : IChannelParticipant
{
    public uint ConstructorId => 0x4f607bef;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether I joined upon specific approval of an admin
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool ViaRequest { get; set; }

    ///<summary>
    /// User ID
    ///</summary>
    public long UserId { get; set; }

    ///<summary>
    /// User that invited me to the channel/supergroup
    ///</summary>
    public long InviterId { get; set; }

    ///<summary>
    /// When did I join the channel/supergroup
    ///</summary>
    public int Date { get; set; }

    ///<summary>
    /// If set, contains the expiration date of the current <a href="https://corefork.telegram.org/api/stars#star-subscriptions">Telegram Star subscription period »</a> for the specified participant.
    ///</summary>
    public int? SubscriptionUntilDate { get; set; }

    public void ComputeFlag()
    {
        if (ViaRequest) { Flags[0] = true; }
        if (/*SubscriptionUntilDate != 0 && */SubscriptionUntilDate.HasValue) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(UserId);
        writer.Write(InviterId);
        writer.Write(Date);
        if (Flags[1]) { writer.Write(SubscriptionUntilDate.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { ViaRequest = true; }
        UserId = reader.ReadInt64();
        InviterId = reader.ReadInt64();
        Date = reader.ReadInt32();
        if (Flags[1]) { SubscriptionUntilDate = reader.ReadInt32(); }
    }
}