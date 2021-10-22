# Migrations commands

## Preparation
### Install dotnet-ef as global tool (https://docs.microsoft.com/en-us/ef/core/cli/dotnet)
`dotnet tool install --global dotnet-ef`

## Create migration
`dotnet ef migrations add MigrationName -p "DddExample.Infrastructure" -o "Data/Migrations"`

## Remove migration
`dotnet ef migrations remove --force`

## Update database
`dotnet ef database update -p "DddExample.Infrastructure" -- --connection "Server=localhost;Database=BooksDatabase;User Id=SA;Password=Strong@Passw0rd;"`