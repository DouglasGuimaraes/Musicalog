# Musicalog

## Steps to Run

### Database Configuration 
1. You may find the script inside: Repository/DatabaseScript/DatabaseScript.sql;
2. After database creation, you can set a proper password for the Musicalog_User (you will need to Change the ConnectionString inside the WebAPI/appsettings.Development.json file) OR use your current Windows Authentication (no changes needed in the solution).

### Start Application

1. You can use the dotnet cli for project restore `dotnet restore` / build(`dotnet build` using the cmd inside the solution folder OR rebuild the application inside Visual Studio;
2. You can use the dontet cli to start the app with the `dotnet run` command OR use the Visual Studio Debug;
3. The default url for local debug will open: http://localhost:5000/swagger.

### Testing Application

1. Make sure the app is running in the local url (http://localhost:5000/swagger) and using cmd, find the solution folder and execute the tests using dotnet cli: <br> `dotnet test`
