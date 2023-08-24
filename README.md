# AspNetJWT - WebApi Base

### Preparing dev env

Required tools and cert settings:
```
dotnet dev-certs https --trust
dotnet tool install -g dotnet-aspnet-codegenerator
```

Project folder:
```
mkdir AspNetJWT
cd AspNetJWT
dotnet new webapi
code .
```

Packages:
```
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.10
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.10
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.10
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 7.0.10
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.10
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 7.0.9
```

### Codes and project settings
After that we add project classes and doing some setting stuff:
```
Entities->Item.cs

Infrastructure->ItemsContext.cs
And add some seed data to this class
ctor -> Database.EnsureCreated(); 
OnModelCreating -> Seed

Program.cs->
builder.Services.AddDbContext<ItemsContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("ItemsConnection")  ));

appsettings.json->
"ConnectionStrings": {
    "ItemsConnection": "Data Source=ItemsDb.db"
  }
```
Build to check if everything is ok
```
dotnet build
```

### Adding controller with using aspnet-codegenerator
```
dotnet aspnet-codegenerator controller -name ItemsController -async -api -m Item -dc ItemsContext -outDir Controllers
```



<sub>You can use SQLite browser to inspect items table. SQLite Browser: https://sqlitebrowser.org/</sub>

