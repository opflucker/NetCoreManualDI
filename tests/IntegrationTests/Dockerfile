ARG BuildConfiguration=Debug

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
ARG BuildConfiguration
WORKDIR /app

COPY src/Application/*.csproj src/Application/
COPY src/Domain/*.csproj src/Domain/
COPY src/EventsDispatching/*.csproj src/EventsDispatching/
COPY src/Persistence/*.csproj src/Persistence/
COPY tests/IntegrationTests/*.csproj tests/IntegrationTests/
RUN dotnet restore tests/IntegrationTests/NetCoreManualDI.IntegrationTests.csproj

COPY src src/
COPY tests/IntegrationTests tests/IntegrationTests/
RUN dotnet publish tests/IntegrationTests/NetCoreManualDI.IntegrationTests.csproj -c $BuildConfiguration -o out --no-restore

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .
