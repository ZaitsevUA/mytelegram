﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Bots;

///<summary>
/// Reorder usernames associated to a bot we own.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// 400 USERNAME_NOT_MODIFIED The username was not modified.
/// See <a href="https://corefork.telegram.org/method/bots.reorderUsernames" />
///</summary>
[TlObject(0x9709b1c2)]
public sealed class RequestReorderUsernames : IRequest<IBool>
{
    public uint ConstructorId => 0x9709b1c2;
    ///<summary>
    /// The bot
    /// See <a href="https://corefork.telegram.org/type/InputUser" />
    ///</summary>
    public MyTelegram.Schema.IInputUser Bot { get; set; }

    ///<summary>
    /// The new order for active usernames. All active usernames must be specified.
    ///</summary>
    public TVector<string> Order { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Bot);
        writer.Write(Order);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Bot = reader.Read<MyTelegram.Schema.IInputUser>();
        Order = reader.Read<TVector<string>>();
    }
}
