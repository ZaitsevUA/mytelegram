﻿// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Available <a href="https://corefork.telegram.org/api/gifts">gifts »</a>.
/// See <a href="https://corefork.telegram.org/constructor/payments.StarGifts" />
///</summary>
[JsonDerivedType(typeof(TStarGiftsNotModified), nameof(TStarGiftsNotModified))]
[JsonDerivedType(typeof(TStarGifts), nameof(TStarGifts))]
public interface IStarGifts : IObject
{

}
