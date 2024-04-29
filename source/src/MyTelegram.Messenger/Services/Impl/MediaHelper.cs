using Google.Protobuf;

namespace MyTelegram.Messenger.Services.Impl;

public class MediaHelper(
    IOptionsMonitor<MyTelegramMessengerServerOptions> options,
    ILogger<MediaHelper> logger)
    : IMediaHelper
{
    public async Task<IEncryptedFile> SaveEncryptedFileAsync(long reqMsgId,
        IInputEncryptedFile encryptedFile)
    {
        var client = GrpcClientFactory.CreateMediaServiceClient(options.CurrentValue.FileServerGrpcServiceUrl);
        var r = await client
            .SaveEncryptedFileAsync(new SaveEncryptedFileRequest
            {
                EncryptedFile = ByteString.CopyFrom(encryptedFile.ToBytes()),
                ReqMsgId = reqMsgId
            }).ResponseAsync;

        return new TEncryptedFile
        {
            AccessHash = r.AccessHash,
            DcId = r.DcId,
            Id = r.Id,
            KeyFingerprint = r.KeyFingerprint,
            Size = r.Size
        };
    }

    public async Task<SavePhotoResult> SavePhotoAsync(long reqMsgId,
        long fileId,
        bool hasVideo,
        double? videoStartTs,
        int parts,
        string name,
        string md5)
    {
        var client = GrpcClientFactory.CreateMediaServiceClient(options.CurrentValue.FileServerGrpcServiceUrl);
        var r = await client.SavePhotoAsync(new SavePhotoRequest
        {
            FileId = fileId,
            HasVideo = hasVideo,
            Md5 = md5 ?? string.Empty,
            Name = name ?? string.Empty,
            Parts = parts,
            ReqMsgId = reqMsgId,
            VideoStartTs = videoStartTs ?? 0
        }).ResponseAsync;

        return new SavePhotoResult(r.PhotoId, r.Photo.Span.ToArray().ToTObject<IPhoto>());
    }

    public async Task<IMessageMedia> SaveMediaAsync(IInputMedia media)
    {
        try
        {
            var client = GrpcClientFactory.CreateMediaServiceClient(options.CurrentValue.FileServerGrpcServiceUrl);
            var r = await client.SaveMediaAsync(new SaveMediaRequest
            {
                Media = ByteString.CopyFrom(media.ToBytes())
            })
                .ResponseAsync;

            return r.Media.Span.ToArray().ToTObject<IMessageMedia>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Save media failed,serviceUrl={FileServerGrpcServiceUrl}",options.CurrentValue.FileServerGrpcServiceUrl);
            RpcErrors.RpcErrors400.FileIdInvalid.ThrowRpcError();
        }

        return new TMessageMediaEmpty();
    }

    public MessageType GeMessageType(IMessageMedia media)
    {
        if (media == null)
        {
            return MessageType.Text;
        }

        return media switch
        {
            TMessageMediaContact => MessageType.Contacts,
            TMessageMediaDice => MessageType.Game,
            TMessageMediaDocument => MessageType.Document,
            TMessageMediaEmpty => MessageType.Text,
            TMessageMediaGame => MessageType.Game,
            TMessageMediaGeo => MessageType.Geo,
            TMessageMediaGeoLive => MessageType.Geo,
            TMessageMediaInvoice => MessageType.Voice,
            TMessageMediaPhoto => MessageType.Photo,
            TMessageMediaPoll => MessageType.Poll,
            TMessageMediaUnsupported => MessageType.Text,
            TMessageMediaVenue => MessageType.Geo,
            TMessageMediaWebPage => MessageType.Url,
            _ => throw new ArgumentOutOfRangeException(nameof(media))
        };
    }
}
