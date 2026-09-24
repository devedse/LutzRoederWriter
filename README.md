# Devedse.LutzRoederWriter

[![GitHub Actions](https://github.com/devedse/LutzRoederWriter/actions/workflows/githubactionsbuilds.yml/badge.svg)](https://github.com/devedse/LutzRoederWriter/actions/workflows/githubactionsbuilds.yml)
[![NuGet](https://img.shields.io/nuget/v/Devedse.LutzRoederWriter.svg)](https://www.nuget.org/packages/Devedse.LutzRoederWriter/)

A modern NuGet distribution of **Lutz Roeder's Writer**, preserving the original `Writer` namespaces and `htmlwriter.dll` assembly identity for drop-in compatibility.

The package retains the established public API and behavior while providing a maintainable SDK-style project, current Windows target frameworks, automated tests, NuGet packaging, symbols, and release automation.

## Package

```shell
dotnet add package Devedse.LutzRoederWriter
```

The package targets:

- .NET Framework 4.6.2
- .NET 10 for Windows

Writer is built on Windows Forms, COM, and the legacy MSHTML engine. It is therefore Windows-only, including on modern .NET.

.NET 8 and .NET 9 removed the legacy `StatusBar`, `ContextMenu`, `Menu`, and `MenuItem` types that occur in Writer's public API. Those types are available again for binary compatibility in .NET 10. Targeting .NET 8 or .NET 9 would therefore require an API-breaking rewrite and would not be a one-for-one replacement.

## Compatibility

The package is intended as a one-for-one replacement for the original `htmlwriter.dll`:

- Assembly name: `htmlwriter`
- Assembly version: `1.0.0.0`
- Root namespaces: `Writer`, `Writer.Forms`, and `Writer.Html`
- Original public type and member names are preserved

Applications using the original assembly can replace the file reference with a `PackageReference`:

```xml
<ItemGroup>
  <PackageReference Include="Devedse.LutzRoederWriter" Version="1.0.1" />
</ItemGroup>
```

The `Devedse.` package prefix intentionally distinguishes this altered distribution
from Lutz Roeder's original software. The installed assembly remains named
`htmlwriter.dll` to preserve compatibility.

## Documentation

- [Usage guide](docs/usage.md)
- [Migrating from the original DLL](docs/migration.md)
- [Architecture and maintenance notes](docs/architecture.md)
- XML documentation is included beside `htmlwriter.dll` in the NuGet package for IDE IntelliSense.

## Versioning

Like [DeveMazeGeneratorCore](https://github.com/devedse/DeveMazeGeneratorCore), release builds use `1.0.<build number>`. The assembly version remains `1.0.0.0` so existing compiled consumers can bind to the replacement DLL.

## Attribution and license

The original software was written by **Lutz Roeder** and remains under his original license. This repository is an altered source distribution modernized and packaged by **Devedse**; it must not be represented as the original source release.

See [LICENSE](LICENSE) for the unmodified license text and [NOTICE](NOTICE) for a summary of the alterations.

## Building

```shell
dotnet restore LutzRoederWriter.slnx
dotnet build LutzRoederWriter.slnx -c Release
dotnet test LutzRoederWriter.slnx -c Release --no-build
dotnet pack src/LutzRoederWriter/LutzRoederWriter.csproj -c Release --no-build
```
