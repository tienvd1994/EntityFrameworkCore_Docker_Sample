##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["EntityFrameworkCoreSample/EntityFrameworkCoreSample.csproj", "EntityFrameworkCoreSample/"]
#RUN dotnet restore "EntityFrameworkCoreSample/EntityFrameworkCoreSample.csproj"
#COPY . .
#WORKDIR "/src/EntityFrameworkCoreSample"
#RUN dotnet build "EntityFrameworkCoreSample.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "EntityFrameworkCoreSample.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "EntityFrameworkCoreSample.dll"]
#

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY EntityFrameworkCoreSample/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY EntityFrameworkCoreSample/. ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "EntityFrameworkCoreSample.dll"]