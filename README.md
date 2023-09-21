# Customer Management

## Assignment
- The goal of this test is to create an API to register a new user and authenticate
- Generate another API to generate the customer based on the token generated

## Solution
* - REST API using minipal API, CQRS, Mediatr, Dapper, Automapper, SQL Server, xUnit
  - Clean Architecture, TDD
  - The authentication is customized

## Requirements
You must have *docker* installed on your operating system (Linux, Windows or Mac).  

# Steps to run the application

*Obs.: Navigate the folder until you reach the project' folder.*

### Run the command:
- ` docker-compose -f docker-compose.yml up` 

### Test using OpenAPI
- http://localhost:5221/swagger/index.html for Authentication API
- http://localhost:8080/swagger/index.html for Customer API

# Steps to debug the application

1. docker-compose -f docker-compose.develop.yml up -d
2. Open the solution with Visual Studio  
3. Right-click into the Solution -> `Properties`  
4. Check `Multiple startup projects` then set as bellow:  
![Startup](/images/MultipleStartupProject.png?raw=true)  
5. Click on Start