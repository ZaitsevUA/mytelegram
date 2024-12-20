﻿// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.searchStickers" />
///</summary>
internal sealed class SearchStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchStickers, MyTelegram.Schema.Messages.IFoundStickers>,
    Messages.ISearchStickersHandler
{
    protected override Task<MyTelegram.Schema.Messages.IFoundStickers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearchStickers obj)
    {
        throw new NotImplementedException();
    }
}
