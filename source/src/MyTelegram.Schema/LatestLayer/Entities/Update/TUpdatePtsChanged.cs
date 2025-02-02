﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/updates">Common message box sequence PTS</a> has changed, <a href="https://corefork.telegram.org/api/updates#fetching-state">state has to be refetched using updates.getState</a>
/// See <a href="https://corefork.telegram.org/constructor/updatePtsChanged" />
///</summary>
[TlObject(0x3354678f)]
public sealed class TUpdatePtsChanged : IUpdate
{
    public uint ConstructorId => 0x3354678f;


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