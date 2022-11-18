#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Demo.WorkService/Demo.WorkService.csproj", "Demo.WorkService/"]
RUN dotnet restore "Demo.WorkService/Demo.WorkService.csproj"
COPY . .
WORKDIR "/src/Demo.WorkService"
RUN dotnet build "Demo.WorkService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.WorkService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.WorkService.dll"]