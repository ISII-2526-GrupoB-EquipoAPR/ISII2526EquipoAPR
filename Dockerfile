# Dockerfile con versión EXPLÍCITA 8.0.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0.100 AS build
WORKDIR /src
COPY ["AppForSEI12526.API.csproj", "."]
RUN dotnet restore "./AppForSEI12526.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./AppForSEI12526.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./AppForSEI12526.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppForSEI12526.API.dll"]