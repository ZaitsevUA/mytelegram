namespace MyTelegram;

public class RpcException : Exception
{
    public RpcException(RpcError rpcError) : base(rpcError.Message)
    {
        RpcError = rpcError;
    }

    public RpcException(string message, RpcError rpcError) : base(message)
    {
        RpcError = rpcError;
    }

    public RpcException(string message, Exception inner, RpcError rpcError) : base(message, inner)
    {
        RpcError = rpcError;
    }

    public RpcError RpcError { get; }
}