using System;
using System.ComponentModel;

namespace Writer.Forms
{
	/// <summary>
	/// Represents a visual separator between command-bar items.
	/// </summary>
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class CommandBarSeparator : CommandBarItem
	{
		public CommandBarSeparator()
			: base("-")
		{
		}
	}
}
