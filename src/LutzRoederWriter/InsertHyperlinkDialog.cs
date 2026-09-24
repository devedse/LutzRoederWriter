using System;
using System.Drawing;
using System.Windows.Forms;

namespace Writer
{
	internal class InsertHyperlinkDialog : Dialog
	{
		public InsertHyperlinkDialog()
		{
			base.ClientSize = new Size(404, 88);
			this.Text = "Insert Hyperlink";
			Button button = new Button();
			button.FlatStyle = FlatStyle.System;
			button.Location = new Point(244, 60);
			button.Size = new Size(75, 23);
			button.TabIndex = 3;
			button.Text = "OK";
			button.DialogResult = DialogResult.OK;
			base.Controls.Add(button);
			base.AcceptButton = button;
			Label label = new Label();
			label.Location = new Point(8, 36);
			label.Size = new Size(36, 15);
			label.TabIndex = 0;
			label.Text = "&URL:";
			label.FlatStyle = FlatStyle.System;
			base.Controls.Add(label);
			this.urlTextBox.Location = new Point(84, 32);
			this.urlTextBox.Size = new Size(316, 20);
			this.urlTextBox.TabIndex = 2;
			base.Controls.Add(this.urlTextBox);
			Button button2 = new Button();
			button2.FlatStyle = FlatStyle.System;
			button2.Location = new Point(324, 60);
			button2.Size = new Size(75, 23);
			button2.TabIndex = 4;
			button2.Text = "Cancel";
			button2.DialogResult = DialogResult.Cancel;
			base.Controls.Add(button2);
			base.CancelButton = button2;
			this.descriptionTextBox.Location = new Point(84, 8);
			this.descriptionTextBox.Size = new Size(316, 20);
			this.descriptionTextBox.TabIndex = 1;
			this.descriptionTextBox.Text = "";
			base.Controls.Add(this.descriptionTextBox);
			Label label2 = new Label();
			label2.Location = new Point(8, 12);
			label2.Size = new Size(65, 16);
			label2.TabIndex = 0;
			label2.Text = "&Description:";
			label2.FlatStyle = FlatStyle.System;
			base.Controls.Add(label2);
			this.urlTextBox.Focus();
		}
		public string Description
		{
			get
			{
				return this.descriptionTextBox.Text;
			}
			set
			{
				if (value != null)
				{
					this.descriptionTextBox.Text = value;
				}
			}
		}
		public string Url
		{
			get
			{
				return this.urlTextBox.Text;
			}
		}
		private TextBox urlTextBox = new TextBox();
		private TextBox descriptionTextBox = new TextBox();
	}
}
