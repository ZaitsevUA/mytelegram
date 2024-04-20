namespace MyTelegram.SmsSender;

public class SmsSenderFactory(ILogger<SmsSenderFactory> logger, IEnumerable<ISmsSender> smsSenderList, INullSmsSender nullSmsSender)
    : ISmsSenderFactory
{
    private readonly List<ISmsSender> _smsSenderList = smsSenderList.ToList();

    public ISmsSender Create(string phoneNumber)
    {
        if (_smsSenderList.All(p => !p.Enabled))
        {
            logger.LogWarning("All SMS sender disabled,SMS will not be sent");
            return nullSmsSender;
        }
        var index = Random.Shared.Next(0, _smsSenderList.Count);

        return _smsSenderList[index];
    }
}