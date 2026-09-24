using System;
using System.Linq;
using System.Reflection;
using Writer;
using Xunit;

namespace LutzRoederWriter.Tests;

public class AssemblyCompatibilityTests
{
    [Fact]
    public void AssemblyIdentityMatchesOriginal()
    {
        AssemblyName assemblyName = typeof(CommandManager).Assembly.GetName();

        Assert.Equal("htmlwriter", assemblyName.Name);
        Assert.Equal(new Version(1, 0, 0, 0), assemblyName.Version);
        Assert.Empty(assemblyName.GetPublicKeyToken() ?? Array.Empty<byte>());
    }

    [Fact]
    public void OriginalPublicTypesRemainAvailable()
    {
        string[] expectedTypeNames =
        [
            "Writer.CommandManager",
            "Writer.CommandState",
            "Writer.Document",
            "Writer.Forms.CommandBar",
            "Writer.Forms.CommandBarButton",
            "Writer.Forms.CommandBarButtonBase",
            "Writer.Forms.CommandBarCheckBox",
            "Writer.Forms.CommandBarCollection",
            "Writer.Forms.CommandBarComboBox",
            "Writer.Forms.CommandBarContextMenu",
            "Writer.Forms.CommandBarControl",
            "Writer.Forms.CommandBarItem",
            "Writer.Forms.CommandBarItemCollection",
            "Writer.Forms.CommandBarManager",
            "Writer.Forms.CommandBarMenu",
            "Writer.Forms.CommandBarSeparator",
            "Writer.Forms.CommandBarStyle",
            "Writer.Forms.CommandBarTypeConverter",
            "Writer.Html.HtmlAlignment",
            "Writer.Html.HtmlControl",
            "Writer.Html.HtmlElement",
            "Writer.Html.HtmlFontSize",
            "Writer.Html.HtmlFormat",
            "Writer.Html.HtmlFormatter",
            "Writer.Html.HtmlSelection",
            "Writer.Html.HtmlSelectionType",
            "Writer.Html.HtmlTextFormatting",
            "Writer.Html.NativeMethods",
            "Writer.Html.NativeMethods+BSCFlags",
            "Writer.Html.NativeMethods+COMMSG",
            "Writer.Html.NativeMethods+COMRECT",
            "Writer.Html.NativeMethods+DISPPARAMS",
            "Writer.Html.NativeMethods+DOCHOSTUIINFO",
            "Writer.Html.NativeMethods+EXCEPINFO",
            "Writer.Html.NativeMethods+FORMATETC",
            "Writer.Html.NativeMethods+HTML_PAINTER_INFO",
            "Writer.Html.NativeMethods+HTMLDocument",
            "Writer.Html.NativeMethods+IHTMLSelectionObject",
            "Writer.Html.NativeMethods+IHTMLTextContainer",
            "Writer.Html.NativeMethods+IPersistStreamInit",
            "Writer.Html.NativeMethods+IPropertyNotifySink",
            "Writer.Html.NativeMethods+IServiceProvider",
            "Writer.Html.NativeMethods+IStream",
            "Writer.Html.NativeMethods+NMCUSTOMDRAW",
            "Writer.Html.NativeMethods+NMHDR",
            "Writer.Html.NativeMethods+POINT",
            "Writer.Html.NativeMethods+RECT",
            "Writer.Html.NativeMethods+STATDATA",
            "Writer.Html.NativeMethods+STATSTG",
            "Writer.Html.NativeMethods+STGMEDIUM",
            "Writer.Html.NativeMethods+tagLOGPALETTE",
            "Writer.Html.NativeMethods+tagOIFI",
            "Writer.Html.NativeMethods+tagOLECMD",
            "Writer.Html.NativeMethods+tagOleMenuGroupWidths",
            "Writer.Html.NativeMethods+tagOLEVERB",
            "Writer.Html.NativeMethods+tagSIZE",
            "Writer.Html.NativeMethods+tagSIZEL",
            "Writer.ICommandTarget",
            "Writer.htmlwriter",
        ];

        string[] actualTypeNames = typeof(CommandManager).Assembly
            .GetExportedTypes()
            .Select(type => type.FullName!)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        Assert.Equal(expectedTypeNames.OrderBy(name => name, StringComparer.Ordinal), actualTypeNames);
    }
}

