// ReSharper disable All

namespace MyTelegram.Messenger.Handlers;
public abstract class ForwardRequestToNewHandler<TInput, TNewInput, TOutput>(IHandlerHelper handlerHelper) : BaseObjectHandler<TInput, TOutput>
    where TInput : IRequest<TOutput>
    where TNewInput : IRequest<TOutput>
    where TOutput : IObject
{
    protected override async Task<TOutput> HandleCoreAsync(IRequestInput request, TInput obj)
    {
        var newRequestData = GetNewData(request, obj);
        if (newRequestData.ConstructorId == obj.ConstructorId)
        {
            throw new InvalidOperationException("The new and old handlers cannot be the same");
        }

        if (handlerHelper.TryGetHandler(newRequestData.ConstructorId, out var handler))
        {
            var result = await handler.HandleAsync(request, GetNewData(request, obj));

            return (TOutput)result;
        }

        throw new NotImplementedException();
    }

    protected abstract TNewInput GetNewData(IRequestInput request, TInput obj);
}
