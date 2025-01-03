﻿using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Text;
using EventFlow.Exceptions;

namespace MyTelegram.EventFlow;

public class MyInMemoryEventPersistence(ILogger<MyInMemoryEventPersistence> logger)
    : IInMemoryEventPersistence, IDisposable
{
    private readonly AsyncLock _asyncLock = new();
    private readonly ConcurrentDictionary<string, ImmutableList<InMemoryCommittedDomainEvent>> _eventStore = new();

    public void Dispose()
    {
        _asyncLock.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<IReadOnlyCollection<ICommittedDomainEvent>> CommitEventsAsync(
        IIdentity id,
        IReadOnlyCollection<SerializedEvent> serializedEvents,
        CancellationToken cancellationToken)
    {
        if (serializedEvents.Count == 0)
        {
            return [];
        }

        using (await _asyncLock.WaitAsync(cancellationToken).ConfigureAwait(false))
        {
            var globalCount = _eventStore.Values.Sum(events => events.Count);

            var newCommittedDomainEvents = serializedEvents
                .Select((e,
                    i) =>
                {
                    var committedDomainEvent = new InMemoryCommittedDomainEvent
                    {
                        AggregateId = id.Value,
                        AggregateName = e.Metadata[MetadataKeys.AggregateName],
                        AggregateSequenceNumber = e.AggregateSequenceNumber,
                        Data = e.SerializedData,
                        Metadata = e.SerializedMetadata,
                        GlobalSequenceNumber = globalCount + i + 1
                    };
                    logger.LogTrace("Committing event {CommittedEvent}", committedDomainEvent);
                    return committedDomainEvent;
                })
                .ToList();

            var expectedVersion = newCommittedDomainEvents.First().AggregateSequenceNumber - 1;
            var lastEvent = newCommittedDomainEvents.Last();

            var updateResult = _eventStore.AddOrUpdate(id.Value,
                _ => ImmutableList<InMemoryCommittedDomainEvent>.Empty.AddRange(newCommittedDomainEvents),
                (_,
                    collection) => collection.Count == expectedVersion
                    ? collection.AddRange(newCommittedDomainEvents)
                    : collection);

            if (updateResult.Last() != lastEvent)
            {
                throw new OptimisticConcurrencyException(string.Empty);
            }

            return newCommittedDomainEvents;
        }
    }

    public Task<IReadOnlyCollection<ICommittedDomainEvent>> LoadCommittedEventsAsync(IIdentity id,
        int fromEventSequenceNumber, int toEventSequenceNumber,
        CancellationToken cancellationToken)
    {
        return LoadCommittedEventsAsync(
            id,
            fromEventSequenceNumber,
            e => e.AggregateSequenceNumber >= fromEventSequenceNumber &&
                 e.AggregateSequenceNumber <= toEventSequenceNumber);
    }

    public Task DeleteEventsAsync(IIdentity id,
        CancellationToken cancellationToken)
    {
        var deleted = _eventStore.TryRemove(id.Value, out var committedDomainEvents);

        if (deleted)
        {
            logger.LogTrace(
                "Deleted entity with ID {Id} by deleting all of its {EventCount} events",
                id,
                committedDomainEvents!.Count);
        }

        // _logger.LogInformation("Current count:{Count},removed count:{RemovedCount}", _eventStore.Count, committedDomainEvents?.Count);
        return Task.FromResult(0);
    }

    public Task<AllCommittedEventsPage> LoadAllCommittedEvents(
        GlobalPosition globalPosition,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var startPosition = globalPosition.IsStart
            ? 0
            : long.Parse(globalPosition.Value);

        var committedDomainEvents = _eventStore
            .SelectMany(kv => kv.Value)
            .Where(e => e.GlobalSequenceNumber >= startPosition)
            .OrderBy(e => e.GlobalSequenceNumber)
            .Take(pageSize)
            .ToList();

        var nextPosition = committedDomainEvents.Count > 0
            ? committedDomainEvents.Max(e => e.GlobalSequenceNumber) + 1
            : startPosition;

        return Task.FromResult(new AllCommittedEventsPage(new GlobalPosition(nextPosition.ToString()),
            committedDomainEvents));
    }

    public Task<IReadOnlyCollection<ICommittedDomainEvent>> LoadCommittedEventsAsync(
        IIdentity id,
        int fromEventSequenceNumber,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<ICommittedDomainEvent> result;

        if (_eventStore.TryGetValue(id.Value, out var committedDomainEvent))
        {
            result = fromEventSequenceNumber <= 1
                ? committedDomainEvent
                : committedDomainEvent.Where(e => e.AggregateSequenceNumber >= fromEventSequenceNumber).ToList();
        }
        else
        {
            result = new List<InMemoryCommittedDomainEvent>();
        }

        return Task.FromResult(result);
    }

    private Task<IReadOnlyCollection<ICommittedDomainEvent>> LoadCommittedEventsAsync(IIdentity id,
        int fromEventSequenceNumber, Func<InMemoryCommittedDomainEvent, bool> filter)
    {
        IReadOnlyCollection<ICommittedDomainEvent> result;

        if (_eventStore.TryGetValue(id.Value, out var committedDomainEvent))
        {
            result = fromEventSequenceNumber <= 1
                ? committedDomainEvent
                : committedDomainEvent.Where(filter).ToList();
        }
        else
        {
            result = new List<InMemoryCommittedDomainEvent>();
        }

        return Task.FromResult(result);
    }

    private class InMemoryCommittedDomainEvent : ICommittedDomainEvent
    {
        public string AggregateName { private get; init; } = default!;
        public long GlobalSequenceNumber { get; init; }
        public string AggregateId { get; init; } = default!;
        public int AggregateSequenceNumber { get; init; }
        public string Data { get; init; } = default!;
        public string Metadata { get; init; } = default!;

        private static string PrettifyJson(string json)
        {
            try
            {
                //var obj = JsonConvert.DeserializeObject(json);
                //var prettyJson = JsonConvert.SerializeObject(obj, Formatting.Indented);
                //JsonSerializer.Deserialize(json, typeof(object), new JsonSerializerOptions { WriteIndented = true });
                //return prettyJson;
                return json;
            }
            catch (Exception)
            {
                return json;
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLineFormat("{0} v{1} ==================================",
                    AggregateName,
                    AggregateSequenceNumber)
                .AppendLine(PrettifyJson(Metadata))
                .AppendLine("---------------------------------")
                .AppendLine(PrettifyJson(Data))
                .Append("---------------------------------")
                .ToString();
        }
    }
}