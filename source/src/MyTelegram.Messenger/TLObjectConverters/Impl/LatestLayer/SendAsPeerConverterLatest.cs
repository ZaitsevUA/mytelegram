namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;
//using IPeer=MyTelegram.Schema.Channels.IPeer;

public class SendAsPeerConverterLatest : ISendAsPeerConverterLatest
{
    public virtual int Layer => Layers.LayerLatest;

    public int RequestLayer { get; set; }

    public virtual ISendAsPeers ToSendAsPeers(IList<IChat> channels)
    {
        var sendAsPeers = new TSendAsPeers
        {
            Peers = new TVector<ISendAsPeer>(channels.Select(p => new TSendAsPeer
            {
                Peer = new TPeerChannel
                {
                    ChannelId = p.Id
                }
            })),
            Chats = new(),
            Users = new(),
        };

        return sendAsPeers;
    }
}