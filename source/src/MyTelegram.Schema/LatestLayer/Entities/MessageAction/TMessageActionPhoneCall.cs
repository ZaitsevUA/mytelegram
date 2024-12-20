﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A phone call
/// See <a href="https://corefork.telegram.org/constructor/messageActionPhoneCall" />
///</summary>
[TlObject(0x80e11a7f)]
public sealed class TMessageActionPhoneCall : IMessageAction
{
    public uint ConstructorId => 0x80e11a7f;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Is this a video call?
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Video { get; set; }

    ///<summary>
    /// Call ID
    ///</summary>
    public long CallId { get; set; }

    ///<summary>
    /// If the call has ended, the reason why it ended
    /// See <a href="https://corefork.telegram.org/type/PhoneCallDiscardReason" />
    ///</summary>
    public MyTelegram.Schema.IPhoneCallDiscardReason? Reason { get; set; }

    ///<summary>
    /// Duration of the call in seconds
    ///</summary>
    public int? Duration { get; set; }

    public void ComputeFlag()
    {
        if (Video) { Flags[2] = true; }
        if (Reason != null) { Flags[0] = true; }
        if (/*Duration != 0 && */Duration.HasValue) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(CallId);
        if (Flags[0]) { writer.Write(Reason); }
        if (Flags[1]) { writer.Write(Duration.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[2]) { Video = true; }
        CallId = reader.ReadInt64();
        if (Flags[0]) { Reason = reader.Read<MyTelegram.Schema.IPhoneCallDiscardReason>(); }
        if (Flags[1]) { Duration = reader.ReadInt32(); }
    }
}