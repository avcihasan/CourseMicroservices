#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Clients/Course.Web/Course.Web.csproj", "Clients/Course.Web/"]
COPY ["Shared/Course.Shared/Course.Shared.csproj", "Shared/Course.Shared/"]
RUN dotnet restore "Clients/Course.Web/Course.Web.csproj"
COPY . .
WORKDIR "/src/Clients/Course.Web"
RUN dotnet build "Course.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Course.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Course.Web.dll"]