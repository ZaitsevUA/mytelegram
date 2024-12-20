﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Refund a <a href="https://corefork.telegram.org/api/stars">Telegram Stars</a> transaction, see <a href="https://corefork.telegram.org/api/payments#6-refunds">here »</a> for more info.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHARGE_ALREADY_REFUNDED The transaction was already refunded.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.refundStarsCharge" />
///</summary>
[TlObject(0x25ae8f4a)]
public sealed class RequestRefundStarsCharge : IRequest<MyTelegram.Schema.IUpdates>
{
    public uint ConstructorId => 0x25ae8f4a;
    ///<summary>
    /// User to refund.
    /// See <a href="https://corefork.telegram.org/type/InputUser" />
    ///</summary>
    public MyTelegram.Schema.IInputUser UserId { get; set; }

    ///<summary>
    /// Transaction ID.
    ///</summary>
    public string ChargeId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(UserId);
        writer.Write(ChargeId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        UserId = reader.Read<MyTelegram.Schema.IInputUser>();
        ChargeId = reader.ReadString();
    }
}
