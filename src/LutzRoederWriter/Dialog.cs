using System;
using System.Drawing;
using System.Windows.Forms;

namespace Writer
{
	internal class Dialog : Form
	{
		public Dialog()
		{
			this.Text = Resource.GetString("ApplicationName");
			base.Icon = null;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.Font = new Font("Tahoma", 8.25f);
			base.ControlBox = true;
			base.MaximizeBox = (base.MinimizeBox = false);
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
		}
		public bool Run()
		{
			return base.ShowDialog() == DialogResult.OK;
		}
	}
}
