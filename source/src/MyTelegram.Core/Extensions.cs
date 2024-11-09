using System.Numerics;
using System.Reflection;

namespace MyTelegram.Core;

public static class Extensions
{
    public static void RegisterServices(this IServiceCollection services, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();

        var singletonBaseInterface = typeof(ISingletonDependency);
        var transientBaseInterface = typeof(ITransientDependency);

        var types = assembly.GetTypes()
            .Where(p => p != singletonBaseInterface && p != transientBaseInterface && !p.IsAbstract)
            .ToList()
            ;
        var singletonTypes = types.Where(singletonBaseInterface.IsAssignableFrom);
        var transientTypes = types.Where(transientBaseInterface.IsAssignableFrom);

        foreach (var type in singletonTypes)
        {
            var baseInterfaces = type.GetInterfaces();
            foreach (var baseInterface in baseInterfaces)
            {
                services.AddSingleton(baseInterface, type);
            }

            services.AddSingleton(type);
        }

        foreach (var type in transientTypes)
        {
            var baseInterfaces = type.GetInterfaces();
            foreach (var baseInterface in baseInterfaces)
            {
                services.AddTransient(baseInterface, type);
            }

            services.AddTransient(type);
        }
    }

    public static int ToInt32(this uint value)
    {
        return BitConverter.ToInt32(BitConverter.GetBytes(value));
    }

    public static long ToInt64(this ulong value)
    {
        return BitConverter.ToInt64(BitConverter.GetBytes(value));
    }

    public static byte[] ToBytes256(this byte[] data)
    {
        if (data.Length == 256)
        {
            return data;
        }

        if (data.Length > 256)
        {
            throw new ArgumentException("Data length must be less than 256");
        }

        var newData = new byte[256];
        data.CopyTo(newData, 256 - data.Length);

        return newData;
    }

    public static BigInteger ToBigEndianBigInteger(this byte[] data)
    {
        return new BigInteger(data, true, true);
    }

    public static string ToBase64Url(this Span<byte> buffer)
    {
        return Convert.ToBase64String(buffer).ToBase64Url();
    }


    public static string ToBase64Url(this byte[] buffer)
    {
        return Convert.ToBase64String(buffer).ToBase64Url();
    }

    public static string ToBase64Url(this string base64Data)
    {
        return base64Data
            .Replace("=", string.Empty)
            .Replace("/", "_")
            .Replace("+", "-");
    }

    private static byte[] HexToBytes(string hex)
    {
        var text = hex.Replace(" ", string.Empty).Replace("\r\n", string.Empty).Replace("\n", string.Empty);
        return StringToByteArray(text);
    }

    public static byte[] StringToByteArray(string hex)
    {
        return Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
    }

    public static byte[] ToByteArray(this BitArray bitArray)
    {
        var bytes = new byte[(bitArray.Length - 1) / 8 + 1];
        bitArray.CopyTo(bytes, 0);

        return bytes;
    }

    public static byte[] ToBytes(this string hex)
    {
        return HexToBytes(hex);
    }

    public static string RemoveRsaKeyFormat(this string key)
    {
        return key
            .Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "")
            .Replace("-----BEGIN RSA PUBLIC KEY-----", "").Replace("-----END RSA PUBLIC KEY-----", "")
            .Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "")
            .Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "")
            .Replace(Environment.NewLine, "");
    }


    public static string ToHexString(this byte[] buffer)
    {
        return BitConverter.ToString(buffer).Replace("-", string.Empty);
    }

    public static int ToInt(this BitArray bitArray)
    {
        return BitConverter.ToInt32(ToByteArray(bitArray));
    }

    public static string ToPhoneNumber(this string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return string.Empty;
        }

        return phoneNumber.Replace("+", string.Empty).Replace(" ", string.Empty);
    }
}