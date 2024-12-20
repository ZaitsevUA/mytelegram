﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Button to start a game
/// See <a href="https://corefork.telegram.org/constructor/keyboardButtonGame" />
///</summary>
[TlObject(0x50f41ccf)]
public sealed class TKeyboardButtonGame : IKeyboardButton
{
    public uint ConstructorId => 0x50f41ccf;
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