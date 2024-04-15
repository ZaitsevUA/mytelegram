﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Returns sparse positions of messages of the specified type in the chat to be used for shared media scroll implementation.Returns the results in reverse chronological order (i.e., in order of decreasing message_id).
/// See <a href="https://corefork.telegram.org/method/messages.getSearchResultsPositions" />
///</summary>
[TlObject(0x9c7f2f10)]
public sealed class RequestGetSearchResultsPositions : IRequest<MyTelegram.Schema.Messages.ISearchResultsPositions>
{
    public uint ConstructorId => 0x9c7f2f10;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Peer where to search
    /// See <a href="https://corefork.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }

    ///<summary>
    /// Search within the <a href="https://corefork.telegram.org/api/saved-messages">saved message dialog »</a> with this ID.
    /// See <a href="https://corefork.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer? SavedPeerId { get; set; }

    ///<summary>
    /// Message filter, <a href="https://corefork.telegram.org/constructor/inputMessagesFilterEmpty">inputMessagesFilterEmpty</a>, <a href="https://corefork.telegram.org/constructor/inputMessagesFilterMyMentions">inputMessagesFilterMyMentions</a> filters are not supported by this method.
    /// See <a href="https://corefork.telegram.org/type/MessagesFilter" />
    ///</summary>
    public MyTelegram.Schema.IMessagesFilter Filter { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/offsets">Offsets for pagination, for more info click here</a>
    ///</summary>
    public int OffsetId { get; set; }

    ///<summary>
    /// Maximum number of results to return, <a href="https://corefork.telegram.org/api/offsets">see pagination</a>
    ///</summary>
    public int Limit { get; set; }

    public void ComputeFlag()
    {
        if (SavedPeerId != null) { Flags[2] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Peer);
        if (Flags[2]) { writer.Write(SavedPeerId); }
        writer.Write(Filter);
        writer.Write(OffsetId);
        writer.Write(Limit);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Peer = reader.Read<MyTelegram.Schema.IInputPeer>();
        if (Flags[2]) { SavedPeerId = reader.Read<MyTelegram.Schema.IInputPeer>(); }
        Filter = reader.Read<MyTelegram.Schema.IMessagesFilter>();
        OffsetId = reader.ReadInt32();
        Limit = reader.ReadInt32();
    }
}
