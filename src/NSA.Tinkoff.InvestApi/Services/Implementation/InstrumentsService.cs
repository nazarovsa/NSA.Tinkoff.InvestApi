using Grpc.Core;
using NSA.Tinkoff.InvestApi.Contracts;
using NSA.Tinkoff.InvestApi.Exceptions;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public sealed class InstrumentsService : IInstrumentsService
{
    private readonly IInvestApiClient _client;

    public InstrumentsService(IInvestApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<Instrument?> GetInstrumentById(string id,
        InstrumentIdType idType,
        string? classCode,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var request = new InstrumentRequest()
            {
                Id = id,
                IdType = idType,
            };

            if (classCode != null)
            {
                request.ClassCode = classCode;
            }

            var result = _client.InstrumentsServiceClient.GetInstrumentByAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            var response =  await result.ResponseAsync;
            return response.Instrument;
        }
        catch (RpcException ex)
        {
            if (ex.StatusCode == StatusCode.NotFound)
                return null;
            
            throw new ApiException(ex);
        }
    }
}