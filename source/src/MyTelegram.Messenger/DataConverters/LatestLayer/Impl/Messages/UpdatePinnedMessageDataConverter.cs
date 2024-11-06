//// ReSharper disable All

//using EventFlow.Aggregates;

//namespace MyTelegram.Handlers.Messages;

//public interface IUpdatePinnedMessageDataConverter : ILayeredDataConverter<SendOutboxMessageCompletedSagaEvent, MyTelegram.Schema.IUpdates>
//{

//}

//internal sealed class UpdatePinnedMessageDataConverter(ILayeredService<IMessageConverter> messageLayeredService) : IUpdatePinnedMessageDataConverter
//{
//    public string Name => "MyTelegram.Handlers.Messages.IUpdatePinnedMessageHandler";
//    public int Layer => Layers.LayerLatest;

//    public MyTelegram.Schema.IUpdates Convert(SendOutboxMessageCompletedSagaEvent data)
//    {
//        var item = data.MessageItem;
//        var updateMessageId = new TUpdateMessageID
//        {
//            Id = item.MessageId,
//            RandomId = item.RandomId
//        };
//        var fromPeer = item.SendAs ?? item.SenderPeer;

//        var m = new TMessageService
//        {
//            Action = new TMessageActionPinMessage(),
//            Date = item.Date,
//            FromId = fromPeer.ToPeer(),
//            Out = item.IsOut,
//            PeerId = item.ToPeer.ToPeer(),
//            Id = item.MessageId,
//            ReplyTo = item.InputReplyTo.ToMessageReplyHeader()
//        };
//        throw new NotImplementedException();
//    }
//}
