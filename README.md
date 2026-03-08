# WhiteBox CRUD Template

A C# solution template for building CRUD applications with layered architecture, clean code, and best practices.

## Description

This template provides a structured approach to developing CRUD (Create, Read, Update, Delete) applications in C#. It follows clean architecture principles with separate layers for domain logic, use cases, and validation. The template includes base classes and interfaces to accelerate development while maintaining scalability and maintainability.

## Features

- **Layered Architecture**: Separates concerns into Domain, Use Cases, and Validation layers.
- **Base Classes**: Includes `BaseModel`, `BaseUseCase`, `BaseRepository`, and validators for common CRUD operations.
- **Dependency Injection Ready**: Designed to work seamlessly with DI containers.
- **Unit Tests**: Comes with a test project using xUnit for testing use cases and repositories.
- **Validation**: Integrated FluentValidation for model validation.
- **Clean Code Principles**: Promotes SOLID principles and maintainable code.

## Project Structure

```
CRUDTemplate.sln
├── CRUDTemplate.Domain/
│   ├── Interface/
│   │   ├── IBaseRepository.cs
│   │   ├── IBaseUseCase.cs
│   │   └── InterfaceExampleRepository.cs
│   ├── Model/
│   │   ├── BaseModel.cs
│   │   └── ModelExample.cs
│   ├── UseCase/
│   │   ├── BaseUseCase.cs
│   │   └── ExampleUseCase.cs
│   └── Validator/
│       ├── BaseModelValidator.cs
│       └── ModelExampleValidator.cs
└── CRUDTemplate.Test/
    ├── ExampleUseCaseTest.cs
    ├── RepositoryExampleTest.cs
    ├── Base/
    │   ├── BaseRepositoryTest.cs
    │   └── BaseUseCaseTest.cs
    └── CRUDTemplate.Test.csproj
```

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or later, or any C# IDE

### Installation

1. Install the template:
   ```
   dotnet new install <path-to-template-directory>
   ```

2. Create a new project from the template:
   ```
   dotnet new crud-template-classlib -n MyCrudApp
   ```

3. Navigate to the project directory:
   ```
   cd MyCrudApp
   ```

4. Restore dependencies:
   ```
   dotnet restore
   ```

5. Run tests:
   ```
   dotnet test
   ```

### Usage

- Extend `BaseModel` for your entities.
- Implement `IBaseRepository` for data access.
- Create use cases inheriting from `BaseUseCase`.
- Add validators using `BaseModelValidator`.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This template is provided as-is. See the license file for details.

## Author

Juan Ignacio Brecevich