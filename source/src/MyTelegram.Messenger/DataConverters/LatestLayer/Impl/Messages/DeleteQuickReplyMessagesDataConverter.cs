//// ReSharper disable All

//namespace MyTelegram.Handlers.Messages;

//public interface IDeleteQuickReplyMessagesDataConverter : ILayeredDataConverter<DeleteQuickReplyMessagesCompletedSagaEvent, MyTelegram.Schema.IUpdates>
//{

//}

//internal sealed class DeleteQuickReplyMessagesDataConverter : IDeleteQuickReplyMessagesDataConverter
//{
//    public string Name => "MyTelegram.Handlers.Messages.IDeleteQuickReplyMessagesHandler";
//    public int Layer => Layers.LayerLatest;

//    public MyTelegram.Schema.IUpdates Convert(DeleteQuickReplyMessagesCompletedSagaEvent data)
//    {
//        return new TUpdates
//        {
//            Updates = new TVector<IUpdate>(new TUpdateDeleteQuickReplyMessages
//            {
//                ShortcutId = data.ShortcutId,
//                Messages = new TVector<int>(data.MessageIds)
//            }),
//            Users = [],
//            Chats = [],
//            Date = DateTime.UtcNow.ToTimestamp()
//        };
//    }
//}
