FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Roboline.WebApi/Roboline.WebApi.csproj", "src/Roboline.WebApi/"]
COPY ["src/Roboline.Persistence/Roboline.Persistence.csproj", "src/Roboline.Persistence/"]
COPY ["src/Roboline.Domain/Roboline.Domain.csproj", "src/Roboline.Domain/"]
COPY ["src/Roboline.Service/Roboline.Service.csproj", "src/Roboline.Service/"]
RUN dotnet restore "src/Roboline.WebApi/Roboline.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Roboline.WebApi"
RUN dotnet build "Roboline.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Roboline.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Roboline.WebApi.dll"]
