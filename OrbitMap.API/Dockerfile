FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["OrbitMap.API/OrbitMap.API.csproj", "OrbitMap.API/"]
COPY ["OrbitMap.Domain/OrbitMap.Domain.csproj", "OrbitMap.Domain/"]
COPY ["OrbitMap.Repository/OrbitMap.Repository.csproj", "OrbitMap.Repository/"]
RUN dotnet restore "OrbitMap.API/OrbitMap.API.csproj"
COPY . .
WORKDIR "OrbitMap.API"
RUN dotnet build "OrbitMap.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrbitMap.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrbitMap.API.dll"]