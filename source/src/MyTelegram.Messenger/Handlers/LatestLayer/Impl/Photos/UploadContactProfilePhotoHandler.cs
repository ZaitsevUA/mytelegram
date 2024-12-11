// ReSharper disable All

namespace MyTelegram.Handlers.Photos;

///<summary>
/// Upload a custom profile picture for a contact, or suggest a new profile picture to a contact.The <code>file</code>, <code>video</code> and <code>video_emoji_markup</code> flags are mutually exclusive.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/photos.uploadContactProfilePhoto" />
///</summary>
internal sealed class UploadContactProfilePhotoHandler : RpcResultObjectHandler<MyTelegram.Schema.Photos.RequestUploadContactProfilePhoto, MyTelegram.Schema.Photos.IPhoto>,
    Photos.IUploadContactProfilePhotoHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IMediaHelper _mediaHelper;
    private readonly IPeerHelper _peerHelper;

    public UploadContactProfilePhotoHandler(ICommandBus commandBus, IMediaHelper mediaHelper, IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _mediaHelper = mediaHelper;
        _peerHelper = peerHelper;
    }

    protected override async Task<MyTelegram.Schema.Photos.IPhoto> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Photos.RequestUploadContactProfilePhoto obj)
    {
        var file = obj.File ?? obj.Video;
        var md5 = string.Empty;
        var parts = 0;
        var name = string.Empty;
        switch (file)
        {
            case TInputFile inputFile:
                md5 = inputFile.Md5Checksum;
                name = inputFile.Name;
                break;
            case TInputFileBig inputFileBig:
                name = inputFileBig.Name;
                parts = inputFileBig.Parts;
                break;
        }

        VideoSizeEmojiMarkup? videoSizeEmojiMarkup = null;
        if (obj.VideoEmojiMarkup != null)
        {
            switch (obj.VideoEmojiMarkup)
            {
                case TVideoSizeEmojiMarkup videoSizeEmojiMarkup1:
                    videoSizeEmojiMarkup = new VideoSizeEmojiMarkup(videoSizeEmojiMarkup1.EmojiId,
                        videoSizeEmojiMarkup1.BackgroundColors.ToList());
                    break;
            }
        }

        var photoId = 0L;
        IPhoto? photo = null;
        if (file != null)
        {
            var r = file == null
                ? null
                : await _mediaHelper.SavePhotoAsync(input.ReqMsgId,
                    input.UserId,
                    file.GetFileId(),
                    obj.Video != null,
                    obj.VideoStartTs,
                    parts,
                    name,
                    md5 ?? string.Empty);
            photoId = r?.PhotoId ?? 0;
            photo = r?.Photo;
        }

        var peer = _peerHelper.GetPeer(obj.UserId);
        var command = new UpdateContactProfilePhotoCommand(
            ContactId.Create(input.UserId, peer.PeerId),
            input.ToRequestInfo(),
            input.UserId,
            peer.PeerId,
            photoId,
            obj.Suggest,
            !obj.Suggest ? null : new TMessageActionSuggestProfilePhoto
            {
                Photo = photo
            }.ToBytes().ToHexString()
        );

        await _commandBus.PublishAsync(command, default);

        return null!;
    }
}
