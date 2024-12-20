﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// Fetch the List of active (or active and hidden) stories, see <a href="https://corefork.telegram.org/api/stories#watching-stories">here »</a> for more info on watching stories.
/// See <a href="https://corefork.telegram.org/method/stories.getAllStories" />
///</summary>
[TlObject(0xeeb0d625)]
public sealed class RequestGetAllStories : IRequest<MyTelegram.Schema.Stories.IAllStories>
{
    public uint ConstructorId => 0xeeb0d625;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// If <code>next</code> and <code>state</code> are both set, uses the passed <code>state</code> to paginate to the next results; if neither <code>state</code> nor <code>next</code> are set, fetches the initial page; if <code>state</code> is set and <code>next</code> is not set, check for changes in the active/hidden peerset, see <a href="https://corefork.telegram.org/api/stories#watching-stories">here »</a> for more info on the full flow.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Next { get; set; }

    ///<summary>
    /// If set, fetches the hidden active story list, otherwise fetches the active story list, see <a href="https://corefork.telegram.org/api/stories#watching-stories">here »</a> for more info on the full flow.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Hidden { get; set; }

    ///<summary>
    /// If <code>next</code> and <code>state</code> are both set, uses the passed <code>state</code> to paginate to the next results; if neither <code>state</code> nor <code>next</code> are set, fetches the initial page; if <code>state</code> is set and <code>next</code> is not set, check for changes in the active/hidden peerset, see <a href="https://corefork.telegram.org/api/stories#watching-stories">here »</a> for more info on the full flow.
    ///</summary>
    public string? State { get; set; }

    public void ComputeFlag()
    {
        if (Next) { Flags[1] = true; }
        if (Hidden) { Flags[2] = true; }
        if (State != null) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        if (Flags[0]) { writer.Write(State); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[1]) { Next = true; }
        if (Flags[2]) { Hidden = true; }
        if (Flags[0]) { State = reader.ReadString(); }
    }
}
