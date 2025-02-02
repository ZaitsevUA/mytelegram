﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A <a href="https://corefork.telegram.org/api/saved-messages">saved message dialog</a> was pinned/unpinned
/// See <a href="https://corefork.telegram.org/constructor/updateSavedDialogPinned" />
///</summary>
[TlObject(0xaeaf9e74)]
public sealed class TUpdateSavedDialogPinned : IUpdate
{
    public uint ConstructorId => 0xaeaf9e74;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether the dialog was pinned
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Pinned { get; set; }

    ///<summary>
    /// The dialog
    /// See <a href="https://corefork.telegram.org/type/DialogPeer" />
    ///</summary>
    public MyTelegram.Schema.IDialogPeer Peer { get; set; }

    public void ComputeFlag()
    {
        if (Pinned) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Peer);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Pinned = true; }
        Peer = reader.Read<MyTelegram.Schema.IDialogPeer>();
    }
}