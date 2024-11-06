namespace MyTelegram.Messenger.Services.Caching;

internal sealed class AccessHashHelper(
    IQueryProcessor queryProcessor,
    IPeerHelper peerHelper)
    : IAccessHashHelper, ISingletonDependency
{
    private readonly ConcurrentDictionary<long, long> _accessHashCaches = new();

    public void AddAccessHash(long id, long accessHash)
    {
        _accessHashCaches.TryAdd(id, accessHash);
    }


    public async Task<bool> IsAccessHashValidAsync(long id,
        long accessHash, AccessHashType? accessHashType = null)
    {
        if (_accessHashCaches.TryGetValue(id, out var cachedAccessHash))
        {
            return accessHash == cachedAccessHash;
        }
        if (accessHashType == null)
        {
            var peer = peerHelper.GetPeer(id);
            switch (peer.PeerType)
            {
                case PeerType.Channel:
                    accessHashType = AccessHashType.Channel;
                    break;
                case PeerType.User:
                    accessHashType = AccessHashType.User;
                    break;
                case PeerType.Self:
                    return true;
            }
        }

        var accessHashReadModel = await queryProcessor.ProcessAsync(new GetAccessHashQueryByIdQuery(id));

        if (accessHashReadModel != null)
        {
            _accessHashCaches.TryAdd(accessHashReadModel.AccessId, accessHashReadModel.AccessHash);
            return accessHash == accessHashReadModel.AccessHash;
        }

        switch (accessHashType)
        {
            case AccessHashType.User:
                var userReadModel = await queryProcessor.ProcessAsync(new GetUserByIdQuery(id));
                if (userReadModel != null)
                {
                    _accessHashCaches.TryAdd(id, userReadModel.AccessHash);
                    return accessHash == userReadModel.AccessHash;
                }

                break;

            case AccessHashType.Channel:
                var channelReadModel = await queryProcessor.ProcessAsync(new GetChannelByIdQuery(id));
                if (channelReadModel != null)
                {
                    _accessHashCaches.TryAdd(id, channelReadModel.AccessHash);
                    return accessHash == channelReadModel.AccessHash;
                }

                break;

            case AccessHashType.WallPaper:
                break;
        }

        return false;
    }

    public async Task CheckAccessHashAsync(long id,
        long accessHash, AccessHashType? accessHashType = null)
    {
        if (!await IsAccessHashValidAsync(id, accessHash, accessHashType))
        {
            RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
        }
    }

    public Task CheckAccessHashAsync(IInputPeer? inputPeer) =>
        inputPeer switch
        {
            TInputPeerChannel inputPeerChannel => CheckAccessHashAsync(inputPeerChannel.ChannelId,
                inputPeerChannel.AccessHash),
            TInputPeerChannelFromMessage inputPeerChannelFromMessage => CheckAccessHashAsync(inputPeerChannelFromMessage
                .Peer),
            TInputPeerUser inputPeerUser => CheckAccessHashAsync(inputPeerUser.UserId, inputPeerUser.AccessHash),
            TInputPeerUserFromMessage inputPeerUserFromMessage => CheckAccessHashAsync(inputPeerUserFromMessage.Peer),
            _ => Task.CompletedTask
        };

    public Task CheckAccessHashAsync(IInputUser inputUser)
    {
        if (inputUser is TInputUser tInputUser)
        {
            return CheckAccessHashAsync(tInputUser.UserId, tInputUser.AccessHash);
        }

        return Task.CompletedTask;
    }

    public Task CheckAccessHashAsync(IInputChannel inputChannel)
    {
        if (inputChannel is TInputChannel tInputChannel)
        {
            return CheckAccessHashAsync(tInputChannel.ChannelId, tInputChannel.AccessHash);
        }

        return Task.CompletedTask;
    }

    //public Task CheckAccessHashAsync(Peer peer)
    //{
    //    return CheckAccessHashAsync(peer.PeerId, peer.AccessHash);
    //}
}

