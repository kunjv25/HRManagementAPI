# HR Management API

A backend **HR Management System Web API** built with ASP.NET Core and Entity Framework Core.
This project is being developed as a learning project to understand real-world Web API development, database integration, validation, error handling, and application architecture.

## Features

* Employee CRUD operations
* Department CRUD operations
* Employee ↔ Department relationship
* Employee pagination
* Employee search
* Employee Filter
* DTO-based request and response models
* Input validation
* Department name uniqueness
* Employee email uniqueness
* Global exception handling
* SQL Server database integration
* Entity Framework Core migrations
* Async/await database operations

## Technologies Used

* C#
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* LINQ
* Swagger / OpenAPI
* Visual Studio
* Git & GitHub

## Project Structure


HRManagementAPI
│
├── Controllers
│   ├── EmployeesController.cs
│   └── DepartmentsController.cs
│
├── Data
│   └── HRDbContext.cs
│
├── DTOs
│   ├── Department
│   │   ├── DepartmentCreateDto.cs
│   │   ├── DepartmentResponseDto.cs
│   │   └── DepartmentUpdateDto.cs
│   │
│   └── Employee
│       ├── EmployeeCreateDto.cs
│       ├── EmployeeResponseDto.cs
│       ├── EmployeeUpdateDto.cs
│       └── EmployeePagedResponseDto.cs
│
├── Middleware
│   └── ExceptionHandlingMiddleware.cs
│
├── Models
│   ├── Employee.cs
│   └── Department.cs
│
├── Services
│   ├── Interfaces
│   │   ├── IEmployeeService.cs
│   │   └── IDepartmentService.cs
│   │
│   ├── EmployeeService.cs
│   └── DepartmentService.cs
│
├── Migrations
│
├── Program.cs
├── appsettings.json
└── README.md


## Database

The project uses **SQL Server** with Entity Framework Core.

### Main Tables

#### Employees

* Id
* FirstName
* LastName
* Email
* Phone
* Salary
* JoiningDate
* LeavingDate
* IsActive
* DepartmentId

#### Departments

* Id
* DepartmentName

### Relationship

Each employee belongs to one department.


Department
    1
    │
    │
    │
    ∞
Employee


'Employee.DepartmentId' references 'Department.Id'.

## API Endpoints

### Employees

| Method | Endpoint            | Description        |
| ------ | --------------------| ------------------ |
| GET    | /api/employees      | Get all employees  |
| GET    | /api/employees/{id} | Get employee by ID |
| POST   | /api/employees      | Create employee    |
| PUT    | /api/employees/{id} | Update employee    |
| DELETE | /api/employees/{id} | Delete employee    |

### Employee Search and Pagination

Pagination
GET /api/employees?pageNumber=1&pageSize=10


Search:
GET /api/employees?search=kunj


Pagination + search:
GET /api/employees?pageNumber=1&pageSize=10&search=kunj


### Departments

| Method | Endpoint              | Description          |
| ------ | ----------------------| -------------------- |
| GET    | /api/departments      | Get all departments  |
| GET    | /api/departments/{id} | Get department by ID |
| POST   | /api/departments      | Create department    |
| PUT    | /api/departments/{id} | Update department    |
| DELETE | /api/departments/{id} | Delete department    |

## Validation and Error Handling

The API uses Data Annotations for DTO validation.

Examples:

* Required fields
* String length validation
* Email validation
* Phone validation
* Salary range validation

Global exception handling is implemented using custom middleware.

Unhandled exceptions are logged and returned to the client as a controlled JSON response instead of exposing internal exception details.

Example:

{
  "status": 500,
  "message": "An unexpected error occurred."


## Running the Project

### 1. Clone the repository

git clone <github-repository-url>


### 2. Configure SQL Server

Update the connection string in: appsettings.json

Example:
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=HRManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
}


### 3. Apply migrations

Run powershell :
Update-Database


Or using the .NET CLI:

bash
dotnet ef database update


### 4. Run the application

bash
dotnet run


Open Swagger to test the API.

## Learning Goals

This project is being developed to understand:

* ASP.NET Core Web API
* REST API principles
* Dependency Injection
* Service Layer architecture
* DTOs
* Entity Framework Core
* LINQ and IQueryable
* SQL Server
* Relationships and foreign keys
* Pagination and filtering
* Validation
* Global exception handling
* Async programming
* Git and GitHub
* API deployment

## Future Improvements

Planned features include:

* Authentication and Authorization
* JWT access and refresh tokens
* Role-based access control
* Logging
* Advanced filtering and sorting
* Unit testing
* Integration testing
* Frontend web application
* Azure deployment
* Employee attendance
* Leave management
* Payroll management
