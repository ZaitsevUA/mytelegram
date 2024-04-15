using MyTelegram.Domain.Aggregates.Temp;
using MyTelegram.Domain.Commands.Temp;
using StartDeleteMessagesCommand = MyTelegram.Domain.Commands.Temp.StartDeleteMessagesCommand;

namespace MyTelegram.Domain.CommandHandlers.Temp;

public class StartForwardMessagesCommandHandler : CommandHandler<TempAggregate, TempId, StartForwardMessagesCommand>
{
    public override Task ExecuteAsync(TempAggregate aggregate, StartForwardMessagesCommand command, CancellationToken cancellationToken)
    {
        aggregate.StartForwardMessages(command.RequestInfo, command.Silent, command.Background, command.WithMyScore,
            command.DropAuthor, command.DropMediaCaptions, command.NoForwards, command.FromPeer, command.ToPeer,
            command.MessageIds, command.RandomIds, command.ScheduleDate, command.SendAs,
            command.ForwardFromLinkedChannel);
        return Task.CompletedTask;
    }
}

public class
    StartDeleteReplyMessagesCommandHandler : CommandHandler<TempAggregate, TempId, StartDeleteReplyMessagesCommand>
{
    public override Task ExecuteAsync(TempAggregate aggregate, StartDeleteReplyMessagesCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartDeleteReplyMessages(command.RequestInfo, command.ChannelId, command.MessageIds, command.NewTopMessageId);

        return Task.CompletedTask;
    }
}

public class
    StartSetDiscussionGroupCommandHandler : CommandHandler<TempAggregate, TempId, StartSetDiscussionGroupCommand>
{
    public override Task ExecuteAsync(TempAggregate aggregate, StartSetDiscussionGroupCommand command, CancellationToken cancellationToken)
    {
        aggregate.StartSetChannelDiscussionGroup(command.RequestInfo, command.BroadcastChannelId, command.DiscussionGroupChannelId);
        return Task.CompletedTask;
    }
}

public class
    StartPinForwardedChannelMessageCommandHandler : CommandHandler<TempAggregate, TempId,
        StartPinForwardedChannelMessageCommand>
{
    public override Task ExecuteAsync(TempAggregate aggregate, StartPinForwardedChannelMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartPinForwardedChannelMessage(command.RequestInfo, command.ChannelId, command.MessageId);

        return Task.CompletedTask;
    }
}

public class StartDeleteMessagesCommandHandler : CommandHandler<TempAggregate, TempId, StartDeleteMessagesCommand>
{
    public override Task ExecuteAsync(TempAggregate aggregate, StartDeleteMessagesCommand command, CancellationToken cancellationToken)
    {
        aggregate.StartDeleteMessages(command.RequestInfo, command.MessageItems, command.Revoke, command.DeleteGroupMessagesForEveryone, command.NewTopMessageId, command.NewTopMessageIdForOtherParticipant);

        return Task.CompletedTask;
    }
}

public class StartDeleteHistoryCommandHandler : CommandHandler<TempAggregate, TempId, StartDeleteHistoryCommand>
{
    public override Task ExecuteAsync(TempAggregate aggregate, StartDeleteHistoryCommand command, CancellationToken cancellationToken)
    {
        aggregate.StartDeleteHistory(command.RequestInfo, command.MessageItems, command.Revoke, command.DeleteGroupMessagesForEveryone);

        return Task.CompletedTask;
    }
}

public class StartDeleteChannelMessagesCommandHandler : CommandHandler<TempAggregate, TempId, StartDeleteChannelMessagesCommand>
{
    public override Task ExecuteAsync(TempAggregate aggregate, StartDeleteChannelMessagesCommand command, CancellationToken cancellationToken)
    {
        aggregate.StartDeleteChannelMessages(command.RequestInfo, command.ChannelId, command.MessageIds, command.NewTopMessageId, command.NewTopMessageIdForDiscussionGroup, command.DiscussionGroupChannelId, command.RepliesMessageIds);

        return Task.CompletedTask;
    }
}

public class StartDeleteParticipantHistoryCommandHandler : CommandHandler<TempAggregate, TempId, StartDeleteParticipantHistoryCommand>
{
    public override Task ExecuteAsync(TempAggregate aggregate, StartDeleteParticipantHistoryCommand command, CancellationToken cancellationToken)
    {
        aggregate.StartDeleteParticipantHistory(command.RequestInfo, command.ChannelId, command.MessageIds, command.NewTopMessageId);

        return Task.CompletedTask;
    }
}