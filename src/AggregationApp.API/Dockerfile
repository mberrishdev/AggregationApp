#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AggregationApp.API/AggregationApp.API.csproj", "src/AggregationApp.API/"]
COPY ["src/AggregationApp.Application/AggregationApp.Application.csproj", "src/AggregationApp.Application/"]
COPY ["src/AggregationApp.Domain/AggregationApp.Domain.csproj", "src/AggregationApp.Domain/"]
COPY ["src/AggregationApp.Infrastructure/AggregationApp.Infrastructure.csproj", "src/AggregationApp.Infrastructure/"]
COPY ["src/AggregationApp.Persistence/AggregationApp.Persistence.csproj", "src/AggregationApp.Persistence/"]
RUN dotnet restore "src/AggregationApp.API/AggregationApp.API.csproj"
COPY . .
WORKDIR "/src/src/AggregationApp.API"
RUN dotnet build "AggregationApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AggregationApp.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AggregationApp.API.dll"]