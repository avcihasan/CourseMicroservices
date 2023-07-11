FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Catalog/Course.Services.Catalog/Course.Services.Catalog.csproj", "Services/Catalog/Course.Services.Catalog/"]
COPY ["Shared/Course.Shared/Course.Shared.csproj", "Shared/Course.Shared/"]
RUN dotnet restore "Services/Catalog/Course.Services.Catalog/Course.Services.Catalog.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Course.Services.Catalog"
RUN dotnet build "Course.Services.Catalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Course.Services.Catalog.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Course.Services.Catalog.dll"]