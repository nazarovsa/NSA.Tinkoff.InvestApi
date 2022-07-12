# NSA.Tinkoff.InvestApi

[![Build & test](https://github.com/nazarovsa/NSA.Tinkoff.InvestApi/actions/workflows/dotnet-build-and-test.yml/badge.svg)](https://github.com/nazarovsa/NSA.Tinkoff.InvestApi/actions/workflows/dotnet-build-and-test.yml)
[![nuget version](https://img.shields.io/nuget/v/NSA.Tinkoff.InvestApi)](https://www.nuget.org/packages/NSA.Tinkoff.InvestApi/)
[![Nuget](https://img.shields.io/nuget/dt/NSA.Tinkoff.InvestApi?color=%2300000)](https://www.nuget.org/packages/NSA.Tinkoff.InvestApi/)
## About
This is .Net API for interact with [Tinkoff Invest OpenApi](https://github.com/Tinkoff/investAPI). Please, write your questions and suggestions to issues.

Library uses GRPC to interact with Tinkoff API.

## Usage
In progress...

## Services 
- **IUsersService** - Information about user (accounts).
- **IInstrumentsService** - Information about instruments.
- **IOrdersService** - Post/Cancel/Get information about order.
- **IOperationsService** - Get information about operations.
- **IMarketDataStreamService** - Information about market in real time.

## Contributing
In progress...

## Remarks
### Reason of services' methods async
If we return a task with GRPC result from a method, we may have RpcException at awaiting code.  
This approach allows throwing unified `ApiException` despite a little lack of performance.
