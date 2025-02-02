﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Group created
/// See <a href="https://corefork.telegram.org/constructor/messageActionChatCreate" />
///</summary>
[TlObject(0xbd47cbad)]
public sealed class TMessageActionChatCreate : IMessageAction
{
    public uint ConstructorId => 0xbd47cbad;
    ///<summary>
    /// Group name
    ///</summary>
    public string Title { get; set; }

    ///<summary>
    /// List of group members
    ///</summary>
    public TVector<long> Users { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Title);
        writer.Write(Users);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Title = reader.ReadString();
        Users = reader.Read<TVector<long>>();
    }
}