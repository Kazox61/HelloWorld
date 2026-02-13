FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . ./
WORKDIR /app/Server
RUN dotnet publish -c Release -o /app/ServerApp/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/ServerApp/publish /app/Server

EXPOSE 1987
CMD ["./Server/Server"]