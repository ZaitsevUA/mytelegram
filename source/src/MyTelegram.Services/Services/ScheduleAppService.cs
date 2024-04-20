using HWT;
using MyTelegram.Core;

namespace MyTelegram.Services.Services;

public class ScheduleAppService(IRandomHelper randomHelper) : IScheduleAppService //, ISingletonDependency
{
    private readonly HashedWheelTimer _timer = new(TimeSpan.FromMilliseconds(100),
        100000,
        0);

    public long Execute(Action action,
        TimeSpan timeSpan)
    {
        _timer.NewTimeout(new ActionTimeTask(action), timeSpan);
        return randomHelper.NextLong();
    }

    public Task ExecuteAsync(Action action,
        TimeSpan timeSpan)
    {
        _timer.NewTimeout(new ActionTimeTask(action), timeSpan);
        return Task.CompletedTask;
    }
}