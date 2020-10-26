FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY VisitorLog.csproj .
RUN dotnet restore VisitorLog.csproj
COPY . .
RUN dotnet publish VisitorLog.csproj -c Release -o /VisitorLog

FROM base AS final
WORKDIR /app
COPY --from=build /VisitorLog .
ENTRYPOINT ["dotnet", "VisitorLog.dll"]