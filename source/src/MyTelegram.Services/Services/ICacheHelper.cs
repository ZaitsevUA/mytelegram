using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Services.Services;

public interface ICacheHelper<in TKey, TValue>
{
    bool TryAdd(TKey key, TValue value);
    bool TryGetValue(TKey key, [NotNullWhen(true)] out TValue? value);
    bool TryRemove(TKey key, out TValue? value);
}