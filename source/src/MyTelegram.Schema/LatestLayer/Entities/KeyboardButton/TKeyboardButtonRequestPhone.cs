﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Button to request a user's phone number
/// See <a href="https://corefork.telegram.org/constructor/keyboardButtonRequestPhone" />
///</summary>
[TlObject(0xb16a6c29)]
public sealed class TKeyboardButtonRequestPhone : IKeyboardButton
{
    public uint ConstructorId => 0xb16a6c29;
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