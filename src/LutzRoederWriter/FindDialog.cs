using System;
using System.Drawing;
using System.Windows.Forms;

namespace Writer
{
	internal class FindDialog : Dialog
	{
		public FindDialog()
		{
			base.ClientSize = new Size(344, 86);
			this.Text = "Find";
			Label label = new Label();
			label.FlatStyle = FlatStyle.System;
			label.Location = new Point(8, 12);
			label.Size = new Size(56, 16);
			label.TabIndex = 0;
			label.Text = "Fi&nd what:";
			base.Controls.Add(label);
			this.searchTextBox.Location = new Point(64, 8);
			this.searchTextBox.Size = new Size(184, 20);
			this.searchTextBox.TabIndex = 1;
			this.searchTextBox.Text = "";
			this.searchTextBox.TextChanged += this.TextChangeSearchTextBox;
			base.Controls.Add(this.searchTextBox);
			this.findNextButton.FlatStyle = FlatStyle.System;
			this.findNextButton.Location = new Point(256, 8);
			this.findNextButton.TabIndex = 6;
			this.findNextButton.Text = "&Find Next";
			this.findNextButton.DialogResult = DialogResult.OK;
			this.findNextButton.Enabled = false;
			base.Controls.Add(this.findNextButton);
			base.AcceptButton = this.findNextButton;
			Button button = new Button();
			button.FlatStyle = FlatStyle.System;
			button.Location = new Point(256, 36);
			button.TabIndex = 7;
			button.Text = "Cancel";
			button.DialogResult = DialogResult.Cancel;
			base.Controls.Add(button);
			base.CancelButton = button;
			this.upRadio.FlatStyle = FlatStyle.System;
			this.upRadio.Location = new Point(8, 16);
			this.upRadio.Size = new Size(40, 16);
			this.upRadio.TabIndex = 5;
			this.upRadio.Text = "&Up";
			base.Controls.Add(this.upRadio);
			RadioButton radioButton = new RadioButton();
			radioButton.Checked = true;
			radioButton.FlatStyle = FlatStyle.System;
			radioButton.Location = new Point(48, 16);
			radioButton.Size = new Size(48, 16);
			radioButton.TabIndex = 4;
			radioButton.TabStop = true;
			radioButton.Text = "&Down";
			base.Controls.Add(radioButton);
			GroupBox groupBox = new GroupBox();
			groupBox.Controls.Add(radioButton);
			groupBox.Controls.Add(this.upRadio);
			groupBox.FlatStyle = FlatStyle.System;
			groupBox.Location = new Point(144, 32);
			groupBox.Size = new Size(104, 40);
			groupBox.TabIndex = 5;
			groupBox.TabStop = false;
			groupBox.Text = "Direction";
			base.Controls.Add(groupBox);
			this.wholeCheckBox.FlatStyle = FlatStyle.System;
			this.wholeCheckBox.Location = new Point(8, 36);
			this.wholeCheckBox.Size = new Size(136, 16);
			this.wholeCheckBox.TabIndex = 2;
			this.wholeCheckBox.Text = "Match &whole word only";
			base.Controls.Add(this.wholeCheckBox);
			this.caseCheckBox.FlatStyle = FlatStyle.System;
			this.caseCheckBox.Location = new Point(8, 56);
			this.caseCheckBox.Size = new Size(128, 16);
			this.caseCheckBox.TabIndex = 3;
			this.caseCheckBox.Text = "Match &case";
			base.Controls.Add(this.caseCheckBox);
			this.searchTextBox.Focus();
		}
		public string SearchText
		{
			get
			{
				return this.searchTextBox.Text;
			}
			set
			{
				this.searchTextBox.Text = value;
			}
		}
		public bool IsUp
		{
			get
			{
				return this.upRadio.Checked;
			}
			set
			{
				this.upRadio.Checked = value;
			}
		}
		public bool IsWholeChecked
		{
			get
			{
				return this.wholeCheckBox.Checked;
			}
			set
			{
				this.wholeCheckBox.Checked = value;
			}
		}
		public bool IsCaseChecked
		{
			get
			{
				return this.caseCheckBox.Checked;
			}
			set
			{
				this.caseCheckBox.Checked = value;
			}
		}
		private void TextChangeSearchTextBox(object sender, EventArgs e)
		{
			if (this.searchTextBox.Text != string.Empty)
			{
				this.findNextButton.Enabled = true;
			}
			else
			{
				this.findNextButton.Enabled = false;
			}
		}
		private TextBox searchTextBox = new TextBox();
		private RadioButton upRadio = new RadioButton();
		private CheckBox wholeCheckBox = new CheckBox();
		private CheckBox caseCheckBox = new CheckBox();
		private Button findNextButton = new Button();
	}
}
