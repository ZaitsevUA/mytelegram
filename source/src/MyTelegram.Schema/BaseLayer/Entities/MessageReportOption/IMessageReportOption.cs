﻿// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Report menu option
/// See <a href="https://corefork.telegram.org/constructor/MessageReportOption" />
///</summary>
[JsonDerivedType(typeof(TMessageReportOption), nameof(TMessageReportOption))]
public interface IMessageReportOption : IObject
{
    ///<summary>
    /// Option title
    ///</summary>
    string Text { get; set; }

    ///<summary>
    /// Option identifier: if the user selects this option, re-invoke <a href="https://corefork.telegram.org/method/messages.report">messages.report</a>, passing this option to <code>option</code>
    ///</summary>
    byte[] Option { get; set; }
}
