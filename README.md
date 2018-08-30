# SportsStore - MVC
Aplikacja sklepu sportowego w ramach nauki **Microsoft ASP.NET Core 2.0 MVC**.

---

**Co będzie potrzebne?**
- [x] Visual Studio 2017 lub Visual Studio Code
- [x] Framework .NET Core (przynajmniej 2.0)
- [x] SQL Server


**Klonowanie i konfiguracja:**
```
git clone https://github.com/michaelspace/SportsStore.git
cd SportsStore/SportsStore
dotnet restore
dotnet ef migrations add Initial --context ApplicationDbContext
dotnet ef database update --context ApplicationDbContext
dotnet ef migrations add Initial --context AppIdentityDbContext
dotnet ef database update --context AppIdentityDbContext
```

---

**Uruchamianie:**
```
dotnet run
```
