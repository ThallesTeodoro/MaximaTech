# MaximaTech

The purpose of this project is to show some of what I know. The project consists of make a CRUD of products.

## Technologies

- C# (Csharp)
- .Net Core
- Entity Framework Core
- Mysql

## Directory Structure

```
.
├── docker
│   ├── dotnet
│   │   └── Dockerfile
│   ├── mysql
|   |   └── custom.cnf
│   └── mysql
|       └── nginx.conf
├── source
│   ├── MaximaTech.Core
│   │   ├── DTOs
│   │   ├── Entities
│   │   └── Interfaces
│   │      ├── Repositores
│   │      └── Services
│   ├── MaximaTech.Domain
│   │   ├── Commands
│   │   ├── Exceptions
│   │   └── Handlers
│   ├── MaximaTech.Infrastructure
│   │   ├── Data
│   │   ├── Migrations
│   │   ├── Repositories
│   │   ├── Services
│   │   └── Validations
│   └── MaximaTech.Web
│       ├── Configuration
│       ├── Controllers
│       ├── Extensions
│       ├── Filters
│       ├── Helpers
│       ├── Models
│       ├── Properties
│       ├── Views
│       ├── wwwroot
│       ├── Startup.cs
│       └── Program.cs
├── tests
│   └── MaximaTech.IntegrationTests
├── .dockerignore
├── .editorconfig
├── .gitignore
├── docker-compose.yml
├── MaximaTech.sln
└── README.md
```
---

## Setup

### Requirements

- Docker
- Docker Compose

### Up containers

```bash
docker-compose up -d
```

## URLs

- Application: http://localhost:8000
- API: http://localhost:8000/api


## Access

> You will be able to access the panel with the following credentials:

- Email: admin@maximatech.com
- Password: secret