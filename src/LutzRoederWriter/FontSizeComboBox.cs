using System;
using System.Drawing;
using System.Windows.Forms;

namespace Writer
{
	internal class FontSizeComboBox : ComboBox
	{
		public FontSizeComboBox()
		{
			this.Text = "10";
			this.Font = new Font("Tahoma", 8f);
			base.Width = 40;
			base.DropDownWidth = 40;
			base.MaxDropDownItems = 12;
			this.PopulateFontSizeList();
		}
		private void PopulateFontSizeList()
		{
			base.Items.AddRange(FontSizeComboBox.fontSizes);
		}
		private static readonly string[] fontSizes = new string[] { "1", "2", "3", "4", "5", "6", "7" };
	}
}
