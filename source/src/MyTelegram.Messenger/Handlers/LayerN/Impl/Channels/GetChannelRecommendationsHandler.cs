// ReSharper disable All

namespace MyTelegram.Handlers.Channels.LayerN;

internal sealed class GetChannelRecommendationsHandler(IHandlerHelper handlerHelper) :
    ForwardRequestToNewHandler<MyTelegram.Schema.Channels.LayerN.RequestGetChannelRecommendations,
        MyTelegram.Schema.Channels.RequestGetChannelRecommendations,
        MyTelegram.Schema.Messages.IChats>(handlerHelper),
    Channels.LayerN.IGetChannelRecommendationsHandler
{
    protected override RequestGetChannelRecommendations GetNewData(IRequestInput input, Schema.Channels.LayerN.RequestGetChannelRecommendations obj)
    {
        return new RequestGetChannelRecommendations
        {
            Channel = obj.Channel
        };
    }
}