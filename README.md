# TravelAndTours (.NET 8) - Clean Architecture + CQRS + JWT + Identity (INT keys) + PostgreSQL

This is a production-grade Web API solution using:
- Clean Architecture (Domain / Application / Infrastructure / Api)
- CQRS with MediatR
- FluentValidation
- Repository Pattern + Unit of Work
- EF Core 8 + **PostgreSQL**
- ASP.NET Core Identity with **INT** primary keys
- JWT Authentication + Role-based Authorization
- ProblemDetails + centralized exception handling
- Role/Admin seeding at startup

## Prerequisites
- .NET SDK 8.x
- PostgreSQL (local or remote)

## Configure Database
Edit `src/TravelAndTours.Api/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=travelandtours_db;Username=postgres;Password=postgres"
}
```

## Commands
From the solution folder:

```bash
dotnet restore
dotnet ef migrations add InitialCreate -p src/TravelAndTours.Infrastructure -s src/TravelAndTours.Api
dotnet ef database update -p src/TravelAndTours.Infrastructure -s src/TravelAndTours.Api
dotnet run --project src/TravelAndTours.Api
```

Swagger: `https://localhost:5001/swagger` (or the printed URL).

## Seeded Roles/Admin
At startup it creates:
- Roles: `ADMIN`, `USER`
- Default admin:
  - Email: `admin@local.test`
  - Password: `Admin@12345`
  - Role: `ADMIN`

## Quick Test
1) Login with the admin in Swagger:
- `POST /api/auth/login`
2) Copy the token and click **Authorize** in Swagger.
3) Create product: `POST /api/products` (ADMIN only)

## NuGet note (fix for NU1102)
This solution uses:
- `MediatR` 11.1.0
- `MediatR.Extensions.Microsoft.DependencyInjection` 11.1.0

Those versions exist in older/offline NuGet feeds where MediatR 12 packages may be missing.
