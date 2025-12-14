# Dockerfile para API (.NET 8.0)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar .csproj y restaurar dependencias
COPY ["AppForSEII2526.API.csproj", "./"]
RUN dotnet restore "AppForSEII2526.API.csproj"

# Copiar todo lo demás
COPY . .
WORKDIR "/src"

# Construir y publicar
RUN dotnet build "AppForSEII2526.API.csproj" -c Release -o /app/build
RUN dotnet publish "AppForSEII2526.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AppForSEII2526.API.dll"]