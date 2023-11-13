FROM mcr.microsoft.con/dotnet/sdk:6.0-focal as build
WORKDIR /source

COPY . .

RUN dotnet restore "./StockRestApi.Gateway/StockRestApi.Gateway.csproj" --disable-parallel
RUN dotnet publish "./StockRestApi.Gateway/StockRestApi.Gateway.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.con/dotnet/sdk:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT [ "dotnet", "StockRestApi.Gateway.dll"]