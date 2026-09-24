using System;
using System.Drawing;
using System.Windows.Forms;

namespace Writer
{
	internal class FtpConfigurationDialog : Dialog
	{
		public FtpConfigurationDialog()
		{
			this.Text = "FTP Settings";
			base.ClientSize = new Size(296, 238);
			Label label = new Label();
			label.Location = new Point(8, 14);
			label.Size = new Size(68, 23);
			label.TabStop = false;
			label.Text = "FTP Server:";
			label.TextAlign = ContentAlignment.MiddleLeft;
			label.FlatStyle = FlatStyle.System;
			base.Controls.Add(label);
			this.passwordLabel = new Label();
			this.passwordLabel.Enabled = false;
			this.passwordLabel.Location = new Point(8, 107);
			this.passwordLabel.Size = new Size(76, 23);
			this.passwordLabel.TabIndex = 1;
			this.passwordLabel.Text = "Password:";
			this.passwordLabel.TextAlign = ContentAlignment.MiddleLeft;
			this.passwordLabel.FlatStyle = FlatStyle.System;
			base.Controls.Add(this.passwordLabel);
			this.askForPasswordRadioButton = new RadioButton();
			this.askForPasswordRadioButton.Checked = true;
			this.askForPasswordRadioButton.FlatStyle = FlatStyle.System;
			this.askForPasswordRadioButton.Location = new Point(8, 60);
			this.askForPasswordRadioButton.Size = new Size(228, 20);
			this.askForPasswordRadioButton.TabIndex = 9;
			this.askForPasswordRadioButton.TabStop = true;
			this.askForPasswordRadioButton.Text = "Ask for Password when Publishing";
			this.askForPasswordRadioButton.Enter += this.ShowDescription;
			base.Controls.Add(this.askForPasswordRadioButton);
			this.storePasswordRadioButton = new RadioButton();
			this.storePasswordRadioButton.FlatStyle = FlatStyle.System;
			this.storePasswordRadioButton.Location = new Point(8, 80);
			this.storePasswordRadioButton.Size = new Size(232, 20);
			this.storePasswordRadioButton.TabIndex = 10;
			this.storePasswordRadioButton.Text = "Store Password (not secure)";
			this.storePasswordRadioButton.Enter += this.ShowDescription;
			this.storePasswordRadioButton.CheckedChanged += this.StorePasswordRadioButtonCheckedChanged;
			base.Controls.Add(this.storePasswordRadioButton);
			this.ftpServerTextBox = new TextBox();
			this.ftpServerTextBox.Location = new Point(80, 12);
			this.ftpServerTextBox.Size = new Size(132, 20);
			this.ftpServerTextBox.TabIndex = 4;
			this.ftpServerTextBox.Text = "";
			this.ftpServerTextBox.Enter += this.ShowDescription;
			this.ftpServerTextBox.TextChanged += this.TextBoxesTextChanged;
			base.Controls.Add(this.ftpServerTextBox);
			this.usernameTextBox = new TextBox();
			this.usernameTextBox.Location = new Point(80, 36);
			this.usernameTextBox.Size = new Size(132, 20);
			this.usernameTextBox.TabIndex = 5;
			this.usernameTextBox.Text = "";
			this.usernameTextBox.Enter += this.ShowDescription;
			this.usernameTextBox.TextChanged += this.TextBoxesTextChanged;
			base.Controls.Add(this.usernameTextBox);
			Label label2 = new Label();
			label2.Location = new Point(220, 14);
			label2.Size = new Size(28, 23);
			label2.TabIndex = 7;
			label2.Text = "Port:";
			label2.TextAlign = ContentAlignment.MiddleLeft;
			label2.FlatStyle = FlatStyle.System;
			base.Controls.Add(label2);
			this.portTextBox = new TextBox();
			this.portTextBox.Location = new Point(252, 12);
			this.portTextBox.Size = new Size(36, 20);
			this.portTextBox.TabIndex = 8;
			this.portTextBox.Text = "21";
			this.portTextBox.Enter += this.ShowDescription;
			this.portTextBox.TextChanged += this.TextBoxesTextChanged;
			base.Controls.Add(this.portTextBox);
			Label label3 = new Label();
			label3.Location = new Point(8, 38);
			label3.Size = new Size(68, 23);
			label3.TabIndex = 10;
			label3.Text = "Username:";
			label3.TextAlign = ContentAlignment.MiddleLeft;
			label3.FlatStyle = FlatStyle.System;
			base.Controls.Add(label3);
			Label label4 = new Label();
			label4.Location = new Point(8, 131);
			label4.Size = new Size(76, 23);
			label4.TabIndex = 11;
			label4.Text = "Subdirectory:";
			label4.TextAlign = ContentAlignment.MiddleLeft;
			label4.FlatStyle = FlatStyle.System;
			base.Controls.Add(label4);
			this.passwordTextBox = new TextBox();
			this.passwordTextBox.Enabled = false;
			this.passwordTextBox.Font = new Font("Wingdings", 8.25f);
			this.passwordTextBox.PasswordChar = 'l';
			this.passwordTextBox.Location = new Point(84, 104);
			this.passwordTextBox.Size = new Size(128, 20);
			this.passwordTextBox.TabIndex = 12;
			this.passwordTextBox.Text = "";
			this.passwordTextBox.Enter += this.ShowDescription;
			this.passwordTextBox.TextChanged += this.TextBoxesTextChanged;
			base.Controls.Add(this.passwordTextBox);
			this.subdirectoryTextBox = new TextBox();
			this.subdirectoryTextBox.Location = new Point(84, 128);
			this.subdirectoryTextBox.Size = new Size(128, 20);
			this.subdirectoryTextBox.TabIndex = 13;
			this.subdirectoryTextBox.Text = "";
			this.subdirectoryTextBox.Enter += this.ShowDescription;
			this.subdirectoryTextBox.TextChanged += this.TextBoxesTextChanged;
			base.Controls.Add(this.subdirectoryTextBox);
			this.descriptionLabel = new Label();
			this.descriptionLabel.BorderStyle = BorderStyle.FixedSingle;
			this.descriptionLabel.Location = new Point(4, 156);
			this.descriptionLabel.Size = new Size(288, 45);
			this.descriptionLabel.TabIndex = 14;
			this.descriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
			this.descriptionLabel.FlatStyle = FlatStyle.System;
			base.Controls.Add(this.descriptionLabel);
			this.okButton = new Button();
			this.okButton.DialogResult = DialogResult.OK;
			this.okButton.FlatStyle = FlatStyle.System;
			this.okButton.Location = new Point(136, 208);
			this.okButton.TabIndex = 15;
			this.okButton.Text = "OK";
			this.okButton.Enabled = false;
			base.Controls.Add(this.okButton);
			base.AcceptButton = this.okButton;
			Button button = new Button();
			button.DialogResult = DialogResult.Cancel;
			button.FlatStyle = FlatStyle.System;
			button.Location = new Point(216, 208);
			button.TabIndex = 16;
			button.Text = "Cancel";
			base.Controls.Add(button);
			base.CancelButton = button;
			this.ftpServerTextBox.Focus();
		}
		private bool IsComplete()
		{
			return this.ftpServerTextBox.Text != string.Empty && this.usernameTextBox.Text != string.Empty && this.portTextBox.Text != string.Empty && (!this.passwordLabel.Enabled || this.passwordTextBox.Text != string.Empty);
		}
		private void TextBoxesTextChanged(object sender, EventArgs e)
		{
			bool flag = this.IsComplete();
			if (flag)
			{
				this.okButton.Enabled = true;
			}
			else
			{
				this.okButton.Enabled = false;
			}
		}
		private void StorePasswordRadioButtonCheckedChanged(object sender, EventArgs e)
		{
			if (this.storePasswordRadioButton.Checked)
			{
				this.passwordLabel.Enabled = true;
				this.passwordTextBox.Enabled = true;
			}
			else
			{
				this.passwordTextBox.Text = string.Empty;
				this.passwordLabel.Enabled = false;
				this.passwordTextBox.Enabled = false;
			}
			this.TextBoxesTextChanged(null, null);
		}
		private void ShowDescription(object sender, EventArgs e)
		{
			if (sender == this.ftpServerTextBox)
			{
				this.descriptionLabel.Text = "Type the name of the server, for example, www.yourhost.com. You can also use an IP address like 127.0.0.1.";
			}
			else if (sender == this.usernameTextBox)
			{
				this.descriptionLabel.Text = "Type your server Username here.";
			}
			else if (sender == this.portTextBox)
			{
				this.descriptionLabel.Text = "Most FTP servers use port 21. If your server uses a different port, specify that here.";
			}
			else if (sender == this.passwordTextBox)
			{
				this.descriptionLabel.Text = "Type your server Password here.";
			}
			else if (sender == this.subdirectoryTextBox)
			{
				this.descriptionLabel.Text = "If you need to publish the file in a subdirectory Type the subdirectory here.";
			}
			else if (sender == this.askForPasswordRadioButton)
			{
				this.descriptionLabel.Text = "If you check this Radio Button, Writer will ask you for the FTP server password every time you try to publish to your server.";
			}
			else if (sender == this.storePasswordRadioButton)
			{
				this.descriptionLabel.Text = "If you check this Radio Button, Writer will save your password in its config file, which is not secure.";
			}
		}
		public string FtpServer
		{
			get
			{
				return this.ftpServerTextBox.Text;
			}
			set
			{
				this.ftpServerTextBox.Text = value;
			}
		}
		public string Username
		{
			get
			{
				return this.usernameTextBox.Text;
			}
			set
			{
				this.usernameTextBox.Text = value;
			}
		}
		public string Password
		{
			get
			{
				return this.passwordTextBox.Text;
			}
			set
			{
				this.passwordTextBox.Text = value;
			}
		}
		public string Directory
		{
			get
			{
				return this.subdirectoryTextBox.Text;
			}
			set
			{
				this.subdirectoryTextBox.Text = value;
			}
		}
		public string Port
		{
			get
			{
				return this.portTextBox.Text;
			}
			set
			{
				this.portTextBox.Text = value;
			}
		}
		public bool StorePassword
		{
			set
			{
				this.storePasswordRadioButton.Checked = value;
			}
		}
		private Label passwordLabel;
		private Label descriptionLabel;
		private TextBox ftpServerTextBox;
		private TextBox usernameTextBox;
		private TextBox portTextBox;
		private TextBox passwordTextBox;
		private TextBox subdirectoryTextBox;
		private RadioButton askForPasswordRadioButton;
		private RadioButton storePasswordRadioButton;
		private Button okButton;
	}
}
