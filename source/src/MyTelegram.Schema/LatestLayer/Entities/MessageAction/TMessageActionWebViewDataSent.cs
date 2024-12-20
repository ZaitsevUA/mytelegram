﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Data from an opened <a href="https://corefork.telegram.org/api/bots/webapps">reply keyboard bot mini app</a> was relayed to the bot that owns it (user side service message).Clients should display a service message with the text <code>Data from the «$text» button was transferred to the bot.</code>
/// See <a href="https://corefork.telegram.org/constructor/messageActionWebViewDataSent" />
///</summary>
[TlObject(0xb4c38cb5)]
public sealed class TMessageActionWebViewDataSent : IMessageAction
{
    public uint ConstructorId => 0xb4c38cb5;
    ///<summary>
    /// Text of the <a href="https://corefork.telegram.org/constructor/keyboardButtonSimpleWebView">keyboardButtonSimpleWebView</a> that was pressed to open the web app.
    ///</summary>
    public string Text { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Text);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Text = reader.ReadString();
    }
}