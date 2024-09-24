# Banking Control Panel

## How to Run
1. Make sure to run database using:

    `docker run --name some-postgres -p 5432:5432 -e POSTGRES_USER=test -e POSTGRES_PASSWORD=test@123 -d postgres`

2. Run database migrations using:

    `dotnet database update`

3. Run the app using:

    `dotnet run`

