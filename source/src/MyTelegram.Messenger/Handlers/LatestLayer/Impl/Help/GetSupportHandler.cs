// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Returns the support user for the "ask a question" feature.
/// See <a href="https://corefork.telegram.org/method/help.getSupport" />
///</summary>
internal sealed class GetSupportHandler(IQueryProcessor queryProcessor,
    ILayeredService<IUserConverter> userLayeredService,
    IPhotoAppService photoAppService,
    IPrivacyAppService privacyAppService,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options) : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetSupport, MyTelegram.Schema.Help.ISupport>,
    Help.IGetSupportHandler
{
    protected async override Task<MyTelegram.Schema.Help.ISupport> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetSupport obj)
    {
        var supportUserId = options.CurrentValue.SupportUserId ?? MyTelegramServerDomainConsts.DefaultSupportUserId;
        var userReadModel = await queryProcessor.ProcessAsync(new GetUserByIdQuery(supportUserId));

        if (userReadModel == null)
        {
            supportUserId = MyTelegramServerDomainConsts.DefaultSupportUserId;
            userReadModel = await queryProcessor.ProcessAsync(new GetUserByIdQuery(supportUserId));
        }

        var photos = await photoAppService.GetPhotosAsync(userReadModel);
        var user = userLayeredService.GetConverter(input.Layer).ToUser(input.UserId, userReadModel, photos);
        var privacies = await privacyAppService.GetPrivacyListAsync(userReadModel.UserId);

        return new TSupport
        {
            PhoneNumber = userReadModel.PhoneNumber,
            User = user
        };
    }
}
