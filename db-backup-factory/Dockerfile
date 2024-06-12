FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


RUN pwd
RUN ls -l
RUN echo "Force cache bust" && ls -l

COPY ["dbf-api/dbf-api.csproj", "dbf-api/"]
RUN dotnet restore "dbf-api/dbf-api.csproj"
COPY . .
WORKDIR "/src/dbf-api"
RUN dotnet build "dbf-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dbf-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dbf-api.dll"]
