FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ./VL-Api-Server/Source/VisitorLog.csproj .
RUN dotnet restore VisitorLog.csproj
COPY ./VL-Api-Server/Source/. .
RUN dotnet publish VisitorLog.csproj -c Release -o /VisitorLog
RUN dotnet dev-certs https -ep /VisitorLog/kestral-cert.pfx -p password

FROM node:15-alpine as node
WORKDIR /web
RUN apk --no-cache add python3 make g++
COPY ./VL-Web-Client/. .
RUN npm install
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=build /VisitorLog/. .
RUN mkdir /cert && mv kestral-cert.pfx /cert
COPY --from=node /web/build/. ./wwwroot
ENTRYPOINT ["dotnet", "VisitorLog.dll"]