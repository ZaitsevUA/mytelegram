﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Prompts the user to select and share one or more peers with the bot using <a href="https://corefork.telegram.org/method/messages.sendBotRequestedPeer">messages.sendBotRequestedPeer</a>
/// See <a href="https://corefork.telegram.org/constructor/keyboardButtonRequestPeer" />
///</summary>
[TlObject(0x53d7bfd8)]
public sealed class TKeyboardButtonRequestPeer : IKeyboardButton
{
    public uint ConstructorId => 0x53d7bfd8;
    ///<summary>
    /// Button text
    ///</summary>
    public string Text { get; set; }

    ///<summary>
    /// Button ID, to be passed to <a href="https://corefork.telegram.org/method/messages.sendBotRequestedPeer">messages.sendBotRequestedPeer</a>.
    ///</summary>
    public int ButtonId { get; set; }

    ///<summary>
    /// Filtering criteria to use for the peer selection list shown to the user. <br>The list should display all existing peers of the specified type, and should also offer an option for the user to create and immediately use one or more (up to <code>max_quantity</code>) peers of the specified type, if needed.
    /// See <a href="https://corefork.telegram.org/type/RequestPeerType" />
    ///</summary>
    public MyTelegram.Schema.IRequestPeerType PeerType { get; set; }

    ///<summary>
    /// Maximum number of peers that can be chosen.
    ///</summary>
    public int MaxQuantity { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Text);
        writer.Write(ButtonId);
        writer.Write(PeerType);
        writer.Write(MaxQuantity);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Text = reader.ReadString();
        ButtonId = reader.ReadInt32();
        PeerType = reader.Read<MyTelegram.Schema.IRequestPeerType>();
        MaxQuantity = reader.ReadInt32();
    }
}