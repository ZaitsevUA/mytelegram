namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IMessageConverter : ILayeredConverter
{
    IMessage ToMessage(MessageItem item,
        long selfUserId = 0, long? linkedChannelId = null, int pts = 0,
        List<ReactionCount>? reactions = null,
        List<Reaction>? recentReactions = null,
        //int? editDate = null,
        //bool editHide = false,
        List<UserReaction>? userReactions = null,
        bool mentioned = false
        );

    IMessage ToMessage(InboxMessageEditCompletedSagaEvent aggregateEvent);

    IMessage ToMessage(OutboxMessageEditCompletedSagaEvent aggregateEvent,
        long selfUserId);
    IMessageFwdHeader? ToMessageFwdHeader(MessageFwdHeader? messageFwdHeader);
    IMessageReplyHeader? ToMessageReplyHeader(int? replyToMessageId, int? topMsgId);
    IMessageReplyHeader? ToMessageReplyHeader(IInputReplyTo? inputReplyTo);

    IMessage ToMessage(IMessageReadModel readModel,
        IPollReadModel? pollReadModel,
        List<string>? chosenOptions,
        long selfUserId);
    IList<IMessage> ToMessages(IReadOnlyCollection<IMessageReadModel> readModels,
        IReadOnlyCollection<IPollReadModel>? pollReadModels,
        IReadOnlyCollection<IPollAnswerVoterReadModel>? pollAnswerVoterReadModels,
        long selfUserId);

    IMessage ToDiscussionMessage(long selfUserId,
        IMessageReadModel messageReadModel /*, IReplyReadModel? replyReadModel*/);
}
