# Contributing to TofuPilot C# SDK

Thank you for your interest in contributing to the TofuPilot C# SDK!

## About This Project

This C# SDK was created with [Claude](https://claude.ai), Anthropic's AI assistant, as a port of the official [TofuPilot Python SDK](https://github.com/tofupilot/tofupilot).

## Code of Conduct

This project adheres to the Contributor Covenant code of conduct. By participating, you are expected to uphold this code. Please report unacceptable behavior to support@tofupilot.com.

## Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- An IDE such as Visual Studio, VS Code, or JetBrains Rider

### Setting Up the Development Environment

1. Clone the repository:
   ```bash
   git clone https://github.com/tofupilot/tofupilot-csharp.git
   cd tofupilot-csharp
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

4. Run tests:
   ```bash
   dotnet test
   ```

## Project Structure

```
tofupilot-csharp/
├── src/
│   ├── TofuPilot.Abstractions/    # Shared types, enums, exceptions
│   └── TofuPilot/                 # Main API
├── tests/
│   ├── TofuPilot.Tests/           # Unit tests
│   └── TofuPilot.IntegrationTests/ # Integration tests
└── py/                            # Original Python SDK (reference)
```

## How to Contribute

### Reporting Bugs

If you find a bug, please open an issue with detailed information on how to reproduce it.

### Suggesting Features

You can suggest new features by opening an issue.

### Submitting Pull Requests

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/my-feature`)
3. Make your changes
4. Ensure all tests pass (`dotnet test`)
5. Commit your changes using [Conventional Commits](https://www.conventionalcommits.org/):
   - `fix:` for bug fixes (SemVer patch)
   - `feat:` for new features (SemVer minor)
   - `feat!:`, `fix!:`, etc., for breaking changes (SemVer major)
   - `docs:` for documentation changes
   - `test:` for test changes
   - `refactor:` for refactoring
6. Push to your fork (`git push origin feature/my-feature`)
7. Open a Pull Request

### Commit Message Format

```
type(scope): description

[optional body]

[optional footer]
```

Examples:
```
feat(runs): add support for batch deletion
fix(http): handle timeout exceptions properly
docs: update README with new examples
```

## Development Guidelines

### Code Style

- Follow standard C# naming conventions
- Use nullable reference types (`#nullable enable`)
- All public APIs should have XML documentation comments
- Use `async`/`await` for all I/O operations

### Testing

- Write unit tests for all new functionality
- Use xUnit as the testing framework
- Use FluentAssertions for assertions
- Use Moq for mocking dependencies

### Running Integration Tests

Integration tests require environment variables:

```bash
export TOFUPILOT_URL="https://www.tofupilot.com"
export TOFUPILOT_API_KEY="your-api-key"
export TOFUPILOT_PROCEDURE_ID="your-procedure-id"

dotnet test tests/TofuPilot.IntegrationTests
```

## How is the Package Published?

TofuPilot C# SDK is published to [NuGet](https://www.nuget.org/packages/TofuPilot). Version publishing is handled through GitHub Actions.

### Testing Locally

1. Create a new branch for testing your changes
2. Apply any changes to the code as needed
3. In `Directory.Build.props`, update the version:
   ```xml
   <Version>X.Y.Z-dev</Version>
   ```
4. Build the package:
   ```bash
   dotnet pack -c Release
   ```
5. The `.nupkg` files will be in `src/*/bin/Release/`

### Releasing a Production Version

Only members of the TofuPilot team are allowed to trigger new releases (which automatically upload to NuGet).

To trigger an automatic release:
1. Update the version in `Directory.Build.props`
2. Merge to main

## Questions?

If you have questions, feel free to:
- Open an issue on GitHub
- Contact us at support@tofupilot.com

## License

By contributing, you agree that your contributions will be licensed under the MIT License.
