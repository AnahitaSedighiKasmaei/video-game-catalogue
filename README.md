# Video Game Catalogue

A small full-stack app for browsing and editing a list of video games. An ASP.NET Core
REST API backed by SQL Server, with an Angular front end.

It's deliberately simple on the surface — two screens, a handful of endpoints — so the
interesting part is how it's put together rather than what it does.

## Running it locally

You'll need the .NET 8 SDK, Node 18+, and SQL Server LocalDB (which ships with Visual
Studio and the SQL Server Express installer).

**API**

```bash
cd src/VideoGameCatalogue.API
dotnet run
```

It comes up on http://localhost:5031 (HTTP only — there was no SSL requirement). On the
first run in Development it creates the LocalDB database and applies the migration
automatically, so there's nothing to set up by hand. Swagger is at `/swagger`.

The connection string is in `appsettings.json` and points at
`(localdb)\MSSQLLocalDB`. If you'd rather manage the schema yourself:

```bash
dotnet tool restore
dotnet ef database update --project src/VideoGameCatalogue.Infrastructure --startup-project src/VideoGameCatalogue.Infrastructure
```

**Front end**

```bash
cd client
npm install
npm start
```

Runs on http://localhost:4200 and talks to the API at `http://localhost:5031/api`. The
API already allows that origin through CORS, so as long as both are running you're good.
The API URL lives in `src/environments/environment.ts` if you need to change it.

**Tests**

```bash
dotnet test
```

## How it's structured

The backend follows Clean Architecture — four projects, with all the references pointing
inward toward the domain:

```
Domain          The VideoGame entity and its rules. No framework dependencies at all.
Application     Use cases, DTOs, validation, and the service/repository interfaces.
Infrastructure  EF Core — the DbContext, mappings, the repository, migrations and seed data.
API             Controllers and the exception-handling middleware. Thin.
```

The `client/` folder is the Angular app, organised by feature
(`features/games/{pages,services,models}`) with standalone components and reactive forms.

A few things worth calling out, since they were conscious choices:

- **The `VideoGame` entity guards its own state.** There are no public setters — you go
  through the constructor or `Update()`, and both validate. So you can't end up with a
  game that has a blank title or a rating of 50 sitting in memory, let alone in the
  database. The business rules live in one place instead of being scattered across the
  service and the controller.

- **The repository interface lives in the Application layer, not Infrastructure.** That's
  what lets Infrastructure depend inward. It's a single repository for the `VideoGame`
  aggregate rather than a generic `Repository<T>` — a generic one tends to leak EF/IQueryable
  details across the boundary and rarely pays off on a project this size.

- **Validation happens in two places on purpose.** FluentValidation checks the incoming
  request shape and gives the client clean field-level errors; the entity re-checks its
  invariants because that's the domain's job, not the API's.

- **Mapping is done by hand.** The objects are tiny, so a one-line extension method is
  clearer than wiring up AutoMapper and easier to step through in a debugger.

- **Errors go through one middleware** that turns exceptions into consistent
  `ProblemDetails` responses, so the controllers don't need try/catch blocks.

- **Plain Bootstrap on the front end, not ng-bootstrap.** There's no modal, datepicker or
  typeahead here that would justify the extra dependency — a table and a form only need
  the stylesheet.

## The API

| Method | Route                     | What it does                              |
|--------|---------------------------|-------------------------------------------|
| GET    | `/api/videogames?search=` | List games, with an optional text filter  |
| GET    | `/api/videogames/{id}`    | Get one game                              |
| PUT    | `/api/videogames/{id}`    | Update a game                             |

Validation failures come back as a 400 with the field errors; a missing id is a 404.

## Notes / things I'd add with more time

- I kept the API to list/search/get/update because that's what the two screens need.
  Adding create and delete would be straightforward — a new request DTO, a validator, and
  a couple of repository methods.
- The unit tests cover the domain rules, the service, and the validator. The obvious next
  step would be a few integration tests hitting the API with an in-memory or SQLite
  provider.
- Targeting .NET 8 (the current LTS) and Angular 17 — the Angular version was also what the
  local Node 18 runtime supported cleanly.
