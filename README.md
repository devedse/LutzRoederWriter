# Devedse.LutzRoederWriter

A modern NuGet distribution of **Lutz Roeder's Writer**, preserving the original `Writer` namespaces and `htmlwriter.dll` assembly identity for compatibility.

## Build status

| GitHubActions Builds |
|:--------------------:|
| [![GitHubActions Builds](https://github.com/devedse/LutzRoederWriter/workflows/GitHubActionsBuilds/badge.svg)](https://github.com/devedse/LutzRoederWriter/actions/workflows/githubactionsbuilds.yml) |

## Code Coverage Status

| CodeCov |
|:-------:|
| [![codecov](https://codecov.io/gh/devedse/LutzRoederWriter/branch/master/graph/badge.svg)](https://codecov.io/gh/devedse/LutzRoederWriter) |

## Package

| NuGet |
|:-----:|
| [![NuGet](https://img.shields.io/nuget/v/Devedse.LutzRoederWriter.svg)](https://www.nuget.org/packages/Devedse.LutzRoederWriter/) |

Install with:

dotnet add package Devedse.LutzRoederWriter

The package targets:

- .NET Framework 4.6.2
- .NET 10 for Windows

Writer uses Windows Forms, COM, and the legacy MSHTML engine, so applications using it are Windows-only.

## Compatibility

The package is intended as a drop-in replacement for the original `htmlwriter.dll`.

Important compatibility details:

- Assembly name remains `htmlwriter`
- Original `Writer`, `Writer.Forms`, and `Writer.Html` namespaces are preserved
- Original public API is preserved as much as possible
- The NuGet package name is `Devedse.LutzRoederWriter`

## Documentation

- [Usage guide](docs/usage.md)
- [Migration guide](docs/migration.md)
- [Architecture notes](docs/architecture.md)

## Attribution and license

The original software was written by **Lutz Roeder**.

This repository is an altered and modernized distribution packaged by **Devedse**.

See:

- [LICENSE](LICENSE)
- [NOTICE](NOTICE)

## Links

| Resource | Link |
|:---------|:-----|
| GitHub | https://github.com/devedse/LutzRoederWriter |
| Releases | https://github.com/devedse/LutzRoederWriter/releases |
| NuGet | https://www.nuget.org/packages/Devedse.LutzRoederWriter/ |
| Codecov | https://codecov.io/gh/devedse/LutzRoederWriter |