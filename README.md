# ProductCatalogApi

# Product Catalog & Order Processing API

A production-ready ASP.NET Core Web API.  
The API manages products and orders while preventing stock overselling using
transactional integrity and clean architecture principles.

---

## 🔧 Tech Stack
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Clean / Onion Architecture

---

## ✨ Features
- Product CRUD operations
- Place orders with multiple products
- Prevents overselling using database transactions
- Optimistic concurrency control
- Clean separation of concerns

---

## 🏗 Architecture
- **Api** – Controllers & startup
- **Application** – DTOs
- **Core** – Entities, enums, exceptions
- **Data** – DbContext, EF configurations, seed data
- **DataAccess** – Repositories & Unit of Work
- **Infrastructure** – Business services

---

## 🚀 Setup
```bash
git clone https://github.com/Precious-Adeoye/ProductCatalogApi/
dotnet restore
dotnet ef database update
dotnet run --project ProductCatalogApi
