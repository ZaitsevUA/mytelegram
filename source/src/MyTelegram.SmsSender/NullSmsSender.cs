namespace MyTelegram.SmsSender;

public class NullSmsSender(ILogger<NullSmsSender> logger) : INullSmsSender
{
    public bool Enabled => true;
    public Task SendAsync(SmsMessage smsMessage)
    {
        logger.LogWarning("NullSMSSender: the code will not be sent.PhoneNumber:{To} Text:{Text}", smsMessage.PhoneNumber, smsMessage.Text);

        return Task.CompletedTask;
    }
}