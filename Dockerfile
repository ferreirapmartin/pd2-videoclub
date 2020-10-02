#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY ["src/ApiRest/ApiRest.csproj", "src/ApiRest/"]
RUN dotnet restore "src/ApiRest/ApiRest.csproj"
COPY . .
WORKDIR "/src/src/ApiRest"
RUN dotnet build "ApiRest.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR /src
RUN dotnet test --logger html -r TestResults

FROM build AS publish
RUN dotnet publish "ApiRest.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","ApiRest.dll"]