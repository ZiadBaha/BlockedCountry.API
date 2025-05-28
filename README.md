# Blocked Countries API


---

## 🚀 Project Overview

**Blocked Countries API** is a lightweight, high-performance **.NET 8 Web API** for managing blocked countries and validating IP addresses against a third-party geolocation service. It uses **in-memory storage** for data persistence (no database required), making it suitable for simple, fast deployments or testing environments.

---

## 🎯 Objectives

- Manage a list of blocked countries with support for:
  - Permanent and temporal blocking
  - Duplicate prevention and validation
- Validate IP addresses and fetch country info using third-party APIs (ipapi.co or IPGeolocation.io)
- Log blocked access attempts with full details
- Provide pagination, filtering, and search on blocked countries and logs
- Implement background service to auto-remove expired temporal blocks

---

## 🛠️ Features & Endpoints

| Endpoint                            | Method | Description                                               |
|-----------------------------------|--------|-----------------------------------------------------------|
| `/api/countries/block`             | POST   | Block a country permanently                               |
| `/api/countries/block/{code}`      | DELETE | Remove a country from the blocked list                    |
| `/api/countries/blocked`           | GET    | Get all blocked countries with pagination & filtering    |
| `/api/countries/temporal-block`   | POST   | Temporarily block a country for a specific duration      |
| `/api/ip/lookup?ipAddress={ip}`   | GET    | Lookup country details for an IP address                  |
| `/api/ip/check-block`              | GET    | Check if caller's IP belongs to a blocked country        |
| `/api/logs/blocked-attempts`       | GET    | View paginated logs of blocked IP attempts                |

---

## ⚙️ Technology Stack

- **.NET 8** - Web API framework
- **In-Memory Collections** - Thread-safe storage via `ConcurrentDictionary` & Lists
- **HttpClient** - For external API communication
- **FluentValidation** - Input validation
- **Swagger** - API documentation & testing
- **XUnit & Moq** - Unit testing and mocking framework

---

## 🏗️ Architecture & Design

The project follows **Clean Architecture** principles:

- **Domain Layer**: Core entities and business logic
- **Application Layer**: Interfaces, services, DTOs, validators, and business rules
- **Infrastructure Layer**: In-memory repositories and external API integrations
- **API Layer**: Controllers and request handling
- **UnitTests Layer**: Comprehensive testing with mocks and validation tests

---

## 🧑‍💻 Author
- Ziad Bahaa
- Backend Developer – ASP.NET | Clean Architecture
- 🔗 [LinkedIn](https://www.linkedin.com/in/ziad-bahaa-b04561265/)  
- 🐙 [GitHub](https://github.com/ZiadBaha)
- 📧 [Email](ziadbahaa41@gmail.com)
- 📞 [Phone](01022673000)

