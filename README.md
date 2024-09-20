# HealthEA_Backend
This is backend source for **Digital Personal Health Assistant** project of **SEP490**

## Prerequirements

- .NET Core SDK
- Visual Studio 2017 or higher
- Docker or sql server

## Setup Project 

## Setup MYSQL on Docker 
- User Quản trị: 'sa'   <Br>
- Port: '1433:1433'
- database
```
// pull image 
docker pull mcr.microsoft.com/mssql/server:2017-latest
// create volume
docker volume create device=path_in_host vmssql
//
docker run -e 'ACCEPT_EULA=Y' --name c-mssql -e 'SA_PASSWORD=123456' -p 1433:1433 -v vmssql:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
```

## Setup SQL server for developer

### Conditions
1. Install `Entity Frameworkcore`
```
dotnet add package Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```
2. Change `DBConnection` in `appsettings`
```
"DBConnection": "server =(local); database = <sqlname>;uid=<username>;pwd=<password>;TrustServerCertificate=True"
```

### Manage migrations 

- Add migrations:<br>
```
 dotnet ef migrations add <NameMigration> --context SqlDBContext --output-dir Your/Directory 
```
Or:
```
 add-migration <NameMigration>
```


- Update migrations
```
 dotnet ef migrations add <NameMigration> --context SqlDBContext

```
Or:
```
update-database
```

- Remove migrations:
```
dotnet ef <NameMigration> remove
```

- List migrations:
```
dotnet ef <NameMigration> list
```
