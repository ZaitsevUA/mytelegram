namespace MyTelegram.GatewayServer.Services;

public class MtpConnectionHandler(
    IClientManager clientManager,
    IMtpMessageParser messageParser,
    IMtpMessageDispatcher messageDispatcher,
    ILogger<MtpConnectionHandler> logger,
    IClientDataSender clientDataSender,
    IMessageQueueProcessor<ClientDisconnectedEvent> messageQueueProcessor)
    : ConnectionHandler
{
    public override async Task OnConnectedAsync(ConnectionContext connection)
    {
        logger.LogInformation("[ConnectionId={ConnectionId}] New client connected,RemoteEndPoint:{RemoteEndPoint},online count:{OnlineCount}",
            connection.ConnectionId,
            connection.RemoteEndPoint, clientManager.GetOnlineCount());
        var clientData = new ClientData
        {
            ConnectionContext = connection,
            ConnectionId = connection.ConnectionId,
            ClientType = ClientType.Tcp
        };
        clientManager.AddClient(connection.ConnectionId, clientData);
        connection.ConnectionClosed.Register(() =>
        {
            if (clientManager.TryRemoveClient(connection.ConnectionId, out _))
            {
                messageQueueProcessor.Enqueue(new ClientDisconnectedEvent(clientData.ConnectionId, clientData.AuthKeyId, 0), clientData.AuthKeyId);

            }
            logger.LogInformation("[ConnectionId={ConnectionId}] Client disconnected,RemoteEndPoint:{RemoteEndPoint}",
                connection.ConnectionId,
                connection.RemoteEndPoint);
        });
        _ = ProcessResponseQueueAsync(clientData, connection);
        var input = connection.Transport.Input;
        while (!connection.ConnectionClosed.IsCancellationRequested)
        {
            var result = await input.ReadAsync();
            if (result.IsCanceled)
            {
                break;
            }

            var buffer = result.Buffer;
            if (!clientManager.TryGetClientData(connection.ConnectionId, out _))
            {
                logger.LogWarning("Can not find client data,connectionId={ConnectionId}", connection.ConnectionId);
                break;
            }

            if (!clientData.IsFirstPacketParsed)
            {
                messageParser.ProcessFirstUnencryptedPacket(ref buffer, clientData);
            }

            while (TryParseMessage(ref buffer, clientData, out var mtpMessage))
            {
                await ProcessDataAsync(mtpMessage, clientData);
            }

            input.AdvanceTo(buffer.Start, buffer.End);
            if (result.IsCompleted)
            {
                break;
            }
        }
    }

    private Task ProcessResponseQueueAsync(ClientData clientData, ConnectionContext connectionContext)
    {
        Task.Run(async () =>
        {
            var queue = clientData.ResponseQueue;
            while (await queue.Reader.WaitToReadAsync() && !connectionContext.ConnectionClosed.IsCancellationRequested)
            {
                while (queue.Reader.TryRead(out var response))
                {
                    var encodedBytes = ArrayPool<byte>.Shared.Rent(clientDataSender.GetEncodedDataMaxLength(response.Data.Length));
                    try
                    {
                        var totalCount = clientDataSender.EncodeData(response, clientData, encodedBytes);
                        await connectionContext.Transport.Output.WriteAsync(encodedBytes.AsMemory()[..totalCount]);
                        await connectionContext.Transport.Output.FlushAsync();
                    }
                    finally
                    {
                        ArrayPool<byte>.Shared.Return(encodedBytes);
                    }
                }
            }
        });

        return Task.CompletedTask;
    }

    private Task ProcessDataAsync(IMtpMessage mtpMessage,
        ClientData clientData)
    {
        if (clientData.IsFirstPacketParsed)
        {
            mtpMessage.ConnectionId = clientData.ConnectionId;
            mtpMessage.ClientIp = (clientData.ConnectionContext!.RemoteEndPoint as IPEndPoint)?.Address.ToString() ?? string.Empty;
            return messageDispatcher.DispatchAsync(mtpMessage);
        }

        return Task.CompletedTask;
    }

    private bool TryParseMessage(ref ReadOnlySequence<byte> buffer,
        ClientData clientData,
        [NotNullWhen(true)] out IMtpMessage? mtpMessage)
    {
        if (buffer.Length == 0)
        {
            mtpMessage = default;
            return false;
        }

        var reader = new SequenceReader<byte>(buffer);

        if (reader.Remaining < 4)
        {
            mtpMessage = default;

            return false;
        }

        return messageParser.TryParse(ref buffer, clientData, out mtpMessage);
    }
}
