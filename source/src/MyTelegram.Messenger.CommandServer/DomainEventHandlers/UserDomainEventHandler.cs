namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class UserDomainEventHandler(
    IMessageAppService messageAppService,
    IRandomHelper randomHelper,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options,
    ICommandBus commandBus)
    :
        ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>
{
    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (options.CurrentValue.SetPremiumToTrueAfterUserCreated)
        {
            var command = new UpdateUserPremiumStatusCommand(domainEvent.AggregateIdentity, true);
            await commandBus.PublishAsync(command, default);
        }

        if (!options.CurrentValue.SendWelcomeMessageAfterUserSignIn)
        {
            return;
        }

        if (!domainEvent.AggregateEvent.Bot)
        {
            var welcomeMessage = "Welcome to use MyTelegram!";
            var sendMessageInput = new SendMessageInput(new RequestInfo(0,
                    MyTelegramServerDomainConsts.OfficialUserId,
                    domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
                    domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId,
                    Guid.NewGuid(), 0, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    DeviceType.Desktop
                    ),
                MyTelegramServerDomainConsts.OfficialUserId,
                new Peer(PeerType.User, domainEvent.AggregateEvent.UserId/*, domainEvent.AggregateEvent.AccessHash*/),
                welcomeMessage,
                randomHelper.NextLong());

            await messageAppService.SendMessageAsync(sendMessageInput);
        }
    }
}