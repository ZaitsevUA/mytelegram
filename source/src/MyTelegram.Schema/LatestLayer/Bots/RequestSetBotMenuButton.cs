﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Bots;

///<summary>
/// Sets the <a href="https://corefork.telegram.org/api/bots/menu">menu button action »</a> for a given user or for all users
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BUTTON_TEXT_INVALID The specified button text is invalid.
/// 400 BUTTON_URL_INVALID Button URL invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/bots.setBotMenuButton" />
///</summary>
[TlObject(0x4504d54f)]
public sealed class RequestSetBotMenuButton : IRequest<IBool>
{
    public uint ConstructorId => 0x4504d54f;
    ///<summary>
    /// User ID
    /// See <a href="https://corefork.telegram.org/type/InputUser" />
    ///</summary>
    public MyTelegram.Schema.IInputUser UserId { get; set; }

    ///<summary>
    /// Bot menu button action
    /// See <a href="https://corefork.telegram.org/type/BotMenuButton" />
    ///</summary>
    public MyTelegram.Schema.IBotMenuButton Button { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(UserId);
        writer.Write(Button);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        UserId = reader.Read<MyTelegram.Schema.IInputUser>();
        Button = reader.Read<MyTelegram.Schema.IBotMenuButton>();
    }
}
