using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Writer
{
	internal class FontComboBox : ComboBox
	{
		public FontComboBox()
		{
			this.Text = "Times New Roman";
			this.Font = new Font("Tahoma", 8f);
			base.Width = 125;
			base.DropDownWidth = 200;
			base.DrawMode = DrawMode.OwnerDrawVariable;
			base.MaxDropDownItems = 12;
		}
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.SystemEvents_InstalledFontsChanged(this, EventArgs.Empty);
			SystemEvents.InstalledFontsChanged += this.SystemEvents_InstalledFontsChanged;
		}
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.InstalledFontsChanged -= this.SystemEvents_InstalledFontsChanged;
			base.OnHandleDestroyed(e);
		}
		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			if (e.Index > 0)
			{
				e.ItemHeight = 18;
			}
			base.OnMeasureItem(e);
		}
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (!base.Enabled)
			{
				e.Graphics.FillRectangle(SystemBrushes.Control, e.Bounds);
			}
			else
			{
				e.DrawBackground();
				string text = null;
				if (e.Index == -1)
				{
					text = this.Text;
				}
				else
				{
					text = (string)base.Items[e.Index];
				}
				Font font = null;
				if (text != null && text.Length != 0)
				{
					try
					{
						FontFamily fontFamily = new FontFamily(text);
						FontStyle fontStyle = FontStyle.Regular;
						if (!fontFamily.IsStyleAvailable(fontStyle))
						{
							fontStyle = FontStyle.Italic;
							if (!fontFamily.IsStyleAvailable(fontStyle))
							{
								fontStyle = FontStyle.Bold;
								if (!fontFamily.IsStyleAvailable(fontStyle))
								{
									throw new NotSupportedException();
								}
							}
						}
						font = new Font(text, (float)((double)(e.Bounds.Height - 2) / 1.2), fontStyle, GraphicsUnit.Pixel);
					}
					catch (Exception)
					{
					}
				}
				Rectangle rectangle = new Rectangle(e.Bounds.Left + 2, e.Bounds.Top, e.Bounds.Width - 2, e.Bounds.Height);
				using (TextGraphics textGraphics = new TextGraphics(e.Graphics))
				{
					textGraphics.DrawText(text, rectangle.Location, (font != null) ? font : e.Font, e.ForeColor);
				}
				if (font != null)
				{
					font.Dispose();
				}
			}
		}
		private void SystemEvents_InstalledFontsChanged(object sender, EventArgs e)
		{
			string text = this.Text;
			base.Items.Clear();
			Hashtable hashtable = new Hashtable();
			FontFamily[] families = FontFamily.Families;
			for (int i = 0; i < families.Length; i++)
			{
				hashtable[families[i].Name.ToLower()] = families[i].Name;
			}
			string[] array = new string[hashtable.Count];
			hashtable.Values.CopyTo(array, 0);
			Array.Sort<string>(array);
			base.Items.AddRange(array);
			if (text.Length != 0)
			{
				this.Text = text;
			}
		}
	}
}
