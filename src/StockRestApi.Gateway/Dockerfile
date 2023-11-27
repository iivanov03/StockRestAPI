ARG CSPROJ_FILE

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /source

COPY . .

RUN dotnet restore "./StockRestApi.Gateway/StockRestApi.Gateway.csproj" --disable-parallel
RUN dotnet publish "./StockRestApi.Gateway/StockRestApi.Gateway.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT [ "dotnet", "StockRestApi.Gateway.dll"]