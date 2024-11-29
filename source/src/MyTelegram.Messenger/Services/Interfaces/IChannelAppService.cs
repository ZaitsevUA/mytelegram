namespace MyTelegram.Messenger.Services.Interfaces;

public interface IChannelAppService : IReadModelWithCacheAppService<IChannelReadModel>
{
    Task<IChannelFullReadModel?> GetChannelFullAsync(long channelId);
}