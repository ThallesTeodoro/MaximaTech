# Taxonomy

[Changelog](./changelog.md)

## Directories

- `/source`: Project files
- `/tests`: Project tests
- `/docker`: Docker setup files

---

## Setup (Front-end)

### Requirements

- Docker
- Docker Compose

### Environment file

Create `source/Taxonomy.Api/appsettings.Development.json` file with all content of `source/Taxonomy.Api/appsettings.json` file.

### Up containers

```bash
docker-compose up -d
```

---

## Setup (Back-end)

### Requirements

- Docker
- Docker Compose

### Environment file

Create `source/Taxonomy.Api/appsettings.Development.json` file with all content of `source/Taxonomy.Api/appsettings.json` file.

Change database server name to `localhost` in `TaxonomyContext` variable into `appsettings.Development.json` file.

### Up database container

```bash
docker-compose up -d database
```

### Run project
```bash
dotnet watch -p source/Taxonomy.Api/Taxonomy.Api.csproj run
```

## URLs

- API: http://localhost:5000/api
- Documentation: http://localhost:5000/api/swagger

## Commands

### Install dotnet ef tool

```bash
dotnet tool install --global dotnet-ef
```

### Add migration

```bash
dotnet ef migrations add MigrationDescription -p source/Taxonomy.Infrastructure/Taxonomy.Infrastructure.csproj -s source/Taxonomy.Api/Taxonomy.Api.csproj
```

### Run migrations

```bash
dotnet ef database update -s source/Taxonomy.Api/Taxonomy.Api.csproj
```

### Run tests

```bash
dotnet test
```