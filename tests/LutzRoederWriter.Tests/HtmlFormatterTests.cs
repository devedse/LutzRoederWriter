using System.IO;
using Writer.Html;
using Xunit;

namespace LutzRoederWriter.Tests;

public class HtmlFormatterTests
{
    [Theory]
    [InlineData(
        "<html><body><p>Hello</p></body></html>",
        "<html>\r\n<body>\r\n\t<p>\r\n\t\tHello\r\n\t</p>\r\n</body>\r\n</html>")]
    [InlineData(
        "<div><b>Bold</b><br>Next</div>",
        "<div><b>Bold</b>\r\n\t<br />\r\n\tNext\r\n</div>")]
    [InlineData(
        "<table><tr><td>A</td><td>B</td></tr></table>",
        "<table>\r\n\t<tr>\r\n\t\t<td>\r\n\t\t\tA</td>\r\n\t\t<td>\r\n\t\t\tB</td>\r\n\t</tr>\r\n</table>")]
    [InlineData("<pre>  a\r\n b</pre>", "<pre>  a\r\n b</pre>")]
    [InlineData(
        "<div class=\"x\" data-value=\"a&amp;b\">Text &amp; more</div>",
        "<div class=\"x\" data-value=\"a&amp;b\">Text &amp; more\r\n</div>")]
    [InlineData(
        "<!-- comment --><custom attr='v'>x</custom>",
        "<!-- comment -->\r\n<custom attr='v'>\r\n\tx\r\n</custom>")]
    public void FormatMatchesOriginalBehavior(string input, string expected)
    {
        var formatter = new HtmlFormatter();
        using var output = new StringWriter();

        formatter.Format(input, output);

        Assert.Equal(expected, output.ToString());
    }
}
