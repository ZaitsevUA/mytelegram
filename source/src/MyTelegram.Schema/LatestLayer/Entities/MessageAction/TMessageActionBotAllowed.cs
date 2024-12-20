﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// We have given the bot permission to send us direct messages.The optional fields specify how did we authorize the bot to send us messages.
/// See <a href="https://corefork.telegram.org/constructor/messageActionBotAllowed" />
///</summary>
[TlObject(0xc516d679)]
public sealed class TMessageActionBotAllowed : IMessageAction
{
    public uint ConstructorId => 0xc516d679;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// We have authorized the bot to send us messages by installing the bot's <a href="https://corefork.telegram.org/api/bots/attach">attachment menu</a>.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool AttachMenu { get; set; }

    ///<summary>
    /// We have allowed the bot to send us messages using <a href="https://corefork.telegram.org/method/bots.allowSendMessage">bots.allowSendMessage »</a>.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool FromRequest { get; set; }

    ///<summary>
    /// We have authorized the bot to send us messages by logging into a website via <a href="https://corefork.telegram.org/widgets/login">Telegram Login »</a>; this field contains the domain name of the website on which the user has logged in.
    ///</summary>
    public string? Domain { get; set; }

    ///<summary>
    /// We have authorized the bot to send us messages by opening the specified <a href="https://corefork.telegram.org/api/bots/webapps">bot mini app</a>.
    /// See <a href="https://corefork.telegram.org/type/BotApp" />
    ///</summary>
    public MyTelegram.Schema.IBotApp? App { get; set; }

    public void ComputeFlag()
    {
        if (AttachMenu) { Flags[1] = true; }
        if (FromRequest) { Flags[3] = true; }
        if (Domain != null) { Flags[0] = true; }
        if (App != null) { Flags[2] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        if (Flags[0]) { writer.Write(Domain); }
        if (Flags[2]) { writer.Write(App); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[1]) { AttachMenu = true; }
        if (Flags[3]) { FromRequest = true; }
        if (Flags[0]) { Domain = reader.ReadString(); }
        if (Flags[2]) { App = reader.Read<MyTelegram.Schema.IBotApp>(); }
    }
}