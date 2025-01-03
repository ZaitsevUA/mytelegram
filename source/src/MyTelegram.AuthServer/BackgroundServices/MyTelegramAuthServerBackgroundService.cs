namespace MyTelegram.AuthServer.BackgroundServices;

public class MyTelegramAuthServerBackgroundService(
    ILogger<MyTelegramAuthServerBackgroundService> logger,
    IHandlerHelper handlerHelper
) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        handlerHelper.InitAllHandlers();
        logger.LogInformation("MyTelegram auth server started");

        return Task.CompletedTask;
    }
}