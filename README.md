# BH.Case

## 📌 Project Overview

**BH.Case** is a backend-driven financial application that manages user accounts and transactions. The system allows users to **create accounts, perform transactions, and retrieve account details** while ensuring a structured and scalable architecture utilizing **CQRS, MediatR, and FluentValidation**.

## 🚀 Tech Stack

- **Backend:** .NET 9, ASP.NET Core, MediatR (CQRS)
- **Database:** In-Memory Storage (for testing purposes)
- **API Documentation:** Swagger UI
- **CI/CD:** GitHub Actions
- **Containerization:** Docker & Docker Compose

## 🔧 Installation & Setup

### **1️⃣ Running Locally**

#### Prerequisites:

- .NET 9 SDK
- Node.js & npm
- Docker (if using containerized setup)

#### Steps:

```sh
# Clone the repository
git clone https://github.com/your-repo/BH.Case.git
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

## 📌 API Documentation

The API documentation is available via **Swagger UI**:
- `http://localhost:8081`

### **Key API Endpoints**

#### 1️⃣ Create Account

```http
POST /api/account
```

**Request Body:**

```json
{
  "customerId": 1,
  "initialCredit": 100
}
```

**Response:**

```json
{
  "accountId": 1,
  "customerId": 1,
  "balance": 100
}
```

#### 2️⃣ Get Customer Accounts

```http
GET /api/account/{customerId}
```

**Response:**

```json
[
  {
    "accountId": 1,
    "balance": 200
  }
]
```

#### 3️⃣ Add Transaction

```http
POST /api/transaction
```

**Request Body:**

```json
{
  "accountId": 1,
  "amount": 50
}
```

#### 4️⃣ Get Transactions by Customer ID

```http
GET /api/transaction/{customerId}
```

**Response:**

```json
[
  {
    "accountId": 1,
    "amount": 50,
    "timestamp": "2024-06-01T12:00:00Z"
  }
]
```

#### 5️⃣ Get User Account Details

```http
GET /api/user/{customerId}/details
```

**Response:**

```json
{
  "name": "John",
  "surname": "Doe",
  "totalBalance": 200,
  "accounts": [
    {
      "accountId": 1,
      "balance": 200,
      "transactions": []
    }
  ]
}
```

## 🧪 Testing

### **Run Unit & Integration Tests**

```sh
dotnet test
```

## 🛠 CI/CD Pipeline

The **CI/CD process** is managed using **GitHub Actions** and includes:
- **Build & Test Execution**: Automatically runs on each push & pull request.
- **Docker Build**: Containerized setup is validated.
