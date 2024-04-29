// ReSharper disable All

using MyTelegram.Schema.Updates;

namespace MyTelegram.Handlers.Updates.LayerN;

///<summary>
/// Get new <a href="https://corefork.telegram.org/api/updates">updates</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CDN_METHOD_INVALID You can't call this method in a CDN DC.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 DATE_EMPTY Date empty.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PERSISTENT_TIMESTAMP_EMPTY Persistent timestamp empty.
/// 400 PERSISTENT_TIMESTAMP_INVALID Persistent timestamp invalid.
/// 500 RANDOM_ID_DUPLICATE You provided a random ID that was already used.
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// See <a href="https://corefork.telegram.org/method/updates.getDifference" />
///</summary>
internal sealed class GetDifferenceHandlerLayerN(
    IHandlerHelper handlerHelper)
    : BaseObjectHandler<MyTelegram.Schema.Updates.LayerN.RequestGetDifference, IObject>,
        Updates.IGetDifferenceHandler
{
    protected override async Task<IObject> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Updates.LayerN.RequestGetDifference obj)
    {
        const uint getDifferenceConstructorId = 0x19c2f763;
        if (handlerHelper.TryGetHandler(getDifferenceConstructorId, out var handler))
        {
            var response = await handler.HandleAsync(input, new MyTelegram.Schema.Updates.RequestGetDifference
            {
                Date = obj.Date,
                Pts = obj.Pts,
                Qts = obj.Qts,
                PtsTotalLimit = obj.PtsTotalLimit
            });

            return response!;
        }

        return null!;
    }
}
