#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Starwars.Grpc.Client/Starwars.Grpc.Client.csproj", "Starwars.Grpc.Client/"]
RUN dotnet restore "Starwars.Grpc.Client/Starwars.Grpc.Client.csproj"
COPY . .
WORKDIR "/src/Starwars.Grpc.Client"
RUN dotnet build "Starwars.Grpc.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Starwars.Grpc.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Starwars.Grpc.Client.dll"]