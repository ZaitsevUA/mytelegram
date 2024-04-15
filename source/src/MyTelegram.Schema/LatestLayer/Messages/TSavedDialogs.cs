﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Represents some <a href="https://corefork.telegram.org/api/saved-messages">saved message dialogs »</a>.
/// See <a href="https://corefork.telegram.org/constructor/messages.savedDialogs" />
///</summary>
[TlObject(0xf83ae221)]
public sealed class TSavedDialogs : ISavedDialogs
{
    public uint ConstructorId => 0xf83ae221;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/saved-messages">Saved message dialogs »</a>.
    ///</summary>
    public TVector<MyTelegram.Schema.ISavedDialog> Dialogs { get; set; }

    ///<summary>
    /// List of last messages from each saved dialog
    ///</summary>
    public TVector<MyTelegram.Schema.IMessage> Messages { get; set; }

    ///<summary>
    /// Mentioned chats
    ///</summary>
    public TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Mentioned users
    ///</summary>
    public TVector<MyTelegram.Schema.IUser> Users { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Dialogs);
        writer.Write(Messages);
        writer.Write(Chats);
        writer.Write(Users);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Dialogs = reader.Read<TVector<MyTelegram.Schema.ISavedDialog>>();
        Messages = reader.Read<TVector<MyTelegram.Schema.IMessage>>();
        Chats = reader.Read<TVector<MyTelegram.Schema.IChat>>();
        Users = reader.Read<TVector<MyTelegram.Schema.IUser>>();
    }
}