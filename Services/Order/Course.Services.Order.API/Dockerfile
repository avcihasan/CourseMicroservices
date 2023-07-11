#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Order/Course.Services.Order.API/Course.Services.Order.API.csproj", "Services/Order/Course.Services.Order.API/"]
COPY ["Services/Order/Course.Services.Order.Application/Course.Services.Order.Application.csproj", "Services/Order/Course.Services.Order.Application/"]
COPY ["Shared/Course.Shared/Course.Shared.csproj", "Shared/Course.Shared/"]
COPY ["Services/Order/Course.Services.Order.Domain/Course.Services.Order.Domain.csproj", "Services/Order/Course.Services.Order.Domain/"]
COPY ["Services/Order/Course.Services.Order.Domain.Core/Course.Services.Order.Domain.Core.csproj", "Services/Order/Course.Services.Order.Domain.Core/"]
COPY ["Services/Order/Course.Services.Order.Infrastructure/Course.Services.Order.Infrastructure.csproj", "Services/Order/Course.Services.Order.Infrastructure/"]
RUN dotnet restore "Services/Order/Course.Services.Order.API/Course.Services.Order.API.csproj"
COPY . .
WORKDIR "/src/Services/Order/Course.Services.Order.API"
RUN dotnet build "Course.Services.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Course.Services.Order.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Course.Services.Order.API.dll"]