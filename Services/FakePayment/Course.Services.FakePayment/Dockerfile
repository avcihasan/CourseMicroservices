#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/FakePayment/Course.Services.FakePayment/Course.Services.FakePayment.csproj", "Services/FakePayment/Course.Services.FakePayment/"]
COPY ["Shared/Course.Shared/Course.Shared.csproj", "Shared/Course.Shared/"]
RUN dotnet restore "Services/FakePayment/Course.Services.FakePayment/Course.Services.FakePayment.csproj"
COPY . .
WORKDIR "/src/Services/FakePayment/Course.Services.FakePayment"
RUN dotnet build "Course.Services.FakePayment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Course.Services.FakePayment.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Course.Services.FakePayment.dll"]