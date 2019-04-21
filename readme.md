# Brassy Api

Brassy api is a prototype dotnet core graphql backend service. This project has a series of steps to get the project up and running.

## Requirements

Must have [Dotnet core](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro), and [Node](https://nodejs.org/en/) installed on your machine.

Additionally, you will need [SQL server](https://www.microsoft.com/en-us/sql-server/sql-server-2017), or [Docker](https://www.docker.com/) and a [SQL server container](https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-2017&pivots=cs1-bash) running.

## Getting Started.

To get all seed data + databases set up, please modify the `appsettings.json` connection string info with your SQL information, then take the following steps in your terminal/command line to get the app up and running:

-   `dotnet restore` obtains all NuGet packages
-   `npm i` downloads all npm packages
-   `npm run build` builds the static files for wwwroot
-   `dotnet build` builds a working version of the project
-   `dotnet ef migrations add Inital -o .\EntityFramework\Migrations` generates the migration folder and all `EFCore` requirements.
-   `dotnet run` starts the application, should be running on 5000/5001 by default.
