FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

RUN adduser --disabled-password --gecos "" appuser
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PulseSystem.API/PulseSystem.API.csproj", "PulseSystem.API/"]
COPY ["../PulseSystem.Application/PulseSystem.Application.csproj", "PulseSystem.Application/"]
COPY ["../PulseSystem.Domain/PulseSystem.Domain.csproj", "PulseSystem.Domain/"]
COPY ["../PulseSystem.Infrastructure/PulseSystem.Infrastructure.csproj", "PulseSystem.Infrastructure/"]

RUN dotnet restore "PulseSystem.API/PulseSystem.API.csproj"
COPY .. .
WORKDIR "/src/PulseSystem.API"
RUN dotnet build "PulseSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PulseSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PulseSystem.API.dll"]
