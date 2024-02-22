# Dictionary auth API

## Prerequisites

- [↑ Docker](https://www.docker.com)
- [↑ GNU Make](https://www.gnu.org/software/make)
- [↑ .NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Run application

1\. Run application infrastructure:

```bash
make run-infrastructure

# Shutdown infrastructure:
# make shutdown-infrastructure
```

2\. Run application:

```bash
dotnet run --project "src/Dictionary.Auth.Api"
```

3\. Navigate to <http://localhost:2200>.
