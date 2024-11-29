namespace MyTelegram.Messenger.Services.Interfaces;

public interface IPhotoAppService : IReadModelWithCacheAppService<IPhotoReadModel>
{
    Task<IReadOnlyCollection<IPhotoReadModel>> GetPhotosAsync(IUserReadModel? userReadModel, IContactReadModel? contactReadModel = null);

    Task<IReadOnlyCollection<IPhotoReadModel>> GetPhotosAsync(IReadOnlyCollection<IUserReadModel> userReadModels, IReadOnlyCollection<IContactReadModel>? contactReadModels = null);

    Task<IReadOnlyCollection<IPhotoReadModel>> GetPhotosAsync(IReadOnlyCollection<IChannelReadModel> channelReadModels);
    //Task<IPhotoReadModel?> GetPhotoAsync(long? photoId);
    //Task<IReadOnlyCollection<IPhotoReadModel>> GetPhotosAsync(IUserReadModel userReadModel);
    //Task<IReadOnlyCollection<IPhotoReadModel>> GetPhotosAsync(List<long> photoIds);
}