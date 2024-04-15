// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/folders">folders</a>
/// See <a href="https://corefork.telegram.org/method/messages.getDialogFilters" />
///</summary>
internal sealed class GetDialogFiltersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDialogFilters, MyTelegram.Schema.Messages.IDialogFilters>,
    Messages.IGetDialogFiltersHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IObjectMapper _objectMapper;
    public GetDialogFiltersHandler(IQueryProcessor queryProcessor,
        IObjectMapper objectMapper)
    {
        _queryProcessor = queryProcessor;
        _objectMapper = objectMapper;
    }

    protected override async Task<MyTelegram.Schema.Messages.IDialogFilters> HandleCoreAsync(IRequestInput input,
        RequestGetDialogFilters obj)
    {
        var filterReadModels = await _queryProcessor.ProcessAsync(new GetDialogFiltersQuery(input.UserId), default)
            ;

        var filters = new TVector<IDialogFilter>();
        foreach (var filterReadModel in filterReadModels)
        {
            var filter = _objectMapper.Map<DialogFilter, TDialogFilter>(filterReadModel.Filter);
            filters.Add(filter);
        }

        if (filters.Count == 0)
        {
            filters.Add(new TDialogFilterDefault());
        }

        return new TDialogFilters
        {
            Filters = filters,
            TagsEnabled = true,
        };
    }
}