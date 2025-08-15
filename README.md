# 🔐 Security & Authentication – Microsoft Capstone Project 2025

A full-stack User Management API built with ASP.NET Core and Blazor, demonstrating secure authentication, robust middleware, and production-grade API integration. This project is part of the Microsoft Full-Stack Integration and Security course on Coursera.

---

## 📌 Project Overview

This capstone project showcases:
- Secure user registration and login using **ASP.NET Identity**
- Token-based authentication with **JWT**
- Role-based authorization for protected endpoints
- Custom middleware for **logging**, **error handling**, and **authentication**
- API documentation via **Swagger**
- Modular architecture for scalability and maintainability

---

## 🛠️ Tech Stack

| Layer              | Technology                          |
|-------------------|--------------------------------------|
| Frontend          | Blazor WebAssembly / Blazor Server   |
| Backend           | ASP.NET Core Web API                 |
| Authentication    | ASP.NET Identity, JWT, OAuth         |
| Middleware        | Custom logging, error handling       |
| Documentation     | Swagger / Swashbuckle                |
| Data Persistence  | Entity Framework Core, SQL Server    |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server or LocalDB
- Visual Studio 2022+ or VS Code

### Setup Instructions
```bash
# Clone the repo
git clone https://github.com/frankTheCodeBoy/Security_and_Auth_Microsoft_Capstone_Project_2025.git
cd Security_and_Auth_Microsoft_Capstone_Project_2025

# Restore dependencies
dotnet restore

# Apply migrations and seed database
dotnet ef database update

# Run the API
dotnet run
