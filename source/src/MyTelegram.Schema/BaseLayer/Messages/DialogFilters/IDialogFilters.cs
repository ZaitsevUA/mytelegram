// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/messages.DialogFilters" />
///</summary>
[JsonDerivedType(typeof(TDialogFilters), nameof(TDialogFilters))]
public interface IDialogFilters : IObject
{
    BitArray Flags { get; set; }
    bool TagsEnabled { get; set; }
    TVector<MyTelegram.Schema.IDialogFilter> Filters { get; set; }
}
