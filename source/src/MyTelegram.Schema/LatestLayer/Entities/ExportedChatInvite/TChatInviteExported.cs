﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Exported chat invite
/// See <a href="https://corefork.telegram.org/constructor/chatInviteExported" />
///</summary>
[TlObject(0xa22cbd96)]
public sealed class TChatInviteExported : IExportedChatInvite
{
    public uint ConstructorId => 0xa22cbd96;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether this chat invite was revoked
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Revoked { get; set; }

    ///<summary>
    /// Whether this chat invite has no expiration
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Permanent { get; set; }

    ///<summary>
    /// Whether users importing this invite link will have to be approved to join the channel or group
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool RequestNeeded { get; set; }

    ///<summary>
    /// Chat invitation link
    ///</summary>
    public string Link { get; set; }

    ///<summary>
    /// ID of the admin that created this chat invite
    ///</summary>
    public long AdminId { get; set; }

    ///<summary>
    /// When was this chat invite created
    ///</summary>
    public int Date { get; set; }

    ///<summary>
    /// When was this chat invite last modified
    ///</summary>
    public int? StartDate { get; set; }

    ///<summary>
    /// When does this chat invite expire
    ///</summary>
    public int? ExpireDate { get; set; }

    ///<summary>
    /// Maximum number of users that can join using this link
    ///</summary>
    public int? UsageLimit { get; set; }

    ///<summary>
    /// How many users joined using this link
    ///</summary>
    public int? Usage { get; set; }

    ///<summary>
    /// Number of users that have already used this link to join
    ///</summary>
    public int? Requested { get; set; }

    ///<summary>
    /// For <a href="https://corefork.telegram.org/api/stars#star-subscriptions">Telegram Star subscriptions »</a>, contains the number of chat members which have already joined the chat using the link, but have already left due to expiration of their subscription.
    ///</summary>
    public int? SubscriptionExpired { get; set; }

    ///<summary>
    /// Custom description for the invite link, visible only to admins
    ///</summary>
    public string? Title { get; set; }

    ///<summary>
    /// For <a href="https://corefork.telegram.org/api/stars#star-subscriptions">Telegram Star subscriptions »</a>, contains the pricing of the subscription the user must activate to join the private channel.
    /// See <a href="https://corefork.telegram.org/type/StarsSubscriptionPricing" />
    ///</summary>
    public MyTelegram.Schema.IStarsSubscriptionPricing? SubscriptionPricing { get; set; }

    public void ComputeFlag()
    {
        if (Revoked) { Flags[0] = true; }
        if (Permanent) { Flags[5] = true; }
        if (RequestNeeded) { Flags[6] = true; }
        if (/*StartDate != 0 && */StartDate.HasValue) { Flags[4] = true; }
        if (/*ExpireDate != 0 && */ExpireDate.HasValue) { Flags[1] = true; }
        if (/*UsageLimit != 0 && */UsageLimit.HasValue) { Flags[2] = true; }
        if (/*Usage != 0 && */Usage.HasValue) { Flags[3] = true; }
        if (/*Requested != 0 && */Requested.HasValue) { Flags[7] = true; }
        if (/*SubscriptionExpired != 0 && */SubscriptionExpired.HasValue) { Flags[10] = true; }
        if (Title != null) { Flags[8] = true; }
        if (SubscriptionPricing != null) { Flags[9] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Link);
        writer.Write(AdminId);
        writer.Write(Date);
        if (Flags[4]) { writer.Write(StartDate.Value); }
        if (Flags[1]) { writer.Write(ExpireDate.Value); }
        if (Flags[2]) { writer.Write(UsageLimit.Value); }
        if (Flags[3]) { writer.Write(Usage.Value); }
        if (Flags[7]) { writer.Write(Requested.Value); }
        if (Flags[10]) { writer.Write(SubscriptionExpired.Value); }
        if (Flags[8]) { writer.Write(Title); }
        if (Flags[9]) { writer.Write(SubscriptionPricing); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Revoked = true; }
        if (Flags[5]) { Permanent = true; }
        if (Flags[6]) { RequestNeeded = true; }
        Link = reader.ReadString();
        AdminId = reader.ReadInt64();
        Date = reader.ReadInt32();
        if (Flags[4]) { StartDate = reader.ReadInt32(); }
        if (Flags[1]) { ExpireDate = reader.ReadInt32(); }
        if (Flags[2]) { UsageLimit = reader.ReadInt32(); }
        if (Flags[3]) { Usage = reader.ReadInt32(); }
        if (Flags[7]) { Requested = reader.ReadInt32(); }
        if (Flags[10]) { SubscriptionExpired = reader.ReadInt32(); }
        if (Flags[8]) { Title = reader.ReadString(); }
        if (Flags[9]) { SubscriptionPricing = reader.Read<MyTelegram.Schema.IStarsSubscriptionPricing>(); }
    }
}