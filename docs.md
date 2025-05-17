# ClearerPayAuth Authentication System

## Overview
ClearerPayAuth is a robust authentication system built using ASP.NET Core Web API. It follows the Onion Architecture and Repository Pattern to ensure modularity, scalability, and maintainability. The system provides secure user registration, login, and token-based authentication using JWT.

---

## Features
- **User Registration**: Securely stores user credentials with hashed passwords.
- **User Login**: Validates user credentials and generates JWT tokens.
- **Token Validation**: Middleware to protect endpoints by validating JWT tokens.
- **Onion Architecture**: Clear separation of concerns across layers.
- **Repository Pattern**: Abstracts database access for better maintainability.
- **Dependency Injection**: Ensures loose coupling between components.
- **Secure Password Hashing**: Uses BCrypt for password hashing.

---

## Project Structure
The project is organized into the following layers:

### **1. Domain Layer**
- **Purpose**: Contains core entities and interfaces.
- **Key Components**:
  - `User` Entity:```csharp
public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    }
```  - Repository Interface: `IUserRepository`

### **2. Application Layer**
- **Purpose**: Contains business logic and service interfaces.
- **Key Components**:
  - `AuthService`: Handles user registration and login.
  - `JwtTokenGenerator`: Generates JWT tokens.
  - DTOs: `RegisterUserDto`, `LoginUserDto`.

### **3. Infrastructure Layer**
- **Purpose**: Implements repositories and database access.
- **Key Components**:
  - `UserRepository`: Implements `IUserRepository` for database operations.

### **4. Presentation Layer**
- **Purpose**: Contains controllers and middleware.
- **Key Components**:
  - `AuthController`: Exposes API endpoints for authentication.
  - `JwtMiddleware`: Validates JWT tokens for protected endpoints.

---

## API Endpoints

### **1. User Registration**
- **Endpoint**: `POST /api/auth/register`
- **Request Body**:
```json
{
  "username": "string",
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "password": "string"
}
```
- **Response**:
```text
{
  "message": "User registered successfully."
}
```
- **Description**: Registers a new user and stores their credentials securely.
- **Example**:
```bash
curl -X POST http://localhost:5000/api/auth/register \
-H "Content-Type: application/json" \
-d '{
  "firstName": "Test",
  "lastName": "User",
  "email": "testuser@example.com",
  "password": "password123"
  }'
  ```
- **Notes**: Passwords are hashed before storage.
- **Error Handling**: Returns appropriate error messages for invalid input or existing users.
- **Security**: Passwords are hashed using BCrypt before storage.
- **Validation**: Ensures all required fields are provided and valid.
- **Response Codes**:
  - `201 Created`: User registered successfully.
  - `400 Bad Request`: Invalid input or user already exists.
  - `500 Internal Server Error`: Server error during registration.
### **2. User Login**
- **Endpoint**: `POST /api/auth/login`
- **Request Body**:
```json
{
  "username": "string",
  "password": "string"
}
```
- **Response**:
```json
{
  "token": "string"
}
```
- **Description**: Authenticates a user and returns a JWT token.
- **Example**:
```bash
curl -X POST http://localhost:5000/api/auth/login \
-H "Content-Type: application/json" \
-d '{
  "username": "testuser",
  "password": "password123"
}'
```
- **Notes**: Token is used for subsequent requests to protected endpoints.
- **Error Handling**: Returns appropriate error messages for invalid credentials.
- **Security**: Token is signed and can be validated using the secret key.
- **Validation**: Ensures all required fields are provided and valid.
- **Response Codes**:
  - `200 OK`: User authenticated successfully, token returned.
  - `401 Unauthorized`: Invalid credentials.
  - `500 Internal Server Error`: Server error during login.

### **3. `Get /api/auth/secure`
- **Endpoint**: `GET /api/auth/secure`
- **Description**: A protected endpoint that requires a valid JWT token.
- **Example**:
```bash
curl --location 'https://localhost:7042/api/Auth/secure' \
--header 'Authorization: Bearer your_token'
```
- **Notes**: Only accessible with a valid JWT token.
- **Error Handling**: Returns `401 Unauthorized` if the token is invalid or missing.
- **Security**: Token is validated using the secret key.
- **Validation**: Ensures the token is present and valid.
- **Response Codes**:
  - `200 OK`: Access granted to the protected resource.
  - `401 Unauthorized`: Invalid or missing token.
  - `500 Internal Server Error`: Server error during request.

### Middleware
- **Purpose**: Validates JWT tokens for protected endpoints.
- **Key Components**:
  - `JwtMiddleware`: Intercepts requests and validates tokens.
 
### Configuration
- **Purpose**: Configures services and middleware in the `Program.com` file.
- **Key Components**:
  - `ConfigureServices`: Registers services, including JWT authentication and repositories.
  - `Configure`: Sets up middleware pipeline, including JWT validation.
