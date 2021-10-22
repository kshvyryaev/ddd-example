# Run application

## Install docker

## Install dotnet-ef as global tool
`dotnet tool install --global dotnet-ef`

## Update dotnet-ef as global tool
`dotnet tool update --global dotnet-ef`

## Run sql server docker
`docker run -d --name sql-server -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Strong@Passw0rd" -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest`

## Apply sql migrations
`dotnet ef database update -p "DddExample.Infrastructure" -- --connection "Server=localhost;Database=SbtSoftwareAccounting;User Id=SA;Password=Strong@Passw0rd;"`

## Run jaeger docker
`docker run -d --name jaeger -e COLLECTOR_ZIPKIN_HOST_PORT=:9411 -p 5775:5775/udp -p 6831:6831/udp -p 6832:6832/udp -p 5778:5778 -p 16686:16686 -p 14268:14268 -p 14250:14250 -p 9411:9411 jaegertracing/all-in-one:1.23`

## Run jaeger ui
http://localhost:16686