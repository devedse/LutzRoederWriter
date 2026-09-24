using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Writer.Forms
{
	/// <summary>
	/// Coordinates command bars and routes keyboard messages to them.
	/// </summary>
	public class CommandBarManager : Control
	{
		public CommandBarManager()
		{
			base.SetStyle(ControlStyles.UserPaint, false);
			base.TabStop = false;
			this.Dock = DockStyle.Top;
			this.commandBars = new CommandBarCollection(this);
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.commandBars = null;
			}
			base.Dispose(disposing);
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CommandBarCollection CommandBars
		{
			get
			{
				return this.commandBars;
			}
		}
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 44);
			}
		}
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				NativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
				{
					Size = Marshal.SizeOf(typeof(NativeMethods.INITCOMMONCONTROLSEX)),
					Flags = 1028
				});
			}
			base.CreateHandle();
		}
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "ReBarWindow32";
				createParams.Style = 1442840576;
				createParams.Style |= 72;
				createParams.Style |= 9728;
				return createParams;
			}
		}
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.ReleaseBands();
			this.BeginUpdate();
			for (int i = 0; i < this.commandBars.Count; i++)
			{
				NativeMethods.REBARBANDINFO rebarbandinfo = this.CreateBandInfo(i);
				NativeMethods.SendMessage(base.Handle, 1034, i, ref rebarbandinfo);
			}
			this.UpdateSize();
			this.EndUpdate();
			this.CaptureBands();
		}
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message message)
		{
			base.WndProc(ref message);
			int num = message.Msg;
			if (num == 78 || num == 8270)
			{
				num = ((NativeMethods.NMHDR)message.GetLParam(typeof(NativeMethods.NMHDR))).code;
				if (num != -841)
				{
					if (num == -831)
					{
						this.UpdateSize();
					}
				}
				else
				{
					this.NotifyChevronPushed(ref message);
				}
			}
		}
		private void NotifyChevronPushed(ref Message message)
		{
			NativeMethods.NMREBARCHEVRON nmrebarchevron = (NativeMethods.NMREBARCHEVRON)message.GetLParam(typeof(NativeMethods.NMREBARCHEVRON));
			int num = nmrebarchevron.wID - 60160;
			if (num < this.commandBars.Count && this.commandBars[num] != null)
			{
				Point point = new Point(nmrebarchevron.rc.left, nmrebarchevron.rc.bottom);
				this.commandBars[num].Show(this, point);
			}
		}
		private void BeginUpdate()
		{
			NativeMethods.SendMessage(base.Handle, 11, 0, 0);
		}
		private void EndUpdate()
		{
			NativeMethods.SendMessage(base.Handle, 11, 1, 0);
		}
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public override bool PreProcessMessage(ref Message msg)
		{
			foreach (object obj in this.commandBars)
			{
				CommandBar commandBar = (CommandBar)obj;
				if (commandBar.PreProcessMessage(ref msg))
				{
					return true;
				}
			}
			return false;
		}
		private void UpdateBand(CommandBar commandBar)
		{
			if (base.IsHandleCreated)
			{
				this.BeginUpdate();
				for (int i = 0; i < this.commandBars.Count; i++)
				{
					NativeMethods.REBARBANDINFO rebarbandinfo = default(NativeMethods.REBARBANDINFO);
					rebarbandinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.REBARBANDINFO));
					rebarbandinfo.fMask = 885;
					NativeMethods.SendMessage(base.Handle, 1029, i, ref rebarbandinfo);
					if (commandBar.Handle == rebarbandinfo.hwndChild)
					{
						if (rebarbandinfo.cyMinChild != commandBar.Height || rebarbandinfo.cx != commandBar.Width || rebarbandinfo.cxIdeal != commandBar.Width)
						{
							rebarbandinfo.cyMinChild = commandBar.Height;
							rebarbandinfo.cx = commandBar.Width;
							rebarbandinfo.cxIdeal = commandBar.Width;
							NativeMethods.SendMessage(base.Handle, 1030, i, ref rebarbandinfo);
						}
					}
				}
				this.UpdateSize();
				this.EndUpdate();
			}
		}
		private void UpdateSize()
		{
			int num = NativeMethods.SendMessage(base.Handle, 1051, 0, 0);
			base.Height = num + 1;
		}
		private NativeMethods.REBARBANDINFO CreateBandInfo(int index)
		{
			CommandBar commandBar = this.commandBars[index];
			NativeMethods.REBARBANDINFO rebarbandinfo = default(NativeMethods.REBARBANDINFO);
			rebarbandinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.REBARBANDINFO));
			rebarbandinfo.fMask = 0;
			rebarbandinfo.clrFore = 0;
			rebarbandinfo.clrBack = 0;
			rebarbandinfo.iImage = 0;
			rebarbandinfo.hbmBack = IntPtr.Zero;
			rebarbandinfo.lParam = 0;
			rebarbandinfo.cxHeader = 0;
			rebarbandinfo.fMask |= 256;
			rebarbandinfo.wID = 60160 + index;
			if (commandBar.Text != null && commandBar.Text.Length != 0)
			{
				rebarbandinfo.fMask |= 4;
				rebarbandinfo.lpText = Marshal.StringToHGlobalUni(commandBar.Text);
				rebarbandinfo.cch = ((commandBar.Text == null) ? 0 : commandBar.Text.Length);
			}
			rebarbandinfo.fMask |= 1;
			rebarbandinfo.fStyle = 164;
			rebarbandinfo.fStyle |= 1;
			rebarbandinfo.fStyle |= 512;
			rebarbandinfo.fMask |= 16;
			rebarbandinfo.hwndChild = commandBar.Handle;
			rebarbandinfo.fMask |= 32;
			rebarbandinfo.cyMinChild = commandBar.Height;
			rebarbandinfo.cxMinChild = 0;
			rebarbandinfo.cyChild = 0;
			rebarbandinfo.cyMaxChild = commandBar.Height;
			rebarbandinfo.cyIntegral = commandBar.Height;
			rebarbandinfo.fMask |= 64;
			rebarbandinfo.cx = commandBar.Width;
			rebarbandinfo.fMask |= 512;
			rebarbandinfo.cxIdeal = commandBar.Width;
			return rebarbandinfo;
		}
		internal void UpdateBands()
		{
			if (base.IsHandleCreated)
			{
				base.RecreateHandle();
			}
		}
		private void CommandBar_HandleCreated(object sender, EventArgs e)
		{
			this.ReleaseBands();
			CommandBar commandBar = (CommandBar)sender;
			this.UpdateBand(commandBar);
			this.CaptureBands();
		}
		private void CommandBar_TextChanged(object sender, EventArgs e)
		{
			CommandBar commandBar = (CommandBar)sender;
			this.UpdateBand(commandBar);
		}
		private void CaptureBands()
		{
			foreach (object obj in this.commandBars)
			{
				CommandBar commandBar = (CommandBar)obj;
				commandBar.HandleCreated += this.CommandBar_HandleCreated;
				commandBar.TextChanged += this.CommandBar_TextChanged;
			}
		}
		private void ReleaseBands()
		{
			foreach (object obj in this.commandBars)
			{
				CommandBar commandBar = (CommandBar)obj;
				commandBar.HandleCreated -= this.CommandBar_HandleCreated;
				commandBar.TextChanged -= this.CommandBar_TextChanged;
			}
		}
		private CommandBarCollection commandBars;
	}
}
