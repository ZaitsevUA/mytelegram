using MyTelegram.Schema;

namespace MyTelegram.ReadModel;

public interface IReplyReadModel : IReadModel
{
    //int ReplyToMsgId { get; }
    long ChannelId { get; }
    int MessageId { get; }


    //long PostChannelId { get; }
    //int? PostMessageId { get; }

    //long PostChannelId { get; }
    int Replies { get; }
    int RepliesPts { get; }

    IReadOnlyCollection<Peer>? RecentRepliers { get; }
    int? MaxId { get; }
    long? CommentChannelId { get; }
    //int ReplyToMsgId { get; }
    //int? TopicMsgId { get; }
    //long? ReplyToPeerId { get; }
    //int Replies { get; }
    //int RepliesPts { get; }

    //int Replies { get; }
    //int RepliesPts { get; }
    //long SavedFromPeerId { get; }
    //int SavedFromMsgId { get; }
    //long ChannelId { get; }
    ////int ReplyToMsgId { get; }
    //int MaxId { get; }
    //IReadOnlyCollection<Peer>? RecentRepliers { get; }
    ////int ReadMaxId { get; }'
    //int ChannelPost { get; }
    //IInputReplyTo InputReplyTo { get; }
}
