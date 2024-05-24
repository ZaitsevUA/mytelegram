namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IDocumentConverter : ILayeredConverter
{
    IDocument ToDocument(IDocumentReadModel documentReadModel);
}