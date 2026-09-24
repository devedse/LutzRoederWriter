# Migrating from `htmlwriter.dll`

## Replace the assembly reference

Remove the direct reference to the original DLL and add:

```xml
<ItemGroup>
  <PackageReference Include="Devedse.LutzRoederWriter" Version="1.0.1" />
</ItemGroup>
```

No namespace change is required. The package deliberately keeps:

- Assembly name `htmlwriter`
- Assembly version `1.0.0.0`
- Namespaces `Writer`, `Writer.Forms`, and `Writer.Html`
- Original public type and member names

## Supported target frameworks

The package contains assets for:

- .NET Framework 4.6.2
- .NET 10 for Windows

.NET 8 and .NET 9 removed the legacy Windows Forms `StatusBar`, `ContextMenu`,
`Menu`, and `MenuItem` types present in Writer's public API. .NET 10 provides
those types for binary compatibility. Shipping .NET 8 or .NET 9 assets would
therefore require changing the public API and would not be a drop-in replacement.

## Platform requirements

Writer requires:

- Windows
- Windows Forms
- An STA UI thread
- A running Windows message loop for hosted controls
- MSHTML/COM components available on the operating system

The package does not make Writer cross-platform and does not replace MSHTML with
a modern browser engine.

## Compatibility guarantees

The project protects drop-in compatibility through automated checks for:

- Assembly name and version
- The established public type set
- Representative `HtmlFormatter` behavior on both target frameworks
- Successful builds for .NET Framework 4.6.2 and .NET 10 for Windows

## Binding behavior

The NuGet package version follows `1.0.<build number>`, while
`AssemblyVersion` remains `1.0.0.0`. This allows existing compiled consumers to
bind to the replacement assembly without changing the original assembly identity.
