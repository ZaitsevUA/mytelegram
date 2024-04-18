namespace MyTelegram.Messenger.Services.Impl;

public class DataCenterHelper(IOptions<MyTelegramMessengerServerOptions> options) : IDataCenterHelper
{
    public int GetMediaDcId()
    {
        var defaultDcId = MyTelegramServerDomainConsts.MediaDcId;
        var dc = options.Value.DcOptions?.FirstOrDefault(p => p.Id == defaultDcId);
        if (dc != null)
        {
            return defaultDcId;
        }

        return options.Value.ThisDcId;
    }
}