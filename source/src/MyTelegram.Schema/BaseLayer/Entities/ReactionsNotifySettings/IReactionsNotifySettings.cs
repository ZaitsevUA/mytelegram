// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/ReactionsNotifySettings" />
///</summary>
[JsonDerivedType(typeof(TReactionsNotifySettings), nameof(TReactionsNotifySettings))]
public interface IReactionsNotifySettings : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IReactionNotificationsFrom? MessagesNotifyFrom { get; set; }
    MyTelegram.Schema.IReactionNotificationsFrom? StoriesNotifyFrom { get; set; }
    MyTelegram.Schema.INotificationSound Sound { get; set; }
    bool ShowPreviews { get; set; }
}
