using System.Text.Json.Serialization;

namespace MyTelegram.GatewayServer.NativeAot;

[JsonSerializable(typeof(MTProto.EncryptedMessage),TypeInfoPropertyName = nameof(MTProto.EncryptedMessage))]
[JsonSerializable(typeof(MTProto.EncryptedMessageResponse), TypeInfoPropertyName = nameof(MTProto.EncryptedMessageResponse))]
[JsonSerializable(typeof(MTProto.UnencryptedMessage), TypeInfoPropertyName = nameof(MTProto.UnencryptedMessage))]
[JsonSerializable(typeof(MTProto.UnencryptedMessageResponse), TypeInfoPropertyName = nameof(MTProto.UnencryptedMessageResponse))]

[JsonSerializable(typeof(MyTelegram.Core.EncryptedMessage), TypeInfoPropertyName = nameof(MTProto.EncryptedMessage))]
[JsonSerializable(typeof(MyTelegram.Core.EncryptedMessageResponse), TypeInfoPropertyName = nameof(MTProto.EncryptedMessageResponse))]
[JsonSerializable(typeof(MyTelegram.Core.UnencryptedMessage), TypeInfoPropertyName = nameof(MTProto.UnencryptedMessage))]
[JsonSerializable(typeof(MyTelegram.Core.UnencryptedMessageResponse), TypeInfoPropertyName = nameof(MTProto.UnencryptedMessageResponse))]

[JsonSerializable(typeof(AuthKeyNotFoundEvent))]
[JsonSerializable(typeof(MyTelegram.Core.ClientDisconnectedEvent))]

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]

public partial class GatewayServerJsonContext : JsonSerializerContext
{
}