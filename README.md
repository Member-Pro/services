# Member Pro Services

This repository contains the API services that power Member Pro. The API is built on ASP.NET 5 with a Postgres database.

## Local Development

To run Member Pro Services locally, you'll need the [.NET 5 SDK](https://dotnet.microsoft.com/download) and a Postgres
server (I am running Postgres in a [Docker container](https://hub.docker.com/_/postgres/) locally.)

_TODO: Document configuration... there's a lot..._

### Running Database Migrations

The APIs use EntityFramework Core (5.x) for the ORM and database migrations. To run migrations:

```bash
cd ./src/MemberPro.Api

dotnet ef database update
```

> Note: This assumes you have set the connection string in `appsettings.Development.json`.

### Running APIs

```bash
dotnet restore
dotnet build

cd ./src/MemberPro.Api
dotnet run

```

This will start the APIs at https://localhost:5001/

### API Docs

We use Swagger for API docs which can be accessed at https://localhost:5001/api-docs. Authentication is required for
most endpoints.
