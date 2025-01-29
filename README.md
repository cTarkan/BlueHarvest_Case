# BlueHarvest.Case

## 📌 Project Overview

**BlueHarvest.Case** is a backend application designed to manage user accounts and transactions. The project includes an API built with **.NET 9**, a frontend developed with **React**, and a fully integrated **CI/CD pipeline**. The system allows users to **create accounts, make transactions, and retrieve account details**.

## 🚀 Tech Stack

- **Backend:** .NET 9, ASP.NET Core
- **Frontend:** React (Vite), Pure CSS
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
git clone https://github.com/your-repo/BlueHarvest.Case.git
cd BlueHarvest.Case

# Run Backend
cd BlueHarvest_Case.API
dotnet run

# Run Frontend
cd blueharvest_case.frontend
npm install
npm run dev
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

The API documentation is available via **Swagger UI**:
- `http://localhost:8081`

### **Key API Endpoints**

#### 1️⃣ Create Account

```http
POST /api/account/create
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

#### 2️⃣ Get User Account Details

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

#### 3️⃣ Add Transaction

```http
POST /api/transaction/add
```

**Request Body:**

```json
{
  "accountId": 1,
  "amount": 50
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


