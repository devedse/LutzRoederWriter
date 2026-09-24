using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Writer.Forms
{
	/// <summary>
	/// Hosts a Windows Forms combo box inside a command bar.
	/// </summary>
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class CommandBarComboBox : CommandBarControl
	{
		public CommandBarComboBox()
			: base("None")
		{
			this.comboBox = new ComboBox();
			this.comboBox.SelectedIndexChanged += this.ComboBox_SelectedIndexChanged;
		}
		public CommandBarComboBox(string text, ComboBox comboBox)
			: base(text)
		{
			this.comboBox = comboBox;
			this.comboBox.SelectedIndexChanged += this.ComboBox_SelectedIndexChanged;
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.comboBox != null)
				{
					this.comboBox.SelectedIndexChanged -= this.ComboBox_SelectedIndexChanged;
					this.comboBox = null;
				}
			}
			base.Dispose(disposing);
		}
		public int Height
		{
			get
			{
				IntPtr handle = this.comboBox.Handle;
				return this.comboBox.Height;
			}
		}
		public int Width
		{
			get
			{
				IntPtr handle = this.comboBox.Handle;
				return this.comboBox.Width;
			}
		}
		public string Value
		{
			get
			{
				return this.comboBox.Text;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value != this.comboBox.Text)
				{
					this.comboBox.Text = value;
					this.OnPropertyChanged(new PropertyChangedEventArgs("Value"));
				}
			}
		}
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				this.comboBox.Enabled = value;
				base.Enabled = value;
			}
		}
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				this.comboBox.Visible = value;
				base.Visible = value;
			}
		}
		public override string ToString()
		{
			return string.Concat(new string[] { "ComboBox(", base.Text, ",", this.Value, ")" });
		}
		internal ComboBox ComboBox
		{
			get
			{
				return this.comboBox;
			}
		}
		private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			base.PerformClick(EventArgs.Empty);
		}
		private ComboBox comboBox;
	}
}
