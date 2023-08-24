# AspNetJWT

dotnet dev-certs https --trust
dotnet tool install -g dotnet-aspnet-codegenerator


mkdir AspNetJWT
cd AspNetJWT
dotnet new webapi
code .

dotnet add package Microsoft.EntityFrameworkCore --version 7.0.10
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.10
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.10
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 7.0.10
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.10
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 7.0.9

Entities>Item.cs
Infrastructure>ItemsContext.cs
Program.cs>
builder.Services.AddDbContext<ItemsContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("ItemsConnection")  ));
appsettings.json>
"ConnectionStrings": {
    "ItemsConnection": "Data Source=ItemsDb.db"
  }

dotnet build

dotnet aspnet-codegenerator controller -name ItemsController -async -api -m Item -dc ItemsContext -outDir Controllers

https://sqlitebrowser.org/

EnsureCreated
OnModelCreating --> Seed
