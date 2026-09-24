using System;
using System.Collections;
using Writer.Forms;

namespace Writer
{
	/// <summary>
	/// Represents the shared state of all controls bound to a named command.
	/// </summary>
	public class CommandState
	{
		/// <summary>
		/// Initializes a command state for the specified command name.
		/// </summary>
		/// <param name="commandName">The command identifier.</param>
		public CommandState(string commandName)
		{
			this.commandName = commandName;
		}
		internal void AddItem(CommandBarItem item)
		{
			this.items.Add(item);
		}
		internal void RemoveItem(CommandBarItem item)
		{
			this.items.Remove(item);
		}
		internal CommandBarItem ActiveItem
		{
			get
			{
				return this.activeItem;
			}
			set
			{
				this.activeItem = value;
			}
		}
		internal IEnumerable Items
		{
			get
			{
				return this.items;
			}
		}
		/// <summary>
		/// Gets the command identifier.
		/// </summary>
		public string CommandName
		{
			get
			{
				return this.commandName;
			}
		}
		/// <summary>
		/// Gets or sets whether controls bound to this command are visible.
		/// </summary>
		public bool IsVisible
		{
			get
			{
				return this.ActiveItem.Visible;
			}
			set
			{
				foreach (object obj in this.items)
				{
					CommandBarItem commandBarItem = (CommandBarItem)obj;
					commandBarItem.Visible = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets whether controls bound to this command are enabled.
		/// </summary>
		public bool IsEnabled
		{
			get
			{
				return this.ActiveItem.Enabled;
			}
			set
			{
				foreach (object obj in this.items)
				{
					CommandBarItem commandBarItem = (CommandBarItem)obj;
					commandBarItem.Enabled = value;
				}
			}
		}
		/// <summary>
		/// Gets or sets the text shown by controls bound to this command.
		/// </summary>
		public string Text
		{
			get
			{
				return this.ActiveItem.Text;
			}
			set
			{
				foreach (object obj in this.items)
				{
					CommandBarItem commandBarItem = (CommandBarItem)obj;
					commandBarItem.Text = value;
				}
			}
		}
		private string commandName;
		private ArrayList items = new ArrayList();
		private CommandBarItem activeItem = null;
	}
}
