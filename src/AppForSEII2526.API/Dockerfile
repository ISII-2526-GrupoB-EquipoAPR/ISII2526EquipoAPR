FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AppForSEII2526.API.csproj", "./"]
RUN dotnet restore "AppForSEII2526.API.csproj"
COPY . .
RUN dotnet build "AppForSEII2526.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppForSEII2526.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppForSEII2526.API.dll"]