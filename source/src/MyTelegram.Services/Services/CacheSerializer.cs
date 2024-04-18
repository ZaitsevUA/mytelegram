using System.Text;
using EventFlow.Core;
using MyTelegram.Core;

namespace MyTelegram.Services.Services;

public class CacheSerializer(IJsonSerializer jsonSerializer) : ICacheSerializer
{
    public byte[] Serialize<T>(T obj)
    {
        return Encoding.UTF8.GetBytes(jsonSerializer.Serialize(obj));
    }

    public T Deserialize<T>(byte[] bytes)
    {
        return jsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(bytes));
    }
}
