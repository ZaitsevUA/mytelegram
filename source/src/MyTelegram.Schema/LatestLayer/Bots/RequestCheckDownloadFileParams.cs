﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Bots;

///<summary>
/// See <a href="https://corefork.telegram.org/method/bots.checkDownloadFileParams" />
///</summary>
[TlObject(0x50077589)]
public sealed class RequestCheckDownloadFileParams : IRequest<IBool>
{
    public uint ConstructorId => 0x50077589;
    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/InputUser" />
    ///</summary>
    public MyTelegram.Schema.IInputUser Bot { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    public string FileName { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    public string Url { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Bot);
        writer.Write(FileName);
        writer.Write(Url);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Bot = reader.Read<MyTelegram.Schema.IInputUser>();
        FileName = reader.ReadString();
        Url = reader.ReadString();
    }
}
