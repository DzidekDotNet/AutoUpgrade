FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AutoUpgradeService.Service/AutoUpgradeService.Service.csproj", "AutoUpgradeService.Service/"]
RUN dotnet restore "AutoUpgradeService.Service/AutoUpgradeService.Service.csproj"
COPY . .
WORKDIR "/src/AutoUpgradeService.Service"
RUN dotnet build "AutoUpgradeService.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoUpgradeService.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoUpgradeService.Service.dll"]
