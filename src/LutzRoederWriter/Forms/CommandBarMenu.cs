using System;
using System.ComponentModel;

namespace Writer.Forms
{
	/// <summary>
	/// Represents a command-bar item that owns a drop-down collection.
	/// </summary>
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	public class CommandBarMenu : CommandBarItem
	{
		public event EventHandler DropDown;
		public CommandBarMenu()
			: base("None")
		{
		}
		public CommandBarMenu(string text)
			: base(text)
		{
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CommandBarItemCollection Items
		{
			get
			{
				return this.items;
			}
		}
		protected virtual void OnDropDown(EventArgs e)
		{
			if (this.DropDown != null)
			{
				this.DropDown(this, e);
			}
		}
		internal void PerformDropDown(EventArgs e)
		{
			this.OnDropDown(e);
		}
		private CommandBarItemCollection items = new CommandBarItemCollection();
	}
}
