# Architecture and maintenance

## Components

### `Writer.Html`

This is the HTML editing layer.

- `HtmlControl` hosts the MSHTML COM document and exposes editing commands.
- `HtmlSelection` wraps MSHTML text and element selection operations.
- `HtmlTextFormatting` maps formatting properties and actions to MSHTML commands.
- `HtmlElement` wraps DOM elements.
- `HtmlFormatter` is a standalone legacy-HTML formatter.
- `NativeMethods` contains the COM and Win32 interop contracts required by the
  original implementation.

### `Writer.Forms`

This is the command-bar UI layer.

- `CommandBarManager` owns command bars and routes keyboard messages.
- `CommandBar` renders menu, toolbar, and popup styles.
- `CommandBarItem` and its subclasses model buttons, check boxes, combo boxes,
  menus, and separators.
- The implementation uses legacy Win32 common controls to preserve behavior and
  public API compatibility.

### `Writer`

This is the application/document layer.

- `CommandManager`, `CommandState`, and `ICommandTarget` implement named command
  dispatch and status updates.
- `Document` combines an editable HTML document with file and command behavior.
- `htmlwriter` is the complete legacy editor user control.

## Compatibility boundaries

The following identifiers are intentional and must not be modernized without a
major compatibility decision:

- Assembly name: `htmlwriter`
- Assembly version: `1.0.0.0`
- Existing public namespaces, types, members, and COM declarations
- Legacy Windows Forms types exposed by the public API

The NuGet identity is `Devedse.LutzRoederWriter` so the altered distribution is
not confused with an original package from Lutz Roeder.

## Working with `NativeMethods`

`Writer.Html.NativeMethods` and `Writer.Forms.NativeMethods` are low-level
interop layers. They intentionally use legacy COM and Win32 shapes and are not
general-purpose APIs.

When changing them:

1. Preserve GUIDs, interface types, marshaling attributes, field order, and enum values.
2. Preserve the published public API and assembly identity.
3. Test both `net462` and `net10.0-windows`.
4. Avoid stylistic refactoring that changes COM layout or dispatch signatures.

## Release process

GitHub Actions assigns versions as `1.0.<build number>`, builds both target
frameworks, runs tests, creates `.nupkg` and `.snupkg` files, uploads them as
workflow artifacts, and publishes the package from `master` when
`NUGET_API_KEY` is configured.
