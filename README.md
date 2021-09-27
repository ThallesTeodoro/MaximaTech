# MaximaTech

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

## URLs

- API: http://localhost:5000/api
- Documentation: http://localhost:5000/api/swagger

### Run tests

```bash
dotnet test
```