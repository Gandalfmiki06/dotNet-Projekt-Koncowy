# ClinicManager

A modern clinic management system built with ASP.NET Core 10, Entity Framework Core, and Microsoft SQL Server. The application uses Docker for database hosting, MVC Views for the user interface, and NLog for application logging.

## Project Structure

* `ClinicManager.Web/`: Main MVC web application.
    * `Controllers`: MVC Controllers.
    * `DTOs`: Data Transfer Objects.
    * `Mappers`: Object Mappings.
    * `Models`: Database object models.
    * `Services`: Dependency Injection.
    * `ViewModels`: 
    * `Views`: MVC Views
    * `wwwroot/upload`: Document scan uploads
    * `Logs`: NLog logs
* `ClinicManager.Tests/`: Unit test suites
* `ClinicManager.PerformanceTests/`: Performance tests using NBomber.

## How to Run

### Requirements:
* .NET 10 SDK
* Docker & Docker Compose

```
./run.sh
```

# CI/CD

Project uses GitHub Actions for automated unit testing.

Workflow runs:

* on push to `master`,
* on Pull Request to `master`.

### Workflow steps

1. Pull repository (`actions/checkout`)
2. Use .NET 10.0.x (`actions/setup-dotnet`)
3. Restore dotnet packages (`dotnet restore`)
4. Compile project (`dotnet build`)
5. Run tests (`dotnet test`)

Workflow uses concurrency, which stops running pipeline when a new commit arrives, making only the newest code under testing, reducing workload.