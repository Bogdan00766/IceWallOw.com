#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 7053

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IceWallOw.Api/IceWallOw.Api.csproj", "IceWallOw.Api/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["DbInfrastructure/DbInfrastructure.csproj", "DbInfrastructure/"]
RUN dotnet restore "IceWallOw.Api/IceWallOw.Api.csproj"
COPY . .
WORKDIR "/src/IceWallOw.Api"
RUN dotnet build "IceWallOw.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IceWallOw.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IceWallOw.Api.dll"]