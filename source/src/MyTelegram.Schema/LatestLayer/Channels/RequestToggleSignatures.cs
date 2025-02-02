﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Channels;

///<summary>
/// Enable/disable message signatures in channels
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// See <a href="https://corefork.telegram.org/method/channels.toggleSignatures" />
///</summary>
[TlObject(0x418d549c)]
public sealed class RequestToggleSignatures : IRequest<MyTelegram.Schema.IUpdates>
{
    public uint ConstructorId => 0x418d549c;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// If set, enables message signatures.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool SignaturesEnabled { get; set; }

    ///<summary>
    /// If set, messages from channel admins will link to their profiles, just like for group messages: can only be set if the <code>signatures_enabled</code> flag is set.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool ProfilesEnabled { get; set; }

    ///<summary>
    /// Channel
    /// See <a href="https://corefork.telegram.org/type/InputChannel" />
    ///</summary>
    public MyTelegram.Schema.IInputChannel Channel { get; set; }

    public void ComputeFlag()
    {
        if (SignaturesEnabled) { Flags[0] = true; }
        if (ProfilesEnabled) { Flags[1] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Channel);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { SignaturesEnabled = true; }
        if (Flags[1]) { ProfilesEnabled = true; }
        Channel = reader.Read<MyTelegram.Schema.IInputChannel>();
    }
}
