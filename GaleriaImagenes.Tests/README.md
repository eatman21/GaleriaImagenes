# GaleriaImagenes Tests

This test project contains unit and integration tests for the GaleriaImagenes application.

## Test Structure

```
GaleriaImagenes.Tests/
├── AccesoDatos/
│   └── CrudTests.cs          # Tests for data access layer
├── BusinessLogic/
│   └── ImageGalleryServiceTests.cs  # Tests for business logic
└── Entidades/
    └── Image_gallery_Tests.cs  # Tests for entity validation
```

## Running Tests

### Using Visual Studio
1. Open the solution in Visual Studio
2. Go to Test > Run All Tests
3. View results in the Test Explorer window

### Using .NET CLI
```bash
dotnet test
```

### With Code Coverage
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## Test Categories

### Unit Tests
- **CrudTests**: Tests CRUD operations with in-memory database
- **ImageGalleryServiceTests**: Tests business logic and image conversion
- **Image_gallery_Tests**: Tests entity validation rules

### Key Testing Technologies
- **xUnit**: Testing framework
- **FluentAssertions**: Readable assertions
- **Moq**: Mocking framework
- **EntityFrameworkCore.InMemory**: In-memory database for testing

## Test Coverage

The tests cover:
- ✅ CRUD operations (Create, Read, Update, Delete)
- ✅ Entity validation
- ✅ Image conversion (byte array ↔ bitmap)
- ✅ Business logic validation
- ✅ Error handling
- ✅ Edge cases (null values, invalid data, etc.)

## Adding New Tests

When adding new tests, follow these patterns:

```csharp
[Fact]
public async Task MethodName_Scenario_ExpectedBehavior()
{
    // Arrange
    // Set up test data and dependencies

    // Act
    // Execute the method being tested

    // Assert
    // Verify the expected outcome
}
```

## Notes

- All database tests use in-memory databases to avoid external dependencies
- Tests are isolated and can run in parallel
- Each test class implements IDisposable to clean up resources
