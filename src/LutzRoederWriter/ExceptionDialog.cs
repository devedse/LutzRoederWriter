using System;
using System.Drawing;
using System.Windows.Forms;

namespace Writer
{
	internal class ExceptionDialog : Dialog
	{
		public ExceptionDialog()
		{
			this.Text = "Bug Report";
			base.ClientSize = new Size(352, 265);
			Label label = new Label();
			label.Location = new Point(8, 9);
			label.Size = new Size(336, 34);
			label.TabIndex = 0;
			label.Text = "Please tell us what you were doing when you got the error, if you are not sure leave this text box blank and just press Send button.";
			base.Controls.Add(label);
			this.descriptionTextBox = new TextBox();
			this.descriptionTextBox.Location = new Point(8, 43);
			this.descriptionTextBox.Multiline = true;
			this.descriptionTextBox.ScrollBars = ScrollBars.Both;
			this.descriptionTextBox.Size = new Size(336, 165);
			this.descriptionTextBox.TabIndex = 1;
			this.descriptionTextBox.Text = "";
			base.Controls.Add(this.descriptionTextBox);
			Button button = new Button();
			button.FlatStyle = FlatStyle.System;
			button.Location = new Point(268, 235);
			button.TabIndex = 3;
			button.Text = "Cancel";
			button.DialogResult = DialogResult.Cancel;
			base.Controls.Add(button);
			base.CancelButton = button;
			Button button2 = new Button();
			button2.FlatStyle = FlatStyle.System;
			button2.Location = new Point(188, 235);
			button2.TabIndex = 2;
			button2.Text = "&Send";
			button2.DialogResult = DialogResult.OK;
			base.Controls.Add(button2);
			LinkLabel linkLabel = new LinkLabel();
			linkLabel.Location = new Point(8, 216);
			linkLabel.TabIndex = 4;
			linkLabel.TabStop = true;
			linkLabel.Text = "To see what you send to us Click here.";
			linkLabel.Click += this.ReportMessageLinkLabelLinkClicked;
			base.Controls.Add(linkLabel);
			this.descriptionTextBox.Focus();
		}
		private void ReportMessageLinkLabelLinkClicked(object sender, EventArgs e)
		{
			MessageBox.Show(this.reportMessage, "Bug Description");
		}
		public string ReportMessage
		{
			set
			{
				this.reportMessage = value;
			}
		}
		public string UserDescription
		{
			get
			{
				return this.descriptionTextBox.Text;
			}
		}
		private TextBox descriptionTextBox;
		private string reportMessage = string.Empty;
	}
}
