# NSA.Tinkoff.InvestApi

[![Build & test](https://github.com/nazarovsa/NSA.Tinkoff.InvestApi/actions/workflows/dotnet-build-and-test.yml/badge.svg)](https://github.com/nazarovsa/NSA.Tinkoff.InvestApi/actions/workflows/dotnet-build-and-test.yml)

## About
This is .Net API for interact with [Tinkoff Invest OpenApi](https://github.com/Tinkoff/investAPI). Please, write your questions and suggestions to issues.

Library uses GRPC to interact with Tinkoff API.

## Usage
In progress...

## Services 
- **IUsersService** - Information about user (accounts).
- **IInstrumentsService** - Information about instruments.
- **IOrdersService** - Post/Cancel/Get information about order.
- **IMarketDataStreamService** - Information about market in real time.

## Remarks
### Reason of services' methods async
If we return a task with GRPC result from a method, we may have RpcException at awaiting code.  
This approach allows throwing unified `ApiException` despite a little lack of performance.