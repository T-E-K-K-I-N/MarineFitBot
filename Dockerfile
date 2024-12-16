FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MarineFitBot/MarineFitBot.csproj", "MarineFitBot/"]
RUN dotnet restore "MarineFitBot/MarineFitBot.csproj"
COPY . .
WORKDIR "/src/MarineFitBot"
RUN dotnet build "MarineFitBot.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MarineFitBot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarineFitBot.dll"]
