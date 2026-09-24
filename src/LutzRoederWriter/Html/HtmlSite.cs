using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Writer.Html
{
	[ClassInterface(ClassInterfaceType.None)]
	internal class HtmlSite : NativeMethods.IOleClientSite, NativeMethods.IOleContainer, NativeMethods.IOleDocumentSite, NativeMethods.IOleInPlaceSite, NativeMethods.IOleInPlaceFrame, NativeMethods.IDocHostUIHandler, NativeMethods.IPropertyNotifySink, NativeMethods.IAdviseSink, NativeMethods.IOleServiceProvider
	{
		public HtmlSite(HtmlControl hostControl)
		{
			if (hostControl == null || !hostControl.IsHandleCreated)
			{
				throw new ArgumentException("hostControl");
			}
			this.hostControl = hostControl;
			this.hostControl.Resize += this.HostControl_Resize;
		}
		public NativeMethods.IOleCommandTarget CommandTarget
		{
			get
			{
				return this.commandTarget;
			}
		}
		public NativeMethods.IHTMLDocument2 Document
		{
			get
			{
				return this.document;
			}
		}
		public void ActivateHtml()
		{
			Debug.Assert(this.oleObject != null, "How'd we get here when trident is null!");
			try
			{
				NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
				NativeMethods.GetClientRect(this.hostControl.Handle, comrect);
				this.oleObject.DoVerb(-4, NativeMethods.NullIntPtr, this, 0, this.hostControl.Handle, comrect);
			}
			catch (Exception ex)
			{
				Debug.Fail(ex.ToString());
			}
		}
		public void CloseHtml()
		{
			this.hostControl.Resize -= this.HostControl_Resize;
			try
			{
				if (this.propertyNotifySinkCookie != null)
				{
					this.propertyNotifySinkCookie.Disconnect();
					this.propertyNotifySinkCookie = null;
				}
				if (this.document != null)
				{
					this.documentView = null;
					this.document = null;
					this.commandTarget = null;
					this.activeObject = null;
					if (this.adviseSinkCookie != 0)
					{
						this.oleObject.Unadvise(this.adviseSinkCookie);
						this.adviseSinkCookie = 0;
					}
					this.oleObject.Close(1);
					this.oleObject.SetClientSite(null);
					this.oleObject = null;
				}
			}
			catch (Exception ex)
			{
				Debug.Fail(ex.ToString());
			}
		}
		public void CreateHtml()
		{
			Debug.Assert(this.document == null, "Must call CloseHtml before recreating.");
			bool flag = false;
			try
			{
				this.document = (NativeMethods.IHTMLDocument2)new NativeMethods.HTMLDocument();
				this.oleObject = (NativeMethods.IOleObject)this.document;
				this.oleObject.SetClientSite(this);
				flag = true;
				this.propertyNotifySinkCookie = new NativeMethods.ConnectionPointCookie(this.document, this, typeof(NativeMethods.IPropertyNotifySink), false);
				this.oleObject.Advise(this, out this.adviseSinkCookie);
				Debug.Assert(this.adviseSinkCookie != 0);
				this.commandTarget = (NativeMethods.IOleCommandTarget)this.document;
			}
			finally
			{
				if (!flag)
				{
					this.document = null;
					this.oleObject = null;
					this.commandTarget = null;
				}
			}
		}
		public void DeactivateHtml()
		{
		}
		private void HostControl_Resize(object src, EventArgs e)
		{
			if (this.documentView != null)
			{
				NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
				NativeMethods.GetClientRect(this.hostControl.Handle, comrect);
				this.documentView.SetRect(comrect);
			}
		}
		private void OnReadyStateChanged()
		{
			string readyState = this.document.GetReadyState();
			if (string.Compare(readyState, "complete", true) == 0)
			{
				this.OnReadyStateComplete();
			}
		}
		private void OnReadyStateComplete()
		{
			this.hostControl.OnReadyStateComplete(EventArgs.Empty);
		}
		internal void SetFocus()
		{
			if (this.activeObject != null)
			{
				IntPtr zero = IntPtr.Zero;
				if (this.activeObject.GetWindow(out zero) == 0)
				{
					Debug.Assert(zero != IntPtr.Zero);
					NativeMethods.SetFocus(zero);
				}
			}
		}
		public bool TranslateAccelarator(NativeMethods.COMMSG msg)
		{
			if (this.activeObject != null)
			{
				if (this.activeObject.TranslateAccelerator(msg) != 1)
				{
					return true;
				}
			}
			return false;
		}
		int NativeMethods.IOleClientSite.SaveObject()
		{
			return 0;
		}
		int NativeMethods.IOleClientSite.GetMoniker(int dwAssign, int dwWhichMoniker, out object ppmk)
		{
			ppmk = null;
			return -2147467263;
		}
		int NativeMethods.IOleClientSite.GetContainer(out NativeMethods.IOleContainer ppContainer)
		{
			ppContainer = this;
			return 0;
		}
		int NativeMethods.IOleClientSite.ShowObject()
		{
			return 0;
		}
		int NativeMethods.IOleClientSite.OnShowWindow(int fShow)
		{
			return 0;
		}
		int NativeMethods.IOleClientSite.RequestNewObjectLayout()
		{
			return 0;
		}
		void NativeMethods.IOleContainer.ParseDisplayName(object pbc, string pszDisplayName, int[] pchEaten, object[] ppmkOut)
		{
			Debug.Fail("ParseDisplayName - " + pszDisplayName);
			throw new COMException(string.Empty, -2147467263);
		}
		void NativeMethods.IOleContainer.EnumObjects(int grfFlags, object[] ppenum)
		{
			throw new COMException(string.Empty, -2147467263);
		}
		void NativeMethods.IOleContainer.LockContainer(int fLock)
		{
		}
		int NativeMethods.IOleDocumentSite.ActivateMe(NativeMethods.IOleDocumentView viewToActivate)
		{
			Debug.Assert(viewToActivate != null, "Expected the view to be non-null");
			int num;
			if (viewToActivate == null)
			{
				num = -2147024809;
			}
			else
			{
				NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
				NativeMethods.GetClientRect(this.hostControl.Handle, comrect);
				this.documentView = viewToActivate;
				this.documentView.SetInPlaceSite(this);
				this.documentView.UIActivate(1);
				this.documentView.SetRect(comrect);
				this.documentView.Show(1);
				num = 0;
			}
			return num;
		}
		IntPtr NativeMethods.IOleInPlaceSite.GetWindow()
		{
			return this.hostControl.Handle;
		}
		void NativeMethods.IOleInPlaceSite.ContextSensitiveHelp(int fEnterMode)
		{
			throw new COMException(string.Empty, -2147467263);
		}
		int NativeMethods.IOleInPlaceSite.CanInPlaceActivate()
		{
			return 0;
		}
		void NativeMethods.IOleInPlaceSite.OnInPlaceActivate()
		{
		}
		void NativeMethods.IOleInPlaceSite.OnUIActivate()
		{
		}
		void NativeMethods.IOleInPlaceSite.GetWindowContext(out NativeMethods.IOleInPlaceFrame ppFrame, out NativeMethods.IOleInPlaceUIWindow ppDoc, NativeMethods.COMRECT lprcPosRect, NativeMethods.COMRECT lprcClipRect, NativeMethods.tagOIFI lpFrameInfo)
		{
			ppFrame = this;
			ppDoc = null;
			NativeMethods.GetClientRect(this.hostControl.Handle, lprcPosRect);
			NativeMethods.GetClientRect(this.hostControl.Handle, lprcClipRect);
			lpFrameInfo.cb = Marshal.SizeOf(typeof(NativeMethods.tagOIFI));
			lpFrameInfo.fMDIApp = 0;
			lpFrameInfo.hwndFrame = this.hostControl.Handle;
			lpFrameInfo.hAccel = NativeMethods.NullIntPtr;
			lpFrameInfo.cAccelEntries = 0;
		}
		int NativeMethods.IOleInPlaceSite.Scroll(NativeMethods.tagSIZE scrollExtant)
		{
			return -2147467263;
		}
		void NativeMethods.IOleInPlaceSite.OnUIDeactivate(int fUndoable)
		{
		}
		void NativeMethods.IOleInPlaceSite.OnInPlaceDeactivate()
		{
		}
		void NativeMethods.IOleInPlaceSite.DiscardUndoState()
		{
			throw new COMException(string.Empty, -2147467263);
		}
		void NativeMethods.IOleInPlaceSite.DeactivateAndUndo()
		{
		}
		int NativeMethods.IOleInPlaceSite.OnPosRectChange(NativeMethods.COMRECT lprcPosRect)
		{
			return 0;
		}
		IntPtr NativeMethods.IOleInPlaceFrame.GetWindow()
		{
			return this.hostControl.Handle;
		}
		void NativeMethods.IOleInPlaceFrame.ContextSensitiveHelp(int fEnterMode)
		{
			throw new COMException(string.Empty, -2147467263);
		}
		void NativeMethods.IOleInPlaceFrame.GetBorder(NativeMethods.COMRECT lprectBorder)
		{
			throw new COMException(string.Empty, -2147467263);
		}
		void NativeMethods.IOleInPlaceFrame.RequestBorderSpace(NativeMethods.COMRECT pborderwidths)
		{
			throw new COMException(string.Empty, -2147467263);
		}
		void NativeMethods.IOleInPlaceFrame.SetBorderSpace(NativeMethods.COMRECT pborderwidths)
		{
			throw new COMException(string.Empty, -2147467263);
		}
		void NativeMethods.IOleInPlaceFrame.SetActiveObject(NativeMethods.IOleInPlaceActiveObject pActiveObject, string pszObjName)
		{
			this.activeObject = pActiveObject;
		}
		void NativeMethods.IOleInPlaceFrame.InsertMenus(IntPtr hmenuShared, NativeMethods.tagOleMenuGroupWidths lpMenuWidths)
		{
			throw new COMException(string.Empty, -2147467263);
		}
		void NativeMethods.IOleInPlaceFrame.SetMenu(IntPtr hmenuShared, IntPtr holemenu, IntPtr hwndActiveObject)
		{
			throw new COMException(string.Empty, -2147467263);
		}
		void NativeMethods.IOleInPlaceFrame.RemoveMenus(IntPtr hmenuShared)
		{
			throw new COMException(string.Empty, -2147467263);
		}
		void NativeMethods.IOleInPlaceFrame.SetStatusText(string pszStatusText)
		{
		}
		void NativeMethods.IOleInPlaceFrame.EnableModeless(int fEnable)
		{
		}
		int NativeMethods.IOleInPlaceFrame.TranslateAccelerator(NativeMethods.COMMSG lpmsg, short wID)
		{
			return 1;
		}
		int NativeMethods.IDocHostUIHandler.ShowContextMenu(int dwID, NativeMethods.POINT pt, object pcmdtReserved, object pdispReserved)
		{
			if (this.hostControl != null)
			{
				Point point = this.hostControl.PointToClient(new Point(pt.x, pt.y));
				this.hostControl.ContextMenu.Show(this.hostControl, point);
			}
			return 0;
		}
		int NativeMethods.IDocHostUIHandler.GetHostInfo(NativeMethods.DOCHOSTUIINFO info)
		{
			info.dwDoubleClick = 0;
			int num = 132;
			if (!this.hostControl.ScriptEnabled)
			{
				num |= 16;
			}
			info.dwFlags = num;
			return 0;
		}
		int NativeMethods.IDocHostUIHandler.EnableModeless(bool fEnable)
		{
			return 0;
		}
		int NativeMethods.IDocHostUIHandler.ShowUI(int dwID, NativeMethods.IOleInPlaceActiveObject activeObject, NativeMethods.IOleCommandTarget commandTarget, NativeMethods.IOleInPlaceFrame frame, NativeMethods.IOleInPlaceUIWindow doc)
		{
			return 0;
		}
		int NativeMethods.IDocHostUIHandler.HideUI()
		{
			return 0;
		}
		int NativeMethods.IDocHostUIHandler.UpdateUI()
		{
			return 0;
		}
		int NativeMethods.IDocHostUIHandler.OnDocWindowActivate(bool fActivate)
		{
			return -2147467263;
		}
		int NativeMethods.IDocHostUIHandler.OnFrameWindowActivate(bool fActivate)
		{
			return -2147467263;
		}
		int NativeMethods.IDocHostUIHandler.ResizeBorder(NativeMethods.COMRECT rect, NativeMethods.IOleInPlaceUIWindow doc, bool fFrameWindow)
		{
			return -2147467263;
		}
		int NativeMethods.IDocHostUIHandler.GetOptionKeyPath(string[] pbstrKey, int dw)
		{
			pbstrKey[0] = null;
			return 0;
		}
		int NativeMethods.IDocHostUIHandler.GetDropTarget(NativeMethods.IOleDropTarget pDropTarget, out NativeMethods.IOleDropTarget ppDropTarget)
		{
			ppDropTarget = new HtmlSite.DropTarget(new HtmlSite.DataObjectConverter(), pDropTarget);
			return (ppDropTarget != null) ? 0 : (-2147467263);
		}
		int NativeMethods.IDocHostUIHandler.GetExternal(out object ppDispatch)
		{
			ppDispatch = this.hostControl.ScriptObject;
			int num;
			if (ppDispatch != null)
			{
				num = 0;
			}
			else
			{
				num = -2147467263;
			}
			return num;
		}
		int NativeMethods.IDocHostUIHandler.TranslateAccelerator(NativeMethods.COMMSG msg, ref Guid group, int nCmdID)
		{
			return 1;
		}
		int NativeMethods.IDocHostUIHandler.TranslateUrl(int dwTranslate, string strURLIn, out string pstrURLOut)
		{
			pstrURLOut = null;
			return -2147467263;
		}
		int NativeMethods.IDocHostUIHandler.FilterDataObject(NativeMethods.IOleDataObject pDO, out NativeMethods.IOleDataObject ppDORet)
		{
			ppDORet = null;
			return -2147467263;
		}
		void NativeMethods.IPropertyNotifySink.OnChanged(int dispID)
		{
			if (dispID == -525)
			{
				this.OnReadyStateChanged();
			}
		}
		void NativeMethods.IPropertyNotifySink.OnRequestEdit(int dispID)
		{
		}
		void NativeMethods.IAdviseSink.OnDataChange(NativeMethods.FORMATETC pFormat, NativeMethods.STGMEDIUM pStg)
		{
		}
		void NativeMethods.IAdviseSink.OnViewChange(int dwAspect, int index)
		{
		}
		void NativeMethods.IAdviseSink.OnRename(object pmk)
		{
		}
		void NativeMethods.IAdviseSink.OnSave()
		{
		}
		void NativeMethods.IAdviseSink.OnClose()
		{
		}
		int NativeMethods.IOleServiceProvider.QueryService(ref Guid sid, ref Guid iid, out IntPtr ppvObject)
		{
			int num = -2147467262;
			ppvObject = NativeMethods.NullIntPtr;
			return num;
		}
		private HtmlControl hostControl;
		private NativeMethods.IOleObject oleObject;
		private NativeMethods.IHTMLDocument2 document;
		private NativeMethods.IOleDocumentView documentView;
		private NativeMethods.IOleCommandTarget commandTarget;
		private NativeMethods.IOleInPlaceActiveObject activeObject;
		private NativeMethods.ConnectionPointCookie propertyNotifySinkCookie;
		private int adviseSinkCookie;
		internal enum ConverterInfo
		{
			No,
			Yes,
			Unknown
		}
		internal class DataObjectConverter
		{
			public HtmlSite.ConverterInfo CanConvertToHtml(IDataObject dataObject)
			{
				HtmlSite.ConverterInfo converterInfo;
				if (dataObject.GetDataPresent("FileDrop"))
				{
					converterInfo = HtmlSite.ConverterInfo.Yes;
				}
				else
				{
					converterInfo = HtmlSite.ConverterInfo.Unknown;
				}
				return converterInfo;
			}
			public bool ConvertToHtml(IDataObject originalDataObject, DataObject newDataObject)
			{
				bool flag;
				if (originalDataObject.GetDataPresent("FileDrop"))
				{
					string[] array = (string[])originalDataObject.GetData("FileDrop");
					if (array.Length == 1)
					{
						StreamReader streamReader = new StreamReader(array[0]);
						string text = streamReader.ReadToEnd();
						streamReader.Close();
						newDataObject.SetData("HTML", text);
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
				return flag;
			}
		}
		private sealed class DropTarget : NativeMethods.IOleDropTarget
		{
			public DropTarget(HtmlSite.DataObjectConverter converter, NativeMethods.IOleDropTarget originalDropTarget)
			{
				this.converter = converter;
				this.originalDropTarget = originalDropTarget;
			}
			public int OleDragEnter(IntPtr pDataObj, int grfKeyState, long pt, ref int pdwEffect)
			{
				object objectForIUnknown = Marshal.GetObjectForIUnknown(pDataObj);
				DataObject dataObject = new DataObject(objectForIUnknown);
				this.converterInfo = this.converter.CanConvertToHtml(dataObject);
				int num;
				if (this.converterInfo == HtmlSite.ConverterInfo.Yes)
				{
					this.currentDataObj = new DataObject(DataFormats.Html, string.Empty);
					IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this.currentDataObj);
					Guid guid = new Guid("0000010E-0000-0000-C000-000000000046");
					Marshal.QueryInterface(iunknownForObject, ref guid, out this.currentDataObjPtr);
					Marshal.Release(iunknownForObject);
					num = this.originalDropTarget.OleDragEnter(this.currentDataObjPtr, grfKeyState, pt, ref pdwEffect);
				}
				else
				{
					if (this.converterInfo == HtmlSite.ConverterInfo.No)
					{
						pdwEffect = 0;
					}
					else
					{
						if (this.converterInfo == HtmlSite.ConverterInfo.Unknown)
						{
							return this.originalDropTarget.OleDragEnter(pDataObj, grfKeyState, pt, ref pdwEffect);
						}
						Debug.Fail("Unknown ConverterInfo value!");
					}
					num = 0;
				}
				return num;
			}
			public int OleDragOver(int grfKeyState, long pt, ref int pdwEffect)
			{
				int num;
				if (this.converterInfo != HtmlSite.ConverterInfo.No)
				{
					num = this.originalDropTarget.OleDragOver(grfKeyState, pt, ref pdwEffect);
				}
				else
				{
					pdwEffect = 0;
					num = 0;
				}
				return num;
			}
			public int OleDragLeave()
			{
				this.converterInfo = HtmlSite.ConverterInfo.No;
				int num;
				if (this.currentDataObj != null)
				{
					this.currentDataObj = null;
					Marshal.Release(this.currentDataObjPtr);
					this.currentDataObjPtr = IntPtr.Zero;
					num = this.originalDropTarget.OleDragLeave();
				}
				else
				{
					num = 0;
				}
				return num;
			}
			public int OleDrop(IntPtr pDataObj, int grfKeyState, long pt, ref int pdwEffect)
			{
				int num = 0;
				if (this.converterInfo == HtmlSite.ConverterInfo.Yes)
				{
					object objectForIUnknown = Marshal.GetObjectForIUnknown(pDataObj);
					DataObject dataObject = new DataObject(objectForIUnknown);
					bool flag = this.converter.ConvertToHtml(dataObject, this.currentDataObj);
					if (flag)
					{
						IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this.currentDataObj);
						Guid guid = new Guid("0000010E-0000-0000-C000-000000000046");
						Marshal.QueryInterface(iunknownForObject, ref guid, out this.currentDataObjPtr);
						num = this.originalDropTarget.OleDrop(this.currentDataObjPtr, grfKeyState, pt, ref pdwEffect);
						this.currentDataObj = null;
						this.currentDataObjPtr = IntPtr.Zero;
						Marshal.Release(this.currentDataObjPtr);
					}
					else
					{
						pdwEffect = 0;
					}
				}
				else if (this.converterInfo == HtmlSite.ConverterInfo.Unknown)
				{
					num = this.originalDropTarget.OleDrop(pDataObj, grfKeyState, pt, ref pdwEffect);
				}
				this.converterInfo = HtmlSite.ConverterInfo.No;
				return num;
			}
			private DataObject currentDataObj;
			private IntPtr currentDataObjPtr;
			private NativeMethods.IOleDropTarget originalDropTarget;
			private HtmlSite.DataObjectConverter converter;
			private HtmlSite.ConverterInfo converterInfo;
		}
	}
}
