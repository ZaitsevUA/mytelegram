﻿// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Pricing of a <a href="https://corefork.telegram.org/api/invites#paid-invite-links">Telegram Star subscription »</a>.
/// See <a href="https://corefork.telegram.org/constructor/StarsSubscriptionPricing" />
///</summary>
[JsonDerivedType(typeof(TStarsSubscriptionPricing), nameof(TStarsSubscriptionPricing))]
public interface IStarsSubscriptionPricing : IObject
{
    ///<summary>
    /// The user should pay <code>amount</code> stars every <code>period</code> seconds to gain and maintain access to the channel. <br>Currently the only allowed subscription period is <code>30*24*60*60</code>, i.e. the user will be debited amount stars every month.
    ///</summary>
    int Period { get; set; }

    ///<summary>
    /// Price of the subscription in Telegram Stars.
    ///</summary>
    long Amount { get; set; }
}
