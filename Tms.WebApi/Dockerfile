FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Tms.WebApi/Tms.WebApi.csproj", "Tms.WebApi/"]
COPY ["Tms.Services/Tms.Services.csproj", "Tms.Services/"]
COPY ["Tms.Db/Tms.Db.csproj", "Tms.Db/"]
COPY ["Tms.Common/Tms.Common.csproj", "Tms.Common/"]

RUN dotnet restore "Tms.WebApi/Tms.WebApi.csproj"
COPY . .
WORKDIR "/src/Tms.WebApi"
RUN dotnet build "./Tms.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tms.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tms.WebApi.dll"]
