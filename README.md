# Event Sourcing with .NET

This .NET 10 learning repository demonstrates event persistence with Marten and PostgreSQL from a
hosted worker in a minimal ASP.NET Core application.

## Application

`src/MartenApp` exposes a simple HTTP endpoint and runs a background service that appends synthetic
temperature events to one Marten event stream. Marten creates the required PostgreSQL schema when
the application starts.

## Local Services

The Compose file provides PostgreSQL on port `5432` and pgAdmin on port `8080`:

```text
docker-compose up
```

The tracked Compose credentials are local defaults only. Supply non-default values with
`POSTGRES_USER`, `POSTGRES_PASSWORD`, `PGADMIN_DEFAULT_EMAIL` and
`PGADMIN_DEFAULT_PASSWORD` for any shared environment. Set the application connection string with
`ConnectionStrings__Marten`; do not place a real password in `appsettings.json`.

## Development

Open `event-sourcing-dotnet.slnx` with the .NET 10 SDK. The Dev Container provides .NET, pre-commit
and Docker access for Compose without upgrading packages or installing Git hooks at startup.

Run repository linting explicitly:

```text
pre-commit run --all-files
```

Restore and build commands require NuGet access and are intentionally left to the developer. The
[Marten documentation](https://martendb.io/) describes the event-store APIs used by the sample.

## Continuous Integration

Pull requests run reusable lint, version calculation and full-solution build jobs. Releases are
created from successful default-branch runs only.

## Data and Security

Generated PostgreSQL and pgAdmin data stays under the ignored `.docker/` directory. Do not commit
service data, connection strings or event payloads from real systems.