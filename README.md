# CustomIdentityAuth

A .NET 8 project that customizes authentication processes using ASP.NET Identity, providing features like JWT, refresh tokens, and role management for flexible and secure identity management.

## Features

- **ASP.NET Identity Integration**: Leverages ASP.NET Identity for managing users, roles, and claims.
- **JWT Authentication**: Implements JSON Web Tokens for secure user authentication.
- **Refresh Tokens**: Provides mechanisms for refreshing JWTs to maintain user sessions securely.
- **Role Management**: Includes role-based access control (RBAC) to manage user permissions.
- **Custom Result Types**: Utilizes custom result types to enhance the clarity and robustness of API responses.
- **Seed Data**: Implements seed data to initialize the application with default users and roles.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/ersin-kaya/CustomIdentityAuth.git
    cd CustomIdentityAuth
    ```

2. **Set up the database:**

    - Update the connection string in `appsettings.json` to point to your SQL Server instance.

3. **Apply migrations:**

    ```bash
    dotnet ef database update
    ```

4. **Run the application:**

    ```bash
    dotnet run
    ```

### Testing

You can use tools like [Postman](https://www.postman.com/) or [Swagger](https://swagger.io/) to test the API endpoints. Swagger is configured in the project and can be accessed by navigating to `/swagger` when the application is running.

## Usage

- **Authentication:**
  - Use the `/api/auth/login` endpoint to authenticate users.
  - Use the `/api/auth/refresh-token` endpoint to refresh JWTs.

- **User Management:**
  - Use the `/api/users` endpoint to manage users.
  - Use the `/api/roles` endpoint to manage roles.

## Contributing

Contributions are welcome! Please fork this repository, make your changes, and submit a pull request.

## Contact

For more information, please contact `ersin-kaya@outlook.com.tr`
