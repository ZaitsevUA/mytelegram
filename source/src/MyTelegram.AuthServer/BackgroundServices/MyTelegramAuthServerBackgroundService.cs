namespace MyTelegram.AuthServer.BackgroundServices;

public class MyTelegramAuthServerBackgroundService(
    ILogger<MyTelegramAuthServerBackgroundService> logger,
    IHandlerHelper handlerHelper)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        handlerHelper.InitAllHandlers(typeof(MyTelegramAuthServerExtensions).Assembly, 5);
        logger.LogInformation("My telegram auth server init ok");

        return Task.CompletedTask;
    }
}