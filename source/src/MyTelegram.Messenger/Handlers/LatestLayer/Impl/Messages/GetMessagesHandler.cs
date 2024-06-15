// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns the list of messages by their IDs.
/// See <a href="https://corefork.telegram.org/method/messages.getMessages" />
///</summary>
internal sealed class GetMessagesHandler(
    IMessageAppService messageAppService,
    ILogger<GetMessagesHandler> logger,
    ILayeredService<IRpcResultProcessor> layeredService)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessages, MyTelegram.Schema.Messages.IMessages>,
        Messages.IGetMessagesHandler
{

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetMessages obj)
    {

        var idList = new List<int>();
        foreach (var inputMessage in obj.Id)
        {
            if (inputMessage is TInputMessageID inputMessageId)
            {
                idList.Add(inputMessageId.Id);
            }
        }

        var dto = await messageAppService
            .GetMessagesAsync(new GetMessagesInput(input.UserId, input.UserId, idList, null) { Limit = 50 });
        return layeredService.GetConverter(input.Layer).ToMessages(dto, input.Layer);
    }
}