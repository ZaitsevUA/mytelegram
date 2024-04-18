namespace MyTelegram.Domain.Commands.Device;

public class CreateDeviceCommand(
    DeviceId aggregateId,
    RequestInfo requestInfo,
    long permAuthKeyId,
    long tempAuthKeyId,
    long userId,
    int apiId,
    string appName,
    string appVersion,
    long hash,
    bool officialApp,
    bool passwordPending,
    string deviceModel,
    string platform,
    string systemVersion,
    string systemLangCode,
    string langPack,
    string langCode,
    string ip,
    int layer)
    : RequestCommand2<DeviceAggregate, DeviceId, IExecutionResult>(aggregateId, requestInfo)
{
    public int ApiId { get; } = apiId;
    public string AppName { get; } = appName;
    public string AppVersion { get; } = appVersion;
    public string DeviceModel { get; } = deviceModel;
    public long Hash { get; } = hash;
    public string Ip { get; } = ip;
    public string LangCode { get; } = langCode;
    public string LangPack { get; } = langPack;
    public int Layer { get; } = layer;
    public bool OfficialApp { get; } = officialApp;
    public bool PasswordPending { get; } = passwordPending;
    public long PermAuthKeyId { get; } = permAuthKeyId;
    public string Platform { get; } = platform;
    public string SystemLangCode { get; } = systemLangCode;
    public string SystemVersion { get; } = systemVersion;
    public long TempAuthKeyId { get; } = tempAuthKeyId;
    public long UserId { get; } = userId;

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(RequestInfo.ReqMsgId);
        yield return BitConverter.GetBytes(PermAuthKeyId);
        yield return BitConverter.GetBytes(TempAuthKeyId);
        yield return BitConverter.GetBytes(Hash);
    }
}
