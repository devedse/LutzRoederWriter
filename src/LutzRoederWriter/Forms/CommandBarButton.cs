using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Writer.Forms
{
	/// <summary>
	/// Represents a clickable command-bar button with an optional image and keyboard shortcut.
	/// </summary>
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	public class CommandBarButton : CommandBarButtonBase
	{
		public CommandBarButton()
			: base("None")
		{
		}
		public CommandBarButton(string text)
			: base(text)
		{
		}
		public CommandBarButton(string text, EventHandler clickHandler)
			: base(text)
		{
			base.Click += clickHandler;
		}
		public CommandBarButton(string text, EventHandler clickHandler, Keys shortcut)
			: base(text)
		{
			base.Click += clickHandler;
			base.Shortcut = shortcut;
		}
		public CommandBarButton(Image image, string text, EventHandler clickHandler)
			: base(image, text)
		{
			base.Click += clickHandler;
		}
		public CommandBarButton(Image image, string text, EventHandler clickHandler, Keys shortcut)
			: base(image, text)
		{
			base.Click += clickHandler;
			base.Shortcut = shortcut;
		}
		public override string ToString()
		{
			return "Button(" + base.Text + ")";
		}
	}
}
