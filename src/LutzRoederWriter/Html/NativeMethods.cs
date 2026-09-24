using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Writer.Html
{
	[SuppressUnmanagedCodeSecurity]
	public sealed class NativeMethods
	{
		private NativeMethods()
		{
		}
		[DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int CreateURLMoniker(NativeMethods.IMoniker pmkContext, string szURL, out NativeMethods.IMoniker ppmk);
		[DllImport("ole32.dll", PreserveSig = false)]
		internal static extern void CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, out NativeMethods.IStream pStream);
		[DllImport("ole32.dll", PreserveSig = false)]
		internal static extern void GetHGlobalFromStream(NativeMethods.IStream pStream, out IntPtr pHGlobal);
		[DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern int CreateBindCtx(int dwReserved, out NativeMethods.IBindCtx ppbc);
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern bool GetClientRect(IntPtr hWnd, [In] [Out] NativeMethods.COMRECT rect);
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern IntPtr SetFocus(IntPtr hWnd);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern IntPtr GlobalLock(IntPtr handle);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern bool GlobalUnlock(IntPtr handle);
		public const int WM_MOUSEMOVE = 512;
		public const int WM_LBUTTONDOWN = 513;
		public const int BEHAVIOR_EVENT_APPLYSTYLE = 2;
		public const int BEHAVIOR_EVENT_CONTENTREADY = 0;
		public const int BEHAVIOR_EVENT_CONTENTSAVE = 4;
		public const int BEHAVIOR_EVENT_DOCUMENTCONTEXTCHANGE = 3;
		public const int BEHAVIOR_EVENT_DOCUMENTREADY = 1;
		public const int DISPID_HTMLELEMENTEVENTS_ONDBLCLICK = -601;
		public const int DISPID_HTMLELEMENTEVENTS_ONDRAGSTART = -2147418101;
		public const int DISPID_HTMLELEMENTEVENTS_ONDRAG = -2147418092;
		public const int DISPID_HTMLELEMENTEVENTS_ONMOUSEDOWN = -605;
		public const int DISPID_HTMLELEMENTEVENTS_ONMOUSEUP = -607;
		public const int DISPID_HTMLELEMENTEVENTS_ONMOUSEMOVE = -606;
		public const int DISPID_HTMLELEMENTEVENTS_ONMOVE = 1035;
		public const int DISPID_HTMLELEMENTEVENTS_ONMOVESTART = 1038;
		public const int DISPID_HTMLELEMENTEVENTS_ONMOVEEND = 1039;
		public const int DISPID_HTMLELEMENTEVENTS_ONRESIZESTART = 1040;
		public const int DISPID_HTMLELEMENTEVENTS_ONRESIZEEND = 1041;
		public const int DISPID_READYSTATE = -525;
		public const int DISPID_XOBJ_MIN = -2147418112;
		public const int DISPID_XOBJ_MAX = -2147352577;
		public const int DISPID_XOBJ_BASE = -2147418112;
		public const int DOCHOSTUIDBLCLICK_DEFAULT = 0;
		public const int DOCHOSTUIDBLCLICK_SHOWCODE = 2;
		public const int DOCHOSTUIDBLCLICK_SHOWPROPERTIES = 1;
		public const int DOCHOSTUIFLAG_ACTIVATE_CLIENTHIT_ONLY = 512;
		public const int DOCHOSTUIFLAG_DIALOG = 1;
		public const int DOCHOSTUIFLAG_DISABLE_COOKIE = 1024;
		public const int DOCHOSTUIFLAG_DISABLE_HELP_MENU = 2;
		public const int DOCHOSTUIFLAG_DISABLE_OFFSCREEN = 64;
		public const int DOCHOSTUIFLAG_DISABLE_SCRIPT_INACTIVE = 16;
		public const int DOCHOSTUIFLAG_DIV_BLOCKDEFAULT = 256;
		public const int DOCHOSTUIFLAG_FLAT_SCROLLBAR = 128;
		public const int DOCHOSTUIFLAG_NO3DBORDER = 4;
		public const int DOCHOSTUIFLAG_OPENNEWWIN = 32;
		public const int DOCHOSTUIFLAG_SCROLL_NO = 8;
		public const int DOCHOSTUIFLAG_ENABLE_INPLACE_NAVIGATION = 65536;
		public const int DROPEFFECT_NONE = 0;
		public const int DROPEFFECT_COPY = 1;
		public const int DROPEFFECT_MOVE = 2;
		public const int DROPEFFECT_LINK = 4;
		public const int E_ABORT = -2147467260;
		public const int E_ACCESSDENIED = -2147024891;
		public const int E_FAIL = -2147467259;
		public const int E_HANDLE = -2147024890;
		public const int E_INVALIDARG = -2147024809;
		public const int E_POINTER = -2147467261;
		public const int E_NOTIMPL = -2147467263;
		public const int E_NOINTERFACE = -2147467262;
		public const int E_OUTOFMEMORY = -2147024882;
		public const int E_UNEXPECTED = -2147418113;
		public const int ELEMENTDESCRIPTOR_FLAGS_LITERAL = 1;
		public const int ELEMENTDESCRIPTOR_FLAGS_NESTED_LITERAL = 2;
		public const int ELEMENTNAMESPACE_FLAGS_ALLOWANYTAG = 1;
		public const int ELEMENTNAMESPACE_FLAGS_QUERYFORUNKNOWNTAGS = 2;
		public const int ELEMENT_CORNER_BOTTOM = 3;
		public const int ELEMENT_CORNER_BOTTOMLEFT = 7;
		public const int ELEMENT_CORNER_BOTTOMRIGHT = 8;
		public const int ELEMENT_CORNER_LEFT = 2;
		public const int ELEMENT_CORNER_NONE = 0;
		public const int ELEMENT_CORNER_RIGHT = 4;
		public const int ELEMENT_CORNER_TOP = 1;
		public const int ELEMENT_CORNER_TOPLEFT = 5;
		public const int ELEMENT_CORNER_TOPRIGHT = 6;
		public const int HTMLPAINTER_3DSURFACE = 512;
		public const int HTMLPAINTER_ALPHA = 4;
		public const int HTMLPAINTER_COMPLEX = 8;
		public const int HTMLPAINTER_OPAQUE = 1;
		public const int HTMLPAINTER_OVERLAY = 16;
		public const int HTMLPAINTER_HITTEST = 32;
		public const int HTMLPAINTER_NOBAND = 1024;
		public const int HTMLPAINTER_NODC = 4096;
		public const int HTMLPAINTER_NOPHYSICALCLIP = 8192;
		public const int HTMLPAINTER_NOSAVEDC = 16384;
		public const int HTMLPAINTER_SURFACE = 256;
		public const int HTMLPAINTER_TRANSPARENT = 2;
		public const int HTMLPAINT_ZORDER_ABOVE_CONTENT = 7;
		public const int HTMLPAINT_ZORDER_ABOVE_FLOW = 6;
		public const int HTMLPAINT_ZORDER_BELOW_CONTENT = 4;
		public const int HTMLPAINT_ZORDER_BELOW_FLOW = 5;
		public const int HTMLPAINT_ZORDER_NONE = 0;
		public const int HTMLPAINT_ZORDER_REPLACE_ALL = 1;
		public const int HTMLPAINT_ZORDER_REPLACE_CONTENT = 2;
		public const int HTMLPAINT_ZORDER_REPLACE_BACKGROUND = 3;
		public const int HTMLPAINT_ZORDER_WINDOW_TOP = 8;
		public const int IDM_COPY = 15;
		public const int IDM_CUT = 16;
		public const int IDM_DELETE = 17;
		public const int IDM_FONTNAME = 18;
		public const int IDM_FONTSIZE = 19;
		public const int IDM_PASTE = 26;
		public const int IDM_PRINT = 27;
		public const int IDM_REDO = 29;
		public const int IDM_SELECTALL = 31;
		public const int IDM_UNDO = 43;
		public const int IDM_BACKCOLOR = 51;
		public const int IDM_BOLD = 52;
		public const int IDM_ITALIC = 56;
		public const int IDM_JUSTIFYCENTER = 57;
		public const int IDM_JUSTIFYLEFT = 59;
		public const int IDM_JUSTIFYRIGHT = 60;
		public const int IDM_UNDERLINE = 63;
		public const int IDM_STRIKETHROUGH = 91;
		public const int IDM_PRINTPREVIEW = 2003;
		public const int IDM_1D_ELEMENT = 2396;
		public const int IDM_2D_ELEMENT = 2395;
		public const int IDM_2D_POSITION = 2394;
		public const int IDM_ABSOLUTE_POSITION = 2397;
		public const int IDM_ADDTOGLYPHTABLE = 2337;
		public const int IDM_ATOMICSELECTION = 2399;
		public const int IDM_BLOCKFMT = 2234;
		public const int IDM_CHECKBOX = 2163;
		public const int IDM_BUTTON = 2167;
		public const int IDM_CLEARSELECTION = 2007;
		public const int IDM_CSSEDITING_LEVEL = 2406;
		public const int IDM_DROPDOWNBOX = 2165;
		public const int IDM_EMPTYGLYPHTABLE = 2336;
		public const int IDM_FORECOLOR = 55;
		public const int IDM_HYPERLINK = 2124;
		public const int IDM_IMAGE = 2168;
		public const int IDM_INDENT = 2186;
		public const int IDM_LISTBOX = 2166;
		public const int IDM_LIVERESIZE = 2398;
		public const int IDM_MULTIPLESELECTION = 2393;
		public const int IDM_NOACTIVATEDESIGNTIMECONTROLS = 2333;
		public const int IDM_NOACTIVATEJAVAAPPLETS = 2334;
		public const int IDM_NOACTIVATENORMALOLECONTROLS = 2332;
		public const int IDM_NOFIXUPURLSONPASTE = 2335;
		public const int IDM_ORDERLIST = 2184;
		public const int IDM_OUTDENT = 2187;
		public const int IDM_PERSISTDEFAULTVALUES = 7100;
		public const int IDM_PRESERVEUNDOALWAYS = 6049;
		public const int IDM_PROTECTMETATAGS = 7101;
		public const int IDM_RADIOBUTTON = 2164;
		public const int IDM_REMOVEFROMGLYPHTABLE = 2338;
		public const int IDM_REPLACEGLYPHCONTENTS = 2339;
		public const int IDM_RESPECTVISIBILITY_INDESIGN = 2405;
		public const int IDM_SETDIRTY = 2342;
		public const int IDM_SHOWZEROBORDERATDESIGNTIME = 2328;
		public const int IDM_SUBSCRIPT = 2247;
		public const int IDM_SUPERSCRIPT = 2248;
		public const int IDM_TEXTBOX = 2161;
		public const int IDM_TEXTAREA = 2162;
		public const int IDM_UNLINK = 2125;
		public const int IDM_UNORDERLIST = 2185;
		public const int INET_E_DEFAULT_ACTION = -2146697199;
		public const int INET_E_USE_DEFAULT_PROTOCOLHANDLER = -2146697199;
		public const int OLEIVERB_DISCARDUNDOSTATE = -6;
		public const int OLEIVERB_HIDE = -3;
		public const int OLEIVERB_INPLACEACTIVATE = -5;
		public const int OLECLOSE_NOSAVE = 1;
		public const int OLEIVERB_OPEN = -2;
		public const int OLEIVERB_PRIMARY = 0;
		public const int OLEIVERB_PROPERTIES = -7;
		public const int OLEIVERB_SHOW = -1;
		public const int OLEIVERB_UIACTIVATE = -4;
		public const int S_FALSE = 1;
		public const int S_OK = 0;
		public const int BITMAPINFO_MAX_COLORSIZE = 256;
		public const int WM_KEYFIRST = 256;
		public const int WM_KEYLAST = 264;
		public const int WM_KEYDOWN = 256;
		public const int WM_KEYUP = 257;
		public static Guid ElementBehaviorFactory = new Guid("3050f429-98b5-11cf-bb82-00aa00bdce0b");
		public static Guid Guid_MSHTML = new Guid("DE4BA900-59CA-11CF-9592-444553540000");
		public static Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");
		public static IntPtr NullIntPtr = (IntPtr)0;
		[Flags]
		public enum BSCFlags
		{
			BSCF_FIRSTDATANOTIFICATION = 1,
			BSCF_INTERMEDIATEDATANOTIFICATION = 2,
			BSCF_LASTDATANOTIFICATION = 4,
			BSCF_DATAFULLYAVAILABLE = 8,
			BSCF_AVAILABLEDATASIZEUNKNOWN = 16
		}
		[ComVisible(false)]
		internal sealed class OLECMDEXECOPT
		{
			public const int OLECMDEXECOPT_DODEFAULT = 0;
			public const int OLECMDEXECOPT_PROMPTUSER = 1;
			public const int OLECMDEXECOPT_DONTPROMPTUSER = 2;
			public const int OLECMDEXECOPT_SHOWHELP = 3;
		}
		[ComVisible(false)]
		internal sealed class OLECMDF
		{
			public const int OLECMDF_SUPPORTED = 1;
			public const int OLECMDF_ENABLED = 2;
			public const int OLECMDF_LATCHED = 4;
			public const int OLECMDF_NINCHED = 8;
		}
		[ComVisible(false)]
		internal sealed class StreamConsts
		{
			public const int LOCK_WRITE = 1;
			public const int LOCK_EXCLUSIVE = 2;
			public const int LOCK_ONLYONCE = 4;
			public const int STATFLAG_DEFAULT = 0;
			public const int STATFLAG_NONAME = 1;
			public const int STATFLAG_NOOPEN = 2;
			public const int STGC_DEFAULT = 0;
			public const int STGC_OVERWRITE = 1;
			public const int STGC_ONLYIFCURRENT = 2;
			public const int STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 4;
			public const int STREAM_SEEK_SET = 0;
			public const int STREAM_SEEK_CUR = 1;
			public const int STREAM_SEEK_END = 2;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagLOGPALETTE
		{
			[MarshalAs(UnmanagedType.U2)]
			public short palVersion;
			[MarshalAs(UnmanagedType.U2)]
			public short palNumEntries;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagOIFI
		{
			[MarshalAs(UnmanagedType.U4)]
			public int cb;
			[MarshalAs(UnmanagedType.I4)]
			public int fMDIApp;
			public IntPtr hwndFrame;
			public IntPtr hAccel;
			[MarshalAs(UnmanagedType.U4)]
			public int cAccelEntries;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagOLECMD
		{
			[MarshalAs(UnmanagedType.U4)]
			public int cmdID;
			[MarshalAs(UnmanagedType.U4)]
			public int cmdf;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagOleMenuGroupWidths
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public int[] widths = new int[6];
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagOLEVERB
		{
			[MarshalAs(UnmanagedType.I4)]
			public int lVerb;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string lpszVerbName;
			[MarshalAs(UnmanagedType.U4)]
			public int fuFlags;
			[MarshalAs(UnmanagedType.U4)]
			public int grfAttribs;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public class COMMSG
		{
			public IntPtr hwnd;
			public int message;
			public IntPtr wParam;
			public IntPtr lParam;
			public int time;
			public int pt_x;
			public int pt_y;
		}
		[ComVisible(true)]
		[StructLayout(LayoutKind.Sequential)]
		public class COMRECT
		{
			public COMRECT()
			{
			}
			public COMRECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}
			public static NativeMethods.COMRECT FromXYWH(int x, int y, int width, int height)
			{
				return new NativeMethods.COMRECT(x, y, x + width, y + height);
			}
			public int left;
			public int top;
			public int right;
			public int bottom;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class DISPPARAMS
		{
			public IntPtr rgvarg;
			public IntPtr rgdispidNamedArgs;
			[MarshalAs(UnmanagedType.U4)]
			public int cArgs;
			[MarshalAs(UnmanagedType.U4)]
			public int cNamedArgs;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class EXCEPINFO
		{
			[MarshalAs(UnmanagedType.U2)]
			public short wCode;
			[MarshalAs(UnmanagedType.U2)]
			public short wReserved;
			[MarshalAs(UnmanagedType.BStr)]
			public string bstrSource;
			[MarshalAs(UnmanagedType.BStr)]
			public string bstrDescription;
			[MarshalAs(UnmanagedType.BStr)]
			public string bstrHelpFile;
			[MarshalAs(UnmanagedType.U4)]
			public int dwHelpContext;
			public IntPtr dwReserved;
			public IntPtr dwFillIn;
			[MarshalAs(UnmanagedType.I4)]
			public int scode;
		}
		[ComVisible(true)]
		[StructLayout(LayoutKind.Sequential)]
		public class DOCHOSTUIINFO
		{
			[MarshalAs(UnmanagedType.U4)]
			public int cbSize;
			[MarshalAs(UnmanagedType.I4)]
			public int dwFlags;
			[MarshalAs(UnmanagedType.I4)]
			public int dwDoubleClick;
			[MarshalAs(UnmanagedType.I4)]
			public int dwReserved1;
			[MarshalAs(UnmanagedType.I4)]
			public int dwReserved2;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class FORMATETC
		{
			[MarshalAs(UnmanagedType.I4)]
			public int cfFormat;
			public IntPtr ptd;
			[MarshalAs(UnmanagedType.I4)]
			public int dwAspect;
			[MarshalAs(UnmanagedType.I4)]
			public int lindex;
			[MarshalAs(UnmanagedType.I4)]
			public int tymed;
		}
		[ComVisible(true)]
		[StructLayout(LayoutKind.Sequential)]
		public class HTML_PAINTER_INFO
		{
			[MarshalAs(UnmanagedType.I4)]
			public int lFlags;
			[MarshalAs(UnmanagedType.I4)]
			public int lZOrder;
			[MarshalAs(UnmanagedType.Struct)]
			public Guid iidDrawObject;
			[MarshalAs(UnmanagedType.Struct)]
			public NativeMethods.RECT rcBounds;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public class NMHDR
		{
			public IntPtr hwndFrom;
			public int idFrom;
			public int code;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public class NMCUSTOMDRAW
		{
			public NativeMethods.NMHDR nmcd;
			public int dwDrawStage;
			public IntPtr hdc;
			public NativeMethods.RECT rc;
			public int dwItemSpec;
			public int uItemState;
			public IntPtr lItemlParam;
		}
		[ComVisible(true)]
		[StructLayout(LayoutKind.Sequential)]
		public class POINT
		{
			public POINT()
			{
			}
			public POINT(int x, int y)
			{
				this.x = x;
				this.y = y;
			}
			public int x;
			public int y;
		}
		[ComVisible(true)]
		public struct RECT
		{
			public RECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}
			public static NativeMethods.RECT FromXYWH(int x, int y, int width, int height)
			{
				return new NativeMethods.RECT(x, y, x + width, y + height);
			}
			public int left;
			public int top;
			public int right;
			public int bottom;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class STATDATA
		{
			[MarshalAs(UnmanagedType.U4)]
			public int advf;
			[MarshalAs(UnmanagedType.U4)]
			public int dwConnection;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public class STGMEDIUM
		{
			[MarshalAs(UnmanagedType.I4)]
			public int tymed;
			public IntPtr unionmember;
			public IntPtr pUnkForRelease;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public class STATSTG
		{
			[MarshalAs(UnmanagedType.LPTStr)]
			public string pwcsName;
			[MarshalAs(UnmanagedType.I4)]
			public int type;
			[MarshalAs(UnmanagedType.I8)]
			public long cbSize;
			[MarshalAs(UnmanagedType.I8)]
			public long mtime;
			[MarshalAs(UnmanagedType.I8)]
			public long ctime;
			[MarshalAs(UnmanagedType.I8)]
			public long atime;
			[MarshalAs(UnmanagedType.I8)]
			public long grfMode;
			[MarshalAs(UnmanagedType.I8)]
			public long grfLocksSupported;
			[MarshalAs(UnmanagedType.I4)]
			public int clsid_data1;
			[MarshalAs(UnmanagedType.I2)]
			public short clsid_data2;
			[MarshalAs(UnmanagedType.I2)]
			public short clsid_data3;
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b0;
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b1;
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b2;
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b3;
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b4;
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b5;
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b6;
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b7;
			[MarshalAs(UnmanagedType.I8)]
			public long grfStateBits;
			[MarshalAs(UnmanagedType.I8)]
			public long reserved;
		}
		[ComVisible(false)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagSIZE
		{
			[MarshalAs(UnmanagedType.I4)]
			public int cx;
			[MarshalAs(UnmanagedType.I4)]
			public int cy;
		}
		[ComVisible(true)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagSIZEL
		{
			[MarshalAs(UnmanagedType.I4)]
			public int cx;
			[MarshalAs(UnmanagedType.I4)]
			public int cy;
		}
		[ComVisible(false)]
		internal class ConnectionPointCookie
		{
			public ConnectionPointCookie(object source, object sink, Type eventInterface)
				: this(source, sink, eventInterface, true)
			{
			}
			public ConnectionPointCookie(object source, object sink, Type eventInterface, bool throwException)
			{
				Exception ex = null;
				if (source is NativeMethods.IConnectionPointContainer)
				{
					NativeMethods.IConnectionPointContainer connectionPointContainer = (NativeMethods.IConnectionPointContainer)source;
					try
					{
						Guid guid = eventInterface.GUID;
						connectionPointContainer.FindConnectionPoint(ref guid, out this.connectionPoint);
					}
					catch (Exception)
					{
						this.connectionPoint = null;
					}
					if (this.connectionPoint == null)
					{
						ex = new ArgumentException("The source object does not expose the " + eventInterface.Name + " event inteface");
					}
					else if (!eventInterface.IsInstanceOfType(sink))
					{
						ex = new InvalidCastException("The sink object does not implement the eventInterface");
					}
					else
					{
						try
						{
							this.connectionPoint.Advise(sink, out this.cookie);
						}
						catch
						{
							this.cookie = 0;
							this.connectionPoint = null;
							ex = new Exception("IConnectionPoint::Advise failed for event interface '" + eventInterface.Name + "'");
						}
					}
				}
				else
				{
					ex = new InvalidCastException("The source object does not expost IConnectionPointContainer");
				}
				if (!throwException || (this.connectionPoint != null && this.cookie != 0))
				{
					return;
				}
				if (ex == null)
				{
					throw new ArgumentException("Could not create connection point for event interface '" + eventInterface.Name + "'");
				}
				throw ex;
			}
			public void Disconnect()
			{
				if (this.connectionPoint != null && this.cookie != 0)
				{
					this.connectionPoint.Unadvise(this.cookie);
					this.cookie = 0;
					this.connectionPoint = null;
				}
			}
			~ConnectionPointCookie()
			{
				this.Disconnect();
			}
			private NativeMethods.IConnectionPoint connectionPoint;
			private int cookie;
		}
		[Guid("0000010F-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		internal interface IAdviseSink
		{
			void OnDataChange([In] NativeMethods.FORMATETC pFormatetc, [In] NativeMethods.STGMEDIUM pStgmed);
			void OnViewChange([MarshalAs(UnmanagedType.U4)] [In] int dwAspect, [MarshalAs(UnmanagedType.I4)] [In] int lindex);
			void OnRename([MarshalAs(UnmanagedType.Interface)] [In] object pmk);
			void OnSave();
			void OnClose();
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0000000e-0000-0000-C000-000000000046")]
		[ComVisible(true)]
		internal interface IBindCtx
		{
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("00000001-0000-0000-C000-000000000046")]
		[ComVisible(true)]
		[ComImport]
		internal interface IClassFactory
		{
			[PreserveSig]
			int CreateInstance([MarshalAs(UnmanagedType.Interface)] [In] object pUnkOuter, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object obj);
			[PreserveSig]
			int LockServer([In] bool fLock);
		}
		[Guid("BD3F23C0-D43E-11CF-893B-00AA00BDCE1A")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[ComImport]
		internal interface IDocHostUIHandler
		{
			[PreserveSig]
			int ShowContextMenu([MarshalAs(UnmanagedType.U4)] [In] int dwID, [In] NativeMethods.POINT pt, [MarshalAs(UnmanagedType.Interface)] [In] object pcmdtReserved, [MarshalAs(UnmanagedType.Interface)] [In] object pdispReserved);
			[PreserveSig]
			int GetHostInfo([In] [Out] NativeMethods.DOCHOSTUIINFO info);
			[PreserveSig]
			int ShowUI([MarshalAs(UnmanagedType.I4)] [In] int dwID, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleInPlaceActiveObject activeObject, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleCommandTarget commandTarget, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleInPlaceFrame frame, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleInPlaceUIWindow doc);
			[PreserveSig]
			int HideUI();
			[PreserveSig]
			int UpdateUI();
			[PreserveSig]
			int EnableModeless([MarshalAs(UnmanagedType.Bool)] [In] bool fEnable);
			[PreserveSig]
			int OnDocWindowActivate([MarshalAs(UnmanagedType.Bool)] [In] bool fActivate);
			[PreserveSig]
			int OnFrameWindowActivate([MarshalAs(UnmanagedType.Bool)] [In] bool fActivate);
			[PreserveSig]
			int ResizeBorder([In] NativeMethods.COMRECT rect, [In] NativeMethods.IOleInPlaceUIWindow doc, [In] bool fFrameWindow);
			[PreserveSig]
			int TranslateAccelerator([In] NativeMethods.COMMSG msg, [In] ref Guid group, [MarshalAs(UnmanagedType.I4)] [In] int nCmdID);
			[PreserveSig]
			int GetOptionKeyPath([MarshalAs(UnmanagedType.LPArray)] [Out] string[] pbstrKey, [MarshalAs(UnmanagedType.U4)] [In] int dw);
			[PreserveSig]
			int GetDropTarget([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleDropTarget pDropTarget, [MarshalAs(UnmanagedType.Interface)] out NativeMethods.IOleDropTarget ppDropTarget);
			[PreserveSig]
			int GetExternal([MarshalAs(UnmanagedType.Interface)] out object ppDispatch);
			[PreserveSig]
			int TranslateUrl([MarshalAs(UnmanagedType.U4)] [In] int dwTranslate, [MarshalAs(UnmanagedType.LPWStr)] [In] string strURLIn, [MarshalAs(UnmanagedType.LPWStr)] out string pstrURLOut);
			[PreserveSig]
			int FilterDataObject([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleDataObject pDO, [MarshalAs(UnmanagedType.Interface)] out NativeMethods.IOleDataObject ppDORet);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("3050F425-98B5-11CF-BB82-00AA00BDCE0B")]
		[ComVisible(true)]
		internal interface IElementBehavior
		{
			void Init([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IElementBehaviorSite pBehaviorSite);
			void Notify([MarshalAs(UnmanagedType.U4)] [In] int dwEvent, [In] IntPtr pVar);
			void Detach();
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[Guid("3050F429-98B5-11CF-BB82-00AA00BDCE0B")]
		internal interface IElementBehaviorFactory
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IElementBehavior FindBehavior([MarshalAs(UnmanagedType.BStr)] [In] string bstrBehavior, [MarshalAs(UnmanagedType.BStr)] [In] string bstrBehaviorUrl, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IElementBehaviorSite pSite);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[Guid("3050F427-98B5-11CF-BB82-00AA00BDCE0B")]
		internal interface IElementBehaviorSite
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetElement();
			void RegisterNotification([MarshalAs(UnmanagedType.I4)] [In] int lEvent);
		}
		[ComVisible(true)]
		[Guid("3050F659-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IElementBehaviorSiteOM2
		{
			[return: MarshalAs(UnmanagedType.I4)]
			int RegisterEvent([MarshalAs(UnmanagedType.BStr)] [In] string pchEvent, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetEventCookie([MarshalAs(UnmanagedType.BStr)] [In] string pchEvent);
			void FireEvent([MarshalAs(UnmanagedType.I4)] [In] int lCookie, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLEventObj pEventObject);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLEventObj CreateEventObject();
			void RegisterName([MarshalAs(UnmanagedType.BStr)] [In] string pchName);
			void RegisterUrn([MarshalAs(UnmanagedType.BStr)] [In] string pchUrn);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementDefaults GetDefaults();
		}
		[ComVisible(true)]
		[Guid("3050F671-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IElementNamespace
		{
			void AddTag([MarshalAs(UnmanagedType.BStr)] [In] string tagName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
		}
		[ComVisible(true)]
		[Guid("3050F672-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IElementNamespaceFactory
		{
			void Create([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IElementNamespace pNamespace);
		}
		[Guid("3050F7FD-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		internal interface IElementNamespaceFactoryCallback
		{
			void Resolve([MarshalAs(UnmanagedType.BStr)] [In] string nameSpace, [MarshalAs(UnmanagedType.BStr)] [In] string tagName, [MarshalAs(UnmanagedType.BStr)] [In] string attributes, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IElementNamespace pNamespace);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("3050F670-98B5-11CF-BB82-00AA00BDCE0B")]
		[ComVisible(true)]
		internal interface IElementNamespaceTable
		{
			void AddNamespace([MarshalAs(UnmanagedType.BStr)] [In] string nameSpace, [MarshalAs(UnmanagedType.BStr)] [In] string urn, [MarshalAs(UnmanagedType.I4)] [In] int lFlags, [In] ref object factory);
		}
		[Guid("00000103-0000-0000-C000-000000000046")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IEnumFORMATETC
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Next([MarshalAs(UnmanagedType.U4)] [In] int celt, [Out] NativeMethods.FORMATETC rgelt, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] int[] pceltFetched);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Skip([MarshalAs(UnmanagedType.U4)] [In] int celt);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Reset();
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Clone([MarshalAs(UnmanagedType.LPArray)] [Out] NativeMethods.IEnumFORMATETC[] ppenum);
		}
		[ComVisible(true)]
		[Guid("B3E7C340-EF97-11CE-9BC9-00AA00608E01")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IEnumOleUndoUnits
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Next([MarshalAs(UnmanagedType.U4)] [In] int numDesired, out IntPtr unit, [MarshalAs(UnmanagedType.U4)] out int numReceived);
			void Bogus();
			[PreserveSig]
			int Skip([MarshalAs(UnmanagedType.I4)] [In] int numToSkip);
			[PreserveSig]
			int Reset();
			[PreserveSig]
			int Clone([MarshalAs(UnmanagedType.Interface)] [Out] NativeMethods.IEnumOleUndoUnits enumerator);
		}
		[ComVisible(true)]
		[Guid("00000104-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IEnumOLEVERB
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Next([MarshalAs(UnmanagedType.U4)] int celt, [Out] NativeMethods.tagOLEVERB rgelt, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pceltFetched);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Skip([MarshalAs(UnmanagedType.U4)] [In] int celt);
			void Reset();
			void Clone(out NativeMethods.IEnumOLEVERB ppenum);
		}
		[Guid("00000105-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		internal interface IEnumSTATDATA
		{
			void Next([MarshalAs(UnmanagedType.U4)] [In] int celt, [Out] NativeMethods.STATDATA rgelt, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pceltFetched);
			void Skip([MarshalAs(UnmanagedType.U4)] [In] int celt);
			void Reset();
			void Clone([MarshalAs(UnmanagedType.LPArray)] [Out] NativeMethods.IEnumSTATDATA[] ppenum);
		}
		[ComVisible(true)]
		[Guid("3050f1d8-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		internal interface IHtmlBodyElement
		{
			void put_background([MarshalAs(UnmanagedType.BStr)] [In] string v);
			[return: MarshalAs(UnmanagedType.BStr)]
			string get_background();
			void put_bgProperties([MarshalAs(UnmanagedType.BStr)] [In] string v);
			[return: MarshalAs(UnmanagedType.BStr)]
			string get_bgProperties();
			void put_leftMargin([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_leftMargin();
			void put_topMargin([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_topMargin();
			void put_rightMargin([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_rightMargin();
			void put_bottomMargin([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_bottomMargin();
			void put_noWrap([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_noWrap();
			void put_bgColor([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_bgColor();
			void put_text([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_text();
			void put_link([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_link();
			void put_vLink([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_vLink();
			void put_aLink([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_aLink();
			void put_onload([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_onload();
			void put_onunload([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_onunload();
			void put_scroll([MarshalAs(UnmanagedType.BStr)] [In] string s);
			[return: MarshalAs(UnmanagedType.BStr)]
			string get_scroll();
			void put_onselect([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_onselect();
			void put_onbeforeunload([MarshalAs(UnmanagedType.Interface)] [In] object o);
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_onbeforeunload();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLTxtRange createTextRange();
		}
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComVisible(true)]
		[Guid("3050F4E9-98B5-11CF-BB82-00AA00BDCE0B")]
		internal interface IHtmlControlElement
		{
			void SetTabIndex([MarshalAs(UnmanagedType.I2)] [In] short p);
			[return: MarshalAs(UnmanagedType.I2)]
			short GetTabIndex();
			void Focus();
			void SetAccessKey([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetAccessKey();
			void SetOnblur([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnblur();
			void SetOnfocus([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnfocus();
			void SetOnresize([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnresize();
			void Blur();
			void AddFilter([MarshalAs(UnmanagedType.Interface)] [In] object pUnk);
			void RemoveFilter([MarshalAs(UnmanagedType.Interface)] [In] object pUnk);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientHeight();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientWidth();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientTop();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientLeft();
		}
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComVisible(true)]
		[Guid("3050F29C-98B5-11CF-BB82-00AA00BDCE0B")]
		internal interface IHtmlControlRange
		{
			void Select();
			void Add([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHtmlControlElement item);
			void Remove([MarshalAs(UnmanagedType.I4)] [In] int index);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement Item([MarshalAs(UnmanagedType.I4)] [In] int index);
			void ScrollIntoView([MarshalAs(UnmanagedType.Struct)] [In] object varargStart);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandSupported([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandEnabled([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandState([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandIndeterm([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.BStr)]
			string QueryCommandText([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Struct)]
			object QueryCommandValue([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool ExecCommand([MarshalAs(UnmanagedType.BStr)] [In] string cmdID, [MarshalAs(UnmanagedType.Bool)] [In] bool showUI, [MarshalAs(UnmanagedType.Struct)] [In] object value);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool ExecCommandShowHelp([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement CommonParentElement();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetLength();
		}
		[ComVisible(true)]
		[Guid("3050f65e-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHtmlControlRange2
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int addElement([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement element);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComVisible(true)]
		[Guid("3050F3DB-98B5-11CF-BB82-00AA00BDCE0B")]
		internal interface IHTMLCurrentStyle
		{
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPosition();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetStyleFloat();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetColor();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundColor();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontFamily();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontStyle();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontObject();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetFontWeight();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetFontSize();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundImage();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundPositionX();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundPositionY();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundRepeat();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderLeftColor();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderTopColor();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderRightColor();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderBottomColor();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderTopStyle();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderRightStyle();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderBottomStyle();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderLeftStyle();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderTopWidth();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderRightWidth();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderBottomWidth();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderLeftWidth();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLeft();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetTop();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetWidth();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetHeight();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingLeft();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingTop();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingRight();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingBottom();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextAlign();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextDecoration();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDisplay();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetVisibility();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetZIndex();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLetterSpacing();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLineHeight();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetTextIndent();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetVerticalAlign();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundAttachment();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginTop();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginRight();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginBottom();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginLeft();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetClear();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyleType();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStylePosition();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyleImage();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetClipTop();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetClipRight();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetClipBottom();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetClipLeft();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetOverflow();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPageBreakBefore();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPageBreakAfter();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCursor();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTableLayout();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderCollapse();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDirection();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBehavior();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetUnicodeBidi();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetRight();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBottom();
		}
		[ComVisible(true)]
		[Guid("25336920-03F9-11CF-8FD0-00AA00686F13")]
		[ComImport]
		public class HTMLDocument
		{
		}
		[ComVisible(true)]
		[Guid("626FC520-A41E-11CF-A731-00A0C9082637")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLDocument
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetScript();
		}
		[Guid("332C4425-26CB-11D0-B483-00C04FD90119")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComVisible(true)]
		internal interface IHTMLDocument2
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetScript();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetAll();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetBody();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetActiveElement();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetImages();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetApplets();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetLinks();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetForms();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetAnchors();
			void SetTitle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTitle();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetScripts();
			void SetDesignMode([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDesignMode();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLSelectionObject GetSelection();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetReadyState();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetFrames();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetEmbeds();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetPlugins();
			void SetAlinkColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetAlinkColor();
			void SetBgColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBgColor();
			void SetFgColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetFgColor();
			void SetLinkColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLinkColor();
			void SetVlinkColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetVlinkColor();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetReferrer();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetLocation();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetLastModified();
			void SetURL([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetURL();
			void SetDomain([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDomain();
			void SetCookie([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCookie();
			void SetExpando([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetExpando();
			void SetCharset([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCharset();
			void SetDefaultCharset([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDefaultCharset();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetMimeType();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFileSize();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFileCreatedDate();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFileModifiedDate();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFileUpdatedDate();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetSecurity();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetProtocol();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetNameProp();
			void DummyWrite([MarshalAs(UnmanagedType.I4)] [In] int psarray);
			void DummyWriteln([MarshalAs(UnmanagedType.I4)] [In] int psarray);
			[return: MarshalAs(UnmanagedType.Interface)]
			object Open([MarshalAs(UnmanagedType.BStr)] [In] string URL, [MarshalAs(UnmanagedType.Struct)] [In] object name, [MarshalAs(UnmanagedType.Struct)] [In] object features, [MarshalAs(UnmanagedType.Struct)] [In] object replace);
			void Close();
			void Clear();
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandSupported([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandEnabled([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandState([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandIndeterm([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.BStr)]
			string QueryCommandText([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Struct)]
			object QueryCommandValue([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool ExecCommand([MarshalAs(UnmanagedType.BStr)] [In] string cmdID, [MarshalAs(UnmanagedType.Bool)] [In] bool showUI, [MarshalAs(UnmanagedType.Struct)] [In] object value);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool ExecCommandShowHelp([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement CreateElement([MarshalAs(UnmanagedType.BStr)] [In] string eTag);
			void SetOnhelp([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnhelp();
			void SetOnclick([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnclick();
			void SetOndblclick([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndblclick();
			void SetOnkeyup([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeyup();
			void SetOnkeydown([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeydown();
			void SetOnkeypress([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeypress();
			void SetOnmouseup([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseup();
			void SetOnmousedown([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmousedown();
			void SetOnmousemove([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmousemove();
			void SetOnmouseout([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseout();
			void SetOnmouseover([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseover();
			void SetOnreadystatechange([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnreadystatechange();
			void SetOnafterupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnafterupdate();
			void SetOnrowexit([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowexit();
			void SetOnrowenter([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowenter();
			int SetOndragstart([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragstart();
			void SetOnselectstart([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnselectstart();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement ElementFromPoint([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLWindow2 GetParentWindow();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyleSheetsCollection GetStyleSheets();
			void SetOnbeforeupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforeupdate();
			void SetOnerrorupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnerrorupdate();
			[return: MarshalAs(UnmanagedType.BStr)]
			string toString();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyleSheet CreateStyleSheet([MarshalAs(UnmanagedType.BStr)] [In] string bstrHref, [MarshalAs(UnmanagedType.I4)] [In] int lIndex);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("3050f662-98b5-11cf-bb82-00aa00bdce0b")]
		[ComVisible(true)]
		internal interface IHTMLEditDesigner
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int PreHandleEvent([In] int dispId, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLEventObj eventObj);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int PostHandleEvent([In] int dispId, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLEventObj eventObj);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int TranslateAccelerator([In] int dispId, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLEventObj eventObj);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int PostEditorEventNotify([In] int dispId, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLEventObj eventObj);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("3050f6a0-98b5-11cf-bb82-00aa00bdce0b")]
		[ComVisible(true)]
		internal interface IHTMLEditHost
		{
			void SnapRect([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement pElement, [In] [Out] NativeMethods.COMRECT rcNew, [MarshalAs(UnmanagedType.I4)] [In] int nHandle);
		}
		[Guid("3050f663-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		internal interface IHTMLEditServices
		{
			[return: MarshalAs(UnmanagedType.I4)]
			int AddDesigner([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLEditDesigner designer);
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetSelectionServices([MarshalAs(UnmanagedType.Interface)] [In] object markupContainer);
			[return: MarshalAs(UnmanagedType.I4)]
			int MoveToSelectionAnchor([MarshalAs(UnmanagedType.Interface)] [In] object markupPointer);
			[return: MarshalAs(UnmanagedType.I4)]
			int MoveToSelectionEnd([MarshalAs(UnmanagedType.Interface)] [In] object markupPointer);
			[return: MarshalAs(UnmanagedType.I4)]
			int RemoveDesigner([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLEditDesigner designer);
		}
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[Guid("3050F1FF-98B5-11CF-BB82-00AA00BDCE0B")]
		internal interface IHTMLElement
		{
			void SetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.Struct)] [In] object AttributeValue, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
			void GetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags, [MarshalAs(UnmanagedType.LPArray)] [Out] object[] pvars);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool RemoveAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
			void SetClassName([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetClassName();
			void SetId([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetId();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTagName();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetParentElement();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyle GetStyle();
			void SetOnhelp([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnhelp();
			void SetOnclick([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnclick();
			void SetOndblclick([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndblclick();
			void SetOnkeydown([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeydown();
			void SetOnkeyup([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeyup();
			void SetOnkeypress([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeypress();
			void SetOnmouseout([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseout();
			void SetOnmouseover([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseover();
			void SetOnmousemove([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmousemove();
			void SetOnmousedown([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmousedown();
			void SetOnmouseup([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseup();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetDocument();
			void SetTitle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTitle();
			void SetLanguage([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetLanguage();
			void SetOnselectstart([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnselectstart();
			void ScrollIntoView([MarshalAs(UnmanagedType.Struct)] [In] object varargStart);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool Contains([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement pChild);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetSourceIndex();
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetRecordNumber();
			void SetLang([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetLang();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOffsetLeft();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOffsetTop();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOffsetWidth();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOffsetHeight();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetOffsetParent();
			void SetInnerHTML([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetInnerHTML();
			void SetInnerText([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetInnerText();
			void SetOuterHTML([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetOuterHTML();
			void SetOuterText([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetOuterText();
			void InsertAdjacentHTML([MarshalAs(UnmanagedType.BStr)] [In] string whereText, [MarshalAs(UnmanagedType.BStr)] [In] string html);
			void InsertAdjacentText([MarshalAs(UnmanagedType.BStr)] [In] string whereText, [MarshalAs(UnmanagedType.BStr)] [In] string text);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetParentTextEdit();
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetIsTextEdit();
			void Click();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetFilters();
			void SetOndragstart([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragstart();
			[return: MarshalAs(UnmanagedType.BStr)]
			string toString();
			void SetOnbeforeupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforeupdate();
			void SetOnafterupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnafterupdate();
			void SetOnerrorupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnerrorupdate();
			void SetOnrowexit([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowexit();
			void SetOnrowenter([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowenter();
			void SetOndatasetchanged([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndatasetchanged();
			void SetOndataavailable([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndataavailable();
			void SetOndatasetcomplete([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndatasetcomplete();
			void SetOnfilterchange([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnfilterchange();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetChildren();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetAll();
		}
		[ComVisible(true)]
		[Guid("3050F434-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLElement2
		{
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetScopeName();
			void SetCapture([MarshalAs(UnmanagedType.Bool)] [In] bool containerCapture);
			void ReleaseCapture();
			void SetOnlosecapture([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnlosecapture();
			[return: MarshalAs(UnmanagedType.BStr)]
			string ComponentFromPoint([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);
			void DoScroll([MarshalAs(UnmanagedType.Struct)] [In] object component);
			void SetOnscroll([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnscroll();
			void SetOndrag([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndrag();
			void SetOndragend([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragend();
			void SetOndragenter([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragenter();
			void SetOndragover([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragover();
			void SetOndragleave([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragleave();
			void SetOndrop([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndrop();
			void SetOnbeforecut([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforecut();
			void SetOncut([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOncut();
			void SetOnbeforecopy([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforecopy();
			void SetOncopy([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOncopy();
			void SetOnbeforepaste([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforepaste();
			void SetOnpaste([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnpaste();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLCurrentStyle GetCurrentStyle();
			void SetOnpropertychange([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnpropertychange();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetClientRects();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetBoundingClientRect();
			void SetExpression([MarshalAs(UnmanagedType.BStr)] [In] string propname, [MarshalAs(UnmanagedType.BStr)] [In] string expression, [MarshalAs(UnmanagedType.BStr)] [In] string language);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetExpression([MarshalAs(UnmanagedType.BStr)] [In] object propname);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool RemoveExpression([MarshalAs(UnmanagedType.BStr)] [In] string propname);
			void SetTabIndex([MarshalAs(UnmanagedType.I2)] [In] short p);
			[return: MarshalAs(UnmanagedType.I2)]
			short GetTabIndex();
			void Focus();
			void SetAccessKey([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetAccessKey();
			void SetOnblur([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnblur();
			void SetOnfocus([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnfocus();
			void SetOnresize([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnresize();
			void Blur();
			void AddFilter([MarshalAs(UnmanagedType.Interface)] [In] object pUnk);
			void RemoveFilter([MarshalAs(UnmanagedType.Interface)] [In] object pUnk);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientHeight();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientWidth();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientTop();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientLeft();
			[return: MarshalAs(UnmanagedType.Bool)]
			bool AttachEvent([MarshalAs(UnmanagedType.BStr)] [In] string ev, [MarshalAs(UnmanagedType.Interface)] [In] object pdisp);
			void DetachEvent([MarshalAs(UnmanagedType.BStr)] [In] string ev, [MarshalAs(UnmanagedType.Interface)] [In] object pdisp);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetReadyState();
			void SetOnreadystatechange([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnreadystatechange();
			void SetOnrowsdelete([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowsdelete();
			void SetOnrowsinserted([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowsinserted();
			void SetOncellchange([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOncellchange();
			void SetDir([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDir();
			[return: MarshalAs(UnmanagedType.Interface)]
			object CreateControlRange();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScrollHeight();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScrollWidth();
			void SetScrollTop([MarshalAs(UnmanagedType.I4)] [In] int p);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScrollTop();
			void SetScrollLeft([MarshalAs(UnmanagedType.I4)] [In] int p);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScrollLeft();
			void ClearAttributes();
			void MergeAttributes([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement mergeThis);
			void SetOncontextmenu([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOncontextmenu();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement InsertAdjacentElement([MarshalAs(UnmanagedType.BStr)] [In] string whereText, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement insertedElement);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement ApplyElement([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement apply, [MarshalAs(UnmanagedType.BStr)] [In] string whereText);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetAdjacentText([MarshalAs(UnmanagedType.BStr)] [In] string whereText);
			[return: MarshalAs(UnmanagedType.BStr)]
			string ReplaceAdjacentText([MarshalAs(UnmanagedType.BStr)] [In] string whereText, [MarshalAs(UnmanagedType.BStr)] [In] string newText);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetCanHaveChildren();
			[return: MarshalAs(UnmanagedType.I4)]
			int AddBehavior([MarshalAs(UnmanagedType.BStr)] [In] string bstrUrl, [In] ref object pvarFactory);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool RemoveBehavior([MarshalAs(UnmanagedType.I4)] [In] int cookie);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyle GetRuntimeStyle();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetBehaviorUrns();
			void SetTagUrn([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTagUrn();
			void SetOnbeforeeditfocus([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforeeditfocus();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetReadyStateValue();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetElementsByTagName([MarshalAs(UnmanagedType.BStr)] [In] string v);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyle GetBaseStyle();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLCurrentStyle GetBaseCurrentStyle();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyle GetBaseRuntimeStyle();
			void SetOnmousehover([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmousehover();
			void SetOnkeydownpreview([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeydownpreview();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetBehavior([MarshalAs(UnmanagedType.BStr)] [In] string bstrName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrUrn);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[Guid("3050F21F-98B5-11CF-BB82-00AA00BDCE0B")]
		[ComVisible(true)]
		internal interface IHTMLElementCollection
		{
			[return: MarshalAs(UnmanagedType.BStr)]
			string toString();
			void SetLength([MarshalAs(UnmanagedType.I4)] [In] int p);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetLength();
			[return: MarshalAs(UnmanagedType.Interface)]
			object Get_newEnum();
			[return: MarshalAs(UnmanagedType.Interface)]
			object Item([MarshalAs(UnmanagedType.Struct)] [In] object name, [MarshalAs(UnmanagedType.Struct)] [In] object index);
			[return: MarshalAs(UnmanagedType.Interface)]
			object Tags([MarshalAs(UnmanagedType.Struct)] [In] object tagName);
		}
		[Guid("3050F6C9-98B5-11CF-BB82-00AA00BDCE0B")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLElementDefaults
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyle GetStyle();
			void SetTabStop([MarshalAs(UnmanagedType.Bool)] [In] bool v);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTabStop();
			void SetViewInheritStyle([MarshalAs(UnmanagedType.Bool)] [In] bool v);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetViewInheritStyle();
			void SetViewMasterTab([MarshalAs(UnmanagedType.Bool)] [In] bool v);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetViewMasterTab();
			void SetScrollSegmentX([MarshalAs(UnmanagedType.I4)] [In] int v);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScrollSegmentX();
			void SetScrollSegmentY([MarshalAs(UnmanagedType.I4)] [In] object p);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScrollSegmentY();
			void SetIsMultiLine([MarshalAs(UnmanagedType.Bool)] [In] bool v);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetIsMultiLine();
			void SetContentEditable([MarshalAs(UnmanagedType.BStr)] [In] string v);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetContentEditable();
			void SetCanHaveHTML([MarshalAs(UnmanagedType.Bool)] [In] bool v);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetCanHaveHTML();
			void SetViewLink([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLDocument viewLink);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDocument GetViewLink();
			void SetFrozen([MarshalAs(UnmanagedType.Bool)] [In] bool v);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetFrozen();
		}
		[ComVisible(true)]
		[Guid("3050F33C-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IHTMLElementEvents
		{
			void Bogus1();
			void Bogus2();
			void Bogus3();
			void Invoke([MarshalAs(UnmanagedType.U4)] [In] int id, [In] ref Guid g, [MarshalAs(UnmanagedType.U4)] [In] int lcid, [MarshalAs(UnmanagedType.U4)] [In] int dwFlags, [In] NativeMethods.DISPPARAMS pdp, [MarshalAs(UnmanagedType.LPArray)] [Out] object[] pvarRes, [Out] NativeMethods.EXCEPINFO pei, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] nArgError);
		}
		[Guid("3050F32D-98B5-11CF-BB82-00AA00BDCE0B")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLEventObj
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetSrcElement();
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetAltKey();
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetCtrlKey();
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetShiftKey();
			void SetReturnValue([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetReturnValue();
			void SetCancelBubble([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetCancelBubble();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetFromElement();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetToElement();
			void SetKeyCode([MarshalAs(UnmanagedType.I4)] [In] int p);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetKeyCode();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetButton();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetEventType();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetQualifier();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetReason();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetX();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetY();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientX();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientY();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOffsetX();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOffsetY();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScreenX();
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScreenY();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetSrcFilter();
		}
		[Guid("3050F6A6-98B5-11CF-BB82-00AA00BDCE0B")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IHTMLPainter
		{
			void Draw([MarshalAs(UnmanagedType.I4)] [In] int leftBounds, [MarshalAs(UnmanagedType.I4)] [In] int topBounds, [MarshalAs(UnmanagedType.I4)] [In] int rightBounds, [MarshalAs(UnmanagedType.I4)] [In] int bottomBounds, [MarshalAs(UnmanagedType.I4)] [In] int leftUpdate, [MarshalAs(UnmanagedType.I4)] [In] int topUpdate, [MarshalAs(UnmanagedType.I4)] [In] int rightUpdate, [MarshalAs(UnmanagedType.I4)] [In] int bottomUpdate, [MarshalAs(UnmanagedType.U4)] [In] int lDrawFlags, [In] IntPtr hdc, [In] IntPtr pvDrawObject);
			void OnResize([MarshalAs(UnmanagedType.I4)] [In] int cx, [MarshalAs(UnmanagedType.I4)] [In] int cy);
			void GetPainterInfo([Out] NativeMethods.HTML_PAINTER_INFO htmlPainterInfo);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool HitTestPoint([MarshalAs(UnmanagedType.I4)] [In] int ptx, [MarshalAs(UnmanagedType.I4)] [In] int pty, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pbHit, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] plPartID);
		}
		[ComVisible(true)]
		[Guid("3050f6a7-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IHTMLPaintSite
		{
			void InvalidatePainterInfo();
			void InvalidateRect([In] IntPtr pRect);
		}
		[Guid("3050F3CF-98B5-11CF-BB82-00AA00BDCE0B")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLRuleStyle
		{
			void SetFontFamily([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontFamily();
			void SetFontStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontStyle();
			void SetFontObject([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontObject();
			void SetFontWeight([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontWeight();
			void SetFontSize([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetFontSize();
			void SetFont([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFont();
			void SetColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetColor();
			void SetBackground([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackground();
			void SetBackgroundColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundColor();
			void SetBackgroundImage([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundImage();
			void SetBackgroundRepeat([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundRepeat();
			void SetBackgroundAttachment([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundAttachment();
			void SetBackgroundPosition([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundPosition();
			void SetBackgroundPositionX([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundPositionX();
			void SetBackgroundPositionY([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundPositionY();
			void SetWordSpacing([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetWordSpacing();
			void SetLetterSpacing([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLetterSpacing();
			void SetTextDecoration([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextDecoration();
			void SetTextDecorationNone([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationNone();
			void SetTextDecorationUnderline([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationUnderline();
			void SetTextDecorationOverline([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationOverline();
			void SetTextDecorationLineThrough([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationLineThrough();
			void SetTextDecorationBlink([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationBlink();
			void SetVerticalAlign([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetVerticalAlign();
			void SetTextTransform([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextTransform();
			void SetTextAlign([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextAlign();
			void SetTextIndent([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetTextIndent();
			void SetLineHeight([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLineHeight();
			void SetMarginTop([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginTop();
			void SetMarginRight([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginRight();
			void SetMarginBottom([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginBottom();
			void SetMarginLeft([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginLeft();
			void SetMargin([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetMargin();
			void SetPaddingTop([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingTop();
			void SetPaddingRight([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingRight();
			void SetPaddingBottom([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingBottom();
			void SetPaddingLeft([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingLeft();
			void SetPadding([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPadding();
			void SetBorder([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorder();
			void SetBorderTop([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderTop();
			void SetBorderRight([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderRight();
			void SetBorderBottom([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderBottom();
			void SetBorderLeft([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderLeft();
			void SetBorderColor([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderColor();
			void SetBorderTopColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderTopColor();
			void SetBorderRightColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderRightColor();
			void SetBorderBottomColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderBottomColor();
			void SetBorderLeftColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderLeftColor();
			void SetBorderWidth([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderWidth();
			void SetBorderTopWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderTopWidth();
			void SetBorderRightWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderRightWidth();
			void SetBorderBottomWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderBottomWidth();
			void SetBorderLeftWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderLeftWidth();
			void SetBorderStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderStyle();
			void SetBorderTopStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderTopStyle();
			void SetBorderRightStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderRightStyle();
			void SetBorderBottomStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderBottomStyle();
			void SetBorderLeftStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderLeftStyle();
			void SetWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetWidth();
			void SetHeight([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetHeight();
			void SetStyleFloat([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetStyleFloat();
			void SetClear([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetClear();
			void SetDisplay([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDisplay();
			void SetVisibility([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetVisibility();
			void SetListStyleType([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyleType();
			void SetListStylePosition([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStylePosition();
			void SetListStyleImage([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyleImage();
			void SetListStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyle();
			void SetWhiteSpace([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetWhiteSpace();
			void SetTop([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetTop();
			void SetLeft([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLeft();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPosition();
			void SetZIndex([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetZIndex();
			void SetOverflow([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetOverflow();
			void SetPageBreakBefore([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPageBreakBefore();
			void SetPageBreakAfter([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPageBreakAfter();
			void SetCssText([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCssText();
			void SetCursor([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCursor();
			void SetClip([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetClip();
			void SetFilter([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFilter();
			void SetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.Struct)] [In] object AttributeValue, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool RemoveAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
		}
		[Guid("3050F25A-98B5-11CF-BB82-00AA00BDCE0B")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		public interface IHTMLSelectionObject
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			object CreateRange();
			void Empty();
			void Clear();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetSelectionType();
		}
		[ComVisible(true)]
		[Guid("3050f230-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		public interface IHTMLTextContainer
		{
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object createControlRange();
			int get_ScrollHeight();
			int get_ScrollWidth();
			int get_ScrollTop();
			int get_ScrollLeft();
			void put_ScrollHeight(int i);
			void put_ScrollWidth(int i);
			void put_ScrollTop(int i);
			void put_ScrollLeft(int i);
		}
		[ComVisible(true)]
		[Guid("3050F220-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLTxtRange
		{
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetHtmlText();
			void SetText([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetText();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement ParentElement();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLTxtRange Duplicate();
			[return: MarshalAs(UnmanagedType.Bool)]
			bool InRange([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLTxtRange range);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool IsEqual([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLTxtRange range);
			void ScrollIntoView([MarshalAs(UnmanagedType.Bool)] [In] bool fStart);
			void Collapse([MarshalAs(UnmanagedType.Bool)] [In] bool Start);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool Expand([MarshalAs(UnmanagedType.BStr)] [In] string Unit);
			[return: MarshalAs(UnmanagedType.I4)]
			int Move([MarshalAs(UnmanagedType.BStr)] [In] string Unit, [MarshalAs(UnmanagedType.I4)] [In] int Count);
			[return: MarshalAs(UnmanagedType.I4)]
			int MoveStart([MarshalAs(UnmanagedType.BStr)] [In] string Unit, [MarshalAs(UnmanagedType.I4)] [In] int Count);
			[return: MarshalAs(UnmanagedType.I4)]
			int MoveEnd([MarshalAs(UnmanagedType.BStr)] [In] string Unit, [MarshalAs(UnmanagedType.I4)] [In] int Count);
			void Select();
			void PasteHTML([MarshalAs(UnmanagedType.BStr)] [In] string html);
			void MoveToElementText([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement element);
			void SetEndPoint([MarshalAs(UnmanagedType.BStr)] [In] string how, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLTxtRange SourceRange);
			[return: MarshalAs(UnmanagedType.I4)]
			int CompareEndPoints([MarshalAs(UnmanagedType.BStr)] [In] string how, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLTxtRange SourceRange);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool FindText([MarshalAs(UnmanagedType.BStr)] [In] string String, [MarshalAs(UnmanagedType.I4)] [In] int Count, [MarshalAs(UnmanagedType.I4)] [In] int Flags);
			void MoveToPoint([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBookmark();
			[return: MarshalAs(UnmanagedType.Bool)]
			bool MoveToBookmark([MarshalAs(UnmanagedType.BStr)] [In] string Bookmark);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandSupported([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandEnabled([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandState([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandIndeterm([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.BStr)]
			string QueryCommandText([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Struct)]
			object QueryCommandValue([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool ExecCommand([MarshalAs(UnmanagedType.BStr)] [In] string cmdID, [MarshalAs(UnmanagedType.Bool)] [In] bool showUI, [MarshalAs(UnmanagedType.Struct)] [In] object value);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool ExecCommandShowHelp([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);
		}
		[Guid("3050F25E-98B5-11CF-BB82-00AA00BDCE0B")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLStyle
		{
			void SetFontFamily([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontFamily();
			void SetFontStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontStyle();
			void SetFontObject([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontObject();
			void SetFontWeight([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontWeight();
			void SetFontSize([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetFontSize();
			void SetFont([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFont();
			void SetColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetColor();
			void SetBackground([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackground();
			void SetBackgroundColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundColor();
			void SetBackgroundImage([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundImage();
			void SetBackgroundRepeat([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundRepeat();
			void SetBackgroundAttachment([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundAttachment();
			void SetBackgroundPosition([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundPosition();
			void SetBackgroundPositionX([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundPositionX();
			void SetBackgroundPositionY([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundPositionY();
			void SetWordSpacing([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetWordSpacing();
			void SetLetterSpacing([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLetterSpacing();
			void SetTextDecoration([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextDecoration();
			void SetTextDecorationNone([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationNone();
			void SetTextDecorationUnderline([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationUnderline();
			void SetTextDecorationOverline([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationOverline();
			void SetTextDecorationLineThrough([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationLineThrough();
			void SetTextDecorationBlink([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationBlink();
			void SetVerticalAlign([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetVerticalAlign();
			void SetTextTransform([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextTransform();
			void SetTextAlign([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextAlign();
			void SetTextIndent([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetTextIndent();
			void SetLineHeight([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLineHeight();
			void SetMarginTop([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginTop();
			void SetMarginRight([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginRight();
			void SetMarginBottom([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginBottom();
			void SetMarginLeft([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginLeft();
			void SetMargin([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetMargin();
			void SetPaddingTop([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingTop();
			void SetPaddingRight([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingRight();
			void SetPaddingBottom([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingBottom();
			void SetPaddingLeft([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingLeft();
			void SetPadding([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPadding();
			void SetBorder([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorder();
			void SetBorderTop([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderTop();
			void SetBorderRight([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderRight();
			void SetBorderBottom([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderBottom();
			void SetBorderLeft([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderLeft();
			void SetBorderColor([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderColor();
			void SetBorderTopColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderTopColor();
			void SetBorderRightColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderRightColor();
			void SetBorderBottomColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderBottomColor();
			void SetBorderLeftColor([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderLeftColor();
			void SetBorderWidth([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderWidth();
			void SetBorderTopWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderTopWidth();
			void SetBorderRightWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderRightWidth();
			void SetBorderBottomWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderBottomWidth();
			void SetBorderLeftWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderLeftWidth();
			void SetBorderStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderStyle();
			void SetBorderTopStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderTopStyle();
			void SetBorderRightStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderRightStyle();
			void SetBorderBottomStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderBottomStyle();
			void SetBorderLeftStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderLeftStyle();
			void SetWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetWidth();
			void SetHeight([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetHeight();
			void SetStyleFloat([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetStyleFloat();
			void SetClear([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetClear();
			void SetDisplay([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDisplay();
			void SetVisibility([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetVisibility();
			void SetListStyleType([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyleType();
			void SetListStylePosition([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStylePosition();
			void SetListStyleImage([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyleImage();
			void SetListStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyle();
			void SetWhiteSpace([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetWhiteSpace();
			void SetTop([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetTop();
			void SetLeft([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLeft();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPosition();
			void SetZIndex([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetZIndex();
			void SetOverflow([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetOverflow();
			void SetPageBreakBefore([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPageBreakBefore();
			void SetPageBreakAfter([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPageBreakAfter();
			void SetCssText([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCssText();
			void SetPixelTop([MarshalAs(UnmanagedType.I4)] [In] int p);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetPixelTop();
			void SetPixelLeft([MarshalAs(UnmanagedType.I4)] [In] int p);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetPixelLeft();
			void SetPixelWidth([MarshalAs(UnmanagedType.I4)] [In] int p);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetPixelWidth();
			void SetPixelHeight([MarshalAs(UnmanagedType.I4)] [In] int p);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetPixelHeight();
			void SetPosTop([MarshalAs(UnmanagedType.R4)] [In] float p);
			[return: MarshalAs(UnmanagedType.R4)]
			float GetPosTop();
			void SetPosLeft([MarshalAs(UnmanagedType.R4)] [In] float p);
			[return: MarshalAs(UnmanagedType.R4)]
			float GetPosLeft();
			void SetPosWidth([MarshalAs(UnmanagedType.R4)] [In] float p);
			[return: MarshalAs(UnmanagedType.R4)]
			float GetPosWidth();
			void SetPosHeight([MarshalAs(UnmanagedType.R4)] [In] float p);
			[return: MarshalAs(UnmanagedType.R4)]
			float GetPosHeight();
			void SetCursor([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCursor();
			void SetClip([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetClip();
			void SetFilter([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFilter();
			void SetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.Struct)] [In] object AttributeValue, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool RemoveAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
		}
		[ComVisible(true)]
		[Guid("3050F2E3-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLStyleSheet
		{
			void SetTitle([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTitle();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyleSheet GetParentStyleSheet();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetOwningElement();
			void SetDisabled([MarshalAs(UnmanagedType.Bool)] [In] bool p);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetDisabled();
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetReadOnly();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyleSheetsCollection GetImports();
			void SetHref([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetHref();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetStyleSheetType();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetId();
			[return: MarshalAs(UnmanagedType.I4)]
			int AddImport([MarshalAs(UnmanagedType.BStr)] [In] string bstrURL, [MarshalAs(UnmanagedType.I4)] [In] int lIndex);
			[return: MarshalAs(UnmanagedType.I4)]
			int AddRule([MarshalAs(UnmanagedType.BStr)] [In] string bstrSelector, [MarshalAs(UnmanagedType.BStr)] [In] string bstrStyle, [MarshalAs(UnmanagedType.I4)] [In] int lIndex);
			void RemoveImport([MarshalAs(UnmanagedType.I4)] [In] int lIndex);
			void RemoveRule([MarshalAs(UnmanagedType.I4)] [In] int lIndex);
			void SetMedia([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetMedia();
			void SetCssText([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCssText();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyleSheetRulesCollection GetRules();
		}
		[Guid("3050F37E-98B5-11CF-BB82-00AA00BDCE0B")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLStyleSheetsCollection
		{
			[return: MarshalAs(UnmanagedType.I4)]
			int GetLength();
			[return: MarshalAs(UnmanagedType.Interface)]
			object Get_newEnum();
			[return: MarshalAs(UnmanagedType.Struct)]
			object Item([In] ref object pvarIndex);
		}
		[Guid("3050F357-98B5-11CF-BB82-00AA00BDCE0B")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLStyleSheetRule
		{
			void SetSelectorText([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetSelectorText();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLRuleStyle GetStyle();
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetReadOnly();
		}
		[ComVisible(true)]
		[Guid("3050F2E5-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLStyleSheetRulesCollection
		{
			[return: MarshalAs(UnmanagedType.I4)]
			int GetLength();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyleSheetRule Item([MarshalAs(UnmanagedType.I4)] [In] int index);
		}
		[ComVisible(true)]
		[Guid("332C4427-26CB-11D0-B483-00C04FD90119")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLWindow2
		{
			[return: MarshalAs(UnmanagedType.Struct)]
			object Item([In] ref object pvarIndex);
			[return: MarshalAs(UnmanagedType.I4)]
			int GetLength();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetFrames();
			void SetDefaultStatus([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDefaultStatus();
			void SetStatus([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetStatus();
			[return: MarshalAs(UnmanagedType.I4)]
			int SetTimeout([MarshalAs(UnmanagedType.BStr)] [In] string expression, [MarshalAs(UnmanagedType.I4)] [In] int msec, [In] ref object language);
			void ClearTimeout([MarshalAs(UnmanagedType.I4)] [In] int timerID);
			void Alert([MarshalAs(UnmanagedType.BStr)] [In] string message);
			[return: MarshalAs(UnmanagedType.Bool)]
			bool Confirm([MarshalAs(UnmanagedType.BStr)] [In] string message);
			[return: MarshalAs(UnmanagedType.Struct)]
			object Prompt([MarshalAs(UnmanagedType.BStr)] [In] string message, [MarshalAs(UnmanagedType.BStr)] [In] string defstr);
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetImage();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetLocation();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetHistory();
			void Close();
			void SetOpener([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOpener();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetNavigator();
			void SetName([MarshalAs(UnmanagedType.BStr)] [In] string p);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetName();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLWindow2 GetParent();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLWindow2 Open([MarshalAs(UnmanagedType.BStr)] [In] string URL, [MarshalAs(UnmanagedType.BStr)] [In] string name, [MarshalAs(UnmanagedType.BStr)] [In] string features, [MarshalAs(UnmanagedType.Bool)] [In] bool replace);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLWindow2 GetSelf();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLWindow2 GetTop();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLWindow2 GetWindow();
			void Navigate([MarshalAs(UnmanagedType.BStr)] [In] string URL);
			void SetOnfocus([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnfocus();
			void SetOnblur([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnblur();
			void SetOnload([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnload();
			void SetOnbeforeunload([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforeunload();
			void SetOnunload([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnunload();
			void SetOnhelp([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnhelp();
			void SetOnerror([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnerror();
			void SetOnresize([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnresize();
			void SetOnscroll([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnscroll();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDocument2 GetDocument();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLEventObj GetEvent();
			[return: MarshalAs(UnmanagedType.Interface)]
			object Get_newEnum();
			[return: MarshalAs(UnmanagedType.Struct)]
			object ShowModalDialog([MarshalAs(UnmanagedType.BStr)] [In] string dialog, [In] ref object varArgIn, [In] ref object varOptions);
			void ShowHelp([MarshalAs(UnmanagedType.BStr)] [In] string helpURL, [MarshalAs(UnmanagedType.Struct)] [In] object helpArg, [MarshalAs(UnmanagedType.BStr)] [In] string features);
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetScreen();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetOption();
			void Focus();
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetClosed();
			void Blur();
			void Scroll([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetClientInformation();
			[return: MarshalAs(UnmanagedType.I4)]
			int SetInterval([MarshalAs(UnmanagedType.BStr)] [In] string expression, [MarshalAs(UnmanagedType.I4)] [In] int msec, [In] ref object language);
			void ClearInterval([MarshalAs(UnmanagedType.I4)] [In] int timerID);
			void SetOffscreenBuffering([MarshalAs(UnmanagedType.Struct)] [In] object p);
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOffscreenBuffering();
			[return: MarshalAs(UnmanagedType.Struct)]
			object ExecScript([MarshalAs(UnmanagedType.BStr)] [In] string code, [MarshalAs(UnmanagedType.BStr)] [In] string language);
			void ScrollBy([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);
			void ScrollTo([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);
			void MoveTo([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);
			void MoveBy([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);
			void ResizeTo([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);
			void ResizeBy([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetExternal();
		}
		[Guid("0000000F-0000-0000-C000-000000000046")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IMoniker
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int IsDirty();
			void Load([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm);
			void Save([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm, [MarshalAs(UnmanagedType.Bool)] [In] bool fClearDirty);
			[return: MarshalAs(UnmanagedType.I8)]
			long GetSizeMax();
			[return: MarshalAs(UnmanagedType.Interface)]
			object BindToObject([MarshalAs(UnmanagedType.Interface)] [In] object pbc, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pMkToLeft, [In] ref Guid riidResult);
			[return: MarshalAs(UnmanagedType.Interface)]
			object BindToStorage([MarshalAs(UnmanagedType.Interface)] [In] object pbc, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pMkToLeft, [In] ref Guid riidResult);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IMoniker Reduce([MarshalAs(UnmanagedType.Interface)] [In] object pbc, [MarshalAs(UnmanagedType.I4)] [In] int dwReduceHowFar, [MarshalAs(UnmanagedType.Interface)] [In] [Out] NativeMethods.IMoniker pMkToLeft);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IMoniker Reduce([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pMkRight, [MarshalAs(UnmanagedType.Bool)] [In] bool fOnlyIfNotGeneric);
			[return: MarshalAs(UnmanagedType.Interface)]
			object Reduce([MarshalAs(UnmanagedType.Bool)] [In] bool fForward);
			void IsEqual([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pOtherMoniker);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Hash();
			void IsRunning([MarshalAs(UnmanagedType.Interface)] [In] object pbc, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pMkToLeft, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pMkNewlyRunning);
			[return: MarshalAs(UnmanagedType.LPStruct)]
			object GetTimeOfLastChange([MarshalAs(UnmanagedType.Interface)] [In] object pbc, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pMkToLeft);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IMoniker Inverse();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IMoniker CommonPrefixWith([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pMkOther);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IMoniker RelativePathTo([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pMkOther);
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDisplayName([MarshalAs(UnmanagedType.Interface)] [In] object pbc, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pMkOther);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IMoniker ParseDisplayName([MarshalAs(UnmanagedType.Interface)] [In] object pbc, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pMkToLeft, [MarshalAs(UnmanagedType.BStr)] [In] string pszDisplayName, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pchEaten);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int IsSystemMoniker();
		}
		[ComVisible(true)]
		[Guid("00000118-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleClientSite
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int SaveObject();
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetMoniker([MarshalAs(UnmanagedType.U4)] [In] int dwAssign, [MarshalAs(UnmanagedType.U4)] [In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object ppmk);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetContainer([MarshalAs(UnmanagedType.Interface)] out NativeMethods.IOleContainer container);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ShowObject();
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OnShowWindow([MarshalAs(UnmanagedType.I4)] [In] int fShow);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int RequestNewObjectLayout();
		}
		[ComVisible(true)]
		[Guid("B722BCCB-4E68-101B-A2BC-00AA00404770")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IOleCommandTarget
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int QueryStatus(ref Guid pguidCmdGroup, int cCmds, [In] [Out] NativeMethods.tagOLECMD prgCmds, [In] [Out] int pCmdText);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Exec(ref Guid pguidCmdGroup, int nCmdID, int nCmdexecopt, [MarshalAs(UnmanagedType.LPArray)] [In] object[] pvaIn, [MarshalAs(UnmanagedType.LPArray)] [Out] object[] pvaOut);
		}
		[Guid("0000011B-0000-0000-C000-000000000046")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleContainer
		{
			void ParseDisplayName([MarshalAs(UnmanagedType.Interface)] [In] object pbc, [MarshalAs(UnmanagedType.BStr)] [In] string pszDisplayName, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pchEaten, [MarshalAs(UnmanagedType.LPArray)] [Out] object[] ppmkOut);
			void EnumObjects([MarshalAs(UnmanagedType.U4)] [In] int grfFlags, [MarshalAs(UnmanagedType.LPArray)] [Out] object[] ppenum);
			void LockContainer([MarshalAs(UnmanagedType.I4)] [In] int fLock);
		}
		[ComVisible(true)]
		[Guid("0000010E-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IOleDataObject
		{
			int OleGetData(NativeMethods.FORMATETC pFormatetc, [Out] NativeMethods.STGMEDIUM pMedium);
			int OleGetDataHere(NativeMethods.FORMATETC pFormatetc, [In] [Out] NativeMethods.STGMEDIUM pMedium);
			int OleQueryGetData(NativeMethods.FORMATETC pFormatetc);
			int OleGetCanonicalFormatEtc(NativeMethods.FORMATETC pformatectIn, [Out] NativeMethods.FORMATETC pformatetcOut);
			int OleSetData(NativeMethods.FORMATETC pFormatectIn, NativeMethods.STGMEDIUM pmedium, int fRelease);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IEnumFORMATETC OleEnumFormatEtc([MarshalAs(UnmanagedType.U4)] [In] int dwDirection);
			int OleDAdvise(NativeMethods.FORMATETC pFormatetc, [MarshalAs(UnmanagedType.U4)] [In] int advf, [MarshalAs(UnmanagedType.Interface)] [In] object pAdvSink, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pdwConnection);
			int OleDUnadvise([MarshalAs(UnmanagedType.U4)] [In] int dwConnection);
			int OleEnumDAdvise([MarshalAs(UnmanagedType.LPArray)] [Out] object[] ppenumAdvise);
		}
		[ComVisible(true)]
		[Guid("B722BCC7-4E68-101B-A2BC-00AA00404770")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IOleDocumentSite
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ActivateMe([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleDocumentView pViewToActivate);
		}
		[Guid("B722BCC6-4E68-101B-A2BC-00AA00404770")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleDocumentView
		{
			void SetInPlaceSite([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleInPlaceSite pIPSite);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IOleInPlaceSite GetInPlaceSite();
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetDocument();
			void SetRect([In] NativeMethods.COMRECT prcView);
			void GetRect([Out] NativeMethods.COMRECT prcView);
			void SetRectComplex([In] NativeMethods.COMRECT prcView, [In] NativeMethods.COMRECT prcHScroll, [In] NativeMethods.COMRECT prcVScroll, [In] NativeMethods.COMRECT prcSizeBox);
			void Show([MarshalAs(UnmanagedType.I4)] [In] int fShow);
			void UIActivate([MarshalAs(UnmanagedType.I4)] [In] int fUIActivate);
			void Open();
			void CloseView([MarshalAs(UnmanagedType.U4)] [In] int dwReserved);
			void SaveViewState([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm);
			void ApplyViewState([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm);
			void Clone([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleInPlaceSite pIPSiteNew, [MarshalAs(UnmanagedType.LPArray)] [Out] NativeMethods.IOleDocumentView[] ppViewNew);
		}
		[ComVisible(true)]
		[Guid("00000121-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IOleDropSource
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int QueryContinueDrag([MarshalAs(UnmanagedType.I4)] [In] int fEscapePressed, [MarshalAs(UnmanagedType.U4)] [In] int grfKeyState);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GiveFeedback([MarshalAs(UnmanagedType.U4)] [In] int dwEffect);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[Guid("00000122-0000-0000-C000-000000000046")]
		[ComImport]
		internal interface IOleDropTarget
		{
			[PreserveSig]
			int OleDragEnter(IntPtr pDataObj, [MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [MarshalAs(UnmanagedType.U8)] [In] long pt, [In] [Out] ref int pdwEffect);
			[PreserveSig]
			int OleDragOver([MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [MarshalAs(UnmanagedType.U8)] [In] long pt, [In] [Out] ref int pdwEffect);
			[PreserveSig]
			int OleDragLeave();
			[PreserveSig]
			int OleDrop(IntPtr pDataObj, [MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [MarshalAs(UnmanagedType.U8)] [In] long pt, [In] [Out] ref int pdwEffect);
		}
		[Guid("00000117-0000-0000-C000-000000000046")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IOleInPlaceActiveObject
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetWindow(out IntPtr hwnd);
			void ContextSensitiveHelp([MarshalAs(UnmanagedType.I4)] [In] int fEnterMode);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int TranslateAccelerator([MarshalAs(UnmanagedType.LPStruct)] [In] NativeMethods.COMMSG lpmsg);
			void OnFrameWindowActivate([MarshalAs(UnmanagedType.I4)] [In] int fActivate);
			void OnDocWindowActivate([MarshalAs(UnmanagedType.I4)] [In] int fActivate);
			void ResizeBorder([In] NativeMethods.COMRECT prcBorder, [In] NativeMethods.IOleInPlaceUIWindow pUIWindow, [MarshalAs(UnmanagedType.I4)] [In] int fFrameWindow);
			void EnableModeless([MarshalAs(UnmanagedType.I4)] [In] int fEnable);
		}
		[ComVisible(true)]
		[Guid("00000116-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IOleInPlaceFrame
		{
			IntPtr GetWindow();
			void ContextSensitiveHelp([MarshalAs(UnmanagedType.I4)] [In] int fEnterMode);
			void GetBorder([Out] NativeMethods.COMRECT lprectBorder);
			void RequestBorderSpace([In] NativeMethods.COMRECT pborderwidths);
			void SetBorderSpace([In] NativeMethods.COMRECT pborderwidths);
			void SetActiveObject([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleInPlaceActiveObject pActiveObject, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszObjName);
			void InsertMenus([In] IntPtr hmenuShared, [In] [Out] NativeMethods.tagOleMenuGroupWidths lpMenuWidths);
			void SetMenu([In] IntPtr hmenuShared, [In] IntPtr holemenu, [In] IntPtr hwndActiveObject);
			void RemoveMenus([In] IntPtr hmenuShared);
			void SetStatusText([MarshalAs(UnmanagedType.BStr)] [In] string pszStatusText);
			void EnableModeless([MarshalAs(UnmanagedType.I4)] [In] int fEnable);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int TranslateAccelerator([MarshalAs(UnmanagedType.LPStruct)] [In] NativeMethods.COMMSG lpmsg, [MarshalAs(UnmanagedType.U2)] [In] short wID);
		}
		[Guid("00000113-0000-0000-C000-000000000046")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IOleInPlaceObject
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetWindow(out IntPtr hwnd);
			void ContextSensitiveHelp([MarshalAs(UnmanagedType.I4)] [In] int fEnterMode);
			void InPlaceDeactivate();
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int UIDeactivate();
			void SetObjectRects([In] NativeMethods.COMRECT lprcPosRect, [In] NativeMethods.COMRECT lprcClipRect);
			void ReactivateAndUndo();
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[Guid("00000119-0000-0000-C000-000000000046")]
		[ComImport]
		internal interface IOleInPlaceSite
		{
			IntPtr GetWindow();
			void ContextSensitiveHelp([MarshalAs(UnmanagedType.I4)] [In] int fEnterMode);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int CanInPlaceActivate();
			void OnInPlaceActivate();
			void OnUIActivate();
			void GetWindowContext(out NativeMethods.IOleInPlaceFrame ppFrame, out NativeMethods.IOleInPlaceUIWindow ppDoc, [Out] NativeMethods.COMRECT lprcPosRect, [Out] NativeMethods.COMRECT lprcClipRect, [In] [Out] NativeMethods.tagOIFI lpFrameInfo);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Scroll([MarshalAs(UnmanagedType.U4)] [In] NativeMethods.tagSIZE scrollExtent);
			void OnUIDeactivate([MarshalAs(UnmanagedType.I4)] [In] int fUndoable);
			void OnInPlaceDeactivate();
			void DiscardUndoState();
			void DeactivateAndUndo();
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OnPosRectChange([In] NativeMethods.COMRECT lprcPosRect);
		}
		[Guid("00000115-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		internal interface IOleInPlaceUIWindow
		{
			IntPtr GetWindow();
			void ContextSensitiveHelp([MarshalAs(UnmanagedType.I4)] [In] int fEnterMode);
			void GetBorder([Out] NativeMethods.COMRECT lprectBorder);
			void RequestBorderSpace([In] NativeMethods.COMRECT pborderwidths);
			void SetBorderSpace([In] NativeMethods.COMRECT pborderwidths);
			void SetActiveObject([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleInPlaceActiveObject pActiveObject, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszObjName);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[Guid("00000112-0000-0000-C000-000000000046")]
		[ComImport]
		internal interface IOleObject
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int SetClientSite([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleClientSite pClientSite);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientSite(out NativeMethods.IOleClientSite site);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int SetHostNames([MarshalAs(UnmanagedType.LPWStr)] [In] string szContainerApp, [MarshalAs(UnmanagedType.LPWStr)] [In] string szContainerObj);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Close([MarshalAs(UnmanagedType.I4)] [In] int dwSaveOption);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int SetMoniker([MarshalAs(UnmanagedType.U4)] [In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] [In] object pmk);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetMoniker([MarshalAs(UnmanagedType.U4)] [In] int dwAssign, [MarshalAs(UnmanagedType.U4)] [In] int dwWhichMoniker, out object moniker);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int InitFromData([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleDataObject pDataObject, [MarshalAs(UnmanagedType.I4)] [In] int fCreation, [MarshalAs(UnmanagedType.U4)] [In] int dwReserved);
			int GetClipboardData([MarshalAs(UnmanagedType.U4)] [In] int dwReserved, out NativeMethods.IOleDataObject data);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int DoVerb([MarshalAs(UnmanagedType.I4)] [In] int iVerb, [In] IntPtr lpmsg, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleClientSite pActiveSite, [MarshalAs(UnmanagedType.I4)] [In] int lindex, [In] IntPtr hwndParent, [In] NativeMethods.COMRECT lprcPosRect);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int EnumVerbs(out NativeMethods.IEnumOLEVERB e);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OleUpdate();
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int IsUpToDate();
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetUserClassID([In] [Out] ref Guid pClsid);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetUserType([MarshalAs(UnmanagedType.U4)] [In] int dwFormOfType, [MarshalAs(UnmanagedType.LPWStr)] out string userType);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int SetExtent([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, [In] NativeMethods.tagSIZEL pSizel);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetExtent([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, [Out] NativeMethods.tagSIZEL pSizel);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Advise([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IAdviseSink pAdvSink, out int cookie);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Unadvise([MarshalAs(UnmanagedType.U4)] [In] int dwConnection);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int EnumAdvise(out NativeMethods.IEnumSTATDATA e);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetMiscStatus([MarshalAs(UnmanagedType.U4)] [In] int dwAspect, out int misc);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int SetColorScheme([In] NativeMethods.tagLOGPALETTE pLogpal);
		}
		[Guid("894AD3B0-EF97-11CE-9BC9-00AA00608E01")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IOleUndoUnit
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Do([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleUndoManager undoManager);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetDescription([MarshalAs(UnmanagedType.BStr)] out string bStr);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetUnitType([MarshalAs(UnmanagedType.I4)] out int clsid, [MarshalAs(UnmanagedType.I4)] out int plID);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OnNextAdd();
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[Guid("A1FAF330-EF97-11CE-9BC9-00AA00608E01")]
		internal interface IOleParentUndoUnit : NativeMethods.IOleUndoUnit
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Open([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleParentUndoUnit parentUnit);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Close([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleParentUndoUnit parentUnit, [MarshalAs(UnmanagedType.Bool)] [In] bool fCommit);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Add([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleUndoUnit undoUnit);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int FindUnit([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleUndoUnit undoUnit);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetParentState([MarshalAs(UnmanagedType.I8)] out long state);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
		[ComVisible(true)]
		[ComImport]
		internal interface IOleServiceProvider
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int QueryService([In] ref Guid guidService, [In] ref Guid riid, out IntPtr ppvObject);
		}
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("D001F200-EF97-11CE-9BC9-00AA00608E01")]
		internal interface IOleUndoManager
		{
			void Open([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleParentUndoUnit parentUndo);
			void Close([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleParentUndoUnit parentUndo, [MarshalAs(UnmanagedType.Bool)] [In] bool fCommit);
			void Add([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleUndoUnit undoUnit);
			[return: MarshalAs(UnmanagedType.I8)]
			long GetOpenParentState();
			void DiscardFrom([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleUndoUnit undoUnit);
			void UndoTo([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleUndoUnit undoUnit);
			void RedoTo([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleUndoUnit undoUnit);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IEnumOleUndoUnits EnumUndoable();
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IEnumOleUndoUnits EnumRedoable();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetLastUndoDescription();
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetLastRedoDescription();
			void Enable([MarshalAs(UnmanagedType.Bool)] [In] bool fEnable);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[Guid("79eac9c9-baf9-11ce-8c82-00aa004ba90b")]
		internal interface IPersistMoniker
		{
			void GetClassID([In] [Out] ref Guid pClassID);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int IsDirty();
			void Load([In] int fFullyAvailable, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pmk, [MarshalAs(UnmanagedType.Interface)] [In] object pbc, [In] int grfMode);
			void Save([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pimkName, [MarshalAs(UnmanagedType.Interface)] [In] object pbc, [MarshalAs(UnmanagedType.Bool)] [In] bool fRemember);
			void SaveCompleted([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IMoniker pmk, [MarshalAs(UnmanagedType.Interface)] [In] object pbc);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IMoniker GetCurMoniker();
		}
		[Guid("7FD52380-4E07-101B-AE2D-08002B2EC713")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPersistStreamInit
		{
			void GetClassID([In] [Out] ref Guid pClassID);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int IsDirty();
			void Load([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm);
			void Save([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm, [MarshalAs(UnmanagedType.I4)] [In] int fClearDirty);
			void GetSizeMax([MarshalAs(UnmanagedType.LPArray)] [Out] long pcbSize);
			void InitNew();
		}
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("9BFBBC02-EFF1-101A-84ED-00AA00341D07")]
		public interface IPropertyNotifySink
		{
			void OnChanged(int dispID);
			void OnRequestEdit(int dispID);
		}
		[Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		public interface IServiceProvider
		{
			[return: MarshalAs(UnmanagedType.I4)]
			int QueryService([In] ref Guid sid, [In] ref Guid iid, out IntPtr service);
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[Guid("0000000C-0000-0000-C000-000000000046")]
		public interface IStream
		{
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Read([In] IntPtr buf, [MarshalAs(UnmanagedType.I4)] [In] int len);
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Write([In] IntPtr buf, [MarshalAs(UnmanagedType.I4)] [In] int len);
			[return: MarshalAs(UnmanagedType.I8)]
			long Seek([MarshalAs(UnmanagedType.I8)] [In] long dlibMove, [MarshalAs(UnmanagedType.I4)] [In] int dwOrigin);
			void SetSize([MarshalAs(UnmanagedType.I8)] [In] long libNewSize);
			[return: MarshalAs(UnmanagedType.I8)]
			long CopyTo([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.LPArray)] [Out] long[] pcbRead);
			void Commit([MarshalAs(UnmanagedType.I4)] [In] int grfCommitFlags);
			void Revert();
			void LockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.I4)] [In] int dwLockType);
			void UnlockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.I4)] [In] int dwLockType);
			void Stat([Out] NativeMethods.STATSTG pstatstg, [MarshalAs(UnmanagedType.I4)] [In] int grfStatFlag);
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IStream Clone();
		}
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
		[ComImport]
		internal interface IConnectionPoint
		{
			void GetConnectionInterface(out Guid interfaceIdentifier);
			void GetConnectionPointContainer(out NativeMethods.IConnectionPointContainer container);
			void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int cookie);
			void Unadvise(int cookie);
			void EnumConnections(out object enumerator);
		}
		[Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IConnectionPointContainer
		{
			void EnumConnectionPoints(out object enumerator);
			void FindConnectionPoint([In] ref Guid riid, out NativeMethods.IConnectionPoint connectionPoint);
		}
	}
}
