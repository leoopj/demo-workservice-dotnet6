#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NewGO-Integration-Senior/NewGO-Integration-Senior.csproj", "NewGO-Integration-Senior/"]
RUN dotnet restore "NewGO-Integration-Senior/NewGO-Integration-Senior.csproj"
COPY . .
WORKDIR "/src/NewGO-Integration-Senior"
RUN dotnet build "NewGO-Integration-Senior.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewGO-Integration-Senior.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewGO-Integration-Senior.dll"]