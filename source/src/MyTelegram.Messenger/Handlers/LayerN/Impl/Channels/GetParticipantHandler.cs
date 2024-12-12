// ReSharper disable All

using NotSupportedException = System.NotSupportedException;

namespace MyTelegram.Handlers.Channels.LayerN;

///<summary>
/// Get info about a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> participant
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PARTICIPANT_ID_INVALID The specified participant ID is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// See <a href="https://corefork.telegram.org/method/channels.getParticipant" />
///</summary>
internal sealed class GetParticipantHandler(IHandlerHelper handlerHelper)
    : ForwardRequestToNewHandler<MyTelegram.Schema.Channels.LayerN.RequestGetParticipant,
            MyTelegram.Schema.Channels.RequestGetParticipant>(handlerHelper),
        Channels.LayerN.IGetParticipantHandler
{
    protected override RequestGetParticipant GetNewData(IRequestInput input, Schema.Channels.LayerN.RequestGetParticipant obj)
    {
        if (obj.UserId is TInputUser inputUser)
        {
            return new RequestGetParticipant
            {
                Channel = obj.Channel,
                Participant = new TInputPeerUser
                {
                    UserId = inputUser.UserId,
                    AccessHash = inputUser.AccessHash
                }
            };
        }

        throw new NotSupportedException();
    }
}
