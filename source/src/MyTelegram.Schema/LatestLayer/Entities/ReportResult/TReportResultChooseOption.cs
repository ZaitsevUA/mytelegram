﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The user must choose one of the following options, and then <a href="https://corefork.telegram.org/method/messages.report">messages.report</a> must be re-invoked, passing the option's <code>option</code> identifier to <a href="https://corefork.telegram.org/method/messages.report">messages.report</a>.<code>option</code>.
/// See <a href="https://corefork.telegram.org/constructor/reportResultChooseOption" />
///</summary>
[TlObject(0xf0e4e0b6)]
public sealed class TReportResultChooseOption : IReportResult
{
    public uint ConstructorId => 0xf0e4e0b6;
    ///<summary>
    /// Title of the option popup
    ///</summary>
    public string Title { get; set; }

    ///<summary>
    /// Available options, rendered as menu entries.
    ///</summary>
    public TVector<MyTelegram.Schema.IMessageReportOption> Options { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Title);
        writer.Write(Options);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Title = reader.ReadString();
        Options = reader.Read<TVector<MyTelegram.Schema.IMessageReportOption>>();
    }
}