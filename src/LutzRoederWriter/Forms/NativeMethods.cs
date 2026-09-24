using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Writer.Forms
{
	internal sealed class NativeMethods
	{
		private NativeMethods()
		{
		}
		[DllImport("comctl32.dll")]
		public static extern bool InitCommonControlsEx(NativeMethods.INITCOMMONCONTROLSEX icc);
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetParent(IntPtr hWnd);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref NativeMethods.RECT lParam);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref NativeMethods.POINT lParam);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref NativeMethods.TBBUTTON lParam);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref NativeMethods.TBBUTTONINFO lParam);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref NativeMethods.REBARBANDINFO lParam);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetCurrentThreadId();
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SetWindowsHookEx(int hookid, NativeMethods.HookProc pfnhook, IntPtr hinst, int threadid);
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool UnhookWindowsHookEx(IntPtr hhook);
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr CallNextHookEx(IntPtr hhook, int code, IntPtr wparam, IntPtr lparam);
		[DllImport("comctl32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImageList_DrawIndirect(ref NativeMethods.IMAGELISTDRAWPARAMS pimldp);
		[DllImport("comctl32.dll")]
		public static extern int DllGetVersion(ref NativeMethods.DLLVERSIONINFO dvi);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SystemParametersInfo(int nAction, int nParam, ref int value, int ignore);
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool MessageBeep(int type);
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);
		public static void DrawImage(Graphics graphics, Image image, Point point, bool disabled)
		{
			if (!disabled)
			{
				Rectangle rectangle = new Rectangle(point, image.Size);
				graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
			}
			else
			{
				NativeMethods.DLLVERSIONINFO dllversioninfo = default(NativeMethods.DLLVERSIONINFO);
				dllversioninfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.DLLVERSIONINFO));
				NativeMethods.DllGetVersion(ref dllversioninfo);
				if (dllversioninfo.dwMajorVersion < 6)
				{
					ImageAttributes imageAttributes = new ImageAttributes();
					Rectangle rectangle = new Rectangle(point, image.Size);
					imageAttributes.SetColorMatrix(new ColorMatrix(new float[][]
					{
						new float[] { 0.2222f, 0.2222f, 0.2222f, 0f, 0f },
						new float[] { 0.2222f, 0.2222f, 0.2222f, 0f, 0f },
						new float[] { 0.2222f, 0.2222f, 0.2222f, 0f, 0f },
						new float[] { 0.3333f, 0.3333f, 0.3333f, 0.75f, 0f },
						new float[] { 0f, 0f, 0f, 0f, 1f }
					}));
					graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
				}
				else
				{
					ImageList imageList = new ImageList();
					imageList.ImageSize = image.Size;
					imageList.ColorDepth = ColorDepth.Depth32Bit;
					imageList.Images.Add(image);
					IntPtr hdc = graphics.GetHdc();
					NativeMethods.IMAGELISTDRAWPARAMS imagelistdrawparams = default(NativeMethods.IMAGELISTDRAWPARAMS);
					imagelistdrawparams.cbSize = Marshal.SizeOf(typeof(NativeMethods.IMAGELISTDRAWPARAMS));
					imagelistdrawparams.himl = imageList.Handle;
					imagelistdrawparams.i = 0;
					imagelistdrawparams.hdcDst = hdc;
					imagelistdrawparams.x = point.X;
					imagelistdrawparams.y = point.Y;
					imagelistdrawparams.cx = 0;
					imagelistdrawparams.cy = 0;
					imagelistdrawparams.xBitmap = 0;
					imagelistdrawparams.yBitmap = 0;
					imagelistdrawparams.fStyle = 1;
					imagelistdrawparams.fState = 4;
					imagelistdrawparams.Frame = -100;
					NativeMethods.ImageList_DrawIndirect(ref imagelistdrawparams);
					graphics.ReleaseHdc(hdc);
				}
			}
		}
		public const string TOOLBARCLASSNAME = "ToolbarWindow32";
		public const int WS_CHILD = 1073741824;
		public const int WS_VISIBLE = 268435456;
		public const int WS_CLIPCHILDREN = 33554432;
		public const int WS_CLIPSIBLINGS = 67108864;
		public const int WS_BORDER = 8388608;
		public const int CCS_NODIVIDER = 64;
		public const int CCS_NORESIZE = 4;
		public const int CCS_NOPARENTALIGN = 8;
		public const int I_IMAGECALLBACK = -1;
		public const int I_IMAGENONE = -2;
		public const int TBSTYLE_TOOLTIPS = 256;
		public const int TBSTYLE_FLAT = 2048;
		public const int TBSTYLE_LIST = 4096;
		public const int TBSTYLE_TRANSPARENT = 32768;
		public const int TBSTYLE_EX_DRAWDDARROWS = 1;
		public const int TBSTYLE_EX_HIDECLIPPEDBUTTONS = 16;
		public const int TBSTYLE_EX_DOUBLEBUFFER = 128;
		public const int CDRF_DODEFAULT = 0;
		public const int CDRF_SKIPDEFAULT = 4;
		public const int CDRF_NOTIFYITEMDRAW = 32;
		public const int CDDS_PREPAINT = 1;
		public const int CDDS_ITEM = 65536;
		public const int CDDS_ITEMPREPAINT = 65537;
		public const int CDIS_HOT = 64;
		public const int CDIS_SELECTED = 1;
		public const int CDIS_DISABLED = 4;
		public const int WM_SETREDRAW = 11;
		public const int WM_CANCELMODE = 31;
		public const int WM_NOTIFY = 78;
		public const int WM_KEYDOWN = 256;
		public const int WM_KEYUP = 257;
		public const int WM_CHAR = 258;
		public const int WM_SYSKEYDOWN = 260;
		public const int WM_SYSKEYUP = 261;
		public const int WM_COMMAND = 273;
		public const int WM_MENUCHAR = 288;
		public const int WM_MOUSEMOVE = 512;
		public const int WM_LBUTTONDOWN = 513;
		public const int WM_MOUSELAST = 522;
		public const int WM_USER = 1024;
		public const int WM_REFLECT = 8192;
		public const int NM_CUSTOMDRAW = -12;
		public const int TTN_NEEDTEXTA = -520;
		public const int TTN_NEEDTEXTW = -530;
		public const int TBN_QUERYINSERT = -706;
		public const int TBN_DROPDOWN = -710;
		public const int TBN_HOTITEMCHANGE = -713;
		public const int TBIF_IMAGE = 1;
		public const int TBIF_TEXT = 2;
		public const int TBIF_STATE = 4;
		public const int TBIF_STYLE = 8;
		public const int TBIF_COMMAND = 32;
		public const int TBIF_SIZE = 64;
		public const int MNC_EXECUTE = 2;
		public const int ICC_BAR_CLASSES = 4;
		public const int ICC_COOL_CLASSES = 1024;
		public const int TTF_RTLREADING = 4;
		public const int TB_PRESSBUTTON = 1027;
		public const int TB_INSERTBUTTON = 1045;
		public const int TB_DELETEBUTTON = 1046;
		public const int TB_BUTTONCOUNT = 1048;
		public const int TB_GETITEMRECT = 1053;
		public const int TB_BUTTONSTRUCTSIZE = 1054;
		public const int TB_SETBUTTONSIZE = 1056;
		public const int TB_SETIMAGELIST = 1072;
		public const int TB_GETRECT = 1075;
		public const int TB_SETBUTTONINFO = 1088;
		public const int TB_HITTEST = 1093;
		public const int TB_GETHOTITEM = 1095;
		public const int TB_SETHOTITEM = 1096;
		public const int TB_SETEXTENDEDSTYLE = 1108;
		public const int TBSTATE_CHECKED = 1;
		public const int TBSTATE_ENABLED = 4;
		public const int TBSTATE_HIDDEN = 8;
		public const int BTNS_BUTTON = 0;
		public const int BTNS_SEP = 1;
		public const int BTNS_DROPDOWN = 8;
		public const int BTNS_AUTOSIZE = 16;
		public const int BTNS_WHOLEDROPDOWN = 128;
		public const int WH_MSGFILTER = -1;
		public const int MSGF_MENU = 2;
		public const string REBARCLASSNAME = "ReBarWindow32";
		public const int RBS_VARHEIGHT = 512;
		public const int RBS_BANDBORDERS = 1024;
		public const int RBS_AUTOSIZE = 8192;
		public const int RBN_FIRST = -831;
		public const int RBN_HEIGHTCHANGE = -831;
		public const int RBN_AUTOSIZE = -834;
		public const int RBN_CHEVRONPUSHED = -841;
		public const int RB_GETBANDINFO = 1029;
		public const int RB_SETBANDINFO = 1030;
		public const int RB_GETRECT = 1033;
		public const int RB_INSERTBAND = 1034;
		public const int RB_GETBARHEIGHT = 1051;
		public const int RBBIM_CHILD = 16;
		public const int RBBIM_CHILDSIZE = 32;
		public const int RBBIM_STYLE = 1;
		public const int RBBIM_ID = 256;
		public const int RBBIM_SIZE = 64;
		public const int RBBIM_IDEALSIZE = 512;
		public const int RBBIM_TEXT = 4;
		public const int RBBS_BREAK = 1;
		public const int RBBS_CHILDEDGE = 4;
		public const int RBBS_FIXEDBMP = 32;
		public const int RBBS_GRIPPERALWAYS = 128;
		public const int RBBS_USECHEVRON = 512;
		public const int ILD_TRANSPARENT = 1;
		public const int ILS_SATURATE = 4;
		public const int SPI_GETFLATMENU = 4130;
		public const int HWND_NOTOPMOST = -2;
		public const int SW_SHOWNORMAL = 1;
		public const int SWP_NOACTIVATE = 16;
		public const int SWP_NOMOVE = 2;
		public const int SWP_NOSIZE = 1;
		public const int SWP_NOZORDER = 4;
		public const int SWP_SHOWWINDOW = 64;
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal class INITCOMMONCONTROLSEX
		{
			public int Size = 8;
			public int Flags;
		}
		internal struct POINT
		{
			public int x;
			public int y;
		}
		internal struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}
		internal struct NMHDR
		{
			public IntPtr hwndFrom;
			public IntPtr idFrom;
			public int code;
		}
		internal struct NMTOOLBAR
		{
			public NativeMethods.NMHDR hdr;
			public int iItem;
			public NativeMethods.TBBUTTON tbButton;
			public int cchText;
			public IntPtr pszText;
		}
		internal struct NMCUSTOMDRAW
		{
			public NativeMethods.NMHDR hdr;
			public int dwDrawStage;
			public IntPtr hdc;
			public NativeMethods.RECT rc;
			public IntPtr dwItemSpec;
			public int uItemState;
			public IntPtr lItemlParam;
		}
		internal struct LPNMTBCUSTOMDRAW
		{
			public NativeMethods.NMCUSTOMDRAW nmcd;
			public IntPtr hbrMonoDither;
			public IntPtr hbrLines;
			public IntPtr hpenLines;
			public int clrText;
			public int clrMark;
			public int clrTextHighlight;
			public int clrBtnFace;
			public int clrBtnHighlight;
			public int clrHighlightHotTrack;
			public NativeMethods.RECT rcText;
			public int nStringBkMode;
			public int nHLStringBkMode;
		}
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct TOOLTIPTEXT
		{
			public NativeMethods.NMHDR hdr;
			public IntPtr lpszText;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szText;
			public IntPtr hinst;
			public int uFlags;
		}
		internal struct TOOLTIPTEXTA
		{
			public NativeMethods.NMHDR hdr;
			public IntPtr lpszText;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szText;
			public IntPtr hinst;
			public int uFlags;
		}
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct TBBUTTON
		{
			public int iBitmap;
			public int idCommand;
			public byte fsState;
			public byte fsStyle;
			public byte bReserved0;
			public byte bReserved1;
			public IntPtr dwData;
			public IntPtr iString;
		}
		internal struct TBBUTTONINFO
		{
			public int cbSize;
			public int dwMask;
			public int idCommand;
			public int iImage;
			public byte fsState;
			public byte fsStyle;
			public short cx;
			public IntPtr lParam;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpszText;
			public int cchText;
		}
		internal delegate IntPtr HookProc(int code, IntPtr param1, IntPtr param2);
		internal struct MSG
		{
			public IntPtr hwnd;
			public int message;
			public IntPtr wParam;
			public IntPtr lParam;
			public int time;
			public int pt_x;
			public int pt_y;
		}
		internal struct REBARBANDINFO
		{
			public int cbSize;
			public int fMask;
			public int fStyle;
			public int clrFore;
			public int clrBack;
			public IntPtr lpText;
			public int cch;
			public int iImage;
			public IntPtr hwndChild;
			public int cxMinChild;
			public int cyMinChild;
			public int cx;
			public IntPtr hbmBack;
			public int wID;
			public int cyChild;
			public int cyMaxChild;
			public int cyIntegral;
			public int cxIdeal;
			public int lParam;
			public int cxHeader;
		}
		internal struct NMREBARCHEVRON
		{
			public NativeMethods.NMHDR hdr;
			public int uBand;
			public int wID;
			public int lParam;
			public NativeMethods.RECT rc;
			public int lParamNM;
		}
		internal struct IMAGELISTDRAWPARAMS
		{
			public int cbSize;
			public IntPtr himl;
			public int i;
			public IntPtr hdcDst;
			public int x;
			public int y;
			public int cx;
			public int cy;
			public int xBitmap;
			public int yBitmap;
			public int rgbBk;
			public int rgbFg;
			public int fStyle;
			public int dwRop;
			public int fState;
			public int Frame;
			public int crEffect;
		}
		internal struct DLLVERSIONINFO
		{
			public int cbSize;
			public int dwMajorVersion;
			public int dwMinorVersion;
			public int dwBuildNumber;
			public int dwPlatformID;
		}
	}
}
