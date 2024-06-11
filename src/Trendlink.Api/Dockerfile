FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Trendlink.Api/Trendlink.Api.csproj", "src/Trendlink.Api/"]
RUN dotnet restore "./src/Trendlink.Api/Trendlink.Api.csproj"
COPY . .
WORKDIR "/src/src/Trendlink.Api"
RUN dotnet build "./Trendlink.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Trendlink.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trendlink.Api.dll"]