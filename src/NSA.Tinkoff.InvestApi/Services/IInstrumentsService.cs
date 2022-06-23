using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public interface IInstrumentsService
{
    Task<Instrument?> GetInstrumentById(string id,
        InstrumentIdType idType,
        string? classCode,
        CancellationToken cancellationToken = default);
}