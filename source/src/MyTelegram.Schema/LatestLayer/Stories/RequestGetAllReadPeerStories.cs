﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// Obtain the latest read story ID for all peers when first logging in, returned as a list of <a href="https://corefork.telegram.org/constructor/updateReadStories">updateReadStories</a> updates, see <a href="https://corefork.telegram.org/api/stories#watching-stories">here »</a> for more info.
/// See <a href="https://corefork.telegram.org/method/stories.getAllReadPeerStories" />
///</summary>
[TlObject(0x9b5ae7f9)]
public sealed class RequestGetAllReadPeerStories : IRequest<MyTelegram.Schema.IUpdates>
{
    public uint ConstructorId => 0x9b5ae7f9;

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
