#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sd.Crm.Backend/Sd.Crm.Backend.csproj", "Sd.Crm.Backend/"]
RUN dotnet restore "./Sd.Crm.Backend/./Sd.Crm.Backend.csproj"
COPY . .
WORKDIR "/src/Sd.Crm.Backend"
RUN dotnet build "./Sd.Crm.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sd.Crm.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

ENV ASPNETCORE_ENVIRONMENT Production
ENV DATABASE_HOST localhost
ENV DATABASE_PASSWORD Str0ngP@ssw0rd!
ENV DATABASE_LOGIN sa
ENV DATABASE_NAME Sd
ENV ASPNETCORE_URLS http://+:5000

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sd.Crm.Backend.dll"]