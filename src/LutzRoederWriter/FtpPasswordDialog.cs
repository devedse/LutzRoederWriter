using System;
using System.Drawing;
using System.Windows.Forms;

namespace Writer
{
	internal class FtpPasswordDialog : Dialog
	{
		public FtpPasswordDialog()
		{
			this.Text = "Password";
			base.ClientSize = new Size(328, 74);
			Label label = new Label();
			label.Location = new Point(8, 8);
			label.Size = new Size(168, 23);
			label.Text = "Please enter your Password:";
			label.TextAlign = ContentAlignment.BottomLeft;
			label.FlatStyle = FlatStyle.System;
			base.Controls.Add(label);
			this.passwordTextBox = new TextBox();
			this.passwordTextBox.Font = new Font("Wingdings", 8.25f);
			this.passwordTextBox.Location = new Point(8, 36);
			this.passwordTextBox.PasswordChar = 'l';
			this.passwordTextBox.Size = new Size(224, 20);
			this.passwordTextBox.Text = "";
			base.Controls.Add(this.passwordTextBox);
			Button button = new Button();
			button.DialogResult = DialogResult.OK;
			button.FlatStyle = FlatStyle.System;
			button.Location = new Point(244, 12);
			button.Text = "OK";
			button.FlatStyle = FlatStyle.System;
			base.Controls.Add(button);
			base.AcceptButton = button;
			Button button2 = new Button();
			button2.DialogResult = DialogResult.Cancel;
			button2.FlatStyle = FlatStyle.System;
			button2.Location = new Point(244, 40);
			button2.Text = "Cancel";
			button2.FlatStyle = FlatStyle.System;
			base.Controls.Add(button2);
			base.CancelButton = button2;
			this.passwordTextBox.Focus();
		}
		public string Password
		{
			get
			{
				return this.passwordTextBox.Text;
			}
		}
		private TextBox passwordTextBox;
	}
}
