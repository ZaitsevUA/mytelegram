// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public interface IEditMessageDataConverter :
    ILayeredDataConverter<OutboxMessageEditCompletedSagaEvent, Schema.IUpdates>,
    ILayeredDataConverter<InboxMessageEditCompletedSagaEvent, Schema.IUpdates>
{
    IUpdates ToEditQuickReplyMessageUpdates(OutboxMessageEditCompletedSagaEvent data);
}

internal sealed class EditMessageDataConverter(ILayeredService<IMessageConverter> messageLayeredService)
    : IEditMessageDataConverter
{
    public int Layer => Layers.LayerLatest;

    public string Name => "MyTelegram.Handlers.Messages.IEditMessageHandler";

    public IUpdates Convert(InboxMessageEditCompletedSagaEvent data)
    {
        var update = new TUpdateEditMessage
        {
            Message = messageLayeredService.Converter.ToMessage(data.NewMessageItem),
            Pts = data.NewMessageItem.Pts,
            PtsCount = 1
        };

        return new TUpdates
        {
            Updates = new TVector<IUpdate>(update),
            Users = [],
            Chats = [],
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0
        };
    }

    public Schema.IUpdates Convert(OutboxMessageEditCompletedSagaEvent data)
    {
        IUpdate update = data.NewMessageItem.ToPeer.PeerType switch
        {
            PeerType.Channel => new TUpdateEditChannelMessage
            {
                Pts = data.NewMessageItem.Pts,
                PtsCount = 1,
                Message = messageLayeredService.GetConverter(data.RequestInfo.Layer).ToMessage(data, 0)
            },
            _ => new TUpdateEditMessage
            {
                Message = messageLayeredService.GetConverter(data.RequestInfo.Layer).ToMessage(data, data.NewMessageItem.SenderPeer.PeerId),
                Pts = data.NewMessageItem.Pts,
                PtsCount = 1
            }
        };

        return new TUpdates
        {
            Updates = new TVector<IUpdate>(update),
            Users = [],
            Chats = [],
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0
        };
    }

    public IUpdates ToEditQuickReplyMessageUpdates(OutboxMessageEditCompletedSagaEvent data)
    {
        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(new TUpdateQuickReplyMessage
            {
                Message = messageLayeredService.GetConverter(data.RequestInfo.Layer)
                    .ToMessage(data.NewMessageItem, data.RequestInfo.UserId)
            }),
            Users = [],
            Chats = [],
            Date = DateTime.UtcNow.ToTimestamp()
        };

        return updates;
    }
}