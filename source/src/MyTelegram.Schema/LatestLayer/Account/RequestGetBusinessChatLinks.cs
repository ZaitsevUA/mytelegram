﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.getBusinessChatLinks" />
///</summary>
[TlObject(0x6f70dde1)]
public sealed class RequestGetBusinessChatLinks : IRequest<MyTelegram.Schema.Account.IBusinessChatLinks>
{
    public uint ConstructorId => 0x6f70dde1;

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {

    }
}
