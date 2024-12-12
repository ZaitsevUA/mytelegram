﻿// ReSharper disable All

namespace MyTelegram.Handlers.Auth.LayerN;

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
internal sealed class SendCodeHandler(IHandlerHelper handlerHelper) : ForwardRequestToNewHandler<
        MyTelegram.Schema.Auth.LayerN.RequestSendCode,
        MyTelegram.Schema.Auth.RequestSendCode>(handlerHelper),
    Auth.LayerN.ISendCodeHandler
{
    protected override RequestSendCode GetNewData(IRequestInput input, Schema.Auth.LayerN.RequestSendCode obj)
    {
        return new RequestSendCode
        {
            ApiHash = obj.ApiHash,
            ApiId = obj.ApiId,
            PhoneNumber = obj.PhoneNumber,
            Settings = new TCodeSettings
            {
                AllowFlashcall = obj.AllowFlashcall,
            }
        };
    }
}
