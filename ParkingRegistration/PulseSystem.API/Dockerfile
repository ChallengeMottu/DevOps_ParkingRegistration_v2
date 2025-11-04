# Etapa 1: Imagem base de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Usuário não root por segurança
RUN adduser --disabled-password --gecos "" appuser
USER appuser

# Etapa 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia apenas o .csproj primeiro (para otimizar cache)
COPY ["PulseSystem.API/PulseSystem.API.csproj", "PulseSystem.API/"]

# Se seu projeto tiver outras camadas, adicione as linhas abaixo:
COPY ["PulseSystem.Application/PulseSystem.Application.csproj", "PulseSystem.Application/"]
COPY ["PulseSystem.Domain/PulseSystem.Domain.csproj", "PulseSystem.Domain/"]
COPY ["PulseSystem.Infrastructure/PulseSystem.Infrastructure.csproj", "PulseSystem.Infrastructure/"]

# Restaura dependências
RUN dotnet restore "PulseSystem.API/PulseSystem.API.csproj"

# Copia o restante do código
COPY . .

# Define o diretório de build
WORKDIR "/src/PulseSystem.API"

# Compila o projeto
RUN dotnet build "PulseSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa 3: Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PulseSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa 4: Final — imagem de runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PulseSystem.API.dll"]
