# CurrencyExchange API

## Overview
The CurrencyExchange API is a solution designed to handle currency exchange operations, including quotes and transfers. It is built using a modular architecture with separate projects for API, Application, Domain, Infrastructure, and Contracts.

## Project Structure

- **CurrencyExchange.Api**: Contains the API controllers and entry point for the application.
- **CurrencyExchange.Application**: Implements the application logic, including commands and queries for Quotes and Transfers.
- **CurrencyExchange.Domain**: Defines the core domain models and business logic.
- **CurrencyExchange.Infrastructure**: Handles persistence and external service integrations.
- **CurrencyExchange.Contracts**: Contains shared contracts and DTOs used across the solution.

## Getting Started

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```

2. Navigate to the project directory:
   ```bash
   cd CurrencyExchange
   ```

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Build the solution:
   ```bash
   dotnet build
   ```

5. Run the API:
   ```bash
   dotnet run --project CurrencyExchange.Api
   ```

## Configuration

The API uses `appsettings.json` and `appsettings.Development.json` for configuration. Update these files to configure API keys, and other settings.

## External API Integration

The CurrencyExchange solution integrates with the [Exchange Rates API](https://exchangeratesapi.io/) to fetch real-time exchange rates. To use this API, you need to generate an access key and configure it as a secret in your project.

### Steps to Generate an Access Key

1. Visit the [Exchange Rates API website](https://exchangeratesapi.io/).
2. Sign up for an account if you don’t already have one.
3. After logging in, navigate to the API keys section.
4. Generate a new API key and copy it.

### Adding the Access Key to Your Project

Follow the steps below to securely store the access key in your project:

#### 1. Enable User Secrets in Your Project

Run the following command from your project directory:
```bash
dotnet user-secrets init
```
This command adds a line in your `.csproj` file:
```xml
<UserSecretsId>your-project-guid</UserSecretsId>
```

#### 2. Add the Secret

Use the CLI to store your secret locally:
```bash
dotnet user-secrets set "ExternalExchangeRatesApi:AccessKey" "your_real_access_key"
```

The secret will be stored securely on your local machine and will not be included in source control. You can access this secret in your application using the `IConfiguration` interface.


## Patterns used in the Solution

The CurrencyExchange API solution follows several design and architectural patterns to ensure scalability, maintainability, and testability. Below are the key patterns used:

### 1. **Clean Architecture**
   - The solution is organized into layers: API, Application, Domain, Infrastructure, and Contracts.
   - Each layer has a specific responsibility and depends only on the layers below it.

### 2. **Dependency Injection**
   - All dependencies are injected using the built-in .NET Core Dependency Injection framework.
   - The `DependencyInjection.cs` files in various projects handle the registration of services.

### 3. **CQRS (Command Query Responsibility Segregation)**
   - The Application layer uses the CQRS pattern to separate read and write operations.
   - Commands are used for write operations (e.g., creating or updating data).
   - Queries are used for read operations (e.g., fetching data).

### 4. **Mediator Pattern**
   - The MediatR library is used to implement the mediator pattern.
   - All commands and queries are handled by their respective handlers, promoting loose coupling.

### 5. **Validation Pipeline**
   - A validation behavior is implemented in the Application layer to validate requests before they reach their handlers.
   - This ensures that invalid requests are caught early in the pipeline.

### 6. **Repository Pattern**
   - The Infrastructure layer uses the repository pattern to abstract data access logic.
   - This promotes testability and decouples the domain logic from the data access logic.

### 7. **Decorator Pattern**
   - Added InMemory caching using decorator pattern to cache results from external provider api.
   - ICacheService is generic, can be extended to any other future inmemory caching requirements.

### 8. **Configuration Management**
   - Configuration settings are managed using `appsettings.json` and environment-specific files like `appsettings.Development.json`.
   - Options pattern is used to bind configuration settings to strongly-typed objects.

### 11. **Result Pattern**
   - The solution implements the Result pattern using the `ErrorOr` package.
   - This pattern is used to encapsulate both successful results and errors in a single return type.
   - It improves error handling and ensures that all outcomes are explicitly handled.
   - Example:
     ```csharp
     public ErrorOr<Quote> GetQuote(string currencyCode)
     {
         if (string.IsNullOrEmpty(currencyCode))
         {
             return Error.Validation("CurrencyCode", "Currency code cannot be null or empty.");
         }

         var quote = _quoteService.GetQuoteByCurrency(currencyCode);
         return quote ?? Error.NotFound("Quote", "Quote not found.");
     }
     ```

## Testing

Unit tests are located in the `CurrencyExchange.Application.Tests` project. To run the tests, use the following command:
```bash
dotnet test
```

## Next Steps and Improvements

To further enhance the CurrencyExchange API, consider implementing the following features and improvements:


### 1. **Security Enhancements**
   - Use HTTPS for all API communications.
   - Implement authentication and authorization using OAuth2 or JWT.
   - Validate all incoming data to prevent injection attacks.

### 2. **API Versioning**
   - Introduce versioning to ensure backward compatibility as the API evolves.
   - Example:
     ```csharp
     [ApiVersion("1.0")]
     [Route("api/v{version:apiVersion}/[controller]")]
     public class QuotesController : ControllerBase
     {
         // ...existing code...
     }
     ```

### 3. **Idempotency**
   - Implement idempotency for operations like transfers to prevent duplicate processing.
   - Use unique request identifiers and store processed requests in a database or cache.
   - Example:
     - Add an `IdempotencyKey` header to requests.
     - Check if the key has already been processed before executing the operation.

### 4. **Rate Limiting**
   - Introduce rate limiting to prevent abuse and ensure fair usage of the API.
   - Use middleware like `AspNetCoreRateLimit` to configure rate limits per user or IP.

### 5. **Logging and Monitoring**
   - Enhance logging to include structured logs for better traceability.
   - Integrate monitoring tools like Application Insights or ELK Stack for real-time insights.

### 6. **Caching**
   - Implement caching for frequently accessed data like exchange rates to improve performance.
   - Use distributed caching solutions like Redis.

### 7. **Integration Tests**
   - Add integration tests to validate end-to-end functionality of the API.
   - Use tools like `xUnit` or `NUnit` along with a test server to simulate API requests.

### 8. **Scalability**
   - Design the API to handle increased load by scaling horizontally.
   - Use cloud services like Azure or AWS to deploy and manage the API.

These improvements will make the CurrencyExchange API more robust, scalable, and user-friendly.

