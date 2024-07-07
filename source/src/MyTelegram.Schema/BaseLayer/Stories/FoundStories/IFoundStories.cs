// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stories.FoundStories" />
///</summary>
[JsonDerivedType(typeof(TFoundStories), nameof(TFoundStories))]
public interface IFoundStories : IObject
{
    BitArray Flags { get; set; }
    int Count { get; set; }
    TVector<MyTelegram.Schema.IFoundStory> Stories { get; set; }
    string? NextOffset { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
