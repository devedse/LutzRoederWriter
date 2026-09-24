using System;

namespace Writer.Html
{
	/// <summary>
	/// Describes the kind of selection currently exposed by MSHTML.
	/// </summary>
	public enum HtmlSelectionType
	{
		/// <summary>No editable content is selected.</summary>
		Empty,
		/// <summary>A text range is selected.</summary>
		TextSelection,
		/// <summary>One or more elements are selected.</summary>
		ElementSelection
	}
}
