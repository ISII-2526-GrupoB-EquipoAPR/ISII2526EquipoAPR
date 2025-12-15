# Dockerfile para LogViewer (.NET 8.0)
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AppForSEII2526.LogViewer.csproj", "./"]
RUN dotnet restore "AppForSEII2526.LogViewer.csproj"
COPY . .
RUN dotnet build "AppForSEII2526.LogViewer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppForSEII2526.LogViewer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppForSEII2526.LogViewer.dll"]