﻿// Resharper Disable All
namespace MyTelegram.ReadModel.Impl;

public class DocumentReadModel : IDocumentReadModel,
    IAmReadModelFor<DocumentAggregate, DocumentId, EmptyDocumentEvent>
{
    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<DocumentAggregate, DocumentId, EmptyDocumentEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public long AccessHash { get; private set; }
    public byte[]? Attributes { get; private set; }
    public long? CreatorId { get; private set; }
    public int Date { get; private set; }
    public int DcId { get; private set; }
    public long DocumentId { get; private set; }
    public byte[] FileReference { get; private set; } = default!;
    public int? Fingerprint { get; private set; }
    public string Id { get; private set; } = default!;
    public string? Md5CheckSum { get; private set; }
    public string? Name { get; private set; }
    public string MimeType { get; private set; } = string.Empty;
    public long Size { get; private set; }
    public byte[]? Stickers { get; private set; }
    public long? ThumbId { get; private set; }
    public List<PhotoSize>? Thumbs { get; private set; }
    public long? Version { get; set; }
    public long? VideoThumbId { get; private set; }
    public List<VideoSize>? VideoThumbs { get; private set; }
}