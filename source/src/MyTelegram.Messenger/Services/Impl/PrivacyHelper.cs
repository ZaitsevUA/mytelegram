namespace MyTelegram.Messenger.Services.Impl;

public class PrivacyHelper : IPrivacyHelper, ITransientDependency
{
    public void ApplyPrivacy(IPrivacyReadModel? privacyReadModel,
        Action executeOnPrivacyNotMatch,
        long selfUserId,
        bool isContact)
    {

    }
}