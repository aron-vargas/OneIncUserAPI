# OneIncUserAPI

OneIncUserAPI is a .NET 8.0-based web API designed to manage users, roles, and their relationships. It leverages an in-memory database for data storage and provides endpoints for CRUD operations on users, roles, and user-role associations.

## Features

- **User Management**: Add, retrieve, update, and delete users.
- **Role Management**: Manage roles and their descriptions.
- **User-Role Association**: Link users to roles and manage their relationships.
- **In-Memory Database**: Uses `Microsoft.EntityFrameworkCore.InMemory` for lightweight data storage.
- **Swagger Integration**: API documentation and testing via Swagger UI.

## Technologies Used

- **.NET 8.0**
- **Entity Framework Core (In-Memory Database)**
- **ASP.NET Core Web API**
- **Swagger/OpenAPI**

## Project Structure

- **Controllers**: Contains API controllers for managing users, roles, and user-role associations. These controllers define the endpoints and handle HTTP requests, delegating business logic to the Core layer.

- **Core**: The Core layer is the heart of the application, encapsulating the domain logic and data access. It is designed with a clean architecture approach to ensure separation of concerns and maintainability. The Core layer includes the following components:
  - **Domain**: Defines the core entities and their relationships. These entities represent the business models of the application.
    - **Models**: Contains the primary domain models:
      - `AppUser`: Represents a user in the system, including properties like `UserId`, `FirstName`, `LastName`, `Email`, and `LastLogin`.
    - **Common**: Provides base classes and interfaces shared across domain models, such as:
      - `EntityBase`: A base class that includes common properties like `Id`, `CreatedOn`, `CreatedBy`, `UpdateOn`, `UpdatedBy`, and `IsActive`.
      - `IEntityBase`: An interface defining the contract for all entities in the application.
  - **Persistence**: Implements the data access layer using the repository pattern.
    - `ApplicationRepository<T>`: A generic repository that provides CRUD operations for entities. It abstracts the data access logic, ensuring that the Core layer is not tightly coupled to the database implementation.
    - `MemoryAppDB`: An in-memory database context implemented using `Entity Framework Core`. It manages the `DbSet` for entities like `AppUser` and applies configurations during model creation.

- **Program.cs**: Configures services, middleware, and the application pipeline. It sets up dependency injection for the Core components, including the repository and database context, and integrates Swagger for API documentation and testing.