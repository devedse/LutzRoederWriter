using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Writer
{
	internal class AboutDialog : Dialog
	{
		public AboutDialog()
		{
			this.Text = "About";
			base.ClientSize = new Size(310, 200);
			PictureBox pictureBox = new PictureBox();
			pictureBox.Location = new Point(5, 5);
			pictureBox.Size = new Size(304, 88);
			pictureBox.Image = ImageResource.Application;
			base.Controls.Add(pictureBox);
			Label label = new Label();
			label.FlatStyle = FlatStyle.System;
			label.Text = (base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0] as AssemblyTitleAttribute).Title + ", Version " + base.GetType().Assembly.GetName().Version;
			label.Location = new Point(20, 100);
			label.Size = new Size(270, 16);
			base.Controls.Add(label);
			Label label2 = new Label();
			label2.FlatStyle = FlatStyle.System;
			label2.Text = (base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0] as AssemblyCopyrightAttribute).Copyright;
			label2.Location = new Point(20, 118);
			label2.Size = new Size(270, 32);
			base.Controls.Add(label2);
			LinkLabel linkLabel = new LinkLabel();
			linkLabel.FlatStyle = FlatStyle.System;
			linkLabel.Location = new Point(20, 150);
			linkLabel.Size = new Size(200, 16);
			linkLabel.Text = Resource.GetString("Homepage");
			base.Controls.Add(linkLabel);
			Button button = new Button();
			button.FlatStyle = FlatStyle.System;
			button.Location = new Point(230, 170);
			button.Text = Resource.GetString("Ok");
			button.Size = new Size(75, 23);
			button.TabIndex = 0;
			button.DialogResult = DialogResult.OK;
			base.Controls.Add(button);
			base.AcceptButton = button;
			base.CancelButton = button;
		}
	}
}
