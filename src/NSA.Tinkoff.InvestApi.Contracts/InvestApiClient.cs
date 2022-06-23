using Grpc.Core;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Contracts;

public sealed class InvestApiClient : IInvestApiClient
{
    public UsersService.UsersServiceClient UsersServiceClient { get; }

    public InvestApiClient(CallInvoker callInvoker)
    {
        if (callInvoker == null)
        {
            throw new ArgumentNullException(nameof(callInvoker));
        }

        UsersServiceClient = new UsersService.UsersServiceClient(callInvoker);
    }
}