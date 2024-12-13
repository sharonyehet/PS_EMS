## IMPORTANT SETUP NOTES

#### Setup for local environment

1. Clone the project and install required packages.
2. Setup your localhost database with Microsoft SQL Server as below.

```
Server: localhost
Database: PsEMS
```

3. Update the default connection string in appsettings.json with your SQL Authentication.

```
"Server=localhost;Database=PsEMS;User Id={your_user_id};Password={your_password};Encrypt=True;TrustServerCertificate=True;"
```

4. Run data migration to sync your local database schema with `dotnet ef database update`.
5. Start the application with `dotnet run`.
6. Access swagger from http://localhost:5165/index.html
