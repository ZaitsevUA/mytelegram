namespace MyTelegram.GatewayServer.EventHandlers;

public class PingTimeoutEventHandler(IClientManager clientManager) : IEventHandler<PingTimeoutEvent>, ITransientDependency
{
    public Task HandleEventAsync(PingTimeoutEvent eventData)
    {
        if (clientManager.TryGetClientData(eventData.ConnectionId, out var clientData))
        {
            clientData.ConnectionContext?.DisposeAsync();
            clientData.WebSocket?.CloseAsync(WebSocketCloseStatus.Empty, null, default);
        }

        return Task.CompletedTask;
    }
}