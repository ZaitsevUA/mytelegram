﻿// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Contains a URL leading to a page where the user will be able to place ads for the channel/bot, paying using <a href="https://corefork.telegram.org/api/stars#paying-for-ads">Telegram Stars</a>.
/// See <a href="https://corefork.telegram.org/constructor/payments.StarsRevenueAdsAccountUrl" />
///</summary>
[JsonDerivedType(typeof(TStarsRevenueAdsAccountUrl), nameof(TStarsRevenueAdsAccountUrl))]
public interface IStarsRevenueAdsAccountUrl : IObject
{
    ///<summary>
    /// URL to open.
    ///</summary>
    string Url { get; set; }
}
