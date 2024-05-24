namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface ISendAsPeerConverter : ILayeredConverter
{
    ISendAsPeers ToSendAsPeers(IList<IChat> channels);
}