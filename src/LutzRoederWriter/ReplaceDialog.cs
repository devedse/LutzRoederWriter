using System;
using System.Drawing;
using System.Windows.Forms;
using Writer.Html;

namespace Writer
{
	internal class ReplaceDialog : Dialog
	{
		public ReplaceDialog(HtmlControl _htmlControl)
		{
			this.htmlControl = _htmlControl;
			base.ClientSize = new Size(360, 122);
			this.Text = "Replace";
			Label label = new Label();
			label.FlatStyle = FlatStyle.System;
			label.Location = new Point(8, 12);
			label.Size = new Size(56, 16);
			label.TabIndex = 0;
			label.Text = "Fi&nd what:";
			base.Controls.Add(label);
			this.searchTextBox.Location = new Point(84, 8);
			this.searchTextBox.Size = new Size(184, 20);
			this.searchTextBox.TabIndex = 1;
			this.searchTextBox.Text = "";
			this.searchTextBox.TextChanged += this.TextChangeSearchTextBox;
			base.Controls.Add(this.searchTextBox);
			this.findNextButton.FlatStyle = FlatStyle.System;
			this.findNextButton.Location = new Point(276, 8);
			this.findNextButton.TabIndex = 9;
			this.findNextButton.Text = "&Find Next";
			this.findNextButton.Enabled = false;
			this.findNextButton.Click += this.ClickFindNextButton;
			base.Controls.Add(this.findNextButton);
			Button button = new Button();
			button.FlatStyle = FlatStyle.System;
			button.Location = new Point(276, 92);
			button.TabIndex = 12;
			button.Text = "Cancel";
			button.DialogResult = DialogResult.Cancel;
			base.Controls.Add(button);
			base.CancelButton = button;
			RadioButton radioButton = new RadioButton();
			radioButton.Checked = true;
			radioButton.FlatStyle = FlatStyle.System;
			radioButton.Location = new Point(48, 16);
			radioButton.Size = new Size(48, 16);
			radioButton.TabIndex = 7;
			radioButton.TabStop = true;
			radioButton.Text = "&Down";
			base.Controls.Add(radioButton);
			this.upRadio.FlatStyle = FlatStyle.System;
			this.upRadio.Location = new Point(8, 16);
			this.upRadio.Size = new Size(40, 16);
			this.upRadio.TabIndex = 8;
			this.upRadio.Text = "&Up";
			base.Controls.Add(this.upRadio);
			GroupBox groupBox = new GroupBox();
			groupBox.Controls.Add(radioButton);
			groupBox.Controls.Add(this.upRadio);
			groupBox.FlatStyle = FlatStyle.System;
			groupBox.Location = new Point(164, 60);
			groupBox.Size = new Size(104, 40);
			groupBox.TabIndex = 6;
			groupBox.TabStop = false;
			groupBox.Text = "Direction";
			base.Controls.Add(groupBox);
			this.wholeCheckBox.FlatStyle = FlatStyle.System;
			this.wholeCheckBox.Location = new Point(8, 64);
			this.wholeCheckBox.Size = new Size(136, 16);
			this.wholeCheckBox.TabIndex = 4;
			this.wholeCheckBox.Text = "Match &whole word only";
			base.Controls.Add(this.wholeCheckBox);
			this.caseCheckBox.FlatStyle = FlatStyle.System;
			this.caseCheckBox.Location = new Point(8, 80);
			this.caseCheckBox.Size = new Size(128, 16);
			this.caseCheckBox.TabIndex = 5;
			this.caseCheckBox.Text = "Match &case";
			base.Controls.Add(this.caseCheckBox);
			Label label2 = new Label();
			label2.FlatStyle = FlatStyle.System;
			label2.Location = new Point(8, 36);
			label2.Size = new Size(72, 16);
			label2.TabIndex = 2;
			label2.Text = "Re&place with:";
			base.Controls.Add(label2);
			this.replaceTextBox.Location = new Point(84, 32);
			this.replaceTextBox.Size = new Size(184, 20);
			this.replaceTextBox.TabIndex = 3;
			this.replaceTextBox.Text = "";
			base.Controls.Add(this.replaceTextBox);
			this.replaceAllButton.FlatStyle = FlatStyle.System;
			this.replaceAllButton.Location = new Point(276, 64);
			this.replaceAllButton.TabIndex = 11;
			this.replaceAllButton.Text = "Replace &All";
			this.replaceAllButton.Enabled = false;
			this.replaceAllButton.Click += this.ClickReplaceAllButton;
			base.Controls.Add(this.replaceAllButton);
			this.replaceButton.FlatStyle = FlatStyle.System;
			this.replaceButton.Location = new Point(276, 36);
			this.replaceButton.TabIndex = 10;
			this.replaceButton.Text = "&Replace";
			this.replaceButton.Enabled = false;
			this.replaceButton.Click += this.ClickReplaceButton;
			base.Controls.Add(this.replaceButton);
			this.searchTextBox.Focus();
		}
		private void TextChangeSearchTextBox(object sender, EventArgs e)
		{
			if (this.searchTextBox.Text != string.Empty)
			{
				this.findNextButton.Enabled = true;
				this.replaceButton.Enabled = true;
				this.replaceAllButton.Enabled = true;
			}
			else
			{
				this.findNextButton.Enabled = false;
				this.replaceButton.Enabled = false;
				this.replaceAllButton.Enabled = false;
			}
		}
		private void ClickFindNextButton(object sender, EventArgs e)
		{
			if (!this.htmlControl.Find(this.searchTextBox.Text, this.caseCheckBox.Checked, this.wholeCheckBox.Checked, this.upRadio.Checked))
			{
				MessageBox.Show(this, "Finished searching the document.", Resource.GetString("ApplicationName"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		private void ClickReplaceButton(object sender, EventArgs e)
		{
			if (!this.htmlControl.Replace(this.searchTextBox.Text, this.replaceTextBox.Text, this.caseCheckBox.Checked, this.wholeCheckBox.Checked, this.upRadio.Checked))
			{
				MessageBox.Show(this, "Finished searching the document.", Resource.GetString("ApplicationName"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		private void ClickReplaceAllButton(object sender, EventArgs e)
		{
			while (this.htmlControl.Replace(this.searchTextBox.Text, this.replaceTextBox.Text, this.caseCheckBox.Checked, this.wholeCheckBox.Checked, this.upRadio.Checked))
			{
			}
		}
		private TextBox replaceTextBox = new TextBox();
		private CheckBox caseCheckBox = new CheckBox();
		private CheckBox wholeCheckBox = new CheckBox();
		private RadioButton upRadio = new RadioButton();
		private TextBox searchTextBox = new TextBox();
		private Button findNextButton = new Button();
		private Button replaceButton = new Button();
		private Button replaceAllButton = new Button();
		private HtmlControl htmlControl;
	}
}
