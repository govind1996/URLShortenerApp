
# URL Shortener Project Created using React and ASP.NET CORE APIS



## Getting Started

This project has two parts

1.	URLShortenerAPI: REST API created using ASP.NET Core
2.	Shortify: A React UI app to consume the URLShortenerAPI


### About Platform Used 

### Microsoft Visual Studio Community 2019<br>
Link to download Microsoft Visual Studio Community 2017: - https://visualstudio.microsoft.com/vs/ 

### Microsoft SQL Server 2018<br>
Link to download SQL Server Express: - https://www.microsoft.com/en-in/download/

### Visual Studio Code<br>
Link to download Visual studio code: - https://code.visualstudio.com/download 

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

### How To Run Both Projects side by side.
1. First of all Clone repository to your local machine which have two project.
1.1. Setting Up Database
1.1.1. Open UrlShortnerDbContext.cs file and add connection string of Database
1.1.2. Open cmd in DAL folder and run following commands
       - dotnet tool install --global dotnet-ef
       - dotnet ef database update
2.1. Running URLShortenerApi
2.1.1 Open cmd and CD to URLShortenerApi folder and run following command
       - dotnet run
3.1. Running Shortify (UI)
3.1.1 Open cmd and CD to shortify folder and run following command
       - dotnet run
- visit https://localhost:44364/swagger for URLShortnerApi Swagger Documentation
- visit https://localhost:44322/ for the Shortify Application

## Note : Run Visual studio, Visual Studio Code or Command Prompt as Administrator in windows system to avoide some issue.


