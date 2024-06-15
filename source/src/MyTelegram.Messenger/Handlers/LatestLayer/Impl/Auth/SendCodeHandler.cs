// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Send the verification code for login
/// <para>Possible errors</para>
/// Code Type Description
/// 400 API_ID_INVALID API ID invalid.
/// 400 API_ID_PUBLISHED_FLOOD This API id was published somewhere, you can't use it now.
/// 500 AUTH_RESTART Restart the authorization process.
/// 400 PHONE_NUMBER_APP_SIGNUP_FORBIDDEN You can't sign up using this app.
/// 400 PHONE_NUMBER_BANNED The provided phone number is banned from telegram.
/// 400 PHONE_NUMBER_FLOOD You asked for the code too many times.
/// 406 PHONE_NUMBER_INVALID The phone number is invalid.
/// 406 PHONE_PASSWORD_FLOOD You have tried logging in too many times.
/// 400 PHONE_PASSWORD_PROTECTED This phone is password protected.
/// 400 SMS_CODE_CREATE_FAILED An error occurred while creating the SMS code.
/// 400 Sorry, too many invalid attempts to enter your password. Please try again later. &nbsp;
/// See <a href="https://corefork.telegram.org/method/auth.sendCode" />
///</summary>
internal sealed class SendCodeHandler(
    ICommandBus commandBus,
    IRandomHelper randomHelper,
    IPeerHelper peerHelper,
    IOptions<MyTelegramMessengerServerOptions> options,
    IQueryProcessor queryProcessor,
    ICacheManager<FutureAuthTokenCacheItem> cacheManager,
    IHashHelper hashHelper,
    ILayeredService<IAuthorizationConverter> authorizationLayeredService,
    ILayeredService<IUserConverter> userLayeredService,
    IVerificationCodeGenerator verificationCodeGenerator,
    IEventBus eventBus)
    : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestSendCode, MyTelegram.Schema.Auth.ISentCode>,
        Auth.ISendCodeHandler
{
    private readonly int _maxFutureAuthTokens = 20;

    protected override async Task<ISentCode> HandleCoreAsync(IRequestInput input,
        RequestSendCode obj)
    {
        var userReadModel = await queryProcessor.ProcessAsync(new GetUserByPhoneNumberQuery(obj.PhoneNumber.ToPhoneNumber()));
        if (userReadModel != null)
        {
            if (peerHelper.IsBotUser(userReadModel.UserId) || userReadModel.UserId == MyTelegramServerDomainConsts.OfficialUserId)
            {
                RpcErrors.RpcErrors400.PhoneNumberInvalid.ThrowRpcError();
            }

            if (obj.Settings.LogoutTokens?.Count > 0)
            {
                var cacheKeys = obj.Settings.LogoutTokens.Take(_maxFutureAuthTokens).Select(p => FutureAuthTokenCacheItem.GetCacheKey(BitConverter.ToString(hashHelper.Sha1(p)).Replace("-", string.Empty))).ToList();
                var cachedFutureTokens = await cacheManager.GetManyAsync(cacheKeys);

                if (cachedFutureTokens.Any(p => p.Value.UserId == userReadModel.UserId))
                {
                    if (userReadModel.HasPassword)
                    {
                        RpcErrors.RpcErrors401.SessionPasswordNeeded.ThrowRpcError();
                    }
                    else
                    {
                        var user = userLayeredService.GetConverter(input.Layer)
                            .ToUser(userReadModel.UserId, userReadModel, null);

                        await eventBus.PublishAsync(new UserSignInSuccessEvent(input.AuthKeyId, input.PermAuthKeyId,
                            user.Id, PasswordState.None));

                        return new TSentCodeSuccess
                        {
                            Authorization = authorizationLayeredService.GetConverter(input.Layer).CreateAuthorization(user)
                        };
                    }
                }
            }
        }

        var code = verificationCodeGenerator.Generate();

        var phoneCodeHash = Guid.NewGuid().ToString("N");
        var timeout = options.Value.VerificationCodeExpirationSeconds;
        //if (userReadModel != null)
        //{
        //    var loginLog = await _queryProcessor.ProcessAsync(new GetLoginLogQuery(userReadModel.UserId), default)
        // ;
        //    if (loginLog != null)
        //    {
        //        if (loginLog.Count >= _options.Value.ConfirmEmailLoginCount)
        //        {
        //            if (string.IsNullOrEmpty(userReadModel.Email))
        //            {

        //                return new TSentCode
        //                {
        //                    Type = new TSentCodeTypeSetUpEmailRequired
        //                    {
        //                        AppleSigninAllowed = true,
        //                        GoogleSigninAllowed = true
        //                    },
        //                    PhoneCodeHash = phoneCodeHash,
        //                    Timeout = timeout,
        //                };
        //            }
        //        }
        //    }
        //}
        //return new TSentCode
        //{
        //    //Type = new MyTelegram.Schema.Auth.TSentCodeTypeEmailCode
        //    //{
        //    //    GoogleSigninAllowed = true,
        //    //    AppleSigninAllowed = true,
        //    //    EmailPattern = "****@test.com",
        //    //    Length = code.Length,
        //    //},
        //    Type = new TSentCodeTypeSetUpEmailRequired
        //    {
        //        AppleSigninAllowed = true,
        //        GoogleSigninAllowed = true
        //    },
        //    PhoneCodeHash = phoneCodeHash,
        //    Timeout = timeout,
        //};

        var appCodeId = AppCodeId.Create(obj.PhoneNumber.ToPhoneNumber(), phoneCodeHash);
        var sendAppCodeCommand =
            new SendAppCodeCommand(appCodeId,
                input.ToRequestInfo(),
                userReadModel?.UserId ?? 0,
                obj.PhoneNumber.ToPhoneNumber(),
                code,
                phoneCodeHash,
                DateTime.UtcNow.ToTimestamp());
        await commandBus.PublishAsync(sendAppCodeCommand);

        return new TSentCode
        {
            Type = new TSentCodeTypeSms { Length = code.Length },
            PhoneCodeHash = phoneCodeHash,
            Timeout = timeout
        };
    }
}
