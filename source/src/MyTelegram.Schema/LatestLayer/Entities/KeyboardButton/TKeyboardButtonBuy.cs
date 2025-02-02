﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Button to buy a product
/// See <a href="https://corefork.telegram.org/constructor/keyboardButtonBuy" />
///</summary>
[TlObject(0xafd93fbb)]
public sealed class TKeyboardButtonBuy : IKeyboardButton
{
    public uint ConstructorId => 0xafd93fbb;
    ///<summary>
    /// Button text
    ///</summary>
    public string Text { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Text);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Text = reader.ReadString();
    }
}