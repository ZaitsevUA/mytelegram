namespace MyTelegram.Domain.Events.Poll;

public class PollCreatedEvent(
    Peer toPeer,
    long pollId,
    bool multipleChoice,
    bool quiz,
    bool publicVoters,
    string question,
    IReadOnlyCollection<PollAnswer> answers,
    IReadOnlyCollection<string>? correctAnswers,
    string? solution,
    byte[]? solutionEntities)
    : AggregateEvent<PollAggregate, PollId>
{
    public Peer ToPeer { get; } = toPeer;
    public long PollId { get; } = pollId;
    public bool MultipleChoice { get; } = multipleChoice;
    public bool Quiz { get; } = quiz;
    public bool PublicVoters { get; } = publicVoters;
    public string Question { get; } = question;
    public IReadOnlyCollection<PollAnswer> Answers { get; } = answers;
    public IReadOnlyCollection<string>? CorrectAnswers { get; } = correctAnswers;
    public string? Solution { get; } = solution;
    public byte[]? SolutionEntities { get; } = solutionEntities;
}
