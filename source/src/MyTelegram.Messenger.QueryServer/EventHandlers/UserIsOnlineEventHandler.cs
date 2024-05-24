namespace MyTelegram.Messenger.QueryServer.EventHandlers;

public class UserIsOnlineEventHandler(
    ILogger<UserIsOnlineEventHandler> logger,
    IObjectMessageSender objectMessageSender,
    IScheduleAppService scheduleAppService)
    : IEventHandler<UserIsOnlineEvent>
{
    public async Task HandleEventAsync(UserIsOnlineEvent eventData)
    {
        logger.LogInformation("User {UserId} is online,tempAuthKeyId={TempAuthKeyId:x2},permAuthKeyId={PermAuthKeyId:x2}",
            eventData.UserId,
            eventData.TempAuthKeyId,
            eventData.PermAuthKeyId);

        await scheduleAppService.ExecuteAsync(() =>
        {
            var updatesTooLong = new TUpdatesTooLong();
            objectMessageSender.PushSessionMessageToAuthKeyIdAsync(eventData.TempAuthKeyId, updatesTooLong);
        },
            TimeSpan.FromMilliseconds(1500));
    }
}
