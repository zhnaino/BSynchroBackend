ToDoList:
1.Create a Blank Solution:
Create a new blank solution that will house three projects: AccountService, TransactionService, and CommonModels.

2.AccountServiceAPI (ASP.NET Core web api):
Implement an ASP.NET Core web api  to manage user accounts.
Utilize Entity Framework for database operations.
Use Dependency Injection for communication between IRepository and Repository.
Install necessary packages:
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.Extensions.DependencyInjection.Abstractions

3.TransactionService (ASP.NET Core Web API):
Develop an ASP.NET Core Web API for handling financial transactions.
Use Entity Framework for database interactions.
Implement communication with the AccountService through HTTP/REST API.

4.CommonModels (Class Library):
Create a class library containing common models shared between AccountService and TransactionService.

5.Database Configuration:
Update the connection strings in the appsettings.json files of both AccountService and TransactionService.
Ensure that the "Users" table in the database contains at least one user for testing account creation.

6.Database Setup:
Run the following commands in the Package Manager Console to set up the database:
add-migration NameOfMigration  
(can delete the old migration in the project by run Remove-Migration)
update-database
Note: If you want, you can import the BankTask_db SQL script.
