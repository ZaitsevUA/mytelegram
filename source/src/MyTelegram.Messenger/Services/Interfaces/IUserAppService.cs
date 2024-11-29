namespace MyTelegram.Messenger.Services.Interfaces;

public interface IUserAppService : IReadModelWithCacheAppService<IUserReadModel>
{
    Task CheckAccountPremiumStatusAsync(long userId);
}