FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

RUN adduser --disabled-password --gecos "" appuser
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia os csproj individuais (de todas as camadas)
COPY ["ParkingRegistration/PulseSystem.API/PulseSystem.API.csproj", "PulseSystem.API/"]
COPY ["ParkingRegistration/PulseSystem.Application/PulseSystem.Application.csproj", "PulseSystem.Application/"]
COPY ["ParkingRegistration/PulseSystem.Domain/PulseSystem.Domain.csproj", "PulseSystem.Domain/"]
COPY ["ParkingRegistration/PulseSystem.Infrastructure/PulseSystem.Infrastructure.csproj", "PulseSystem.Infrastructure/"]

# Restaura as dependências
RUN dotnet restore "PulseSystem.API/PulseSystem.API.csproj"

# Copia tudo
COPY . .

WORKDIR "/src/PulseSystem.API"
RUN dotnet build "PulseSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PulseSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PulseSystem.API.dll"]
