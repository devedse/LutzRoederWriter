using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;

namespace Writer.Forms
{
	/// <summary>
	/// Adapts Writer command-bar items to a legacy Windows Forms context menu.
	/// </summary>
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	public class CommandBarContextMenu : ContextMenu
	{
		public CommandBarItemCollection Items
		{
			get
			{
				return this.items;
			}
		}
		protected override void OnPopup(EventArgs e)
		{
			base.OnPopup(e);
			this.UpdateItems();
		}
		protected override bool ProcessCmdKey(ref Message message, Keys keyData)
		{
			CommandBarItem[] array = this.items[keyData];
			if (array.Length != 0)
			{
				CommandBarControl commandBarControl = array[0] as CommandBarControl;
				if (commandBarControl != null)
				{
					commandBarControl.PerformClick(EventArgs.Empty);
					return true;
				}
			}
			return base.ProcessCmdKey(ref message, keyData);
		}
		private void UpdateItems()
		{
			this.selectedMenuItem = null;
			base.MenuItems.Clear();
			Size imageSize = CommandBarContextMenu.GetImageSize(this.items);
			foreach (object obj in this.items)
			{
				CommandBarItem commandBarItem = (CommandBarItem)obj;
				base.MenuItems.Add(new CommandBarContextMenu.MenuBarItem(commandBarItem, imageSize, this.font, this.mnemonics));
			}
		}
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
				this.UpdateItems();
			}
		}
		internal bool Mnemonics
		{
			set
			{
				this.mnemonics = value;
			}
		}
		internal Menu SelectedMenuItem
		{
			get
			{
				return this.selectedMenuItem;
			}
			set
			{
				this.selectedMenuItem = value;
			}
		}
		private static Size GetImageSize(CommandBarItemCollection items)
		{
			Size size = new Size(16, 16);
			for (int i = 0; i < items.Count; i++)
			{
				Image image = items[i].Image;
				if (image != null)
				{
					if (image.Width > size.Width)
					{
						size.Width = image.Width;
					}
					if (image.Height > size.Height)
					{
						size.Height = image.Height;
					}
				}
			}
			return size;
		}
		private CommandBarItemCollection items = new CommandBarItemCollection();
		private Font font = SystemInformation.MenuFont;
		private Menu selectedMenuItem = null;
		private bool mnemonics = true;
		private class MenuBarItem : MenuItem
		{
			public MenuBarItem(CommandBarItem item, Size imageSize, Font font, bool mnemonics)
			{
				this.item = item;
				this.imageSize = imageSize;
				this.font = font;
				this.mnemonics = mnemonics;
				this.UpdateItems();
			}
			protected override void OnPopup(EventArgs e)
			{
				CommandBarMenu commandBarMenu = this.item as CommandBarMenu;
				if (commandBarMenu != null)
				{
					commandBarMenu.PerformDropDown(EventArgs.Empty);
				}
				base.OnPopup(e);
				this.UpdateItems();
			}
			private void UpdateItems()
			{
				base.OwnerDraw = true;
				CommandBarSeparator commandBarSeparator = this.item as CommandBarSeparator;
				if (commandBarSeparator != null)
				{
					base.Text = "-";
				}
				else
				{
					base.Text = (this.mnemonics ? this.item.Text : this.item.Text.Replace("&", ""));
				}
				CommandBarMenu commandBarMenu = this.item as CommandBarMenu;
				if (commandBarMenu != null)
				{
					base.MenuItems.Clear();
					Size size = CommandBarContextMenu.GetImageSize(commandBarMenu.Items);
					int num = 0;
					foreach (object obj in commandBarMenu.Items)
					{
						CommandBarItem commandBarItem = (CommandBarItem)obj;
						base.MenuItems.Add(new CommandBarContextMenu.MenuBarItem(commandBarItem, size, this.font, this.mnemonics));
						num += (commandBarItem.Visible ? 1 : 0);
					}
					base.Enabled = num != 0 && this.item.Enabled;
				}
				else
				{
					base.Enabled = this.item.Enabled;
				}
				base.Visible = this.item.Visible;
			}
			protected override void OnClick(EventArgs e)
			{
				base.OnClick(e);
				CommandBarControl commandBarControl = this.item as CommandBarControl;
				if (commandBarControl != null)
				{
					commandBarControl.PerformClick(EventArgs.Empty);
				}
			}
			protected override void OnSelect(EventArgs e)
			{
				CommandBarContextMenu commandBarContextMenu = base.GetContextMenu() as CommandBarContextMenu;
				if (commandBarContextMenu == null)
				{
					throw new NotSupportedException();
				}
				commandBarContextMenu.SelectedMenuItem = this;
				base.OnSelect(e);
			}
			private bool IsFlatMenu
			{
				get
				{
					bool flag;
					if (Environment.OSVersion.Version < new Version(5, 1, 0, 0))
					{
						flag = false;
					}
					else
					{
						int num = 0;
						NativeMethods.SystemParametersInfo(4130, 0, ref num, 0);
						flag = num != 0;
					}
					return flag;
				}
			}
			protected override void OnMeasureItem(MeasureItemEventArgs e)
			{
				base.OnMeasureItem(e);
				Graphics graphics = e.Graphics;
				if (this.item is CommandBarSeparator)
				{
					e.ItemWidth = 0;
					e.ItemHeight = SystemInformation.MenuHeight / 2;
				}
				else
				{
					Size size = new Size(0, 0);
					size.Width += 3 + this.imageSize.Width + 3 + 3 + 1 + 3 + this.imageSize.Width + 3;
					size.Height += 3 + this.imageSize.Height + 3;
					string text = this.item.Text;
					CommandBarButtonBase commandBarButtonBase = this.item as CommandBarButtonBase;
					if (commandBarButtonBase != null && commandBarButtonBase.Shortcut != Keys.None)
					{
						text += TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(null, CultureInfo.InvariantCulture, commandBarButtonBase.Shortcut);
					}
					using (TextGraphics textGraphics = new TextGraphics(graphics))
					{
						Size size2 = textGraphics.MeasureText(text, this.font);
						size.Width += size2.Width;
						size2.Height += 8;
						if (size2.Height > size.Height)
						{
							size.Height = size2.Height;
						}
					}
					e.ItemWidth = size.Width;
					e.ItemHeight = size.Height;
				}
			}
			protected override void OnDrawItem(DrawItemEventArgs e)
			{
				base.OnDrawItem(e);
				Graphics graphics = e.Graphics;
				Rectangle bounds = e.Bounds;
				bool flag = (e.State & DrawItemState.Selected) != DrawItemState.None;
				bool flag2 = (e.State & DrawItemState.Disabled) != DrawItemState.None;
				if (this.item is CommandBarSeparator)
				{
					Rectangle rectangle = new Rectangle(bounds.X, bounds.Y + bounds.Height / 2, bounds.Width, bounds.Height);
					ControlPaint.DrawBorder3D(graphics, rectangle, Border3DStyle.Etched, Border3DSide.Top);
				}
				else
				{
					this.DrawImage(graphics, bounds, flag, flag2);
					int num = 6 + this.imageSize.Width;
					this.DrawBackground(graphics, new Rectangle(bounds.X + num, bounds.Y, bounds.Width - num, bounds.Height), flag);
					using (TextGraphics textGraphics = new TextGraphics(graphics))
					{
						if (base.Text != null && base.Text.Length != 0)
						{
							Size size = textGraphics.MeasureText(base.Text, this.font);
							Point point = default(Point);
							point.X = bounds.X + 3 + this.imageSize.Width + 6;
							point.Y = bounds.Y + (bounds.Height - size.Height) / 2;
							this.DrawText(textGraphics, base.Text, point, flag, flag2);
						}
						CommandBarButtonBase commandBarButtonBase = this.item as CommandBarButtonBase;
						if (commandBarButtonBase != null && commandBarButtonBase.Shortcut != Keys.None)
						{
							string text = TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(null, CultureInfo.InvariantCulture, commandBarButtonBase.Shortcut);
							Size size = textGraphics.MeasureText(text, this.font);
							this.DrawText(textGraphics, text, new Point
							{
								X = bounds.X + bounds.Width - 3 - this.imageSize.Width - 3 - size.Width,
								Y = bounds.Y + (bounds.Height - size.Height) / 2
							}, flag, flag2);
						}
					}
				}
			}
			private void DrawBackground(Graphics graphics, Rectangle rectangle, bool selected)
			{
				Brush brush = (selected ? SystemBrushes.Highlight : SystemBrushes.Menu);
				graphics.FillRectangle(brush, rectangle);
			}
			private void DrawCheck(Graphics graphics, Rectangle rectangle, Color color)
			{
				Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
				Graphics graphics2 = Graphics.FromImage(bitmap);
				ControlPaint.DrawMenuGlyph(graphics2, 0, 0, rectangle.Width, rectangle.Height, MenuGlyph.Checkmark);
				graphics2.Flush();
				bitmap.MakeTransparent(Color.White);
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetRemapTable(new ColorMap[]
				{
					new ColorMap
					{
						OldColor = Color.Black,
						NewColor = color
					}
				});
				graphics.DrawImage(bitmap, rectangle, 0, 0, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttributes);
			}
			private void DrawImage(Graphics graphics, Rectangle bounds, bool selected, bool disabled)
			{
				Rectangle rectangle = new Rectangle(bounds.X, bounds.Y, this.imageSize.Width + 6, bounds.Height);
				Size menuCheckSize = SystemInformation.MenuCheckSize;
				Rectangle rectangle2 = new Rectangle(bounds.X + 1 + (this.imageSize.Width + 6 - menuCheckSize.Width) / 2, bounds.Y + (bounds.Height - menuCheckSize.Height) / 2, menuCheckSize.Width, menuCheckSize.Height);
				CommandBarCheckBox commandBarCheckBox = this.item as CommandBarCheckBox;
				if (this.IsFlatMenu)
				{
					this.DrawBackground(graphics, rectangle, selected);
					if (commandBarCheckBox != null && commandBarCheckBox.IsChecked)
					{
						int num = bounds.Height - 2;
						graphics.DrawRectangle(SystemPens.Highlight, new Rectangle(bounds.X + 1, bounds.Y + 1, this.imageSize.Width + 3, num - 1));
						graphics.FillRectangle(SystemBrushes.Menu, new Rectangle(bounds.X + 2, bounds.Y + 2, this.imageSize.Width + 2, num - 2));
					}
					Image image = this.item.Image;
					if (image != null)
					{
						Point point = new Point(bounds.X + 3, bounds.Y + (bounds.Height - image.Height) / 2);
						NativeMethods.DrawImage(graphics, image, point, disabled);
					}
					else if (commandBarCheckBox != null && commandBarCheckBox.IsChecked)
					{
						Color color = (disabled ? SystemColors.GrayText : SystemColors.MenuText);
						this.DrawCheck(graphics, rectangle2, color);
					}
				}
				else
				{
					Image image = this.item.Image;
					if (image == null)
					{
						this.DrawBackground(graphics, rectangle, selected);
						if (commandBarCheckBox != null && commandBarCheckBox.IsChecked)
						{
							Color color = (disabled ? (selected ? SystemColors.GrayText : SystemColors.ControlDark) : (selected ? SystemColors.HighlightText : SystemColors.MenuText));
							this.DrawCheck(graphics, rectangle2, color);
						}
					}
					else
					{
						this.DrawBackground(graphics, rectangle, false);
						if (commandBarCheckBox != null && commandBarCheckBox.IsChecked)
						{
							ControlPaint.DrawBorder3D(graphics, rectangle, Border3DStyle.SunkenOuter);
						}
						else if (selected)
						{
							ControlPaint.DrawBorder3D(graphics, rectangle, Border3DStyle.RaisedInner);
						}
						Point point = new Point(bounds.X + 3, bounds.Y + (bounds.Height - image.Height) / 2);
						NativeMethods.DrawImage(graphics, image, point, disabled);
					}
				}
			}
			private void DrawText(TextGraphics textGraphics, string text, Point point, bool selected, bool disabled)
			{
				Color color = (disabled ? (selected ? SystemColors.GrayText : SystemColors.ControlDark) : (selected ? SystemColors.HighlightText : SystemColors.MenuText));
				if (!this.IsFlatMenu && disabled && !selected)
				{
					textGraphics.DrawText(text, new Point(point.X + 1, point.Y + 1), this.font, SystemColors.ControlLightLight);
				}
				textGraphics.DrawText(text, point, this.font, color);
			}
			private CommandBarItem item;
			private Size imageSize;
			private Font font;
			private bool mnemonics;
		}
	}
}
