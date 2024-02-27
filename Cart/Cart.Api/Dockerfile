#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cart.Api/Cart.Api.csproj", "Cart.Api/"]
COPY ["Cart.Application/Cart.Application.csproj", "Cart.Application/"]
COPY ["Cart.Domain/Cart.Domain.csproj", "Cart.Domain/"]
COPY ["Cart.Infrastructure/Cart.Infrastructure.csproj", "Cart.Infrastructure/"]
RUN dotnet restore "./Cart.Api/./Cart.Api.csproj"
COPY . .
WORKDIR "/src/Cart.Api"
RUN dotnet build "./Cart.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cart.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.Api.dll"]