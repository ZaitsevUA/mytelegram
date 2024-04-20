namespace MyTelegram.Domain.Aggregates.Photo;

public class PhotoAggregate(PhotoId id) : AggregateRoot<PhotoAggregate, PhotoId>(id);