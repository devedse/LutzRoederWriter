# Devedse.LutzRoederWriter

A modern NuGet distribution of **Lutz Roeder's Writer**, preserving the original `Writer` namespaces and `htmlwriter.dll` assembly identity for drop-in compatibility.

This repository contains an altered and modernized distribution of the original Writer source code. The goal is to keep the established public API and behavior intact while making the library usable from modern .NET projects through NuGet.

The project has been converted to an SDK-style project and includes automated builds, compatibility tests, code coverage, symbol packages, GitHub releases, and automated NuGet publishing.

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

Install the package using the .NET CLI:

```shell
dotnet add package Devedse.LutzRoederWriter
```

Or add it directly to the project file:

```xml
<ItemGroup>
  <PackageReference Include="Devedse.LutzRoederWriter" Version="1.0.1" />
</ItemGroup>
```

The latest published version can be found here:

https://www.nuget.org/packages/Devedse.LutzRoederWriter/

## Target frameworks

The package targets:

- .NET Framework 4.6.2
- .NET 10 for Windows

Writer is based on Windows Forms, COM interoperability, and the legacy MSHTML engine. It is therefore a Windows library, including when used with modern .NET.

The project itself can be cross-compiled on Linux because Windows targeting is explicitly enabled.

### Why .NET 10?

The original Writer API exposes several legacy Windows Forms types, including types such as:

- `StatusBar`
- `ContextMenu`
- `Menu`
- `MenuItem`

These types form part of the original Writer API.

.NET 8 and .NET 9 do not provide all of these legacy APIs required for a compatible build. They are available again when targeting .NET 10 for Windows.

Using .NET 8 or .NET 9 would therefore require changing Writer's public API, which would defeat the goal of keeping this package compatible with the original library.

## Compatibility

The package is intended to be as close as possible to a one-for-one replacement for the original `htmlwriter.dll`.

Compatibility characteristics include:

- Assembly name: `htmlwriter`
- Assembly version: `1.0.0.0`
- Root namespace: `Writer`
- Original `Writer` namespace preserved
- Original `Writer.Forms` namespace preserved
- Original `Writer.Html` namespace preserved
- Original public type names preserved
- Original public member names preserved
- No strong-name public key token
- NuGet package name is separate from the assembly name

This means applications that previously referenced:

```text
htmlwriter.dll
```

can migrate to:

```xml
<PackageReference Include="Devedse.LutzRoederWriter" Version="..." />
```

while the assembly loaded by the application remains:

```text
htmlwriter.dll
```

The `Devedse.` package prefix intentionally distinguishes this altered distribution from Lutz Roeder's original software.

It does not change the original assembly identity.

## Tests

The repository contains automated compatibility and behavioral tests.

These include tests that verify:

- The assembly remains named `htmlwriter`
- The assembly version remains `1.0.0.0`
- The assembly remains unsigned
- Original public types remain available
- HTML formatting behavior remains compatible with the original implementation

The test project targets:

```text
net462
net10.0-windows
```

The GitHub Actions Linux build runs the `.NET Framework 4.6.2` tests using Mono.

The `net10.0-windows` target is cross-compiled on Linux but is not executed there because it is a Windows-specific target.

## Continuous Integration

GitHub Actions performs the complete build and release process.

The workflow is split into separate jobs so building, GitHub publishing, and NuGet publishing remain independent.

### Linux build

The main Linux build:

- Checks out the source
- Installs .NET 10
- Installs Mono
- Restores NuGet packages
- Builds the solution in Release mode
- Runs the .NET Framework 4.6.2 tests
- Collects code coverage
- Uploads coverage to Codecov
- Creates the NuGet package
- Creates the NuGet symbol package
- Uploads the packages as GitHub Actions artifacts

The Linux build itself does not publish anything externally.

### GitHub release

After a successful build on `master`, a separate release job:

- Downloads the NuGet artifacts produced by the build
- Creates a GitHub release
- Uses the generated version number as the Git tag
- Attaches the `.nupkg` package
- Attaches the `.snupkg` symbol package

Releases can be found here:

https://github.com/devedse/LutzRoederWriter/releases

### NuGet release

Another separate release job:

- Downloads the package generated by the Linux build
- Publishes the `.nupkg` to NuGet.org
- Runs only for direct pushes to `master`

Published packages can be found here:

https://www.nuget.org/packages/Devedse.LutzRoederWriter/

## Versioning

GitHub Actions generates package versions in the following format:

```text
1.0.<build number>
```

For example:

```text
1.0.1
1.0.2
1.0.3
```

The generated version is used for:

- NuGet package versions
- File versions
- GitHub release names
- GitHub release tags

The assembly version intentionally remains:

```text
1.0.0.0
```

This is done to preserve compatibility with applications that reference the original Writer assembly identity.

## Code coverage

Code coverage is collected during the Linux GitHub Actions build using `dotnet-coverage`.

Coverage results are uploaded to Codecov.

The current coverage report is available here:

https://codecov.io/gh/devedse/LutzRoederWriter

The GitHub Actions workflow uses Codecov's GitHub OIDC integration, so a separate Codecov upload token is not required.

## Documentation

Additional documentation is available in the `docs` directory:

- [Usage guide](docs/usage.md)
- [Migrating from the original DLL](docs/migration.md)
- [Architecture and maintenance notes](docs/architecture.md)

XML documentation is also generated during the build and included beside `htmlwriter.dll` in the NuGet package for IDE IntelliSense support.

## Building

### Build on Windows

Restore the solution:

```shell
dotnet restore LutzRoederWriter.slnx
```

Build:

```shell
dotnet build LutzRoederWriter.slnx -c Release
```

Run all tests:

```shell
dotnet test LutzRoederWriter.slnx -c Release --no-build
```

Create the NuGet package:

```shell
dotnet pack src/LutzRoederWriter/LutzRoederWriter.csproj -c Release --no-build
```

The generated NuGet packages are written to:

```text
artifacts/packages/
```

### Build on Linux

The project can also be built on Linux.

Install Mono first:

```shell
sudo apt-get update
sudo apt-get install -y mono-complete
```

Restore:

```shell
dotnet restore LutzRoederWriter.slnx
```

Build:

```shell
dotnet build LutzRoederWriter.slnx -c Release
```

Run the .NET Framework 4.6.2 tests:

```shell
dotnet test tests/LutzRoederWriter.Tests/LutzRoederWriter.Tests.csproj \
  -f net462 \
  -c Release \
  --no-build
```

Create the NuGet package:

```shell
dotnet pack src/LutzRoederWriter/LutzRoederWriter.csproj \
  -c Release \
  --no-build
```

The `net10.0-windows` target can be compiled on Linux, but applications targeting it must run on Windows.

## Project structure

The main library is located at:

```text
src/LutzRoederWriter/
```

The automated tests are located at:

```text
tests/LutzRoederWriter.Tests/
```

Documentation is located at:

```text
docs/
```

Generated packages are written to:

```text
artifacts/packages/
```

The main solution is:

```text
LutzRoederWriter.slnx
```

## Original Writer

Writer was originally created by **Lutz Roeder**.

This repository is not intended to represent itself as the original Writer source distribution. It is an altered and modernized distribution intended to make the original library easier to consume from current .NET projects.

The modernization includes, among other things:

- SDK-style project files
- Modern .NET target frameworks
- NuGet packaging
- Source Link
- Symbol packages
- XML documentation generation
- Automated compatibility testing
- Linux-based continuous integration
- Code coverage reporting
- Automated GitHub releases
- Automated NuGet publishing

The goal is to modernize the packaging and development infrastructure without unnecessarily changing the original public API.

## Attribution and license

The original software was written by **Lutz Roeder** and remains subject to his original license.

This repository is an altered source distribution modernized and packaged by **Devedse**.

See:

- [LICENSE](LICENSE) for the original license text
- [NOTICE](NOTICE) for information about this altered distribution

The software in this repository must not be represented as an unmodified original release from Lutz Roeder.

## Links

| Resource | Link |
|:---------|:-----|
| GitHub repository | https://github.com/devedse/LutzRoederWriter |
| GitHub Actions | https://github.com/devedse/LutzRoederWriter/actions |
| GitHub releases | https://github.com/devedse/LutzRoederWriter/releases |
| NuGet package | https://www.nuget.org/packages/Devedse.LutzRoederWriter/ |
| Codecov | https://codecov.io/gh/devedse/LutzRoederWriter |