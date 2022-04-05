ARG BuildConfiguration=Debug

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
ARG BuildConfiguration
WORKDIR /app

COPY src/Application/*.csproj src/Application/
COPY src/Domain/*.csproj src/Domain/
COPY src/EventsDispatching/*.csproj src/EventsDispatching/
COPY src/Persistence/*.csproj src/Persistence/
COPY src/Presentation.WebApi/*.csproj src/Presentation.WebApi/
RUN dotnet restore src/Presentation.WebApi/NetCoreManualDI.WebApi.csproj

COPY src src
RUN dotnet publish src/Presentation.WebApi/NetCoreManualDI.WebApi.csproj -c $BuildConfiguration -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "NetCoreManualDI.WebApi.dll"]
