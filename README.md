# Video Game Catalogue

A small full-stack application for browsing and editing a catalogue of video games.

- **Backend:** ASP.NET Core 8 (LTS), EF Core 8, SQL Server, Clean Architecture
- **Frontend:** Angular 17, standalone components, reactive forms, Bootstrap

## Solution layout

```
src/
  VideoGameCatalogue.Domain          Entities, domain exceptions, business invariants (no framework deps)
  VideoGameCatalogue.Application      Use cases, DTOs, validation, mapping, service & repository contracts
  VideoGameCatalogue.Infrastructure   EF Core DbContext, Fluent configuration, repository, migrations, seed
  VideoGameCatalogue.API              Thin controllers, exception-handling middleware, DI composition
client/                               Angular 17 SPA
tests/
  VideoGameCatalogue.UnitTests        xUnit tests for the domain, service and validators
```

Dependencies point inward: `API → Infrastructure → Application → Domain`. The Domain
project references nothing but the BCL.

## Running it

### API

```bash
cd src/VideoGameCatalogue.API
dotnet run
```

- Serves on `http://localhost:5031` (HTTP only, per the brief — no SSL).
- In Development it applies EF migrations on startup, so the LocalDB database is created
  and seeded automatically on first run.
- Swagger UI: `http://localhost:5031/swagger`.

The connection string lives in `appsettings.json` and targets LocalDB by default:
`Server=(localdb)\MSSQLLocalDB;Database=VideoGameCatalogue;...`.

To manage the schema manually instead:

```bash
dotnet tool restore
dotnet dotnet-ef database update --project src/VideoGameCatalogue.Infrastructure --startup-project src/VideoGameCatalogue.Infrastructure
```

### Frontend

```bash
cd client
npm install
npm start
```

Serves on `http://localhost:4200` and talks to the API at `http://localhost:5031/api`
(configurable in `src/environments/environment.ts`). CORS for this origin is enabled by the API.

### Tests

```bash
dotnet test
```

## API

| Method | Route                       | Purpose                          |
|--------|-----------------------------|----------------------------------|
| GET    | `/api/videogames?search=`   | List games, optional text filter |
| GET    | `/api/videogames/{id}`      | Get a single game                |
| PUT    | `/api/videogames/{id}`      | Update a game                    |

Errors are returned as RFC 7807 `ProblemDetails` (validation failures as
`ValidationProblemDetails`) produced by a single exception-handling middleware.

## Key decisions

- **Clean Architecture with the repository contract in the Application layer** so Infrastructure
  depends inward. A single focused `IVideoGameRepository` (the aggregate root) is used rather than
  a generic repository, which would leak `IQueryable`/EF concerns across the boundary.
- **Rich domain entity.** `VideoGame` enforces its own invariants through the constructor and
  `Update` method; there are no public setters, so an invalid game cannot exist in memory or be
  persisted. Business rules stay out of controllers, services and repositories.
- **Validation in two complementary places.** FluentValidation gives callers fast, field-level
  feedback on the request shape before the use case runs; the entity re-checks invariants as the
  domain's source of truth.
- **Manual mapping** (extension method) instead of AutoMapper — the model is small and explicit
  mapping is easier to read and debug. No mapping library earns its keep here.
- **Plain Bootstrap, not ng-bootstrap** — the UI has no interactive widget that would justify the
  extra dependency.
- **NSubstitute + plain xUnit asserts** in tests; only the repository (a real boundary) is mocked.
  The real validator is used in service tests because validation is part of the use case contract.
```
