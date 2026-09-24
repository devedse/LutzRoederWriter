# Usage guide

`Devedse.LutzRoederWriter` contains two useful entry points:

- `Writer.Html.HtmlControl` is the embeddable HTML editing control.
- `Writer.htmlwriter` is the complete legacy Writer surface with menus, toolbar,
  status bar, document handling, and command routing.

The library is Windows-only because it uses Windows Forms, COM, and MSHTML.
Create controls on an STA thread with a Windows Forms message loop.

## Embed the HTML editor

```csharp
using System;
using System.Windows.Forms;
using Writer.Html;

internal sealed class EditorForm : Form
{
    private readonly HtmlControl editor = new()
    {
        Dock = DockStyle.Fill,
    };

    public EditorForm()
    {
        Controls.Add(editor);

        editor.ReadyStateComplete += (_, _) =>
        {
            editor.LoadHtml(
                "<html><body><h1>Hello</h1><p>Edit this text.</p></body></html>",
                "https://example.test/document.html");
        };
    }

    public string GetHtml()
    {
        return editor.SaveHtml();
    }
}
```

Do not call document-dependent operations until `ReadyStateComplete` has fired
or `IsReady` is `true`.

## Format HTML without showing a control

`HtmlFormatter` is independent of the Windows Forms editor and can format HTML
directly:

```csharp
using System.IO;
using Writer.Html;

var formatter = new HtmlFormatter
{
    IndentChar = ' ',
    IndentSize = 2,
    MaxLineLength = 100,
};

using var output = new StringWriter();
formatter.Format("<div><strong>Hello</strong><br>World</div>", output);

string formattedHtml = output.ToString();
```

The formatter intentionally preserves the behavior of the original Writer DLL,
including its handling of legacy HTML and nonstandard elements.

## Work with the selection

Use `HtmlControl.Selection` to inspect or change selected content:

```csharp
HtmlSelection selection = editor.Selection;

if (selection.Type != HtmlSelectionType.Empty)
{
    selection.WrapSelectionInHyperlink("https://example.com/");
}
```

Use `HtmlControl.TextFormatting` for formatting commands. Check the matching
capability property before applying a command:

```csharp
HtmlTextFormatting formatting = editor.TextFormatting;

if (formatting.CanToggleBold)
{
    formatting.ToggleBold();
}
```

## Script integration

Scripts are disabled by default. If trusted content needs to call a host object:

```csharp
editor.ScriptObject = new HostApi();
editor.ScriptEnabled = true;
```

Only enable scripting for content you trust. MSHTML is a legacy browser engine
and should not be used to host untrusted or Internet-sourced active content.

