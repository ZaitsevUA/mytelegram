﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A callback button was pressed, and the button data was sent to the bot that created the button
/// See <a href="https://corefork.telegram.org/constructor/updateBotCallbackQuery" />
///</summary>
[TlObject(0xb9cfc48d)]
public sealed class TUpdateBotCallbackQuery : IUpdate
{
    public uint ConstructorId => 0xb9cfc48d;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Query ID
    ///</summary>
    public long QueryId { get; set; }

    ///<summary>
    /// ID of the user that pressed the button
    ///</summary>
    public long UserId { get; set; }

    ///<summary>
    /// Chat where the inline keyboard was sent
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    public MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// Message ID
    ///</summary>
    public int MsgId { get; set; }

    ///<summary>
    /// Global identifier, uniquely corresponding to the chat to which the message with the callback button was sent. Useful for high scores in games.
    ///</summary>
    public long ChatInstance { get; set; }

    ///<summary>
    /// Callback data
    ///</summary>
    public byte[]? Data { get; set; }

    ///<summary>
    /// Short name of a Game to be returned, serves as the unique identifier for the game
    ///</summary>
    public string? GameShortName { get; set; }

    public void ComputeFlag()
    {
        if (Data != null) { Flags[0] = true; }
        if (GameShortName != null) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(QueryId);
        writer.Write(UserId);
        writer.Write(Peer);
        writer.Write(MsgId);
        writer.Write(ChatInstance);
        if (Flags[0]) { writer.Write(Data); }
        if (Flags[1]) { writer.Write(GameShortName); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        QueryId = reader.ReadInt64();
        UserId = reader.ReadInt64();
        Peer = reader.Read<MyTelegram.Schema.IPeer>();
        MsgId = reader.ReadInt32();
        ChatInstance = reader.ReadInt64();
        if (Flags[0]) { Data = reader.ReadBytes(); }
        if (Flags[1]) { GameShortName = reader.ReadString(); }
    }
}