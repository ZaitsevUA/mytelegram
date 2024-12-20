﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A new message was added to a <a href="https://corefork.telegram.org/api/business#quick-reply-shortcuts">quick reply shortcut »</a>.
/// See <a href="https://corefork.telegram.org/constructor/updateQuickReplyMessage" />
///</summary>
[TlObject(0x3e050d0f)]
public sealed class TUpdateQuickReplyMessage : IUpdate
{
    public uint ConstructorId => 0x3e050d0f;
    ///<summary>
    /// The message that was added (the <a href="https://corefork.telegram.org/constructor/message">message</a>.<code>quick_reply_shortcut_id</code> field will contain the shortcut ID).
    /// See <a href="https://corefork.telegram.org/type/Message" />
    ///</summary>
    public MyTelegram.Schema.IMessage Message { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Message);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Message = reader.Read<MyTelegram.Schema.IMessage>();
    }
}