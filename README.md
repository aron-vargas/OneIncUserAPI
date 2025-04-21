# OneIncUserAPI

OneIncUserAPI is a .NET 9.0-based web API designed to manage users, roles, and their relationships. It leverages an in-memory database for data storage and provides endpoints for CRUD operations on users, roles, and user-role associations.

## Features

- **User Management**: Add, retrieve, update, and delete users.
- **Role Management**: Manage roles and their descriptions.
- **User-Role Association**: Link users to roles and manage their relationships.
- **In-Memory Database**: Uses `Microsoft.EntityFrameworkCore.InMemory` for lightweight data storage.
- **Swagger Integration**: API documentation and testing via Swagger UI.

## Technologies Used

- **.NET 9.0**
- **Entity Framework Core (In-Memory Database)**
- **ASP.NET Core Web API**
- **Swagger/OpenAPI**

## Project Structure

- **Controllers**: Contains API controllers for managing users, roles, and user-role associations.
- **Core**: Includes core entities (`AppUser`, `AppRole`, `UserRole`) and the repository pattern implementation (`ApplicationRepository`).
- **Program.cs**: Configures services, middleware, and the application pipeline.

## Installation

1. Clone the repository:
```
git clone https://github.com/your-repo/OneIncUserAPI.git
cd OneIncUserAPI
```

2. Ensure you have the .NET 9.0 SDK installed. You can download it from [here](https://dotnet.microsoft.com/download).

3. Restore dependencies:
```
dotnet restore
```

4. Run the application:
```
dotnet run
```

## Usage

Once the application is running, you can access the Swagger UI at:
```
https://localhost:<port>/swagger
```

### Example Endpoints

- **Get All Users**: `GET /user/all`
- **Get User by ID**: `GET /user/{UserId}`
- **Add User**: `POST /user/add`
- **Delete User**: `DELETE /user/{UserId}`

## Dependencies

The project uses the following NuGet packages:

- `Microsoft.EntityFrameworkCore.InMemory` (9.0.4): In-memory database provider.
- `Swashbuckle.AspNetCore` (6.5.0): Swagger/OpenAPI tools for API documentation.
- `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` (9.0.4): Developer exception page for EF Core.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

