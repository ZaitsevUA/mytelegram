﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/wallpapers">Wallpaper</a> rendering information.
/// See <a href="https://corefork.telegram.org/constructor/wallPaperSettings" />
///</summary>
[TlObject(0x372efcd0)]
public sealed class TWallPaperSettings : IWallPaperSettings
{
    public uint ConstructorId => 0x372efcd0;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// For <a href="https://corefork.telegram.org/api/wallpapers#image-wallpapers">image wallpapers »</a>: if set, the JPEG must be downscaled to fit in 450x450 square and then box-blurred with radius 12.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Blur { get; set; }

    ///<summary>
    /// If set, the background needs to be slightly moved when the device is rotated.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Motion { get; set; }

    ///<summary>
    /// Used for <a href="https://corefork.telegram.org/api/wallpapers#solid-fill">solid »</a>, <a href="https://corefork.telegram.org/api/wallpapers#gradient-fill">gradient »</a> and <a href="https://corefork.telegram.org/api/wallpapers#freeform-gradient-fill">freeform gradient »</a> fills.
    ///</summary>
    public int? BackgroundColor { get; set; }

    ///<summary>
    /// Used for <a href="https://corefork.telegram.org/api/wallpapers#gradient-fill">gradient »</a> and <a href="https://corefork.telegram.org/api/wallpapers#freeform-gradient-fill">freeform gradient »</a> fills.
    ///</summary>
    public int? SecondBackgroundColor { get; set; }

    ///<summary>
    /// Used for <a href="https://corefork.telegram.org/api/wallpapers#freeform-gradient-fill">freeform gradient »</a> fills.
    ///</summary>
    public int? ThirdBackgroundColor { get; set; }

    ///<summary>
    /// Used for <a href="https://corefork.telegram.org/api/wallpapers#freeform-gradient-fill">freeform gradient »</a> fills.
    ///</summary>
    public int? FourthBackgroundColor { get; set; }

    ///<summary>
    /// Used for <a href="https://corefork.telegram.org/api/wallpapers#pattern-wallpapers">pattern wallpapers »</a>.
    ///</summary>
    public int? Intensity { get; set; }

    ///<summary>
    /// Clockwise rotation angle of the gradient, in degrees; 0-359. Should be always divisible by 45.
    ///</summary>
    public int? Rotation { get; set; }

    ///<summary>
    /// If set, this wallpaper can be used as a channel wallpaper and is represented by the specified UTF-8 emoji.
    ///</summary>
    public string? Emoticon { get; set; }

    public void ComputeFlag()
    {
        if (Blur) { Flags[1] = true; }
        if (Motion) { Flags[2] = true; }
        if (/*BackgroundColor != 0 && */BackgroundColor.HasValue) { Flags[0] = true; }
        if (/*SecondBackgroundColor != 0 && */SecondBackgroundColor.HasValue) { Flags[4] = true; }
        if (/*ThirdBackgroundColor != 0 && */ThirdBackgroundColor.HasValue) { Flags[5] = true; }
        if (/*FourthBackgroundColor != 0 && */FourthBackgroundColor.HasValue) { Flags[6] = true; }
        if (/*Intensity != 0 && */Intensity.HasValue) { Flags[3] = true; }
        if (/*Rotation != 0 && */Rotation.HasValue) { Flags[4] = true; }
        if (Emoticon != null) { Flags[7] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        if (Flags[0]) { writer.Write(BackgroundColor.Value); }
        if (Flags[4]) { writer.Write(SecondBackgroundColor.Value); }
        if (Flags[5]) { writer.Write(ThirdBackgroundColor.Value); }
        if (Flags[6]) { writer.Write(FourthBackgroundColor.Value); }
        if (Flags[3]) { writer.Write(Intensity.Value); }
        if (Flags[4]) { writer.Write(Rotation.Value); }
        if (Flags[7]) { writer.Write(Emoticon); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[1]) { Blur = true; }
        if (Flags[2]) { Motion = true; }
        if (Flags[0]) { BackgroundColor = reader.ReadInt32(); }
        if (Flags[4]) { SecondBackgroundColor = reader.ReadInt32(); }
        if (Flags[5]) { ThirdBackgroundColor = reader.ReadInt32(); }
        if (Flags[6]) { FourthBackgroundColor = reader.ReadInt32(); }
        if (Flags[3]) { Intensity = reader.ReadInt32(); }
        if (Flags[4]) { Rotation = reader.ReadInt32(); }
        if (Flags[7]) { Emoticon = reader.ReadString(); }
    }
}