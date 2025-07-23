# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MinervaFoods.Api/MinervaFoods.Api.csproj", "src/MinervaFoods.Api/"]
COPY ["src/MinervaFoods.IoC/MinervaFoods.IoC.csproj", "src/MinervaFoods.IoC/"]
COPY ["src/MinervaFoods.Application/MinervaFoods.Application.csproj", "src/MinervaFoods.Application/"]
COPY ["src/MinervaFoods.Domain/MinervaFoods.Domain.csproj", "src/MinervaFoods.Domain/"]
COPY ["src/MinervaFoods.Helpers/MinervaFoods.Helpers.csproj", "src/MinervaFoods.Helpers/"]
COPY ["src/MinervaFoods.Data/MinervaFoods.Data.csproj", "src/MinervaFoods.Data/"]
RUN dotnet restore "./src/MinervaFoods.Api/MinervaFoods.Api.csproj"
COPY . .
WORKDIR "/src/src/MinervaFoods.Api"
RUN dotnet build "./MinervaFoods.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MinervaFoods.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinervaFoods.Api.dll"]