FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FrontEnd/FrontEnd.Web/FrontEnd.Web.csproj", "FrontEnd/FrontEnd.Web/"]
COPY ["Recipes/Recipes.Public/Recipes.Public.csproj", "Recipes/Recipes.Public/"]
COPY ["Identity/Identity.Public/Identity.Public.csproj", "Identity/Identity.Public/"]
RUN dotnet restore "FrontEnd/FrontEnd.Web/FrontEnd.Web.csproj"
COPY FrontEnd/ FrontEnd/
COPY Recipes/Recipes.Public/ Recipes/Recipes.Public/
COPY Identity/Identity.Public/ Identity/Identity.Public/
WORKDIR "/src/FrontEnd/FrontEnd.Web"
RUN dotnet build "FrontEnd.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FrontEnd.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FrontEnd.Web.dll"]
