namespace MyTelegram.Domain.Commands.Poll;

public class CreatePollCommand(
    PollId aggregateId,
    Peer toPeer,
    long pollId,
    bool multipleChoice,
    bool quiz,
    bool publicVoters,
    string question,
    IReadOnlyCollection<PollAnswer> answers,
    IReadOnlyCollection<string>? correctAnswers,
    string? solution,
    byte[]? solutionEntities,
    byte[]? questionEntities
    )
    : Command<PollAggregate, PollId, IExecutionResult>(aggregateId)
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
    public byte[]? QuestionEntities { get; } = questionEntities;
}