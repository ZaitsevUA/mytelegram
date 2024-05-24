namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class DocumentConverterLatest : IDocumentConverterLatest
{
    public int Layer => Layers.LayerLatest;
    public int RequestLayer { get; set; }

    public IDocument ToDocument(IDocumentReadModel m)
    {
        return new TDocument
        {
            Id = m.DocumentId,
            AccessHash = m.AccessHash,
            Date = m.Date,
            MimeType = m.MimeType,
            Size = m.Size,
            DcId = m.DcId,
            FileReference = m.FileReference,
            Attributes = m.Attributes.ToTObject<TVector<IDocumentAttribute>>(),
            Thumbs = m.Thumbs == null
                ? null
                : new TVector<IPhotoSize>(m.Thumbs.Select(p => new TPhotoSize
                {
                    H = p.H,
                    W = p.W,
                    Size = (int)p.Size,
                    Type = p.Type
                })),
            VideoThumbs = m.VideoThumbs == null
                ? null
                : new TVector<IVideoSize>(m.VideoThumbs.Select(p => new TVideoSize
                {
                    H = p.H,
                    W = p.W,
                    Size = (int)p.Size,
                    Type = p.Type,
                    VideoStartTs = p.VideoStartTs
                }))
        };
    }
}