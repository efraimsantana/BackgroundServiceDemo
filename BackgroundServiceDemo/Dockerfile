FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BackgroundServiceDemo/BackgroundServiceDemo.csproj", "BackgroundServiceDemo/"]
RUN dotnet restore "BackgroundServiceDemo/BackgroundServiceDemo.csproj"
COPY . .
WORKDIR "/src/BackgroundServiceDemo"
RUN dotnet build "./BackgroundServiceDemo.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BackgroundServiceDemo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackgroundServiceDemo.dll"]
