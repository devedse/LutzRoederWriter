using System;
using System.ComponentModel;
using System.Drawing;

namespace Writer.Forms
{
	/// <summary>
	/// Represents a checkable command-bar item.
	/// </summary>
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	public class CommandBarCheckBox : CommandBarButtonBase
	{
		public CommandBarCheckBox()
			: base("None")
		{
		}
		public CommandBarCheckBox(string text)
			: base(text)
		{
		}
		public CommandBarCheckBox(Image image, string text)
			: base(image, text)
		{
		}
		public bool IsChecked
		{
			get
			{
				return this.isChecked;
			}
			set
			{
				if (value != this.isChecked)
				{
					this.isChecked = value;
					this.OnPropertyChanged(new PropertyChangedEventArgs("IsChecked"));
				}
			}
		}
		protected override void OnClick(EventArgs e)
		{
			this.IsChecked = !this.IsChecked;
			base.OnClick(e);
		}
		public override string ToString()
		{
			return string.Concat(new object[] { "CheckBox(", base.Text, ",", this.IsChecked, ")" });
		}
		private bool isChecked = false;
	}
}
