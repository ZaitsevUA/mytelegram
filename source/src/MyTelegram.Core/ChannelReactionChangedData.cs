namespace MyTelegram.Core;

public record ChannelReactionChangedData(Dictionary<long, LayeredData<byte[]>> LayeredData);