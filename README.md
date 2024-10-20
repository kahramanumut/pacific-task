
# 🌊 Pacific Task

## 📚 Project Overview

Pacific Task is a backend project developed using **.NET 8.0**. It follows the **Clean Architecture** principles, making use of layered architecture with clear separation between API, Application, and Data layers. The project utilizes **MediatR** for handling requests and commands in a decoupled manner.

### 🛠️ Technologies Used

- **.NET 8.0 Framework**
- **MediatR (v12.4.1)**: For request handling and implementing the mediator pattern.
- **Dapper**: A lightweight ORM used in the Data layer for database operations.
- **SQLite**: A simple, file-based database used for storing data.
- **XUnit**: Unit testing framework used in the project.
- **Moq**: A library used for mocking dependencies in unit tests.
- **Dependency Injection**: The project follows DI principles for better decoupling and testability.
- **Clean Architecture**: The project adheres to clean architecture principles for separating concerns.

## 📂 Project Structure

- `PacificTask.Api`: The API layer, responsible for exposing endpoints to the clients.
- `PacificTask.Application`: This layer handles the business logic and uses **MediatR** to process requests.
- `PacificTask.Data`: Data access layer, interacting with the database using **Dapper** and **SQLite**.
- `PacificTask.Tests`: Contains unit and integration tests, using **XUnit** and **Moq** for testing.

## 🚀 How to Run the Project

1. Clone this repository.
2. Open the folder in **VS Code**.
3. Make sure you have **.NET 8.0 SDK** installed.
4. Build the solution and run the `PacificTask.Api` project.
5. Use with index.html or API Call

## ✅ Verification

### 1. How did you verify that everything works correctly?

- The project includes unit tests in the `PacificTask.Tests` project.
- Tests were run using **XUnit** and **Moq** to ensure the correctness of the application's behavior, covering the core functionalities.
- Additionally, manual testing of the API endpoints was performed using **index.html** and verified against expected outcomes.
- Here are 5 API call URLs that you can use to test the rules implemented based on the specified conditions (ensure running on https://localhost:7090)

- [User identifier ends with [6, 7, 8, 9]](https://localhost:7090/api/image/testUser7)
- [User identifier ends with [1, 2, 3, 4, 5]](https://localhost:7090/api/image/sampleUser3)
- [User identifier contains at least one vowel (a, e, i, o, u)](https://localhost:7090/api/image/vowelUser)
- [User identifier contains a non-alphanumeric character](https://localhost:7090/api/image/user@name)
- [None of the conditions are met](https://localhost:7090/api/image/simpleUserX)

### 2. How long did it take you to complete the task?

- It took approximately **3 hours** to complete the development of this solution, including testing and verification.

### 3. What else could be done to your solution to make it ready for production?

To make the solution production-ready, the following enhancements could be applied:
- **🔐 Security Improvements**: Implement authentication&authorization mechanisms using **JWT** and validation using **FluentValidation**.
- **⚡ Performance Optimization**: Use caching mechanisms such as **Redis** to optimize data retrieval.
- **📊 Logging and Monitoring**: Implement structured logging using **Serilog** and integrate monitoring tools like **Grafana** for better observability.
- **📦 Database Migrations**: Use **Entity Framework** migrations or a similar tool to manage database schema changes in a production environment.
- **🛡️ Exception Management**: Implement a global exception handling mechanism using middleware to catch and log unhandled exceptions. Ensure proper user-friendly error messages and avoid exposing internal details. You can use Serilog or NLog for structured logging, and consider returning standardized error responses (e.g., with HTTP status codes and clear error messages). Additionally, implementing retry policies (e.g., using Polly) for transient faults can improve reliability.

## 🎯 Conclusion

The Pacific Task project is structured with maintainability and scalability in mind, following modern software development practices and principles. The solution was developed quickly to meet the core requirements while adhering to the KISS principle, ensuring simplicity and clarity in design. It is designed to be extensible and can be easily adapted for future requirements.
