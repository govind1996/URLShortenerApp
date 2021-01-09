
# URL Shortener Project Created using React and ASP.NET CORE APIS



## About Project

This project has two parts

1.	URLShortenerAPI: REST API created using ASP.NET Core
2.	Shortify: A React UI app to consume the URLShortenerAPI


## About Platform Used 

### Microsoft Visual Studio Community 2019<br>
Link to download Microsoft Visual Studio Community 2019: - https://visualstudio.microsoft.com/vs/ 

### Microsoft SQL Server 2018<br>
Link to download SQL Server Express: - https://www.microsoft.com/en-in/download/

### Visual Studio Code<br>
Link to download Visual studio code: - https://code.visualstudio.com/download 

## Depenedencies

### External packages which are used in .Net Core Project
1. JWT Token for Authentication of APIS
2. AutoMapper
3. EntityFramework
4. Swashbuckle.AspNetCore

### External packages which are used in Angular Project
1. @material-ui
2. react-redux
3. moment
4. react-copy-to-clipboard
5. redux-thunk
6. axios

## How To Run Both Projects side by side

1. First of all Clone repository to your local machine which have two project.
2. Setting Up Database
   - Open UrlShortnerDbContext.cs file and add connection string of Database
   - Open cmd in DAL folder and run following commands
       - dotnet tool install --global dotnet-ef
       - dotnet ef database update
3. Running URLShortenerApi
   - Open cmd and CD to URLShortenerApi folder and run following command
       - dotnet run
4. Running Shortify (UI)
   - Open cmd and CD to shortify folder and run following command
       - dotnet run
- visit https://localhost:44364/swagger for URLShortnerApi Swagger Documentation
- visit https://localhost:44322/ for the Shortify UI Application

## Note : Run Visual studio, Visual Studio Code or Command Prompt as Administrator in windows system to avoide some issue.


