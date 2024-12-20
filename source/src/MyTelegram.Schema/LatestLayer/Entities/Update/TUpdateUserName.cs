﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Changes the user's first name, last name and username.
/// See <a href="https://corefork.telegram.org/constructor/updateUserName" />
///</summary>
[TlObject(0xa7848924)]
public sealed class TUpdateUserName : IUpdate
{
    public uint ConstructorId => 0xa7848924;
    ///<summary>
    /// User identifier
    ///</summary>
    public long UserId { get; set; }

    ///<summary>
    /// New first name. Corresponds to the new value of <strong>real_first_name</strong> field of the <a href="https://corefork.telegram.org/constructor/userFull">userFull</a> constructor.
    ///</summary>
    public string FirstName { get; set; }

    ///<summary>
    /// New last name. Corresponds to the new value of <strong>real_last_name</strong> field of the <a href="https://corefork.telegram.org/constructor/userFull">userFull</a> constructor.
    ///</summary>
    public string LastName { get; set; }

    ///<summary>
    /// Usernames.
    ///</summary>
    public TVector<MyTelegram.Schema.IUsername> Usernames { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(UserId);
        writer.Write(FirstName);
        writer.Write(LastName);
        writer.Write(Usernames);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        UserId = reader.ReadInt64();
        FirstName = reader.ReadString();
        LastName = reader.ReadString();
        Usernames = reader.Read<TVector<MyTelegram.Schema.IUsername>>();
    }
}