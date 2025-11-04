FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

RUN adduser --disabled-password --gecos "" appuser
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo .csproj (que está na mesma pasta do Dockerfile)
COPY ["PulseSystem.API.csproj", "./"]

# Restaura dependências
RUN dotnet restore "PulseSystem.API.csproj"

# Copia o restante dos arquivos
COPY . .

# Compila
RUN dotnet build "PulseSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PulseSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PulseSystem.API.dll"]
