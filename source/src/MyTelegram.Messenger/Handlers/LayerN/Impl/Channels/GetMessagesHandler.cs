// ReSharper disable All

using RequestGetMessages = MyTelegram.Schema.Channels.RequestGetMessages;

namespace MyTelegram.Handlers.Channels.LayerN;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> messages
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MESSAGE_IDS_EMPTY No message ids were provided.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/channels.getMessages" />
///</summary>
internal sealed class GetMessagesHandler(IHandlerHelper handlerHelper) : ForwardRequestToNewHandler<
        MyTelegram.Schema.Channels.LayerN.RequestGetMessages,
        MyTelegram.Schema.Channels.RequestGetMessages,
        MyTelegram.Schema.Messages.IMessages>(handlerHelper),
    Channels.LayerN.IGetMessagesHandler
{
    protected override RequestGetMessages GetNewData(IRequestInput input, Schema.Channels.LayerN.RequestGetMessages obj)
    {
        return new RequestGetMessages
        {
            Channel = obj.Channel,
            Id = new TVector<IInputMessage>(obj.Id.Select(p => new TInputMessageID
            {
                Id = p
            }))
        };
    }
}
