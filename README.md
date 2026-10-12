# QM

A .NET application for managing organizational risks and related records.

## Overview

The application is organized into separate projects for its web application, data access, models, and shared utilities. Its domain includes risks, causes, actions, and strategic goals, with Entity Framework Core used for persistence.

## Technology

- C# and ASP.NET Core
- Entity Framework Core
- SQL Server
- MSTest test projects

## Getting started

1. Install the .NET SDK required by the solution.
2. Configure the database connection string using the application's configuration or user secrets. Do not commit credentials.
3. Open the solution in Visual Studio, or run the web application project from the command line:

   ```bash
   dotnet run --project WebApplication2
   ```

4. Apply the required Entity Framework migrations to your configured database before using the application.

The exact startup project, configuration keys, and migration setup may depend on the local environment.

## Tests

Run the test projects from the solution directory:

```bash
dotnet test
```
