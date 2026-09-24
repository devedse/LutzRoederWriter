using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Writer.Forms
{
	/// <summary>
	/// Displays a collection of Writer command items as a menu, toolbar, or popup bar.
	/// </summary>
	[DesignTimeVisible(true)]
	[ToolboxItem(false)]
	[TypeConverter(typeof(CommandBarTypeConverter))]
	public class CommandBar : Control, IDisposable
	{
		public CommandBar(CommandBarManager commandBarManager)
		{
			this.commandBarManager = commandBarManager;
			this.items = new CommandBarItemCollection(this);
			base.SetStyle(ControlStyles.UserPaint, false);
			base.TabStop = false;
			this.Font = SystemInformation.MenuFont;
			this.Dock = DockStyle.Top;
		}
		public CommandBar(CommandBarManager commandBarManager, CommandBarStyle style)
			: this(commandBarManager)
		{
			this.style = style;
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.items.Clear();
				this.items = null;
				this.contextMenu = null;
			}
			base.Dispose(disposing);
		}
		public CommandBarStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
				this.UpdateItems();
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CommandBarItemCollection Items
		{
			get
			{
				return this.items;
			}
		}
		protected override Size DefaultSize
		{
			get
			{
				return new Size(0, 0);
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
				createParams.ClassName = "ToolbarWindow32";
				createParams.Parent = this.commandBarManager.Handle;
				createParams.ExStyle = 0;
				createParams.Style = 1442840576;
				createParams.Style |= 76;
				createParams.Style |= 35072;
				if (this.Style == CommandBarStyle.Menu)
				{
					createParams.Style |= 4096;
				}
				return createParams;
			}
		}
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.AddItems();
		}
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public override bool PreProcessMessage(ref Message message)
		{
			if (message.Msg == 256 || message.Msg == 260)
			{
				Keys keys = (Keys)((int)message.WParam | (int)Control.ModifierKeys);
				if (this.state == CommandBar.State.None)
				{
					CommandBarItem[] array = this.items[keys];
					if (array.Length > 0)
					{
						if (this.PerformClick(array[0]))
						{
							return true;
						}
					}
				}
			}
			bool flag;
			if (this.Style != CommandBarStyle.Menu)
			{
				flag = false;
			}
			else
			{
				if (message.Msg >= 513 && message.Msg <= 522)
				{
					if (message.HWnd != base.Handle && this.state != CommandBar.State.None)
					{
						this.SetState(CommandBar.State.None, -1);
					}
				}
				else if (message.Msg == 261 || message.Msg == 256 || message.Msg == 260)
				{
					Keys keys = (Keys)((int)message.WParam | (int)Control.ModifierKeys);
					Keys keys2 = (Keys)(int)message.WParam;
					if (keys == Keys.F10 || keys2 == Keys.Menu)
					{
						if (message.Msg == 261)
						{
							if (this.state == CommandBar.State.Hot || this.lastState == CommandBar.State.HotTracking)
							{
								this.SetState(CommandBar.State.None, 0);
							}
							else if (this.state == CommandBar.State.None)
							{
								this.SetState(CommandBar.State.Hot, 0);
							}
							return true;
						}
					}
					else if (message.Msg == 256 || message.Msg == 260)
					{
						if (this.PreProcessKeyDown(ref message))
						{
							return true;
						}
					}
				}
				flag = base.PreProcessMessage(ref message);
			}
			return flag;
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.Style == CommandBarStyle.Menu)
			{
				if (e.Button == MouseButtons.Left && e.Clicks == 1)
				{
					Point point = new Point(e.X, e.Y);
					int num = this.HitTest(point);
					if (this.IsValid(num))
					{
						this.TrackDropDown(num);
						return;
					}
				}
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.Style == CommandBarStyle.Menu)
			{
				Point point = new Point(e.X, e.Y);
				if (this.state == CommandBar.State.Hot)
				{
					int num = this.HitTest(point);
					if (this.IsValid(num) && point != this.lastMousePosition)
					{
						this.SetHotItem(num);
					}
					return;
				}
				this.lastMousePosition = point;
			}
			base.OnMouseMove(e);
		}
		private bool PreProcessKeyDown(ref Message message)
		{
			Keys keys = (Keys)((int)message.WParam | (int)Control.ModifierKeys);
			if (this.state == CommandBar.State.Hot)
			{
				int hotItem = this.GetHotItem();
				if (hotItem != -1)
				{
					if (keys == Keys.Left)
					{
						this.SetHotItem(this.GetPreviousItem(hotItem));
						return true;
					}
					if (keys == Keys.Right)
					{
						this.SetHotItem(this.GetNextItem(hotItem));
						return true;
					}
					if (keys == Keys.Up || keys == Keys.Down || keys == Keys.Return)
					{
						this.TrackDropDown(hotItem);
						return true;
					}
				}
				if (keys == Keys.Escape)
				{
					this.SetState(CommandBar.State.None, -1);
					return true;
				}
			}
			bool flag = (keys & Keys.Alt) != Keys.None;
			if (this.state == CommandBar.State.Hot || flag)
			{
				Keys keys2 = keys & Keys.KeyCode;
				char c = (char)keys2;
				if (char.IsDigit(c) || char.IsLetter(c))
				{
					if (this.PreProcessMnemonic(keys2))
					{
						return true;
					}
					if (this.state == CommandBar.State.Hot && !flag)
					{
						NativeMethods.MessageBeep(0);
						return true;
					}
				}
			}
			if (this.state != CommandBar.State.None)
			{
				this.SetState(CommandBar.State.None, -1);
			}
			return false;
		}
		private bool PreProcessMnemonic(Keys keyCode)
		{
			char c = (char)keyCode;
			CommandBarItem[] array = this.items[c];
			bool flag;
			if (array.Length > 0)
			{
				int num = this.items.IndexOf(array[0]);
				this.TrackDropDown(num);
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}
		private bool IsValid(int index)
		{
			int num = NativeMethods.SendMessage(base.Handle, 1048, 0, 0);
			return index >= 0 && index < num;
		}
		private int HitTest(Point point)
		{
			NativeMethods.POINT point2 = default(NativeMethods.POINT);
			point2.x = point.X;
			point2.y = point.Y;
			int num = NativeMethods.SendMessage(base.Handle, 1093, 0, ref point2);
			if (num > 0)
			{
				point = base.PointToScreen(point);
				if (!base.RectangleToScreen(new Rectangle(0, 0, base.Width, base.Height)).Contains(point))
				{
					return -1;
				}
			}
			return num;
		}
		private int GetNextItem(int index)
		{
			if (index < 0)
			{
				throw new ArgumentException("index");
			}
			int num = NativeMethods.SendMessage(base.Handle, 1048, 0, 0);
			int num2 = index;
			do
			{
				num2 = (num2 + 1) % num;
			}
			while (num2 != index && !this.items[num2].Visible);
			return num2;
		}
		private int GetPreviousItem(int index)
		{
			if (index < 0)
			{
				throw new ArgumentException("index");
			}
			int num = NativeMethods.SendMessage(base.Handle, 1048, 0, 0);
			int num2 = index;
			do
			{
				num2 = (num2 + num - 1) % num;
			}
			while (num2 != index && !this.items[num2].Visible);
			return num2;
		}
		private int GetHotItem()
		{
			return NativeMethods.SendMessage(base.Handle, 1095, 0, 0);
		}
		private void SetHotItem(int index)
		{
			NativeMethods.SendMessage(base.Handle, 1096, index, 0);
		}
		private void SetState(CommandBar.State state, int index)
		{
			if (this.state != state)
			{
				if (state == CommandBar.State.None)
				{
					index = -1;
				}
				this.SetHotItem(index);
				if (state == CommandBar.State.HotTracking)
				{
					this.trackEscapePressed = false;
					this.trackHotItem = index;
				}
			}
			this.lastState = this.state;
			this.state = state;
		}
		private void TrackDropDownNext(int index)
		{
			if (index != this.trackHotItem)
			{
				NativeMethods.PostMessage(base.Handle, 31, 0, 0);
				this.trackNextItem = index;
			}
		}
		private void TrackDropDown(int index)
		{
			while (index >= 0)
			{
				this.trackNextItem = -1;
				this.BeginUpdate();
				CommandBarMenu commandBarMenu = this.items[index] as CommandBarMenu;
				if (commandBarMenu != null)
				{
					commandBarMenu.PerformDropDown(EventArgs.Empty);
					this.contextMenu.Items.Clear();
					this.contextMenu.Items.AddRange(commandBarMenu.Items);
					this.contextMenu.Mnemonics = true;
				}
				else
				{
					this.contextMenu.Items.Clear();
					this.contextMenu.Mnemonics = true;
				}
				NativeMethods.SendMessage(base.Handle, 1027, index, -1);
				NativeMethods.PostMessage(base.Handle, 256, 40, 1);
				NativeMethods.PostMessage(base.Handle, 257, 40, 1);
				this.SetState(CommandBar.State.HotTracking, index);
				NativeMethods.HookProc hookProc = new NativeMethods.HookProc(this.DropDownHook);
				GCHandle gchandle = GCHandle.Alloc(hookProc);
				this.hookHandle = NativeMethods.SetWindowsHookEx(-1, hookProc, IntPtr.Zero, NativeMethods.GetCurrentThreadId());
				if (this.hookHandle == IntPtr.Zero)
				{
					throw new SecurityException();
				}
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				NativeMethods.SendMessage(base.Handle, 1075, index, ref rect);
				Point point = new Point(rect.left, rect.bottom);
				this.EndUpdate();
				base.Update();
				this.contextMenu.Show(this, point);
				NativeMethods.UnhookWindowsHookEx(this.hookHandle);
				gchandle.Free();
				this.hookHandle = IntPtr.Zero;
				NativeMethods.SendMessage(base.Handle, 1027, index, 0);
				this.SetState(this.trackEscapePressed ? CommandBar.State.Hot : CommandBar.State.None, index);
				index = this.trackNextItem;
			}
		}
		public void Show(Control control, Point point)
		{
			CommandBarItemCollection commandBarItemCollection = new CommandBarItemCollection();
			Size clientSize = base.ClientSize;
			for (int i = 0; i < this.items.Count; i++)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				NativeMethods.SendMessage(base.Handle, 1053, i, ref rect);
				if (rect.right > clientSize.Width)
				{
					CommandBarItem commandBarItem = this.items[i];
					if (commandBarItem.Visible)
					{
						if (!(commandBarItem is CommandBarSeparator) || commandBarItemCollection.Count != 0)
						{
							commandBarItemCollection.Add(commandBarItem);
						}
					}
				}
			}
			this.contextMenu.Mnemonics = false;
			this.contextMenu.Items.Clear();
			this.contextMenu.Items.AddRange(commandBarItemCollection);
			this.contextMenu.Show(control, point);
		}
		private bool DropDownFilter(ref Message message)
		{
			if (this.state != CommandBar.State.HotTracking)
			{
				throw new InvalidOperationException();
			}
			this.SetHotItem(this.trackHotItem);
			if (message.Msg == 256)
			{
				Keys keys = (Keys)((int)message.WParam | (int)Control.ModifierKeys);
				if (keys == Keys.Left)
				{
					this.TrackDropDownNext(this.GetPreviousItem(this.trackHotItem));
					return true;
				}
				if (keys == Keys.Right && (this.contextMenu.SelectedMenuItem == null || this.contextMenu.SelectedMenuItem.MenuItems.Count == 0))
				{
					this.TrackDropDownNext(this.GetNextItem(this.trackHotItem));
					return true;
				}
				if (keys == Keys.Escape)
				{
					this.trackEscapePressed = true;
				}
			}
			else if (message.Msg == 512 || message.Msg == 513)
			{
				Point point = new Point((int)message.LParam & 65535, (int)message.LParam >> 16);
				point = base.PointToClient(point);
				if (message.Msg == 512)
				{
					if (point != this.lastMousePosition)
					{
						int num = this.HitTest(point);
						if (this.IsValid(num) && num != this.trackHotItem)
						{
							this.TrackDropDownNext(num);
						}
						this.lastMousePosition = point;
					}
				}
				else if (message.Msg == 513)
				{
					if (this.HitTest(point) == this.trackHotItem)
					{
						this.TrackDropDownNext(-1);
						return true;
					}
				}
			}
			return false;
		}
		private IntPtr DropDownHook(int code, IntPtr wparam, IntPtr lparam)
		{
			if (code == 2)
			{
				NativeMethods.MSG msg = (NativeMethods.MSG)Marshal.PtrToStructure(lparam, typeof(NativeMethods.MSG));
				Message message = Message.Create(msg.hwnd, msg.message, msg.wParam, msg.lParam);
				if (this.DropDownFilter(ref message))
				{
					return (IntPtr)1;
				}
			}
			return NativeMethods.CallNextHookEx(this.hookHandle, code, wparam, lparam);
		}
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message message)
		{
			base.WndProc(ref message);
			int num = message.Msg;
			if (num <= 288)
			{
				if (num != 78)
				{
					if (num != 288)
					{
						return;
					}
					this.WmMenuChar(ref message);
					return;
				}
			}
			else if (num != 8270)
			{
				if (num != 8465)
				{
					return;
				}
				int num2 = (int)message.WParam & 65535;
				this.PerformClick(this.items[num2]);
				base.WndProc(ref message);
				base.ResetMouseEventArgs();
				return;
			}
			num = ((NativeMethods.NMHDR)message.GetLParam(typeof(NativeMethods.NMHDR))).code;
			if (num <= -706)
			{
				if (num != -713)
				{
					if (num != -710)
					{
						if (num == -706)
						{
							message.Result = (IntPtr)1;
						}
					}
					else
					{
						this.NotifyDropDown(ref message);
					}
				}
			}
			else if (num != -530)
			{
				if (num != -520)
				{
					if (num == -12)
					{
						this.NotifyCustomDraw(ref message);
					}
				}
				else
				{
					this.NotifyNeedTextA(ref message);
				}
			}
			else
			{
				this.NotifyNeedTextW(ref message);
			}
		}
		private void NotifyCustomDrawMenuBar(ref Message m)
		{
			m.Result = (IntPtr)0;
			NativeMethods.LPNMTBCUSTOMDRAW lpnmtbcustomdraw = (NativeMethods.LPNMTBCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.LPNMTBCUSTOMDRAW));
			bool flag = (lpnmtbcustomdraw.nmcd.uItemState & 64) != 0;
			bool flag2 = (lpnmtbcustomdraw.nmcd.uItemState & 1) != 0;
			if (flag || flag2)
			{
				NativeMethods.RECT rc = lpnmtbcustomdraw.nmcd.rc;
				using (Graphics graphics = Graphics.FromHdc(lpnmtbcustomdraw.nmcd.hdc))
				{
					graphics.FillRectangle(SystemBrushes.Highlight, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top);
				}
				using (TextGraphics textGraphics = new TextGraphics(lpnmtbcustomdraw.nmcd.hdc))
				{
					Font font = this.Font;
					string text = this.items[(int)lpnmtbcustomdraw.nmcd.dwItemSpec].Text;
					Size size = textGraphics.MeasureText(text, font);
					Point point = new Point(rc.left + (rc.right - rc.left - size.Width) / 2, rc.top + (rc.bottom - rc.top - size.Height) / 2);
					textGraphics.DrawText(text, point, font, SystemColors.HighlightText);
				}
				m.Result = (IntPtr)4;
			}
		}
		private void NotifyCustomDrawToolBar(ref Message m)
		{
			m.Result = (IntPtr)0;
			NativeMethods.DLLVERSIONINFO dllversioninfo = default(NativeMethods.DLLVERSIONINFO);
			dllversioninfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.DLLVERSIONINFO));
			NativeMethods.DllGetVersion(ref dllversioninfo);
			if (dllversioninfo.dwMajorVersion < 6)
			{
				NativeMethods.LPNMTBCUSTOMDRAW lpnmtbcustomdraw = (NativeMethods.LPNMTBCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.LPNMTBCUSTOMDRAW));
				NativeMethods.RECT rc = lpnmtbcustomdraw.nmcd.rc;
				Rectangle rectangle = new Rectangle(rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top);
				Graphics graphics = Graphics.FromHdc(lpnmtbcustomdraw.nmcd.hdc);
				CommandBarItem commandBarItem = this.items[(int)lpnmtbcustomdraw.nmcd.dwItemSpec];
				bool flag = (lpnmtbcustomdraw.nmcd.uItemState & 64) != 0;
				bool flag2 = (lpnmtbcustomdraw.nmcd.uItemState & 1) != 0;
				bool flag3 = (lpnmtbcustomdraw.nmcd.uItemState & 4) != 0;
				CommandBarCheckBox commandBarCheckBox = commandBarItem as CommandBarCheckBox;
				if (commandBarCheckBox != null && commandBarCheckBox.IsChecked)
				{
					ControlPaint.DrawBorder3D(graphics, rectangle, Border3DStyle.SunkenOuter);
				}
				else if (flag2)
				{
					ControlPaint.DrawBorder3D(graphics, rectangle, Border3DStyle.SunkenOuter);
				}
				else if (flag)
				{
					ControlPaint.DrawBorder3D(graphics, rectangle, Border3DStyle.RaisedInner);
				}
				Image image = commandBarItem.Image;
				if (image != null)
				{
					Size size = image.Size;
					Point point = new Point(rc.left + (rc.right - rc.left - size.Width) / 2, rc.top + (rc.bottom - rc.top - size.Height) / 2);
					NativeMethods.DrawImage(graphics, image, point, flag3);
				}
				m.Result = (IntPtr)4;
			}
		}
		private void NotifyCustomDraw(ref Message m)
		{
			m.Result = (IntPtr)0;
			int dwDrawStage = ((NativeMethods.LPNMTBCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.LPNMTBCUSTOMDRAW))).nmcd.dwDrawStage;
			if (dwDrawStage != 1)
			{
				if (dwDrawStage == 65537)
				{
					if (this.style == CommandBarStyle.Menu)
					{
						this.NotifyCustomDrawMenuBar(ref m);
					}
					if (this.style == CommandBarStyle.ToolBar)
					{
						this.NotifyCustomDrawToolBar(ref m);
					}
				}
			}
			else
			{
				m.Result = (IntPtr)32;
			}
		}
		private void WmMenuChar(ref Message message)
		{
			Menu menu = this.contextMenu.FindMenuItem(0, message.LParam);
			if (this.contextMenu.Handle == message.LParam)
			{
				menu = this.contextMenu;
			}
			if (menu != null)
			{
				char c = char.ToUpper((char)((int)message.WParam & 65535), CultureInfo.InvariantCulture);
				int num = 0;
				foreach (object obj in menu.MenuItems)
				{
					MenuItem menuItem = (MenuItem)obj;
					if (menuItem != null && menuItem.OwnerDraw && menuItem.Mnemonic == c)
					{
						message.Result = (IntPtr)(131072 | num);
						break;
					}
					if (menuItem.Visible)
					{
						num++;
					}
				}
			}
		}
		private void NotifyDropDown(ref Message message)
		{
			if (this.Style == CommandBarStyle.ToolBar)
			{
				this.TrackDropDown(((NativeMethods.NMTOOLBAR)message.GetLParam(typeof(NativeMethods.NMTOOLBAR))).iItem);
			}
		}
		private void NotifyNeedTextA(ref Message message)
		{
			if (this.Style != CommandBarStyle.Menu)
			{
				NativeMethods.TOOLTIPTEXTA tooltiptexta = (NativeMethods.TOOLTIPTEXTA)message.GetLParam(typeof(NativeMethods.TOOLTIPTEXTA));
				CommandBarItem commandBarItem = this.items[(int)tooltiptexta.hdr.idFrom];
				tooltiptexta.szText = commandBarItem.Text;
				CommandBarButtonBase commandBarButtonBase = commandBarItem as CommandBarButtonBase;
				if (commandBarButtonBase != null && commandBarButtonBase.Shortcut != Keys.None)
				{
					tooltiptexta.szText = tooltiptexta.szText + " (" + TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(null, CultureInfo.InvariantCulture, commandBarButtonBase.Shortcut) + ")";
				}
				tooltiptexta.hinst = IntPtr.Zero;
				if (this.RightToLeft == RightToLeft.Yes)
				{
					tooltiptexta.uFlags |= 4;
				}
				Marshal.StructureToPtr(tooltiptexta, message.LParam, true);
				message.Result = (IntPtr)1;
			}
		}
		private void NotifyNeedTextW(ref Message message)
		{
			if (this.Style != CommandBarStyle.Menu && Marshal.SystemDefaultCharSize == 2)
			{
				NativeMethods.TOOLTIPTEXT tooltiptext = (NativeMethods.TOOLTIPTEXT)message.GetLParam(typeof(NativeMethods.TOOLTIPTEXT));
				CommandBarItem commandBarItem = this.items[(int)tooltiptext.hdr.idFrom];
				tooltiptext.szText = commandBarItem.Text;
				CommandBarButtonBase commandBarButtonBase = commandBarItem as CommandBarButton;
				if (commandBarButtonBase != null && commandBarButtonBase.Shortcut != Keys.None)
				{
					tooltiptext.szText = tooltiptext.szText + " (" + TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(null, CultureInfo.InvariantCulture, commandBarButtonBase.Shortcut) + ")";
				}
				tooltiptext.hinst = IntPtr.Zero;
				if (this.RightToLeft == RightToLeft.Yes)
				{
					tooltiptext.uFlags |= 4;
				}
				Marshal.StructureToPtr(tooltiptext, message.LParam, true);
				message.Result = (IntPtr)1;
			}
		}
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.contextMenu.Font = this.Font;
			this.UpdateItems();
		}
		private bool PerformClick(CommandBarItem item)
		{
			Application.DoEvents();
			CommandBarControl commandBarControl = item as CommandBarControl;
			bool flag;
			if (commandBarControl != null)
			{
				commandBarControl.PerformClick(EventArgs.Empty);
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}
		private void BeginUpdate()
		{
			NativeMethods.SendMessage(base.Handle, 11, 0, 0);
		}
		private void EndUpdate()
		{
			NativeMethods.SendMessage(base.Handle, 11, 1, 0);
		}
		private NativeMethods.TBBUTTONINFO GetButtonInfo(int index)
		{
			CommandBarItem commandBarItem = this.items[index];
			NativeMethods.TBBUTTONINFO tbbuttoninfo = default(NativeMethods.TBBUTTONINFO);
			tbbuttoninfo.cbSize = Marshal.SizeOf(tbbuttoninfo);
			tbbuttoninfo.dwMask = 45;
			tbbuttoninfo.idCommand = index;
			tbbuttoninfo.iImage = -1;
			tbbuttoninfo.fsStyle = 16;
			tbbuttoninfo.fsState = 0;
			tbbuttoninfo.cx = 0;
			tbbuttoninfo.lParam = IntPtr.Zero;
			tbbuttoninfo.lpszText = string.Empty;
			tbbuttoninfo.cchText = 0;
			if (!commandBarItem.Visible)
			{
				tbbuttoninfo.fsState |= 8;
			}
			CommandBarComboBox commandBarComboBox = commandBarItem as CommandBarComboBox;
			if (commandBarComboBox != null)
			{
				tbbuttoninfo.cx = (short)(commandBarComboBox.Width + 4);
				tbbuttoninfo.dwMask = 64;
			}
			if (commandBarItem is CommandBarSeparator)
			{
				tbbuttoninfo.fsStyle |= 1;
			}
			else
			{
				if (commandBarItem.Enabled)
				{
					tbbuttoninfo.fsState |= 4;
				}
				CommandBarMenu commandBarMenu = commandBarItem as CommandBarMenu;
				if (commandBarMenu != null && commandBarMenu.Items.Count > 0)
				{
					tbbuttoninfo.fsStyle |= 8;
				}
				if (this.style == CommandBarStyle.ToolBar)
				{
					if (commandBarItem is CommandBarMenu)
					{
						tbbuttoninfo.fsStyle |= 128;
					}
				}
				CommandBarCheckBox commandBarCheckBox = commandBarItem as CommandBarCheckBox;
				if (commandBarCheckBox != null && commandBarCheckBox.IsChecked)
				{
					tbbuttoninfo.fsState |= 1;
				}
			}
			if (commandBarItem is CommandBarSeparator)
			{
				tbbuttoninfo.iImage = -2;
			}
			else if (commandBarItem.Image != null)
			{
				tbbuttoninfo.iImage = index;
			}
			if (this.Style == CommandBarStyle.Menu && commandBarItem.Text != null && commandBarItem.Text.Length != 0)
			{
				tbbuttoninfo.dwMask |= 2;
				tbbuttoninfo.lpszText = commandBarItem.Text + '\0';
				tbbuttoninfo.cchText = commandBarItem.Text.Length;
			}
			return tbbuttoninfo;
		}
		private void UpdateImageList()
		{
			IntPtr intPtr = IntPtr.Zero;
			if (this.Style != CommandBarStyle.Menu)
			{
				Size size = new Size(8, 8);
				for (int i = 0; i < this.items.Count; i++)
				{
					Image image = this.items[i].Image;
					if (image != null)
					{
						if (image.Width > size.Width)
						{
							size.Width = image.Width;
						}
						if (image.Height > size.Height)
						{
							size.Height = image.Height;
						}
					}
				}
				Image[] array = new Image[this.items.Count];
				for (int i = 0; i < this.items.Count; i++)
				{
					Image image = this.items[i].Image;
					array[i] = ((image != null) ? image : new Bitmap(size.Width, size.Height));
				}
				if (this.imageList == null)
				{
					this.imageList = new ImageList();
					this.imageList.ImageSize = size;
					this.imageList.ColorDepth = ColorDepth.Depth32Bit;
					for (int i = 0; i < array.Length; i++)
					{
						this.imageList.Images.Add(array[i]);
					}
				}
				else if (this.imageList.Images.Count == array.Length)
				{
					for (int i = 0; i < array.Length; i++)
					{
						this.imageList.Images[i] = array[i];
					}
				}
				else
				{
					this.imageList.Images.Clear();
					this.imageList.ImageSize = size;
					for (int i = 0; i < array.Length; i++)
					{
						this.imageList.Images.Add(array[i]);
					}
				}
				intPtr = this.imageList.Handle;
			}
			NativeMethods.SendMessage(base.Handle, 1072, 0, intPtr);
		}
		private void UpdateItems()
		{
			if (base.IsHandleCreated)
			{
				this.BeginUpdate();
				this.RemoveItems();
				this.AddItems();
				this.EndUpdate();
			}
		}
		private void AddItems()
		{
			NativeMethods.SendMessage(base.Handle, 1054, Marshal.SizeOf(typeof(NativeMethods.TBBUTTON)), 0);
			int num = 144;
			if (this.style == CommandBarStyle.ToolBar)
			{
				num |= 1;
			}
			NativeMethods.SendMessage(base.Handle, 1108, 0, num);
			this.UpdateImageList();
			for (int i = 0; i < this.items.Count; i++)
			{
				NativeMethods.TBBUTTON tbbutton = default(NativeMethods.TBBUTTON);
				tbbutton.idCommand = i;
				NativeMethods.SendMessage(base.Handle, 1045, i, ref tbbutton);
				NativeMethods.TBBUTTONINFO buttonInfo = this.GetButtonInfo(i);
				NativeMethods.SendMessage(base.Handle, 1088, i, ref buttonInfo);
			}
			base.Controls.Clear();
			for (int i = 0; i < this.items.Count; i++)
			{
				CommandBarComboBox commandBarComboBox = this.items[i] as CommandBarComboBox;
				if (commandBarComboBox != null)
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					NativeMethods.SendMessage(base.Handle, 1053, i, ref rect);
					rect.top += (rect.bottom - rect.top - commandBarComboBox.Height) / 2;
					commandBarComboBox.ComboBox.Location = new Point(rect.left, rect.top);
					base.Controls.Add(commandBarComboBox.ComboBox);
				}
			}
			this.UpdateSize();
		}
		private void RemoveItems()
		{
			while (NativeMethods.SendMessage(base.Handle, 1048, 0, 0) > 0)
			{
				NativeMethods.SendMessage(base.Handle, 1046, 0, 0);
			}
		}
		private void UpdateSize()
		{
			if (this.style == CommandBarStyle.Menu)
			{
				int num = this.Font.Height;
				using (Graphics graphics = base.CreateGraphics())
				{
					using (TextGraphics textGraphics = new TextGraphics(graphics))
					{
						foreach (object obj in this.items)
						{
							CommandBarItem commandBarItem = (CommandBarItem)obj;
							Size size = textGraphics.MeasureText(commandBarItem.Text, this.Font);
							if (num < size.Height)
							{
								num = size.Height;
							}
						}
					}
				}
				NativeMethods.SendMessage(base.Handle, 1056, 0, (num << 16) | 65535);
			}
			Size size2 = new Size(0, 0);
			for (int i = 0; i < this.items.Count; i++)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				NativeMethods.SendMessage(base.Handle, 1075, i, ref rect);
				int num2 = rect.bottom - rect.top;
				if (num2 > size2.Height)
				{
					size2.Height = num2;
				}
				size2.Width += rect.right - rect.left;
				CommandBarComboBox commandBarComboBox = this.items[i] as CommandBarComboBox;
				if (commandBarComboBox != null && commandBarComboBox.ComboBox != null)
				{
					if (commandBarComboBox.ComboBox.Height > size2.Height)
					{
						size2.Height = commandBarComboBox.ComboBox.Height;
					}
					this.UpdateComboBoxLocation(commandBarComboBox, i);
				}
			}
			base.Size = size2;
		}
		private void UpdateComboBoxLocation(CommandBarComboBox comboBox, int index)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			NativeMethods.SendMessage(base.Handle, 1053, index, ref rect);
			int num = rect.bottom - rect.top;
			if (num > comboBox.ComboBox.Height)
			{
				rect.top += (num - comboBox.ComboBox.Height) / 2;
			}
			comboBox.ComboBox.Location = new Point(rect.left + 2, rect.top);
		}
		internal void AddItem(CommandBarItem item)
		{
			item.PropertyChanged += this.CommandBarItem_PropertyChanged;
			this.UpdateItems();
		}
		internal void RemoveItem(CommandBarItem item)
		{
			item.PropertyChanged -= this.CommandBarItem_PropertyChanged;
			this.UpdateItems();
		}
		private void CommandBarItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (base.IsHandleCreated)
			{
				CommandBarItem commandBarItem = (CommandBarItem)sender;
				int num = this.Items.IndexOf(commandBarItem);
				if (num != -1)
				{
					string propertyName = e.PropertyName;
					if (propertyName != null)
					{
						if (propertyName == "IsVisible")
						{
							this.UpdateItems();
							goto IL_0090;
						}
						if (propertyName == "Image")
						{
							this.UpdateImageList();
							goto IL_0090;
						}
					}
					NativeMethods.TBBUTTONINFO buttonInfo = this.GetButtonInfo(num);
					NativeMethods.SendMessage(base.Handle, 1088, num, ref buttonInfo);
					this.UpdateSize();
					IL_0090:;
				}
			}
		}
		private CommandBarManager commandBarManager = null;
		private CommandBarItemCollection items = new CommandBarItemCollection();
		private CommandBarStyle style = CommandBarStyle.ToolBar;
		private CommandBarContextMenu contextMenu = new CommandBarContextMenu();
		private IntPtr hookHandle = IntPtr.Zero;
		private Point lastMousePosition = new Point(0, 0);
		private int trackHotItem = -1;
		private int trackNextItem = -1;
		private bool trackEscapePressed = false;
		private CommandBar.State state = CommandBar.State.None;
		private CommandBar.State lastState = CommandBar.State.None;
		private ImageList imageList;
		private enum State
		{
			None,
			Hot,
			HotTracking
		}
	}
}
