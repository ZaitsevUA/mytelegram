﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Bots;

///<summary>
/// See <a href="https://corefork.telegram.org/method/bots.updateStarRefProgram" />
///</summary>
[TlObject(0x778b5ab3)]
public sealed class RequestUpdateStarRefProgram : IRequest<MyTelegram.Schema.IStarRefProgram>
{
    public uint ConstructorId => 0x778b5ab3;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/InputUser" />
    ///</summary>
    public MyTelegram.Schema.IInputUser Bot { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    public int CommissionPermille { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    public int? DurationMonths { get; set; }

    public void ComputeFlag()
    {
        if (/*DurationMonths != 0 && */DurationMonths.HasValue) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Bot);
        writer.Write(CommissionPermille);
        if (Flags[0]) { writer.Write(DurationMonths.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Bot = reader.Read<MyTelegram.Schema.IInputUser>();
        CommissionPermille = reader.ReadInt32();
        if (Flags[0]) { DurationMonths = reader.ReadInt32(); }
    }
}
