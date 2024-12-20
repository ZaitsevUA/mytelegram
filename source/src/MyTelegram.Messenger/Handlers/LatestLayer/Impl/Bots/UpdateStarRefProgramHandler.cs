﻿// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// See <a href="https://corefork.telegram.org/method/bots.updateStarRefProgram" />
///</summary>
internal sealed class UpdateStarRefProgramHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestUpdateStarRefProgram, MyTelegram.Schema.IStarRefProgram>,
    Bots.IUpdateStarRefProgramHandler
{
    protected override Task<MyTelegram.Schema.IStarRefProgram> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestUpdateStarRefProgram obj)
    {
        throw new NotImplementedException();
    }
}
