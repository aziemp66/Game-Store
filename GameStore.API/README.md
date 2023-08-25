# Game Store API

## Starting SQL Server

### Powershell
```powershell
$sa_password="[yourPassword(!)Here123]"

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
```

### Bash
```bash
sa_password="[yourPassword(!)Here123]"

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
```

## Setting the connection string to secret manager

### Powershell
```powershell
$sa_password="[yourPassword(!)Here123]"

dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Server=localhost; Database=gamestore_api; User Id=sa; Password=$sa_password; TrustServerCertificate=True;"
```

### Bash
```bash
sa_password="[yourPassword(!)Here123]"

dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Server=localhost; Database=gamestore_api; User Id=sa; Password=$sa_password; TrustServerCertificate=True;"
```