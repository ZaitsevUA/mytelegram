// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/FoundStory" />
///</summary>
[JsonDerivedType(typeof(TFoundStory), nameof(TFoundStory))]
public interface IFoundStory : IObject
{
    MyTelegram.Schema.IPeer Peer { get; set; }
    MyTelegram.Schema.IStoryItem Story { get; set; }
}
