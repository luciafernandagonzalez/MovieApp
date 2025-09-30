# Imagen base de .NET runtime para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagen SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MovieApp.API/MovieApp.API.csproj", "MovieApp.API/"]
COPY ["MovieApp.Application/MovieApp.Application.csproj", "MovieApp.Application/"]
COPY ["MovieApp.Domain/MovieApp.Domain.csproj", "MovieApp.Domain/"]
COPY ["MovieApp.Infrastructure/MovieApp.Infrastructure.csproj", "MovieApp.Infrastructure/"]

RUN dotnet restore "MovieApp.API/MovieApp.API.csproj"

COPY . .
WORKDIR "/src/MovieApp.API"
RUN dotnet build "MovieApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieApp.API.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieApp.API.dll"]