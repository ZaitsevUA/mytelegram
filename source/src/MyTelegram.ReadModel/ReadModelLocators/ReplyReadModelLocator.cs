using System.Text.Json.Serialization;
using EventFlow.Core;
using EventFlow.ValueObjects;
using MyTelegram.Domain.Sagas.Identities;
using MyTelegram.Schema.Extensions;

namespace MyTelegram.ReadModel.ReadModelLocators;

[JsonConverter(typeof(SingleValueObject<ReplyId>))]
public class ReplyId(string value) : Identity<ReplyId>(value)
{
    public static ReplyId Create(long channelId, long messageId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"reply-{channelId}-{messageId}");
    }
}

public class ReplyReadModelLocator : IReplyReadModelLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();

        switch (aggregateEvent)
        {
            case MessageReplyUpdatedEvent messageReplyUpdatedEvent:
                yield return ReplyId.Create(messageReplyUpdatedEvent.OwnerChannelId, messageReplyUpdatedEvent.MessageId)
                    .Value;
                break;

            case ReplyBroadcastChannelCompletedSagaEvent replyBroadcastChannelCompletedSagaEvent:
                yield return ReplyId.Create(replyBroadcastChannelCompletedSagaEvent.ChannelId,
                    replyBroadcastChannelCompletedSagaEvent.MessageId).Value;
                break;

            case ReplyChannelMessageCompletedEvent replyChannelMessageCompletedEvent:
                yield return ReplyId.Create(replyChannelMessageCompletedEvent.ChannelId, replyChannelMessageCompletedEvent.ReplyToMessageId).Value;
                //if (replyChannelMessageCompletedEvent.PostChannelId != null && replyChannelMessageCompletedEvent.PostMessageId != null)
                //{
                //    yield return ReplyId.Create(replyChannelMessageCompletedEvent.PostChannelId.Value,
                //        replyChannelMessageCompletedEvent.PostMessageId.Value).Value;
                //}

                break;
            case MessageReplyCreatedEvent messageReplyCreatedEvent:

                yield return ReplyId
                    .Create(messageReplyCreatedEvent.ChannelId, messageReplyCreatedEvent.MessageId).Value;
                break;

        }
    }
}
