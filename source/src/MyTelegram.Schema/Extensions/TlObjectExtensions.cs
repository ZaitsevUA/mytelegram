using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Schema.Extensions;

public sealed class ArrayPoolBufferWriterWrapper<T>(ArrayBufferWriter<T> writer) : IDisposable
{
    public ArrayBufferWriter<T> Writer => writer;

    public int WrittenCount => writer.WrittenCount;

    public void Dispose()
    {
        writer.Clear();
        ArrayBufferWriterPool<T>.Return(this);
    }
}
public class ArrayBufferWriterPool : ArrayBufferWriterPool<byte> { }

public class ArrayBufferWriterPool<T>
{
    private static readonly ConcurrentQueue<ArrayPoolBufferWriterWrapper<T>> Queue = new();

    public static ArrayPoolBufferWriterWrapper<T> Rent(int initialCapacity = 1024)
    {
        if (Queue.TryDequeue(out var writer))
        {
            return writer;
        }
        return new ArrayPoolBufferWriterWrapper<T>(new ArrayBufferWriter<T>(1024));
    }

    public static void Return(ArrayPoolBufferWriterWrapper<T> writerWrapper)
    {
        writerWrapper.Writer.Clear();
        Queue.Enqueue(writerWrapper);
    }
}

public static class TlObjectExtensions
{
    public static long GetFileId(this IInputFile? file)
    {
        switch (file)
        {
            case null:
                return 0;
            case TInputFile inputFile:
                return inputFile.Id;
            case TInputFileBig inputFileBig:
                return inputFileBig.Id;
            case TInputFileStoryDocument inputFileStoryDocument:
                switch (inputFileStoryDocument.Id)
                {
                    case TInputDocument inputDocument:
                        return inputDocument.Id;
                    case TInputDocumentEmpty:
                        return 0;
                }
                break;
        }

        return 0;
    }

    //private static readonly BytesSerializer BytesSerializer = new();
    //[return: NotNullIfNotNull("obj")]
    //public static byte[]? ToBytes(this IObject? obj)
    //{
    //    if (obj == null)
    //    {
    //        return null;
    //    }

    //    var stream = new MemoryStream();
    //    var bw = new BinaryWriter(stream);
    //    obj.Serialize(bw);

    //    return stream.ToArray();
    //}

    [return:NotNullIfNotNull(nameof(inputReplyTo))]
    public static int? ToReplyToMsgId(this IInputReplyTo? inputReplyTo)
    {
        switch (inputReplyTo)
        {
            case TInputReplyToMessage inputReplyToMessage:
                return inputReplyToMessage.ReplyToMsgId;
            case TInputReplyToStory inputReplyToStory:
                return inputReplyToStory.StoryId;
        }

        return null;
    }

    public static int GetLength(this IObject? obj)
    {
        if (obj == null)
        {
            return 0;
        }

        var writer = ArrayBufferWriterPool.Rent();
        int count;
        try
        {
            obj.Serialize(writer.Writer);
            count = writer.WrittenCount;
        }
        finally
        {
            ArrayBufferWriterPool.Return(writer);
        }

        return count;
    }

    [return: NotNullIfNotNull("obj")]
    public static byte[]? ToBytes(this IObject? obj)
    {
        if (obj == null)
        {
            return null;
        }
        var writer = ArrayBufferWriterPool.Rent();

        try
        {
            obj.Serialize(writer.Writer);
            var bytes = writer.Writer.WrittenSpan.ToArray();

            return bytes;
        }
        finally
        {
            ArrayBufferWriterPool.Return(writer);
        }
    }

    public static TObject? ToTObject<TObject>(this ReadOnlyMemory<byte> readOnlyMemory) where TObject : IObject
    {
        if (readOnlyMemory.Length > 0)
        {
            var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(readOnlyMemory));
            return reader.Read<TObject>();
        }

        return default;
    }

    [return: NotNullIfNotNull("bytes")]
    public static TObject? ToTObject<TObject>(this byte[]? bytes) where TObject : IObject
    {
        return ToTObject<TObject>(readOnlyMemory: bytes);
    }
}