FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["intro.csproj", "./"]
RUN dotnet restore "intro.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "intro.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "intro.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "intro.dll"]
