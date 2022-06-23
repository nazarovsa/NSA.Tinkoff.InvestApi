# NSA.Tinkoff.InvestApi

[![Build & test](https://github.com/nazarovsa/NSA.Tinkoff.InvestApi/actions/workflows/dotnet-build-and-test.yml/badge.svg)](https://github.com/nazarovsa/NSA.Tinkoff.InvestApi/actions/workflows/dotnet-build-and-test.yml)

## Remarks
### Reason of services' methods async
If we return a task with GRPC result from a method, we may have RpcException at awaiting code.  
This approach allows throwing unified `ApiException` despite a little lack of performance.