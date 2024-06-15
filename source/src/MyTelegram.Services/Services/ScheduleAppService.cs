using HWT;
using Timeout = HWT.Timeout;

namespace MyTelegram.Services.Services;
public interface IScheduleAppService
{
    Task<Timeout> ExecuteAsync(Action action, TimeSpan timeSpan);
}

public class ScheduleAppService : IScheduleAppService
{

    private readonly HashedWheelTimer _timer = new(TimeSpan.FromMilliseconds(100),
        100000,
        0);

    public Task<Timeout> ExecuteAsync(Action action, TimeSpan timeSpan)
    {
        var timeout = _timer.NewTimeout(new ActionTimeTask(action), timeSpan);

        return Task.FromResult(timeout);
    }
}