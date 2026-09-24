using System;

namespace Writer.Html
{
	/// <summary>
	/// Specifies the block format applied to the current HTML selection.
	/// </summary>
	public enum HtmlFormat
	{
		/// <summary>Normal body content.</summary>
		Normal,
		/// <summary>Preformatted content.</summary>
		Formatted,
		/// <summary>Level-one heading.</summary>
		Heading1,
		/// <summary>Level-two heading.</summary>
		Heading2,
		/// <summary>Level-three heading.</summary>
		Heading3,
		/// <summary>Level-four heading.</summary>
		Heading4,
		/// <summary>Level-five heading.</summary>
		Heading5,
		/// <summary>Level-six heading.</summary>
		Heading6,
		/// <summary>Paragraph content.</summary>
		Paragraph,
		/// <summary>An ordered list.</summary>
		OrderedList,
		/// <summary>An unordered list.</summary>
		UnorderedList
	}
}
