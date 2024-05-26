FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Recipes.API/Recipes.API.csproj", "Recipes.API/"]
COPY ["Recipes.Business/Recipes.Business.csproj", "Recipes.Business/"]
COPY ["Recipes.Public/Recipes.Public.csproj", "Recipes.Public/"]
COPY ["Recipes.DataAccess/Recipes.DataAccess.csproj", "Recipes.DataAccess/"]
RUN dotnet restore "Recipes.API/Recipes.API.csproj"
COPY . .
WORKDIR /src/Recipes.API
RUN dotnet build "Recipes.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Recipes.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recipes.API.dll"]