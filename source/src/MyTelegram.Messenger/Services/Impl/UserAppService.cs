namespace MyTelegram.Messenger.Services.Impl;

public class UserAppService(IQueryProcessor queryProcessor,
    IReadModelCacheHelper<IUserReadModel> userReadModelCacheHelper) : ReadModelWithCacheAppService<IUserReadModel>(userReadModelCacheHelper), IUserAppService, ITransientDependency
{
    public async Task CheckAccountPremiumStatusAsync(long userId)
    {
        var userReadModel = await queryProcessor.ProcessAsync(new GetUserByIdQuery(userId));
        if (userReadModel == null)
        {
            RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
        }

        if (!userReadModel!.Premium)
        {
            RpcErrors.RpcErrors400.PremiumAccountRequired.ThrowRpcError();
        }
    }

    protected override Task<IUserReadModel?> GetReadModelAsync(long id)
    {
        return queryProcessor.ProcessAsync(new GetUserByIdQuery(id));
    }

    protected override string GetReadModelId(IUserReadModel readModel) => readModel.Id;

    protected override long GetReadModelInt64Id(IUserReadModel readModel) => readModel.UserId;

    protected override Task<IReadOnlyCollection<IUserReadModel>> GetReadModelListAsync(List<long> ids)
    {
        return queryProcessor.ProcessAsync(new GetUsersByUserIdListQuery(ids));
    }
}