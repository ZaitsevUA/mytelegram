﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.getPreparedInlineMessage" />
///</summary>
[TlObject(0x857ebdb8)]
public sealed class RequestGetPreparedInlineMessage : IRequest<MyTelegram.Schema.Messages.IPreparedInlineMessage>
{
    public uint ConstructorId => 0x857ebdb8;
    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/InputUser" />
    ///</summary>
    public MyTelegram.Schema.IInputUser Bot { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    public string Id { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Bot);
        writer.Write(Id);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Bot = reader.Read<MyTelegram.Schema.IInputUser>();
        Id = reader.ReadString();
    }
}
