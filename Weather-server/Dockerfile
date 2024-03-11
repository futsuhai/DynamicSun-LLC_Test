FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Weather-server.csproj", "./Weather-server/"]
RUN dotnet restore "Weather-server/Weather-server.csproj"
COPY . .
WORKDIR "/src/Weather-server"
RUN dotnet build "Weather-server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Weather-server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Weather-server.dll"]
