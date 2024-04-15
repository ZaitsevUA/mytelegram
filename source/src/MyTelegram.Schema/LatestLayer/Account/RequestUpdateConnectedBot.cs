﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.updateConnectedBot" />
///</summary>
[TlObject(0x43d8521d)]
public sealed class RequestUpdateConnectedBot : IRequest<MyTelegram.Schema.IUpdates>
{
    public uint ConstructorId => 0x43d8521d;
    public BitArray Flags { get; set; } = new BitArray(32);
    public bool CanReply { get; set; }
    public bool Deleted { get; set; }
    public MyTelegram.Schema.IInputUser Bot { get; set; }
    public MyTelegram.Schema.IInputBusinessBotRecipients Recipients { get; set; }

    public void ComputeFlag()
    {
        if (CanReply) { Flags[0] = true; }
        if (Deleted) { Flags[1] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Bot);
        writer.Write(Recipients);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { CanReply = true; }
        if (Flags[1]) { Deleted = true; }
        Bot = reader.Read<MyTelegram.Schema.IInputUser>();
        Recipients = reader.Read<MyTelegram.Schema.IInputBusinessBotRecipients>();
    }
}
