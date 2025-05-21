FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base

WORKDIR /src

COPY ["Tms.WebApi/Tms.WebApi.csproj", "Tms.WebApi/"]
COPY ["Tms.Db/Tms.Db.csproj", "Tms.Db/"]

COPY . .

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

CMD dotnet ef database update --project Tms.Db/Tms.Db.csproj --startup-project Tms.WebApi/Tms.WebApi.csproj
