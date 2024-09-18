# Fast Clean Architecture Template

[![Nuget](https://img.shields.io/nuget/v/Fast.Clean.Architecture.Solution.Template?label=NuGet)](https://www.nuget.org/packages/Fast.Clean.Architecture.Solution.Template)
[![Nuget](https://img.shields.io/nuget/dt/Fast.Clean.Architecture.Solution.Template?label=Downloads)](https://www.nuget.org/packages/Fast.Clean.Architecture.Solution.Template)
[![Quality gate](https://sonarcloud.io/api/project_badges/quality_gate?project=christianrd_FastCleanArchitecture)](https://sonarcloud.io/summary/new_code?id=christianrd_FastCleanArchitecture)

The purpose of this template is to offer a simple and effective solution for building enterprise applications 
by harnessing the capabilities of Clean Architecture and ASP.NET Core. 
With this template, you can easily set up a Web API following Clean Architecture and Domain Drive Design principles. 
Starting is quick and easy-just install the .NET template (detailed instructions provided below).


## Getting Started

The following prerequisites are required to build and run the solution:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (latest version)

The easiest way to get started is to install the [.NET template](https://www.nuget.org/packages/Fast.Clean.Architecture.Solution.Template):
```
dotnet new install Clean.Architecture.Solution.Template::8.0.1
```

Once installed, create a new solution using the template. You can choose to use Angular, React, or create a Web API-only solution. Specify the client framework using the `-cf` or `--client-framework` option, and provide the output directory where your project will be created. Here are some examples:

To create a ASP.NET Core Web API solution:
```bash
dotnet new fast-ca-sln -o YourProjectName
```

Launch the app:
```bash
cd src/API
dotnet run
```

To learn more, run the following command:
```bash
dotnet new fast-ca-sln --help
```

You can create use cases (commands or queries) by navigating to `./src/Application` and running `dotnet new fast-ca-usecase`. Here are some examples:

To create a new command:
```bash
dotnet new fast-ca-usecase --name CreateTodoList --feature-name TodoLists --usecase-type command --return-type int
```

To create a query:
```bash
dotnet new fast-ca-usecase -n GetTodos -fn TodoLists -ut query -rt TodosVm
```

To learn more, run the following command:
```bash
dotnet new fast-ca-usecase --help
```

## Database

The template is configured to use SQL Server by default. If you would prefer to use SQLite, create your solution using the following command:

```bash
dotnet new fast-ca-sln --use-oracle
```

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

Running database migrations is easy. Ensure you add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure` (optional if in this folder)
* `--startup-project src/API`
* `--output-dir Data/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\API --output-dir Data\Migrations`

## Deploy

The template includes a full CI/CD pipeline. The pipeline is responsible for building, testing, publishing and deploying the solution to Azure.

## Technologies

* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [Maspter](https://github.com/MapsterMapper/Mapster)
* [FluentResult](https://github.com/altmann/FluentResults)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [NetArchTest.Rules](https://github.com/BenMorris/NetArchTest), [TestContainer](https://dotnet.testcontainers.org/) & [Moq](https://github.com/devlooped/moq)

## Versions
The main branch is now on .NET 8.0.

## Support

If you are having problems, please let me know by [create a new issue](https://github.com/christianrd/FastCleanArchitecture/issues/new/choose).

## License

This project is licensed with the [MIT license](LICENSE).
