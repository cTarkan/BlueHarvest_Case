# BH.Case

## 📌 Project Overview

**BH.Case** is a backend-driven financial application that manages user accounts and transactions. The system allows users to **create accounts, perform transactions, and retrieve account details** while ensuring a structured and scalable architecture utilizing **CQRS, MediatR, and FluentValidation**.

## 🚀 Tech Stack

- **Backend:** 
  - .NET 9, ASP.NET Core
  - MediatR (CQRS Pattern)
  - FluentValidation
  - xUnit & FluentAssertions (Testing)
- **Frontend:**
  - React 18
  - TypeScript
  - CSS3
- **Database:** In-Memory Storage (for testing purposes)
- **API Documentation:** OpenAPI (Swagger UI)
- **CI/CD:** GitHub Actions
- **Containerization:** Docker & Docker Compose

## 🔧 Installation & Setup

### **1️⃣ Running Locally**

#### Prerequisites:

- .NET 9 SDK
- Node.js & npm (v18 or higher)
- TypeScript
- Docker (if using containerized setup)

#### Steps:

```sh
# Clone the repository
git clone https://github.com/cTarkan/BH.CASE
cd BH.Case

# Run Backend
cd BH.Case.API
dotnet run

```

### **2️⃣ Running with Docker**

```sh
docker-compose up --build
```

This will start:

- **Backend:** `http://localhost:8080`
- **Swagger UI:** `http://localhost:8081`
- **Frontend:** `http://localhost:3000`


## 📌 API Documentation

The API documentation is available via Swagger UI at `http://localhost:8081`

### Endpoints Overview

#### 🧑 Customer Endpoints

##### Create Customer
- **Method:** POST
- **Endpoint:** `/api/customer`
- **Description:** Creates a new customer in the system with basic information.

##### Get Customer
- **Method:** GET  
- **Endpoint:** `/api/customer/{customerId}`
- **Description:** Retrieves basic customer information by ID.

##### Get Customer Details
- **Method:** GET
- **Endpoint:** `/api/customer/{customerId}/details`
- **Description:** Retrieves detailed customer information including all accounts and transactions.

#### 💰 Account Endpoints

##### Create Account
- **Method:** POST
- **Endpoint:** `/api/account`
- **Description:** Creates a new account for an existing customer. If initial credit is provided, creates an initial transaction.

##### Get Customer Accounts
- **Method:** GET
- **Endpoint:** `/api/account/{customerId}`
- **Description:** Retrieves all accounts associated with a customer.

#### 💸 Transaction Endpoints

##### Add Transaction
- **Method:** POST
- **Endpoint:** `/api/transaction`
- **Description:** Adds a new transaction to an existing account.

##### Get Customer Transactions
- **Method:** GET
- **Endpoint:** `/api/transaction/{customerId}`
- **Description:** Retrieves all transactions for a specific customer across all their accounts.

### Status Codes

The API uses standard HTTP status codes:

- **200 OK:** Successful GET, PUT, PATCH requests
- **201 Created:** Successful POST requests
- **404 Not Found:** Resource not found
- **400 Bad Request:** Invalid request payload

### CORS Support

The API supports CORS for:
- `http://localhost:8081` (Swagger UI)
- `http://localhost:3000` (Frontend Application)

## 🧪 Testing

### **Run Unit & Integration Tests**

```sh
# Run all tests
dotnet test

# Run specific test project
dotnet test BH.Case.Tests/BH.Case.Tests.csproj
```

## 🛠 CI/CD Pipeline

The **CI/CD process** is managed using **GitHub Actions** and includes:
- **Build & Test Execution**: Automatically runs on each push & pull request.
- **Docker Build**: Containerized setup is validated.
