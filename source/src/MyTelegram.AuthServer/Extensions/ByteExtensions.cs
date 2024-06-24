//namespace MyTelegram.AuthServer.Extensions;

//public static class BytesExtensions
//{
//    public static byte[] ToBytes256(this byte[] data)
//    {
//        if (data.Length == 256)
//        {
//            return data;
//        }

//        if (data.Length > 256)
//        {
//            throw new ArgumentException("Data length must be less than 256");
//        }

//        var newData = new byte[256];
//        data.CopyTo(newData, 256 - data.Length);

//        return newData;
//    }

//    public static BigInteger ToBigEndianBigInteger(this byte[] data)
//    {
//        return new BigInteger(data, true, true);
//    }
//}