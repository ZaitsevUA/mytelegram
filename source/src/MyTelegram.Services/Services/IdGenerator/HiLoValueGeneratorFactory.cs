namespace MyTelegram.Services.Services.IdGenerator;

public class HiLoValueGeneratorFactory(
    ILogger<DefaultHiLoValueGenerator> logger,
    IHiLoHighValueGenerator highValueGenerator)
    : IHiLoValueGeneratorFactory
{
    public HiLoValueGenerator<long> Create(HiLoValueGeneratorState state)
    {
        return new DefaultHiLoValueGenerator(state, logger, highValueGenerator);
    }
}
