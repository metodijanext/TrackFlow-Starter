# TrackFlow-Starter Setup Guide

## 🎯 Quick Start (5 minutes)

### Prerequisites Checklist
- [ ] .NET 8.0.425 SDK installed (`dotnet --version`)
- [ ] SQL Server LocalDB running (`sqllocaldb info`)
- [ ] Node.js v20+ installed (`node --version`)
- [ ] Git installed (`git --version`)
- [ ] Visual Studio 2022 or VS Code open

---

## ✅ Step-by-Step Setup

### 1️⃣ Clone Repository
```bash
git clone https://github.com/metodija-zdravkovski/TrackFlow-Starter.git
cd TrackFlow-Starter
```

### 2️⃣ Setup Database & Run Migrations

Open **PowerShell** or **Command Prompt** in the `TrackFlow-Starter` directory:

```bash
cd src/TrackFlow.WebAPI
dotnet restore
dotnet ef database update
```

**What happens:**
- Creates `TrackFlow` database in SQL Server LocalDB
- Creates tables: Users, Projects, TimeEntries, ProjectUsers
- Seeds default admin user:
  - Email: `admin@trackflow.local`
  - Password: `Admin123!`

### 3️⃣ Run the API

From the same `src/TrackFlow.WebAPI` directory:

```bash
dotnet run
```

**Expected output:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started.
```

Open browser: `https://localhost:5001/swagger`

### 4️⃣ Run the Frontend (New Terminal)

```bash
cd client
npm install
npm run dev
```

**Expected output:**
```
  VITE v5.0.0  ready in 234 ms

  ➜  Local:   http://localhost:5173/
  ➜  press h to show help
```

Open browser: `http://localhost:5173`

---

## 🧪 Test the API

### Using Swagger/OpenAPI (Easiest)
1. Navigate to: `https://localhost:5001/swagger`
2. Click "Login" endpoint
3. Click "Try it out"
4. Enter:
   ```json
   {
     "email": "admin@trackflow.local",
     "password": "Admin123!"
   }
   ```
5. Click "Execute"
6. Copy `accessToken` from response

### Using cURL
```bash
# Login
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@trackflow.local","password":"Admin123!"}'

# Get response with token
# Then use token in Authorization header:
curl -X GET https://localhost:5001/api/users \
  -H "Authorization: Bearer {accessToken}"
```

---

## 🐳 Docker Setup (Alternative)

If you prefer Docker:

```bash
docker-compose up --build
```

This starts:
- **API** on `http://localhost:5000`
- **Frontend** on `http://localhost:3000`
- **SQL Server** on `localhost:1433`

---

## 📂 Project Structure Overview

```
TrackFlow-Starter/
├── src/                      # .NET Backend
│   ├── TrackFlow.Domain/     # Core entities (User, Project, TimeEntry)
│   ├── TrackFlow.Application/ # CQRS handlers, DTOs, interfaces
│   ├── TrackFlow.Infrastructure/ # Database, JWT, repositories
│   └── TrackFlow.WebAPI/     # ASP.NET Core API, Controllers
├── client/                   # React 18 Frontend
│   ├── src/
│   │   ├── api/             # API client (axios)
│   │   ├── components/      # React components
│   │   ├── pages/           # Page components
│   │   └── types/           # TypeScript interfaces
│   └── public/              # Static assets
├── docs/                     # Documentation
│   ├── adrs/                # Architecture Decision Records
│   ├── c4/                  # C4 Architecture diagrams (Week 3)
│   └── guides/              # Setup and development guides
└── README.md                # Main documentation
```

---

## 🔓 Default Credentials

**Admin Account:**
- Email: `admin@trackflow.local`
- Password: `Admin123!`
- Role: Admin

You can create additional users via:
- `POST /api/auth/register` endpoint
- Admin dashboard (when frontend is built)

---

## 🚨 Troubleshooting

### Problem: "Cannot connect to (localdb)\mssqllocaldb"
**Solution:**
```powershell
# Start SQL Server LocalDB
sqllocaldb start mssqllocaldb

# Verify it's running
sqllocaldb info
```

### Problem: "EF Core migration failed"
**Solution:**
```bash
cd src/TrackFlow.WebAPI

# Remove and recreate migration
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Problem: "Port 5000/5001 already in use"
**Solution:**
```bash
# Find and kill process on port 5000
netstat -ano | findstr :5000
taskkill /PID <PID> /F

# Or run API on different port
dotnet run --urls="https://localhost:5005"
```

### Problem: "CORS error in browser"
**Solution:**
- Ensure API is running on `http://localhost:5000`
- Ensure frontend is accessing `http://localhost:5000/api/...`
- Check `appsettings.json` CORS configuration

### Problem: "Node modules/npm issues"
**Solution:**
```bash
cd client
rm -r node_modules
npm cache clean --force
npm install
npm run dev
```

---

## 📚 Learning Resources

### For Backend Development
1. Read: `docs/ARCHITECTURE.md`
2. Review: `docs/adrs/` (Architecture Decision Records)
3. Study: `src/TrackFlow.Domain/` (Entity definitions)
4. Examine: `src/TrackFlow.Application/Features/Auth/` (CQRS example)

### For Frontend Development
1. React docs: https://react.dev/
2. TypeScript docs: https://www.typescriptlang.org/
3. Tailwind CSS: https://tailwindcss.com/
4. Vite: https://vitejs.dev/

### For C4 Modeling (Week 3)
1. C4 Model: https://c4model.com/
2. Create diagrams: `docs/c4/`
3. Use tools: Miro, Lucidchart, Draw.io

---

## 🎓 Week 4 Challenge: TimeEntries CRUD

This is where you implement your first feature:

1. **Create CQRS Commands & Queries**
   - `CreateTimeEntryCommand`
   - `UpdateTimeEntryCommand`
   - `DeleteTimeEntryCommand`
   - `GetTimeEntriesByProjectQuery`
   - `GetTimeEntriesByUserQuery`

2. **Create TimeEntriesController**
   - `POST /api/timeentries`
   - `GET /api/timeentries`
   - `PUT /api/timeentries/{id}`
   - `DELETE /api/timeentries/{id}`

3. **Create React Components**
   - TimeEntryForm
   - TimeEntriesList
   - TimeEntryFilter

**Reference:**
- See `src/TrackFlow.Application/Features/Auth/` for CQRS pattern example
- Follow same structure for TimeEntries feature

---

## ✨ Next Steps

1. ✅ Complete this setup guide
2. ✅ Login to API with admin credentials
3. ✅ Read `docs/ARCHITECTURE.md`
4. ✅ Review `docs/adrs/` to understand design decisions
5. ✅ Prepare for Week 1 class (systems thinking, architecture mindset)
6. ✅ Week 2: Requirements engineering
7. ✅ Week 3: C4 modeling
8. 🔥 **Week 4: Implement TimeEntries CRUD** (YOUR CHALLENGE)

---

## 📞 Support & Questions

- **Stuck?** Check troubleshooting section above
- **Need help?** Ask in course discussion forum or during office hours
- **API issues?** Check Swagger UI: `https://localhost:5001/swagger`
- **Database issues?** Use SQL Server Management Studio (SSMS)

---

## 🎉 You're Ready!

You now have a fully functional Clean Architecture project with:
- ✅ JWT authentication
- ✅ CQRS pattern implementation
- ✅ Entity Framework Core with SQL Server
- ✅ React 18 frontend shell
- ✅ Docker support for consistent environments

**Happy coding!** 🚀

---

*Last updated: 2026*  
*TrackFlow-Starter v1.0.0*  
*Software Engineering Course | Prof. Metodija Zdravkovski*
