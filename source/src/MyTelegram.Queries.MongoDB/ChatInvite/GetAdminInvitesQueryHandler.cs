namespace MyTelegram.QueryHandlers.MongoDB.ChatInvite;

public class GetAdminInvitesQueryHandler(IQueryOnlyReadModelStore<ChatInviteReadModel> store) : IQueryHandler<GetAdminInvitesQuery, IReadOnlyCollection<AdminWithInvites>>
{
    private readonly IQueryOnlyReadModelStore<ChatInviteReadModel> _store = store;

    public async Task<IReadOnlyCollection<AdminWithInvites>> ExecuteQueryAsync(GetAdminInvitesQuery query, CancellationToken cancellationToken)
    {
        //var fluent = await _store
        //    .AggregateAsync(p => p.PeerId == query.ChannelId, p => p.AdminId,
        //        g => new AdminWithInvites(g.Key, g.Count(), g.Count(x => x.Revoked)), cancellationToken: cancellationToken);

        //return await fluent.ToListAsync(cancellationToken);
        return new List<AdminWithInvites>();
    }
}