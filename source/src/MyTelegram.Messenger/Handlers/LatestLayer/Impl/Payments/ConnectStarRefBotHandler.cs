﻿// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.connectStarRefBot" />
///</summary>
internal sealed class ConnectStarRefBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestConnectStarRefBot, MyTelegram.Schema.Payments.IConnectedStarRefBots>,
    Payments.IConnectStarRefBotHandler
{
    protected override Task<MyTelegram.Schema.Payments.IConnectedStarRefBots> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestConnectStarRefBot obj)
    {
        throw new NotImplementedException();
    }
}
