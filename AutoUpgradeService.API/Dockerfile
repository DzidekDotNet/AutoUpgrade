FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AutoUpgradeService.API/AutoUpgradeService.API.csproj", "AutoUpgradeService.API/"]
RUN dotnet restore "AutoUpgradeService.API/AutoUpgradeService.API.csproj"
COPY . .
WORKDIR "/src/AutoUpgradeService.API"
RUN dotnet build "AutoUpgradeService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoUpgradeService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoUpgradeService.API.dll"]
