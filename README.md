# TrackFlow-Starter 📊

**Time Tracking System** built with Clean Architecture, CQRS, and JWT Authentication for the Software Engineering course by Prof. Metodija Zdravkovski.

## 🏗️ Architecture Overview

This project follows **Clean Architecture** principles with **CQRS (Command Query Responsibility Segregation)** pattern:

```
┌─────────────────────────────────────────┐
│         TrackFlow.WebAPI (ASP.NET Core) │
│         ├─ Controllers                  │
│         ├─ Middleware                   │
│         └─ Filters                      │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│      TrackFlow.Application (Business)   │
│      ├─ Commands (CQRS)                │
│      ├─ Queries (CQRS)                 │
│      ├─ Handlers                        │
│      └─ DTOs                            │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│   TrackFlow.Infrastructure (Data Access)│
│   ├─ DbContext (EF Core)               │
│   ├─ Repositories                       │
│   ├─ Services (JWT, Password, etc.)    │
│   └─ Migrations                         │
└────────────┬────────────────────────────┘
             ↓
┌─────────────────────────────────────────┐
│      TrackFlow.Domain (Core Entities)   │
│      ├─ User, Project, TimeEntry       │
│      ├─ Value Objects                   │
│      └─ Domain Logic                    │
└─────────────────────────────────────────┘
```

## 📋 Prerequisites

### Required Software
- **Visual Studio 2022** (Community, Professional, or Enterprise)
  - Workloads: ASP.NET and web development, .NET desktop development, Data storage and processing
- **.NET 8.0 SDK** (version 8.0.425 or later)
  - Download: https://dotnet.microsoft.com/download/dotnet/8.0
- **SQL Server LocalDB** or **SQL Server Express 2022**
  - Download: https://www.microsoft.com/sql-server/sql-server-downloads
  - For LocalDB: usually included with Visual Studio data storage workload
- **Node.js LTS** (v20+ for React frontend)
  - Download: https://nodejs.org/
- **Git** (for version control)
  - Download: https://git-scm.com/

### Verification
Run these commands in PowerShell to verify:
```powershell
# Check .NET SDK
dotnet --version
# Expected: 8.0.425 (or newer)

# Check SQL Server LocalDB
sqllocaldb info
# Expected: MSSQLLocalDB (or similar)

# Check Node.js
node --version
npm --version
# Expected: v20.x.x and 10.x.x
```

---

## 🚀 Getting Started

### Option A: Local Development (Recommended for Development)

#### Step 1: Clone the Repository
```bash
git clone https://github.com/metodija-zdravkovski/TrackFlow-Starter.git
cd TrackFlow-Starter
```

#### Step 2: Restore NuGet Packages
```bash
cd src/TrackFlow.WebAPI
dotnet restore
```

#### Step 3: Apply Database Migrations
```bash
# From TrackFlow.WebAPI directory
dotnet ef database update
```

This will create the SQL Server LocalDB database and seed an admin user:
- **Email:** `admin@trackflow.local`
- **Password:** `Admin123!`

#### Step 4: Run the API
```bash
dotnet run
# API will be available at https://localhost:5001 or http://localhost:5000
# Swagger UI: https://localhost:5001/swagger/index.html
```

#### Step 5: Setup and Run Frontend
```bash
# In a new terminal, from project root
cd client
npm install
npm run dev
# Frontend will be available at http://localhost:5173
```

### Option B: Docker Setup (For Consistent Environments)

#### Prerequisites
- Docker Desktop installed and running

#### Build and Run
```bash
docker-compose up --build
```

This will start:
- **SQL Server** on `localhost:1433`
- **API** on `http://localhost:5000`
- **Frontend** on `http://localhost:3000`

---

## 📚 Project Structure

```
TrackFlow-Starter/
├── src/
│   ├── TrackFlow.Domain/                    # Core domain entities
│   │   ├── Common/                          # Base classes, enums
│   │   │   ├── BaseEntity.cs
│   │   │   └── UserRole.cs
│   │   └── Entities/                        # Domain models
│   │       ├── User.cs
│   │       ├── Project.cs
│   │       ├── TimeEntry.cs                 # ⭐ Empty for Week 4
│   │       └── ProjectUser.cs
│   │
│   ├── TrackFlow.Application/               # Business logic (CQRS)
│   │   ├── Dtos/
│   │   │   └── Auth/                        # Data transfer objects
│   │   │       ├── LoginRequest.cs
│   │   │       ├── RegisterRequest.cs
│   │   │       └── AuthResponse.cs
│   │   ├── Interfaces/                      # Service contracts
│   │   │   ├── IJwtTokenService.cs
│   │   │   ├── IPasswordHasher.cs
│   │   │   ├── IRepository.cs
│   │   │   └── IUnitOfWork.cs
│   │   └── Features/                        # CQRS handlers
│   │       ├── Auth/                        # Login, Register
│   │       ├── Users/                       # User management
│   │       ├── Projects/                    # Project management
│   │       └── TimeEntries/                 # ⭐ Empty for Week 4
│   │
│   ├── TrackFlow.Infrastructure/            # Data access layer
│   │   ├── Data/
│   │   │   ├── TrackFlowDbContext.cs        # EF Core DbContext
│   │   │   ├── Repositories/                # Data access patterns
│   │   │   ├── Migrations/                  # Database migrations
│   │   │   └── Specifications/              # Query specifications
│   │   ├── Identity/
│   │   │   ├── JwtTokenService.cs           # JWT token generation
│   │   │   └── PasswordHasher.cs            # BCrypt hashing
│   │   └── Security/
│   │       └── AuthenticationExtensions.cs
│   │
│   ├── TrackFlow.WebAPI/                    # ASP.NET Core API
│   │   ├── Program.cs                       # Service registration & middleware
│   │   ├── appsettings.json                 # Configuration
│   │   ├── appsettings.Development.json
│   │   ├── Controllers/
│   │   │   ├── AuthController.cs            # Login, Register endpoints
│   │   │   ├── UsersController.cs           # User CRUD
│   │   │   ├── ProjectsController.cs        # Project CRUD
│   │   │   ├── TimeEntriesController.cs     # ⭐ Empty for Week 4
│   │   │   └── HealthController.cs          # Health check
│   │   └── Middleware/
│   │       ├── ExceptionHandlingMiddleware.cs
│   │       └── LoggingMiddleware.cs
│   │
│   └── TrackFlow.Tests/                     # Unit and integration tests
│       ├── Unit/
│       └── Integration/
│
├── client/                                   # React 18 frontend
│   ├── public/
│   ├── src/
│   │   ├── api/                             # API client
│   │   ├── components/                      # React components
│   │   │   ├── Auth/
│   │   │   ├── Layout/
│   │   │   └── Features/
│   │   ├── pages/                           # Page components
│   │   ├── context/                         # React Context (Auth)
│   │   ├── hooks/                           # Custom hooks
│   │   ├── store/                           # Redux Toolkit (state management)
│   │   ├── types/                           # TypeScript interfaces
│   │   ├── utils/                           # Helper functions
│   │   ├── App.tsx
│   │   └── main.tsx
│   ├── package.json
│   ├── tsconfig.json
│   └── vite.config.ts
│
├── docs/                                     # Documentation
│   ├── c4/                                  # C4 Architecture diagrams (Week 3)
│   │   ├── README.md
│   │   ├── system_context.md
│   │   ├── container.md
│   │   ├── component.md
│   │   └── code.md
│   │
│   ├── adrs/                                # Architecture Decision Records
│   │   ├── README.md
│   │   ├── 0001-cqrs-pattern-selection.md
│   │   ├── 0002-clean-architecture-layers.md
│   │   ├── 0003-authentication-jwt.md
│   │   ├── 0004-ef-core-sql-server.md
│   │   ├── 0005-react-typescript-frontend.md
│   │   └── 0006-docker-containerization.md
│   │
│   └── guides/
│       ├── project-setup.md
│       ├── authentication-flow.md
│       ├── adding-new-features.md
│       └── testing-guide.md
│
├── scripts/                                  # Setup and utility scripts
│   ├── setup-local.sh
│   ├── migrate-database.sh
│   └── seed-data.sh
│
├── global.json                              # .NET SDK version constraint
├── TrackFlow.sln                            # Visual Studio solution
├── docker-compose.yml                       # Docker orchestration
├── .dockerignore
├── .gitignore
└── README.md                                # This file
```

---

## 🔐 Authentication Flow

### JWT Token Structure
```
Header:      { "alg": "HS256", "typ": "JWT" }
Payload:     {
               "sub": "userId",
               "email": "user@example.com",
               "role": "Admin|Manager|Employee",
               "iat": 1234567890,
               "exp": 1234571490
             }
Signature:   HMACSHA256(Base64URL(header) + "." + Base64URL(payload), secret)
```

### Login Flow
```
1. Client POST /api/auth/login
   {
     "email": "admin@trackflow.local",
     "password": "Admin123!"
   }

2. Server validates credentials
   ✓ Email exists?
   ✓ Password hash matches?
   ✓ User is active?

3. Server generates tokens
   - AccessToken (expires in 15 minutes)
   - RefreshToken (expires in 7 days)

4. Client receives
   {
     "accessToken": "eyJhbGc...",
     "refreshToken": "base64EncodedToken",
     "expiresIn": 900,
     "user": {
       "id": "uuid",
       "email": "admin@trackflow.local",
       "firstName": "System",
       "lastName": "Administrator",
       "role": "Admin"
     }
   }

5. Client stores token in localStorage and includes in future requests
   Authorization: Bearer {accessToken}
```

### Authorization Levels
```
Public:      /api/auth/login, /api/auth/register, /api/health
Authenticated: All other endpoints (requires valid JWT)
Admin:       User management, system settings
Manager:     Project and team management
Employee:    Time entry submission and viewing personal data
```

---

## 🗄️ Database Schema

### Users Table
```sql
Users (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  Email NVARCHAR(255) UNIQUE NOT NULL,
  FirstName NVARCHAR(100) NOT NULL,
  LastName NVARCHAR(100) NOT NULL,
  PasswordHash NVARCHAR(MAX) NOT NULL,
  Role INT NOT NULL,  -- 1=Admin, 2=Manager, 3=Employee
  IsActive BIT NOT NULL,
  CreatedAt DATETIME2 NOT NULL,
  UpdatedAt DATETIME2 NULL
)
```

### Projects Table
```sql
Projects (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  Name NVARCHAR(255) NOT NULL,
  Description NVARCHAR(1000) NULL,
  ManagerId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY,
  IsActive BIT NOT NULL,
  CreatedAt DATETIME2 NOT NULL,
  UpdatedAt DATETIME2 NULL
)
```

### TimeEntries Table (⭐ Week 4 Student Implementation)
```sql
TimeEntries (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  ProjectId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY,
  UserId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY,
  EntryDate DATETIME2 NOT NULL,
  Hours DECIMAL(5,2) NOT NULL,
  Description NVARCHAR(500) NULL,
  TaskDescription NVARCHAR(500) NULL,
  CreatedAt DATETIME2 NOT NULL,
  UpdatedAt DATETIME2 NULL
)
```

---

## 🎓 Week 4 Student Implementation: TimeEntries CRUD

The `TimeEntries` feature is intentionally left empty for students to implement in Week 4:

### Your Task (Week 4):
Create complete CQRS handlers for TimeEntry management:

1. **Commands** (`src/TrackFlow.Application/Features/TimeEntries/Commands/`)
   - `CreateTimeEntryCommand` + `CreateTimeEntryCommandHandler`
   - `UpdateTimeEntryCommand` + handler
   - `DeleteTimeEntryCommand` + handler

2. **Queries** (`src/TrackFlow.Application/Features/TimeEntries/Queries/`)
   - `GetTimeEntriesByProjectQuery` + handler
   - `GetTimeEntriesByUserQuery` + handler
   - `GetTimeEntryByIdQuery` + handler

3. **Validators**
   - Validate hours > 0
   - Validate date is not in future
   - Validate user is assigned to project

4. **Controllers**
   - `POST /api/timeentries` - Create
   - `GET /api/timeentries?projectId=...` - List by project
   - `PUT /api/timeentries/{id}` - Update
   - `DELETE /api/timeentries/{id}` - Delete

5. **Frontend** (React)
   - Time entry form component
   - Time entries list with filtering
   - Date picker for entry date

### Resources
- ADRs explaining CQRS pattern: `docs/adrs/0001-cqrs-pattern-selection.md`
- Architecture guide: `docs/ARCHITECTURE.md`
- Example: User creation is implemented; use same pattern for TimeEntries

---

## 🔧 Available Endpoints

### Authentication
```
POST   /api/auth/login              Login with email/password
POST   /api/auth/register           Register new user account
POST   /api/auth/refresh-token      Refresh expired access token
```

### Users (Admin/Manager access)
```
GET    /api/users                   List all users
GET    /api/users/{id}              Get user by ID
POST   /api/users                   Create new user
PUT    /api/users/{id}              Update user
DELETE /api/users/{id}              Delete user
GET    /api/users/by-role/{role}    Get users by role
```

### Projects
```
GET    /api/projects                List all projects
GET    /api/projects/{id}           Get project by ID
POST   /api/projects                Create new project
PUT    /api/projects/{id}           Update project
DELETE /api/projects/{id}           Delete project
POST   /api/projects/{id}/members   Add team member
```

### TimeEntries (⭐ Week 4)
```
(All endpoints to be implemented by students)
```

### Health Check
```
GET    /api/health                  API health status
```

---

## 📊 Testing the API

### Using Swagger/OpenAPI
1. Run the API: `dotnet run` from `src/TrackFlow.WebAPI`
2. Open browser: `https://localhost:5001/swagger`
3. Test endpoints interactively

### Using cURL/Postman

#### 1. Login
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@trackflow.local",
    "password": "Admin123!"
  }'
```

#### 2. Use Token in Subsequent Requests
```bash
curl -X GET http://localhost:5000/api/users \
  -H "Authorization: Bearer {accessToken}"
```

---

## 🧪 Running Tests

### Run All Tests
```bash
cd src/TrackFlow.Tests
dotnet test
```

### Run Specific Test Category
```bash
dotnet test --filter "Category=Unit"
dotnet test --filter "Category=Integration"
```

### Run with Coverage
```bash
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

---

## 📖 Learning Resources

### Clean Architecture
- Read: `docs/adrs/0002-clean-architecture-layers.md`
- Key Principle: Independence of frameworks, databases, UI

### CQRS Pattern
- Read: `docs/adrs/0001-cqrs-pattern-selection.md`
- Separate Command (write) from Query (read) operations

### C4 Model (Week 3)
- Create diagrams: `docs/c4/`
- Context → Container → Component → Code levels

### Architecture Decision Records
- Review all ADRs: `docs/adrs/`
- Understand trade-offs and justifications

---

## 🚨 Troubleshooting

### Issue: "Cannot connect to database"
**Solution:**
```powershell
# Verify LocalDB is running
sqllocaldb start mssqllocaldb

# Check connection string in appsettings.json
# Should be: Server=(localdb)\mssqllocaldb;Database=TrackFlow;Trusted_Connection=true;
```

### Issue: "Entity Framework migrations failed"
**Solution:**
```bash
# Remove migrations (if needed)
dotnet ef migrations remove

# Create fresh migration
dotnet ef migrations add InitialCreate

# Apply migration
dotnet ef database update
```

### Issue: "Port 5000/5001 already in use"
**Solution:**
```bash
# Kill process on port 5000
netstat -ano | findstr :5000
taskkill /PID <PID> /F

# Or run on different port
dotnet run --urls="https://localhost:5005"
```

### Issue: "CORS error in frontend"
**Solution:**
- Ensure `appsettings.json` has CORS configured
- Frontend should call `http://localhost:5000` (dev) or appropriate production URL

---

## 📋 Gitflow for Students

### Creating a Feature Branch
```bash
git checkout -b feature/timeentries-crud
```

### Committing Changes
```bash
git add .
git commit -m "feat: implement TimeEntry CRUD operations"
```

### Pushing to Remote
```bash
git push origin feature/timeentries-crud
```

### Creating a Pull Request
1. Go to GitHub repository
2. Click "Compare & pull request"
3. Add description of changes
4. Request review from instructor

---

## 📝 Configuration Files Explained

### `global.json`
Specifies .NET SDK version (8.0.425). Ensures all developers use same version.

### `appsettings.json`
Database connection string, JWT settings, logging configuration.

### `TrackFlow.sln`
Visual Studio solution file. Lists all projects and their dependencies.

### `.csproj` files
Project files defining NuGet dependencies and compilation settings.

---

## 🎯 Next Steps for Week 1

1. ✅ Clone this repository
2. ✅ Restore NuGet packages: `dotnet restore`
3. ✅ Apply migrations: `dotnet ef database update`
4. ✅ Run API: `dotnet run`
5. ✅ Test login endpoint with Swagger
6. ✅ Read `docs/ARCHITECTURE.md`
7. ✅ Understand Clean Architecture by reviewing layer dependencies

---

## 📞 Support

- **Course Materials:** Check `Syllabus/` directory
- **Architecture Examples:** Review `docs/adrs/`
- **Instructor:** Prof. Metodija Zdravkovski
- **Questions?** Ask during office hours or through course discussion board

---

**Happy coding! 🚀**

*TrackFlow-Starter v1.0.0 | Software Engineering Course 2026/27*
