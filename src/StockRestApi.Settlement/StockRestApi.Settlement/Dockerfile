#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StockRestApi.Settlement/StockRestApi.Settlement.csproj", "StockRestApi.Settlement/"]
RUN dotnet restore "StockRestApi.Settlement/StockRestApi.Settlement.csproj"
COPY . .
WORKDIR "/src/StockRestApi.Settlement"
RUN dotnet build "StockRestApi.Settlement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockRestApi.Settlement.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockRestApi.Settlement.dll"]