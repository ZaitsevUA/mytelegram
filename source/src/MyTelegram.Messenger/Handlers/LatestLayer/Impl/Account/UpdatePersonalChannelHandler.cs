// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.updatePersonalChannel" />
///</summary>
internal sealed class UpdatePersonalChannelHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdatePersonalChannel, IBool>,
    Account.IUpdatePersonalChannelHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IQueryProcessor _queryProcessor;

    public UpdatePersonalChannelHandler(ICommandBus commandBus, IAccessHashHelper accessHashHelper, IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _accessHashHelper = accessHashHelper;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdatePersonalChannel obj)
    {
        long? personalChannelId = null;
        switch (obj.Channel)
        {
            case TInputChannel inputChannel:
                await _accessHashHelper.CheckAccessHashAsync(inputChannel);
                var channelReadModel =
                    await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(inputChannel.ChannelId));
                if (channelReadModel!.CreatorId != input.UserId)
                {
                    RpcErrors.RpcErrors400.ChatIdInvalid.ThrowRpcError();
                }

                personalChannelId = inputChannel.ChannelId;
                break;
        }

        var command =
            new UpdatePersonalChannelCommand(UserId.Create(input.UserId), input.ToRequestInfo(), personalChannelId);
        await _commandBus.PublishAsync(command);

        return new TBoolTrue();
    }
}
