# User Authentication and Role Management API

This project is a very basic user authentication and role management API built with C# and .NET Core. It uses JWT (JSON Web Token) to securely authenticate users and manage role-based access.

## Features

- **User Registration**: Users can register with an email, username, and password.
- **Authentication**: Users can log in and receive a JWT token for secure API access.
- **Role Management**: Users are assigned roles (Admin or User), which determine their access to specific endpoints.
- **JWT Security**: The API generates and verifies JWT tokens containing user IDs and roles, signed with a secret key.
- **Database Interaction**: Managed through Entity Framework Core, storing users and roles in the database.

## Endpoints

- `POST /api/auth/signup`: Register a new user.
- `POST /api/auth/signin`: Authenticate a user and generate a JWT token.
- `GET /api/users`: Fetch users. (User, Admin)
- `GET /api/users/{id}`: Fetch one user. (User, Admin)
- `DELETE /api/users{id}`: Delete user data. (Admin Only)
- `PUT /api/users{id}`: Update user data. (Admin Only)
