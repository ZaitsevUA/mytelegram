// ReSharper disable All

namespace MyTelegram.Handlers.Messages.LayerN;

///<summary>
/// Returns the conversation history with one interlocutor / within a chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getHistory" />
///</summary>
internal sealed class GetHistoryHandler(IHandlerHelper handlerHelper) : ForwardRequestToNewHandler<MyTelegram.Schema.Messages.LayerN.RequestGetHistory,
        MyTelegram.Schema.Messages.RequestGetHistory,
        MyTelegram.Schema.Messages.IMessages>(handlerHelper),
    Messages.LayerN.IGetHistoryHandler
{
    protected override RequestGetHistory GetNewData(IRequestInput request, Schema.Messages.LayerN.RequestGetHistory obj)
    {
        return new RequestGetHistory
        {
            AddOffset = obj.AddOffset,
            Limit = obj.Limit,
            MaxId = obj.MaxId,
            MinId = obj.MinId,
            OffsetDate = obj.OffsetDate,
            OffsetId = obj.OffsetId,
            Peer = obj.Peer
        };
    }
}
