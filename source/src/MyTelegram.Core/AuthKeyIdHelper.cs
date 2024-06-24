using System.Buffers.Binary;

namespace MyTelegram.Core;

public class AuthKeyIdHelper(IHashHelper hashHelper) : IAuthKeyIdHelper //, ISingletonDependency
{
    public long GetAuthKeyId(byte[] authKey)
    {
        var shaHash = hashHelper.Sha1(authKey);
        //var auxHash = BitConverter.ToUInt64(shaHash, 0);

        //return BitConverter.ToInt64(shaHash, 8 + 4);

        return BinaryPrimitives.ReadInt64LittleEndian(shaHash.AsSpan(8 + 4));
    }
}

//public record RequestInput
//{
//    //public RequestInput()
//    //{
//    //}
//    //[JsonConstructor]
//    public RequestInput(string clientIp,
//        string connectionId,
//        long userId,
//        long permAuthKeyId,
//        long authKeyId,
//        byte[] authKeyData,
//        byte[] serverSalt,
//        long requestSessionId,
//        long messageId,
//        int seqNumber,
//        uint objectId,
//        //bool isTempAuthKey,
//        bool isAuthKeyActive //,
//        //DeviceType deviceType
//    )
//    {
//        ClientIp = clientIp;
//        ConnectionId = connectionId;
//        UserId = userId;
//        PermAuthKeyId = permAuthKeyId;
//        AuthKeyId = authKeyId;
//        AuthKeyData = authKeyData;
//        ServerSalt = serverSalt;
//        RequestSessionId = requestSessionId;
//        MessageId = messageId;
//        SeqNumber = seqNumber;
//        ObjectId = objectId;
//        //IsTempAuthKey = isTempAuthKey;
//        IsAuthKeyActive = isAuthKeyActive;
//        //DeviceType = deviceType;
//    }

//    public long UserId { get; set; }
//    public string ClientIp { get; set; }
//    public string ConnectionId { get; set; }
//    public long PermAuthKeyId { get; set; }
//    public long AuthKeyId { get; set; }
//    public long RequestSessionId { get; set; }
//    public long MessageId { get; set; }
//    public int SeqNumber { get; set; }

//    public uint ObjectId { get; set; }

//    //public bool IsTempAuthKey { get; }
//    public bool IsAuthKeyActive { get; }
//    //public DeviceType DeviceType { get; }

//    public byte[] AuthKeyData { get; set; }
//    public byte[] ServerSalt { get; set; }

//    //public bool IsPermAuthKey => PermAuthKeyId == AuthKeyId;
//    //public 
//}

//public class RsaHelper : IRsaHelper//, ISingletonDependency
//{
//    private readonly IMyRsaHelper _myRsaHelper;

//    public RsaHelper(IMyRsaHelper myRsaHelper)
//    {
//        _myRsaHelper = myRsaHelper;
//    }
//    //private readonly IHashHelper _hashHelper;

//    //public RsaHelper(IHashHelper hashHelper)
//    //{
//    //    _hashHelper = hashHelper;
//    //}

//    //public Memory<byte> Decrypt(ReadOnlySpan<byte> encryptedSpan,
//    //    string privateKey)
//    //{
//    //    var sw = Stopwatch.StartNew();
//    //    var rsa = new RsaEngine();
//    //    using var reader = new StringReader(privateKey);
//    //    var keyParameter = (RsaPrivateCrtKeyParameters)new PemReader(reader).ReadObject();
//    //    rsa.Init(false, keyParameter);
//    //    var decryptedBytes = rsa.ProcessBlock(encryptedSpan.ToArray(), 0, encryptedSpan.Length);
//    //    sw.Stop();
//    //    encryptedSpan.Dump("encryptedSpan");
//    //    decryptedBytes.Dump("decryptedBytes");
//    //    return decryptedBytes;
//    //}

//    //public long GetFingerprint(string publicKeyWithFormat)
//    //{
//    //    var rsa = RSA.Create();
//    //    rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKeyWithFormat.RemoveRsaKeyFormat()), out _);
//    //    var p = rsa.ExportParameters(false);

//    //    var memory = new MemoryStream();
//    //    var bw = new BinaryWriter(memory);
//    //    //bw.Write(p.Modulus);
//    //    //bw.Write(p.Exponent);
//    //    Serialize(p.Modulus, bw);
//    //    Serialize(p.Exponent, bw);
//    //    var data = memory.ToArray();

//    //    var hash = _hashHelper.Sha1(data);

//    //    return BinaryPrimitives.ReadInt64LittleEndian(hash.AsSpan(hash.Length - 8));
//    //    //return BitConverter.ToInt64(hash.ToArray(), hash.Length - 8);
//    //}

//    //private static void Serialize(byte[] value,
//    //    BinaryWriter writer)
//    //{
//    //    int padding;
//    //    if (value.Length < 254)
//    //    {
//    //        padding = (value.Length + 1) % 4;
//    //        writer.Write((byte)value.Length);
//    //        writer.Write(value);
//    //    }
//    //    else
//    //    {
//    //        padding = value.Length % 4;
//    //        writer.Write((byte)254);
//    //        writer.Write((byte)value.Length);
//    //        writer.Write((byte)(value.Length >> 8));
//    //        writer.Write((byte)(value.Length >> 16));
//    //        writer.Write(value);
//    //    }

//    //    if (padding != 0) padding = 4 - padding;

//    //    for (var i = 0; i < padding; i++) writer.Write((byte)0);
//    //}
//    public Memory<byte> Decrypt(ReadOnlySpan<byte> encryptedSpan,
//        string privateKey)
//    {
//        return _myRsaHelper.Decrypt(encryptedSpan, privateKey);
//    }

//    //public long GetFingerprint(string publicKeyWithFormat)
//    //{
//    //    return _myRsaHelper.GetFingerprint(publicKeyWithFormat);
//    //}
//}