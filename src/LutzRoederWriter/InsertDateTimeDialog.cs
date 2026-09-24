using System;
using System.Drawing;
using System.Windows.Forms;

namespace Writer
{
	internal class InsertDateTimeDialog : Dialog
	{
		public InsertDateTimeDialog()
		{
			base.ClientSize = new Size(280, 222);
			this.Text = "Date and Time";
			Label label = new Label();
			label.Location = new Point(8, 8);
			label.Size = new Size(104, 16);
			label.TabIndex = 0;
			label.Text = "&Available Formats:";
			label.FlatStyle = FlatStyle.System;
			base.Controls.Add(label);
			Button button = new Button();
			button.FlatStyle = FlatStyle.System;
			button.Location = new Point(192, 24);
			button.DialogResult = DialogResult.OK;
			button.TabIndex = 2;
			button.Text = "OK";
			base.Controls.Add(button);
			base.AcceptButton = button;
			Button button2 = new Button();
			button2.FlatStyle = FlatStyle.System;
			button2.Location = new Point(192, 56);
			button2.DialogResult = DialogResult.Cancel;
			button2.TabIndex = 3;
			button2.Text = "Cancel";
			base.Controls.Add(button2);
			base.CancelButton = button2;
			this.dateTimeFomatsListBox.Location = new Point(8, 24);
			this.dateTimeFomatsListBox.Name = "DateTimeFomatsListBox";
			this.dateTimeFomatsListBox.Size = new Size(176, 186);
			this.dateTimeFomatsListBox.TabIndex = 1;
			this.dateTimeFomatsListBox.DoubleClick += this.DoubleClickDateTimeFormatListBox;
			base.Controls.Add(this.dateTimeFomatsListBox);
			this.AddDateTimeFormats();
			this.dateTimeFomatsListBox.SelectedIndex = 0;
			this.dateTimeFomatsListBox.Focus();
		}
		private void AddDateTimeFormats()
		{
			DateTime now = DateTime.Now;
			this.dateTimeFomatsListBox.Items.Add(now.ToString("M/d/yyyy"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("M/d/yy"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("MM/d/yy"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("MM/d/yyyy"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("yy/MM/d"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("yyyy-MM-d"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("d-MMM-yy"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("dddd, MMMM dd, yyyy"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("MMMM d, yyyy"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("dddd, dd MMMM, yyyy"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("d MMMM, yyyy"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("h:mm:ss tt"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("hh:mm:ss tt"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("h:mm:ss"));
			this.dateTimeFomatsListBox.Items.Add(now.ToString("hh:mm:ss"));
		}
		public string Format
		{
			get
			{
				return this.dateTimeFomatsListBox.SelectedItem.ToString();
			}
		}
		private void DoubleClickDateTimeFormatListBox(object sender, EventArgs ea)
		{
			base.DialogResult = DialogResult.OK;
		}
		private ListBox dateTimeFomatsListBox = new ListBox();
	}
}
