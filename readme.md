# Brassy Api

Brassy api is a prototype dotnet core graphql backend service. This project has a series of steps to get the project up and running.

## Getting Started.

To get all seed data + databases set up, please modify the `appsettings.json` connection string info with your SQL information, then take the following steps in your terminal/command line to get the app up and running:

-   `dotnet restore` obtains all NuGet packages
-   `dotnet build` builds a working version of the project
-   `dotnet ef migrations add Inital -o .\EntityFramework\Migrations` generates the migration folder and all `EFCore` requirements.
-   `dotnet run` starts the application, should be running on 5000/5001 by default.
