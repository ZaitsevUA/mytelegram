﻿// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains the webview URL with appropriate theme and user info parameters added
/// See <a href="https://corefork.telegram.org/constructor/WebViewResult" />
///</summary>
[JsonDerivedType(typeof(TWebViewResultUrl), nameof(TWebViewResultUrl))]
public interface IWebViewResult : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, the app must be opened in fullsize mode instead of compact mode.
    ///</summary>
    bool Fullsize { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool Fullscreen { get; set; }

    ///<summary>
    /// Webview session ID (only returned by <a href="https://corefork.telegram.org/api/bots/webapps#inline-button-mini-apps">inline button mini apps</a>, <a href="https://corefork.telegram.org/api/bots/webapps#menu-button-mini-apps">menu button mini apps</a>, <a href="https://corefork.telegram.org/api/bots/webapps#attachment-menu-mini-apps">attachment menu mini apps</a>).
    ///</summary>
    long? QueryId { get; set; }

    ///<summary>
    /// Webview URL to open
    ///</summary>
    string Url { get; set; }
}
