namespace MyTelegram.AuthServer.Services;

public class Step2Helper(
    ILogger<Step2Helper> logger,
    IHashHelper hashHelper,
    IAesHelper aesHelper,
    IMtpHelper mtpHelper,
    IMyRsaHelper myRsaHelper,
    ICacheManager<AuthCacheItem> cacheManager,
    IRsaKeyProvider rsaKeyProvider
) : Step1To3Helper, IStep2Helper, ISingletonDependency
{
    public async Task<Step2Output> GetServerDhParamsAsync(RequestReqDHParams req)
    {
        var cacheKey = GetAuthCacheKey(req.ServerNonce);
        var cachedAuthKey = await cacheManager.GetAsync(cacheKey);
        if (cachedAuthKey == null)
        {
            throw new InvalidOperationException(
                $"GetServerDhParamsAsync: can not find cached auth key info, nonce={req.Nonce.ToHexString()}"
            );
        }

        #region check request

        CheckRequestData(cachedAuthKey.Nonce, req.Nonce);
        CheckRequestData(cachedAuthKey.ServerNonce, req.ServerNonce);
        CheckRequestData(cachedAuthKey.P, req.P);
        CheckRequestData(cachedAuthKey.Q, req.Q);

        var tInnerData = DeserializeRequestTpqInnerData(req, rsaKeyProvider.GetRsaPrivateKey());
        CheckRequestData(cachedAuthKey.P, tInnerData.P);
        CheckRequestData(cachedAuthKey.Q, tInnerData.Q);
        CheckRequestData(cachedAuthKey.ServerNonce, tInnerData.ServerNonce);
        CheckRequestData(cachedAuthKey.Nonce, tInnerData.Nonce);

        #endregion check request

        var isPermanentAuthKey = false;
        int? dcId = null;
        switch (tInnerData)
        {
            case TPQInnerData:
                isPermanentAuthKey = true;
                break;
            case TPQInnerDataDc:
                isPermanentAuthKey = true;
                break;
            case TPQInnerDataTemp:

                break;
            case TPQInnerDataTempDc pqInnerDataTempDc:
                dcId = pqInnerDataTempDc.Dc;
                break;

            //default:
            //    throw new ArgumentOutOfRangeException(nameof(tInnerData));
        }

        var dh2048P = AuthConsts.Dh2048P;
        var g = AuthConsts.G;
        var aAndGa = GenerateAAndGa();

        var newCachedAuthKey = cachedAuthKey with
        {
            IsPermanent = isPermanentAuthKey,
            NewNonce = tInnerData.NewNonce,
            A = aAndGa.a,
            Ga = aAndGa.ga,
            DcId = dcId
        };

        var serverDhInnerData = new TServerDHInnerData
        {
            DhPrime = dh2048P,
            G = g[0],
            GA = aAndGa.ga,
            Nonce = cachedAuthKey.Nonce,
            ServerNonce = cachedAuthKey.ServerNonce,
            ServerTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        await cacheManager.SetAsync(cacheKey, newCachedAuthKey, 600);

        var serverDhParams = SerializeResponse(tInnerData, serverDhInnerData);

        return new Step2Output(tInnerData.NewNonce, serverDhParams);
    }

    private IPQInnerData DeserializeRequestTpqInnerData(
        RequestReqDHParams reqDhParams,
        string privateKey
    )
    {
        // It needs to be converted into a 256-byte array.
        // sometimes the auth key data length is only 255, and 0 needs to be added to the first position.
        var innerDataWithHash = myRsaHelper.Decrypt(reqDhParams.EncryptedData, privateKey);
        if (innerDataWithHash.Length == 256)
        {
            return ParsePqInnerData(innerDataWithHash);
        }

        return ParsePqInnerDataOld(innerDataWithHash);
    }

    private IPQInnerData ParsePqInnerDataOld(ReadOnlySpan<byte> innerDataWithHash)
    {
        var shaHash = innerDataWithHash[..20];
        var innerData = innerDataWithHash[20..];

        var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(innerData.ToArray()));
        var tPqInnerData = reader.Read<IPQInnerData>();
        var length = (int)reader.Consumed;
        var realInnerData = innerData[..length];

        var calculatedHash = hashHelper.Sha1(realInnerData.ToArray());
        if (!shaHash.SequenceEqual(calculatedHash))
        {
            logger.LogWarning(
                "PQInnerData hash mismatch, client sha1 hash: {RequestHash}, server calculated sha1 hash: {ServerCalculatedHash}",
                shaHash.ToArray().ToHexString(),
                calculatedHash.ToArray().ToHexString()
            );
        }

        return tPqInnerData;
    }

    private (byte[] a, byte[] ga) GenerateAAndGa()
    {
        var g = AuthConsts.G.ToBigEndianBigInteger();
        var dhPrime = AuthConsts.DhPrime;
        while (true)
        {
            var aBytes = RandomNumberGenerator.GetBytes(256);
            var a = aBytes.ToBigEndianBigInteger();

            var ga = BigInteger.ModPow(g, a, dhPrime);
            if (IsGoodGaOrGb(ga, dhPrime))
            {
                return (aBytes, ga.ToByteArray(true, true));
            }
        }
    }

    private IPQInnerData ParsePqInnerData(ReadOnlySpan<byte> keyAesEncryptedBytes)
    {
        const int tempKeyLength = 32;
        var tempKeyXor = keyAesEncryptedBytes[..tempKeyLength];
        var aesEncrypted = keyAesEncryptedBytes[tempKeyLength..];
        var aesEncryptedSha256Hash = hashHelper.Sha256(aesEncrypted);
        var tempKey = Xor(tempKeyXor, aesEncryptedSha256Hash);
        Span<byte> tempIv = stackalloc byte[tempKeyLength];
        var dataWithHash = aesHelper.DecryptIge(aesEncrypted, tempKey, tempIv);
        var dataPaddingReversed = dataWithHash[..^32];
        var hash = dataWithHash[^32..];
        dataPaddingReversed.AsSpan().Reverse();
        var dataWithPadding = dataPaddingReversed;
        var calculatedHash = hashHelper.Sha256(tempKey, dataWithPadding);
        if (!hash.SequenceEqual(calculatedHash))
        {
            logger.LogWarning(
                "PQInnerData hash mismatch, client sha1 hash: {RequestHash}, server calculated sha1 hash: {ServerCalculatedHash}",
                hash.ToHexString(),
                calculatedHash.ToHexString()
            );

            throw new ArgumentException("PQInnerData hash mismatch");
        }

        var tPqInnerData = dataWithPadding.ToTObject<IPQInnerData>();

        return tPqInnerData;
    }

    private TServerDHParamsOk SerializeResponse(
        IPQInnerData pqInnerData,
        TServerDHInnerData dhInnerData
    )
    {
        return SerializeResponse(
            pqInnerData.Nonce,
            pqInnerData.NewNonce,
            pqInnerData.ServerNonce,
            dhInnerData
        );
    }

    private TServerDHParamsOk SerializeResponse(
        byte[] nonce,
        byte[] newNonce,
        byte[] serverNonce,
        TServerDHInnerData dhInnerData
    )
    {
        var buffer = dhInnerData.ToBytes();
        var sha1Hash = hashHelper.Sha1(buffer);
        var totalLength = sha1Hash.Length + buffer.Length;
        var answerWithHashBytes = ArrayPool<byte>.Shared.Rent(totalLength);
        try
        {
            var answerWithHashSpan = answerWithHashBytes.AsSpan(0, totalLength);
            sha1Hash.CopyTo(answerWithHashSpan);
            buffer.CopyTo(answerWithHashSpan.Slice(sha1Hash.Length));
            var aesKeyData = mtpHelper.CalcTempAesKeyData(newNonce, serverNonce);
            var encryptedAnswer = aesHelper.EncryptIge(
                answerWithHashSpan,
                aesKeyData.Key,
                aesKeyData.Iv
            );
            return new TServerDHParamsOk
            {
                EncryptedAnswer = encryptedAnswer,
                Nonce = nonce,
                ServerNonce = serverNonce
            };
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(answerWithHashBytes);
        }
    }

    private byte[] Xor(ReadOnlySpan<byte> src, ReadOnlySpan<byte> dest)
    {
        var bytes = new byte[src.Length];
        for (var i = 0; i < src.Length; i++)
        {
            bytes[i] = (byte)(src[i] ^ dest[i]);
        }

        return bytes;
    }
}