namespace MyTelegram.AuthServer.Services;

public class Step3Helper(
    IAesHelper aesHelper,
    IHashHelper hashHelper,
    IMtpHelper mtpHelper,
    ILogger<Step3Helper> logger,
    IAuthKeyIdHelper authKeyIdHelper,
    ICacheManager<AuthCacheItem> cacheManager)
    : Step1To3Helper, IStep3Helper, ISingletonDependency
{
    public async Task<Step3Output> SetClientDhParamsAnswerAsync(RequestSetClientDHParams req)
    {
        var cacheKey = GetAuthCacheKey(req.ServerNonce);
        var cachedAuthKey = await cacheManager.GetAsync(cacheKey);
        if (cachedAuthKey?.A == null)
        {
            throw new InvalidOperationException(
                $"Cannot find cached auth key info, nonce: {req.Nonce.ToHexString()}");
        }

        if (cachedAuthKey.NewNonce == null)
        {
            throw new ArgumentNullException(nameof(cachedAuthKey.NewNonce));
        }

        CheckRequestData(cachedAuthKey.Nonce, req.Nonce, "Nonce");
        CheckRequestData(cachedAuthKey.ServerNonce, req.ServerNonce, "ServerNonce");
        var tempAesKeyData = mtpHelper.CalcTempAesKeyData(cachedAuthKey.NewNonce, cachedAuthKey.ServerNonce);
        var dhInnerData = DeserializeRequest(req, tempAesKeyData);
        CheckRequestData(cachedAuthKey.Nonce, dhInnerData.Nonce, "Nonce");
        CheckRequestData(cachedAuthKey.ServerNonce, dhInnerData.ServerNonce, "ServerNonce");
        var a = cachedAuthKey.A;
        var gb = dhInnerData.GB;

        var authKeyBytes = BigInteger.ModPow(
                gb.ToBigEndianBigInteger(),
                a.ToBigEndianBigInteger(),
                AuthConsts.DhPrime)
            .ToByteArray(true, true)
            .ToBytes256();

        var dto = new Step3Output(authKeyIdHelper.GetAuthKeyId(authKeyBytes),
            authKeyBytes,
            mtpHelper.ComputeSalt(cachedAuthKey.NewNonce, dhInnerData.ServerNonce),
            cachedAuthKey.IsPermanent,
            CreateDhGenOkAnswer(req, cachedAuthKey.NewNonce, authKeyBytes),
            cachedAuthKey.DcId
            );

        return dto;
    }

    private TClientDHInnerData DeserializeRequest(RequestSetClientDHParams serverDhParams,
        AesKeyData aesKeyData)
    {
        var answerWithHash = aesHelper.DecryptIge(serverDhParams.EncryptedData, aesKeyData.Key, aesKeyData.Iv);
        var hash = answerWithHash[..20];
        var answer = answerWithHash[20..];
        var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(answer));
        var obj = reader.Read<TClientDHInnerData>();
        var paddingCount = (int)(answer.Length - reader.Consumed);
        var data = answer[..^paddingCount];
        var calcHash = hashHelper.Sha1(data);
        if (!hash.SequenceEqual(calcHash))
        {
            logger.LogWarning("Answer sha1 hash mismatch, client hash: {RequestHash}, server calculated hash: {ServerCalculatedHash}", hash.ToHexString(), calcHash.ToHexString());

            throw new ArgumentException(
                $"Answer sha1 hash mismatch, client hash: {hash.ToHexString()}, server calculated hash: {calcHash.ToHexString()}");
        }

        return obj;
    }

    private TDhGenOk CreateDhGenOkAnswer(RequestSetClientDHParams req,
        byte[] newNonce,
        byte[] authKey)
    {
        var newNonceHash1 = CreateNewNonceHash(newNonce, authKey, 1);

        return new TDhGenOk
        {
            Nonce = req.Nonce,
            ServerNonce = req.ServerNonce,
            NewNonceHash1 = newNonceHash1
        };
    }

    //private TDhGenRetry CreateDhGenRetryRetryAnswer(RequestSetClientDHParams req, byte[] newNonce, byte[] authKey)
    //{
    //    var newNonceHash2 = CreateNewNonceHash(newNonce, authKey, 2);

    //    return new TDhGenRetry
    //    {
    //        Nonce = req.Nonce,
    //        ServerNonce = req.ServerNonce,
    //        NewNonceHash2 = newNonceHash2
    //    };
    //}

    private byte[] CreateNewNonceHash(byte[] newNonce,
        byte[] authKey, byte n)
    {
        // https://core.telegram.org/mtproto/auth_key#9-server-responds-in-one-of-three-ways
        // new_nonce_hash1, new_nonce_hash2, and new_nonce_hash3 are obtained as the 128 lower - order bits of SHA1 of
        // the byte string derived from the new_nonce string by adding a single byte with the value of 1, 2, or 3, and followed
        // by another 8 bytes with auth_key_aux_hash.Different values are required to prevent an intruder from changing server
        // response dh_gen_ok into dh_gen_retry.

        var authKeyAuxHash = SHA1.HashData(authKey).AsSpan(0, 8);
        // 256+1+8
        Span<byte> newNonceWithAuxHashBytes = stackalloc byte[newNonce.Length + 1 + 8];
        newNonce.CopyTo(newNonceWithAuxHashBytes);
        newNonceWithAuxHashBytes[newNonce.Length] = n;
        authKeyAuxHash.CopyTo(newNonceWithAuxHashBytes[(newNonce.Length + 1)..]);
        var newNonceHashN = hashHelper.Sha1(newNonceWithAuxHashBytes);

        return newNonceHashN.AsSpan(4).ToArray();
    }
}