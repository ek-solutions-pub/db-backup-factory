FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["db-backup-factory/db-backup-factory.csproj", "db-backup-factory/"]
RUN dotnet restore "db-backup-factory/db-backup-factory.csproj"
COPY . .
WORKDIR "/src/db-backup-factory"
RUN dotnet build "db-backup-factory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "db-backup-factory.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "db-backup-factory.dll"]
