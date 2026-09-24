using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Writer.Forms
{
	/// <summary>
	/// Provides ordered access to command-bar items and convenience methods for
	/// creating buttons, menus, check boxes, combo boxes, and separators.
	/// </summary>
	public class CommandBarItemCollection : ICollection, IEnumerable
	{
		internal CommandBarItemCollection()
		{
			this.commandBar = null;
			this.items = new ArrayList();
		}
		internal CommandBarItemCollection(CommandBar commandBar)
		{
			this.commandBar = commandBar;
			this.items = new ArrayList();
		}
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}
		public void Clear()
		{
			while (this.Count > 0)
			{
				this.RemoveAt(0);
			}
		}
		public void Add(CommandBarItem item)
		{
			this.items.Add(item);
			if (this.commandBar != null)
			{
				this.commandBar.AddItem(item);
			}
		}
		public void AddSeparator()
		{
			this.Add(new CommandBarSeparator());
		}
		public CommandBarMenu AddMenu(string text)
		{
			CommandBarMenu commandBarMenu = new CommandBarMenu(text);
			this.Add(commandBarMenu);
			return commandBarMenu;
		}
		public CommandBarMenu AddMenu(Image image, string text)
		{
			CommandBarMenu commandBarMenu = this.AddMenu(text);
			commandBarMenu.Image = image;
			return commandBarMenu;
		}
		public CommandBarMenu AddMenu(string text, EventHandler dropDownHandler)
		{
			CommandBarMenu commandBarMenu = this.AddMenu(text);
			commandBarMenu.DropDown += dropDownHandler;
			return commandBarMenu;
		}
		public CommandBarMenu AddMenu(Image image, string text, EventHandler dropDownHandler)
		{
			CommandBarMenu commandBarMenu = this.AddMenu(text);
			commandBarMenu.Image = image;
			commandBarMenu.DropDown += dropDownHandler;
			return commandBarMenu;
		}
		public CommandBarButton AddButton(string text, EventHandler clickHandler)
		{
			CommandBarButton commandBarButton = new CommandBarButton(text);
			commandBarButton.Click += clickHandler;
			this.Add(commandBarButton);
			return commandBarButton;
		}
		public CommandBarButton AddButton(string text, EventHandler clickHandler, Keys shortcut)
		{
			CommandBarButton commandBarButton = this.AddButton(text, clickHandler);
			commandBarButton.Shortcut = shortcut;
			return commandBarButton;
		}
		public CommandBarButton AddButton(Image image, string text, EventHandler clickHandler)
		{
			CommandBarButton commandBarButton = this.AddButton(text, clickHandler);
			commandBarButton.Image = image;
			return commandBarButton;
		}
		public CommandBarButton AddButton(Image image, string text, EventHandler clickHandler, Keys shortcut)
		{
			CommandBarButton commandBarButton = this.AddButton(text, clickHandler, shortcut);
			commandBarButton.Image = image;
			return commandBarButton;
		}
		public CommandBarCheckBox AddCheckBox(string text)
		{
			CommandBarCheckBox commandBarCheckBox = new CommandBarCheckBox(text);
			this.Add(commandBarCheckBox);
			return commandBarCheckBox;
		}
		public CommandBarCheckBox AddCheckBox(string text, Keys shortcut)
		{
			CommandBarCheckBox commandBarCheckBox = this.AddCheckBox(text);
			commandBarCheckBox.Shortcut = shortcut;
			return commandBarCheckBox;
		}
		public CommandBarCheckBox AddCheckBox(Image image, string text, Keys shortcut)
		{
			CommandBarCheckBox commandBarCheckBox = this.AddCheckBox(text, shortcut);
			commandBarCheckBox.Image = image;
			return commandBarCheckBox;
		}
		public CommandBarCheckBox AddCheckBox(Image image, string text)
		{
			CommandBarCheckBox commandBarCheckBox = this.AddCheckBox(text);
			commandBarCheckBox.Image = image;
			return commandBarCheckBox;
		}
		public CommandBarComboBox AddComboBox(string text, ComboBox comboBox)
		{
			CommandBarComboBox commandBarComboBox = new CommandBarComboBox(text, comboBox);
			this.Add(commandBarComboBox);
			return commandBarComboBox;
		}
		public void AddRange(ICollection items)
		{
			foreach (object obj in items)
			{
				CommandBarItem commandBarItem = (CommandBarItem)obj;
				this.items.Add(commandBarItem);
				if (this.commandBar != null)
				{
					this.commandBar.AddItem(commandBarItem);
				}
			}
		}
		public void Insert(int index, CommandBarItem item)
		{
			this.items.Insert(index, item);
			if (this.commandBar != null)
			{
				this.commandBar.AddItem(item);
			}
		}
		public void RemoveAt(int index)
		{
			CommandBarItem commandBarItem = (CommandBarItem)this.items[index];
			this.items.RemoveAt(index);
			if (this.commandBar != null)
			{
				this.commandBar.RemoveItem(commandBarItem);
			}
		}
		public void Remove(CommandBarItem item)
		{
			if (this.items.Contains(item))
			{
				this.items.Remove(item);
				if (this.commandBar != null)
				{
					this.commandBar.RemoveItem(item);
				}
			}
		}
		public bool Contains(CommandBarItem item)
		{
			return this.items.Contains(item);
		}
		public int IndexOf(CommandBarItem item)
		{
			return this.items.IndexOf(item);
		}
		public CommandBarItem this[int index]
		{
			get
			{
				return (CommandBarItem)this.items[index];
			}
		}
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}
		public void CopyTo(CommandBarItem[] array, int index)
		{
			this.items.CopyTo(array, index);
		}
		internal CommandBarItem[] this[Keys shortcut]
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.items)
				{
					CommandBarItem commandBarItem = (CommandBarItem)obj;
					CommandBarButtonBase commandBarButtonBase = commandBarItem as CommandBarButtonBase;
					if (commandBarButtonBase != null)
					{
						if (commandBarButtonBase.Shortcut == shortcut && commandBarButtonBase.Enabled && commandBarButtonBase.Visible)
						{
							arrayList.Add(commandBarButtonBase);
						}
					}
				}
				foreach (object obj2 in this.items)
				{
					CommandBarItem commandBarItem = (CommandBarItem)obj2;
					CommandBarMenu commandBarMenu = commandBarItem as CommandBarMenu;
					if (commandBarMenu != null)
					{
						arrayList.AddRange(commandBarMenu.Items[shortcut]);
					}
				}
				CommandBarItem[] array = new CommandBarItem[arrayList.Count];
				arrayList.CopyTo(array, 0);
				return array;
			}
		}
		internal CommandBarItem[] this[char mnemonic]
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.items)
				{
					CommandBarItem commandBarItem = (CommandBarItem)obj;
					if (commandBarItem.Visible && commandBarItem.Enabled)
					{
						string text = commandBarItem.Text;
						for (int i = 0; i < text.Length; i++)
						{
							if (text[i] == '&' && i + 1 < text.Length && text[i + 1] != '&')
							{
								if (mnemonic == char.ToUpper(text[i + 1], CultureInfo.InvariantCulture))
								{
									arrayList.Add(commandBarItem);
								}
							}
						}
					}
				}
				CommandBarItem[] array = new CommandBarItem[arrayList.Count];
				arrayList.CopyTo(array, 0);
				return array;
			}
		}
		private CommandBar commandBar;
		private ArrayList items;
	}
}
