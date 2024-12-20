﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Bots;

///<summary>
/// Check whether the specified bot can send us messages
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// See <a href="https://corefork.telegram.org/method/bots.canSendMessage" />
///</summary>
[TlObject(0x1359f4e6)]
public sealed class RequestCanSendMessage : IRequest<IBool>
{
    public uint ConstructorId => 0x1359f4e6;
    ///<summary>
    /// The bot
    /// See <a href="https://corefork.telegram.org/type/InputUser" />
    ///</summary>
    public MyTelegram.Schema.IInputUser Bot { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Bot);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Bot = reader.Read<MyTelegram.Schema.IInputUser>();
    }
}
