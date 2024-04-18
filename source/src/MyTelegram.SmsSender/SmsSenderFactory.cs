namespace MyTelegram.SmsSender;

public class SmsSenderFactory(IEnumerable<ISmsSender> smsSenderList)
    : ISmsSenderFactory
{
    private readonly List<ISmsSender> _smsSenderList = smsSenderList.ToList();

    public ISmsSender Create(string phoneNumber)
    {
        var smsSenders = _smsSenderList.Where(p => p.Enabled).ToList();
        if (smsSenders.Count == 0)
        {
            throw new InvalidOperationException("No SMS sender available");
        }
        var index = Random.Shared.Next(0, smsSenders.Count);

        return _smsSenderList[index];
    }
}