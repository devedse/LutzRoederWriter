using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace Writer.Html
{
	/// <summary>
	/// Hosts the Windows MSHTML document editor and exposes loading, saving, selection,
	/// formatting, clipboard, printing, and editing operations.
	/// </summary>
	/// <remarks>
	/// This control is Windows-only and must be created on an STA thread with a running
	/// Windows Forms message loop. Wait for <see cref="ReadyStateComplete"/> or check
	/// <see cref="IsReady"/> before invoking document-dependent operations.
	/// </remarks>
	public class HtmlControl : Control
	{
		/// <summary>
		/// Initializes an empty HTML editing control.
		/// </summary>
		public HtmlControl()
		{
			this.firstActivation = true;
			base.TabStop = true;
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.url != null)
				{
					HtmlControl.UrlMap[this.url] = null;
				}
			}
			base.Dispose(disposing);
		}
		/// <summary>
		/// Occurs when the hosted MSHTML document has completed initialization.
		/// </summary>
		public event EventHandler ReadyStateComplete
		{
			add
			{
				base.Events.AddHandler(HtmlControl.readyStateCompleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlControl.readyStateCompleteEvent, value);
			}
		}
		protected bool IsCreated
		{
			get
			{
				return this.isCreated;
			}
		}
		/// <summary>
		/// Gets a value indicating whether the hosted document is ready for editing.
		/// </summary>
		public bool IsReady
		{
			get
			{
				return this.isReady;
			}
		}
		internal NativeMethods.IHTMLDocument2 HtmlDocument
		{
			get
			{
				return this.site.Document;
			}
		}
		internal NativeMethods.IOleCommandTarget CommandTarget
		{
			get
			{
				return this.site.CommandTarget;
			}
		}
		/// <summary>
		/// Gets or sets whether scripts in the hosted document may execute.
		/// </summary>
		public bool ScriptEnabled
		{
			get
			{
				return this.scriptEnabled;
			}
			set
			{
				this.scriptEnabled = value;
			}
		}
		/// <summary>
		/// Gets or sets the object exposed to scripts hosted by the document.
		/// </summary>
		public object ScriptObject
		{
			get
			{
				return this.scriptObject;
			}
			set
			{
				this.scriptObject = value;
			}
		}
		public virtual string Url
		{
			get
			{
				return this.url;
			}
		}
		internal static IDictionary UrlMap
		{
			get
			{
				if (HtmlControl.urlMap == null)
				{
					HtmlControl.urlMap = new HybridDictionary(true);
				}
				return HtmlControl.urlMap;
			}
		}
		internal object Execute(int command)
		{
			return this.Execute(command, null);
		}
		internal object Execute(int command, object[] arguments)
		{
			object[] array = new object[1];
			object[] array2 = array;
			int num = this.CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, command, 2, arguments, array2);
			if (num != 0)
			{
				throw new Exception("Execution of MSHTML command ID '" + command + "' failed.");
			}
			return array2[0];
		}
		internal object ExecuteWithUserInterface(int command, object[] arguments)
		{
			object[] array = new object[1];
			object[] array2 = array;
			int num = this.CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, command, 1, arguments, array2);
			if (num != 0)
			{
				throw new Exception("Execution of MSHTML command ID '" + command + "' failed.");
			}
			return array2[0];
		}
		internal bool IsEnabled(int commandId)
		{
			return (this.GetCommandInfo(commandId) & 1) != 0;
		}
		internal bool IsChecked(int commandId)
		{
			return (this.GetCommandInfo(commandId) & 2) != 0;
		}
		internal int GetCommandInfo(int commandId)
		{
			NativeMethods.tagOLECMD tagOLECMD = new NativeMethods.tagOLECMD();
			tagOLECMD.cmdID = commandId;
			this.CommandTarget.QueryStatus(ref NativeMethods.Guid_MSHTML, 1, tagOLECMD, 0);
			return tagOLECMD.cmdf >> 1;
		}
		/// <summary>
		/// Finds an element by its HTML <c>id</c> attribute.
		/// </summary>
		/// <param name="id">The element identifier.</param>
		/// <returns>The matching element, or <see langword="null"/> when no element exists.</returns>
		public HtmlElement GetElementByID(string id)
		{
			NativeMethods.IHTMLElement body = this.site.Document.GetBody();
			NativeMethods.IHTMLElementCollection ihtmlelementCollection = (NativeMethods.IHTMLElementCollection)body.GetAll();
			NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)ihtmlelementCollection.Item(id, 0);
			HtmlElement htmlElement;
			if (ihtmlelement == null)
			{
				htmlElement = null;
			}
			else
			{
				htmlElement = new HtmlElement(ihtmlelement, this);
			}
			return htmlElement;
		}
		/// <summary>
		/// Loads HTML content from a stream.
		/// </summary>
		/// <param name="stream">A readable stream containing HTML text.</param>
		public void LoadHtml(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("LoadHtml : You must specify a non-null stream for content");
			}
			StreamReader streamReader = new StreamReader(stream);
			this.LoadHtml(streamReader.ReadToEnd());
		}
		/// <summary>
		/// Loads HTML content without assigning a base URL.
		/// </summary>
		/// <param name="content">The HTML content.</param>
		public void LoadHtml(string content)
		{
			this.LoadHtml(content, null);
		}
		/// <summary>
		/// Loads HTML content and assigns the URL used to resolve relative links.
		/// </summary>
		/// <param name="content">The HTML content.</param>
		/// <param name="url">The document URL, or <see langword="null"/> when no base URL is required.</param>
		public void LoadHtml(string content, string url)
		{
			if (content == null)
			{
				content = "";
			}
			if (!this.isCreated)
			{
				this.desiredContent = content;
				this.desiredUrl = url;
				this.desiredLoad = true;
			}
			else
			{
				NativeMethods.IStream stream = null;
				IntPtr intPtr = Marshal.StringToHGlobalUni(content);
				NativeMethods.CreateStreamOnHGlobal(intPtr, true, out stream);
				if (stream == null)
				{
					NativeMethods.IPersistStreamInit persistStreamInit = (NativeMethods.IPersistStreamInit)this.site.Document;
					Debug.Assert(persistStreamInit != null, "Expected IPersistStreamInit");
					persistStreamInit.InitNew();
				}
				else
				{
					NativeMethods.IHTMLDocument2 document = this.site.Document;
					if (url == null)
					{
						NativeMethods.IPersistStreamInit persistStreamInit = (NativeMethods.IPersistStreamInit)document;
						Debug.Assert(persistStreamInit != null, "Expected IPersistStreamInit");
						persistStreamInit.Load(stream);
					}
					else
					{
						NativeMethods.IPersistMoniker persistMoniker = (NativeMethods.IPersistMoniker)document;
						NativeMethods.IMoniker moniker = null;
						NativeMethods.CreateURLMoniker(null, url, out moniker);
						NativeMethods.IBindCtx bindCtx = null;
						NativeMethods.CreateBindCtx(0, out bindCtx);
						persistMoniker.Load(1, moniker, bindCtx, 0);
						moniker = null;
						bindCtx = null;
					}
				}
				this.url = url;
			}
		}
		protected virtual void OnCreated(EventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("You must specify a non-null EventArgs for OnCreated");
			}
			object[] array = new object[] { true };
			this.CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, 7100, 0, array, null);
			array[0] = true;
			this.CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, 7101, 0, array, null);
			array[0] = true;
			this.CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, 6049, 0, array, null);
			array[0] = true;
			this.CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, 2332, 0, array, null);
			array[0] = true;
			this.CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, 2333, 0, array, null);
			array[0] = true;
			this.CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, 2334, 0, array, null);
			array[0] = true;
			this.CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, 2335, 0, array, null);
			if (this.designModeDesired)
			{
				this.IsDesignMode = this.designModeDesired;
				this.designModeDesired = false;
			}
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (this.IsReady)
			{
				this.site.SetFocus();
			}
			else
			{
				this.focusDesired = true;
			}
		}
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (this.firstActivation)
			{
				this.site = new HtmlSite(this);
				this.site.CreateHtml();
				this.isCreated = true;
				this.OnCreated(EventArgs.Empty);
				this.site.ActivateHtml();
				this.firstActivation = false;
				if (this.desiredLoad)
				{
					this.LoadHtml(this.desiredContent, this.desiredUrl);
					this.desiredLoad = false;
				}
			}
		}
		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.site.DeactivateHtml();
			this.site.CloseHtml();
			this.site = null;
			base.OnHandleDestroyed(e);
		}
		protected internal virtual void OnReadyStateComplete(EventArgs e)
		{
			this.isReady = true;
			EventHandler eventHandler = (EventHandler)base.Events[HtmlControl.readyStateCompleteEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.focusDesired)
			{
				this.focusDesired = false;
				this.site.ActivateHtml();
				this.site.SetFocus();
			}
			this.persistStream = (NativeMethods.IPersistStreamInit)this.HtmlDocument;
			this.Selection.SynchronizeSelection();
			if (this.multipleSelectionDesired)
			{
				this.MultipleSelectionEnabled = this.multipleSelectionDesired;
			}
			if (this.absolutePositioningDesired)
			{
				this.AbsolutePositioningEnabled = this.absolutePositioningDesired;
			}
		}
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public override bool PreProcessMessage(ref Message m)
		{
			bool flag = false;
			if (m.Msg >= 256 && m.Msg <= 264)
			{
				if (m.Msg == 256)
				{
					flag = this.ProcessCmdKey(ref m, (Keys)((int)m.WParam | (int)Control.ModifierKeys));
				}
				if (!flag)
				{
					int num = (int)m.WParam;
					if ((num != 33 && num != 34) || (Control.ModifierKeys & Keys.Control) == Keys.None)
					{
						NativeMethods.COMMSG commsg = new NativeMethods.COMMSG();
						commsg.hwnd = m.HWnd;
						commsg.message = m.Msg;
						commsg.wParam = m.WParam;
						commsg.lParam = m.LParam;
						flag = this.site.TranslateAccelarator(commsg);
					}
					else
					{
						this.WndProc(ref m);
						flag = true;
					}
				}
			}
			if (!flag)
			{
				flag = base.PreProcessMessage(ref m);
			}
			return flag;
		}
		/// <summary>
		/// Serializes the current document as formatted HTML.
		/// </summary>
		/// <returns>The current HTML document.</returns>
		public string SaveHtml()
		{
			if (!this.IsCreated)
			{
				throw new Exception("HtmlControl.SaveHtml : No HTML to save!");
			}
			string text = string.Empty;
			try
			{
				NativeMethods.IHTMLDocument2 document = this.site.Document;
				NativeMethods.IPersistStreamInit persistStreamInit = (NativeMethods.IPersistStreamInit)document;
				Debug.Assert(persistStreamInit != null, "Expected IPersistStreamInit");
				NativeMethods.IStream stream = null;
				NativeMethods.CreateStreamOnHGlobal(NativeMethods.NullIntPtr, true, out stream);
				persistStreamInit.Save(stream, 1);
				NativeMethods.STATSTG statstg = new NativeMethods.STATSTG();
				stream.Stat(statstg, 1);
				int num = (int)statstg.cbSize;
				byte[] array = new byte[num];
				IntPtr intPtr;
				NativeMethods.GetHGlobalFromStream(stream, out intPtr);
				Debug.Assert(intPtr != NativeMethods.NullIntPtr, "Failed in GetHGlobalFromStream");
				IntPtr intPtr2 = NativeMethods.GlobalLock(intPtr);
				if (intPtr2 != NativeMethods.NullIntPtr)
				{
					Marshal.Copy(intPtr2, array, 0, num);
					NativeMethods.GlobalUnlock(intPtr);
					StreamReader streamReader = null;
					try
					{
						streamReader = new StreamReader(new MemoryStream(array), Encoding.Default);
						text = streamReader.ReadToEnd();
					}
					finally
					{
						if (streamReader != null)
						{
							streamReader.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Fail("HtmlControl.SaveHtml" + ex.ToString());
				text = string.Empty;
			}
			finally
			{
			}
			if (text == null)
			{
				text = string.Empty;
			}
			return text;
		}
		/// <summary>
		/// Serializes the current document to a stream.
		/// </summary>
		/// <param name="stream">The writable destination stream.</param>
		public void SaveHtml(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("SaveHtml : Must specify a non-null stream to which to save");
			}
			string text = this.SaveHtml();
			StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);
			streamWriter.Write(text);
			streamWriter.Flush();
		}
		public bool CanPrint
		{
			get
			{
				return this.IsEnabled(27);
			}
		}
		public void Print()
		{
			this.ExecuteWithUserInterface(27, null);
		}
		public bool CanPrintPreview
		{
			get
			{
				return this.IsEnabled(2003);
			}
		}
		public void PrintPreview()
		{
			this.ExecuteWithUserInterface(2003, null);
		}
		public bool CanCopy
		{
			get
			{
				return this.IsEnabled(15);
			}
		}
		public void Copy()
		{
			if (!this.CanCopy)
			{
				throw new InvalidOperationException();
			}
			this.Execute(15);
		}
		public bool CanCut
		{
			get
			{
				return this.IsEnabled(16);
			}
		}
		public void Cut()
		{
			if (!this.CanCut)
			{
				throw new InvalidOperationException();
			}
			this.Execute(16);
		}
		public bool CanPaste
		{
			get
			{
				return this.IsEnabled(26);
			}
		}
		public void Paste()
		{
			if (!this.CanPaste)
			{
				throw new InvalidOperationException();
			}
			this.Execute(26);
		}
		public bool CanDelete
		{
			get
			{
				return this.IsEnabled(17);
			}
		}
		public void Delete()
		{
			if (!this.CanDelete)
			{
				throw new InvalidOperationException();
			}
			this.Execute(17);
		}
		public bool CanRedo
		{
			get
			{
				return this.IsEnabled(29);
			}
		}
		public void Redo()
		{
			if (!this.CanRedo)
			{
				throw new InvalidOperationException();
			}
			this.Execute(29);
		}
		public string RedoDescription
		{
			get
			{
				return this.CanRedo ? this.UndoManager.GetLastRedoDescription() : string.Empty;
			}
		}
		public bool CanUndo
		{
			get
			{
				return this.IsEnabled(43);
			}
		}
		public void Undo()
		{
			if (!this.CanUndo)
			{
				throw new InvalidOperationException();
			}
			this.Execute(43);
		}
		public string UndoDescription
		{
			get
			{
				return this.CanUndo ? this.UndoManager.GetLastUndoDescription() : string.Empty;
			}
		}
		public bool CanSelectAll
		{
			get
			{
				return this.IsEnabled(31);
			}
		}
		public void SelectAll()
		{
			this.Execute(31);
		}
		public bool AbsolutePositioningEnabled
		{
			get
			{
				return this.absolutePositioningEnabled;
			}
			set
			{
				this.absolutePositioningDesired = value;
				if (this.IsCreated)
				{
					this.absolutePositioningEnabled = value;
					object[] array = new object[] { this.absolutePositioningEnabled };
					this.Execute(2394, array);
				}
			}
		}
		public bool IsDesignMode
		{
			get
			{
				return this.isDesignMode;
			}
			set
			{
				if (this.isDesignMode != value)
				{
					if (!this.IsCreated)
					{
						this.designModeDesired = value;
					}
					else
					{
						this.isDesignMode = value;
						this.HtmlDocument.SetDesignMode(this.isDesignMode ? "on" : "off");
					}
				}
			}
		}
		private NativeMethods.IHTMLEditServices MSHTMLEditServices
		{
			get
			{
				if (this.editServices == null)
				{
					NativeMethods.IServiceProvider serviceProvider = this.HtmlDocument as NativeMethods.IServiceProvider;
					Debug.Assert(serviceProvider != null);
					Guid guid = new Guid(810612729U, 39093, 4559, 187, 130, 0, 170, 0, 189, 206, 11);
					Guid guid2 = typeof(NativeMethods.IHTMLEditServices).GUID;
					IntPtr nullIntPtr = NativeMethods.NullIntPtr;
					int num = serviceProvider.QueryService(ref guid, ref guid2, out nullIntPtr);
					Debug.Assert(num == 0 && nullIntPtr != NativeMethods.NullIntPtr, "Did not get IHTMLEditService");
					if (num == 0 && nullIntPtr != NativeMethods.NullIntPtr)
					{
						this.editServices = (NativeMethods.IHTMLEditServices)Marshal.GetObjectForIUnknown(nullIntPtr);
						Marshal.Release(nullIntPtr);
					}
				}
				return this.editServices;
			}
		}
		public virtual bool IsDirty
		{
			get
			{
				if (this.IsDesignMode && this.IsReady && this.persistStream != null)
				{
					if (this.persistStream.IsDirty() == 0)
					{
						return true;
					}
				}
				return false;
			}
			set
			{
				if (this.IsReady)
				{
					this.Execute(2342, new object[] { value });
				}
			}
		}
		public bool MultipleSelectionEnabled
		{
			get
			{
				return this.multipleSelectionEnabled;
			}
			set
			{
				this.multipleSelectionDesired = value;
				if (this.IsReady)
				{
					this.multipleSelectionEnabled = value;
					object[] array = new object[] { this.multipleSelectionEnabled };
					int num = this.CommandTarget.Exec(ref NativeMethods.Guid_MSHTML, 2393, 0, array, null);
					Debug.Assert(num == 0);
				}
			}
		}
		private NativeMethods.IOleUndoManager UndoManager
		{
			get
			{
				if (this.undoManager == null)
				{
					NativeMethods.IServiceProvider serviceProvider = this.HtmlDocument as NativeMethods.IServiceProvider;
					Debug.Assert(serviceProvider != null);
					Guid guid = typeof(NativeMethods.IOleUndoManager).GUID;
					Guid guid2 = typeof(NativeMethods.IOleUndoManager).GUID;
					IntPtr nullIntPtr = NativeMethods.NullIntPtr;
					if (serviceProvider.QueryService(ref guid2, ref guid, out nullIntPtr) == 0 && nullIntPtr != NativeMethods.NullIntPtr)
					{
						this.undoManager = (NativeMethods.IOleUndoManager)Marshal.GetObjectForIUnknown(nullIntPtr);
						Marshal.Release(nullIntPtr);
					}
				}
				return this.undoManager;
			}
		}
		public bool Find(string searchString, bool matchCase, bool wholeWord, bool searchUp)
		{
			NativeMethods.IHTMLSelectionObject ihtmlselectionObject = this.site.Document.GetSelection();
			bool flag = false;
			if (ihtmlselectionObject != null)
			{
				flag = ihtmlselectionObject.GetSelectionType().Equals("Text");
			}
			NativeMethods.IHTMLTxtRange ihtmltxtRange = null;
			if (flag)
			{
				object obj = ihtmlselectionObject.CreateRange();
				ihtmltxtRange = obj as NativeMethods.IHTMLTxtRange;
			}
			if (ihtmltxtRange == null)
			{
				NativeMethods.IHtmlBodyElement htmlBodyElement = this.site.Document.GetBody() as NativeMethods.IHtmlBodyElement;
				Debug.Assert(htmlBodyElement != null, "Couldn't get body element in HtmlControl.Find");
				flag = false;
				ihtmltxtRange = htmlBodyElement.createTextRange();
			}
			if (searchUp)
			{
				if (flag)
				{
					ihtmltxtRange.MoveEnd("character", -1);
				}
				for (int num = 1; num == 1; num = ihtmltxtRange.MoveStart("textedit", -1))
				{
				}
			}
			else
			{
				if (flag)
				{
					ihtmltxtRange.MoveStart("character", 1);
				}
				for (int num = 1; num == 1; num = ihtmltxtRange.MoveEnd("textedit", 1))
				{
				}
			}
			int num2 = (matchCase ? 4 : 0) | (wholeWord ? 2 : 0);
			int num3 = (searchUp ? (-10000000) : 10000000);
			bool flag2 = ihtmltxtRange.FindText(searchString, num3, num2);
			bool flag3;
			if (flag2)
			{
				ihtmltxtRange.Select();
				ihtmltxtRange.ScrollIntoView(true);
				flag3 = true;
			}
			else
			{
				if (flag)
				{
					ihtmltxtRange = ihtmlselectionObject.CreateRange() as NativeMethods.IHTMLTxtRange;
					if (searchUp)
					{
						ihtmltxtRange.MoveStart("character", 1);
						for (int num = 1; num == 1; num = ihtmltxtRange.MoveEnd("textedit", 1))
						{
						}
					}
					else
					{
						ihtmltxtRange.MoveEnd("character", -1);
						for (int num = 1; num == 1; num = ihtmltxtRange.MoveStart("textedit", -1))
						{
						}
					}
					flag2 = ihtmltxtRange.FindText(searchString, num3, num2);
					if (flag2)
					{
						ihtmltxtRange.Select();
						ihtmltxtRange.ScrollIntoView(true);
						return true;
					}
				}
				flag3 = false;
			}
			return flag3;
		}
		public bool Replace(string searchString, string replaceString, bool matchCase, bool wholeWord, bool searchUp)
		{
			this.Selection.SynchronizeSelection();
			if (this.Selection.Type == HtmlSelectionType.TextSelection && this.Selection.Length > 0)
			{
				NativeMethods.IHTMLTxtRange ihtmltxtRange = this.Selection.Selection as NativeMethods.IHTMLTxtRange;
				int num = (matchCase ? 4 : 0) | (wholeWord ? 2 : 0);
				int num2 = (searchUp ? (-10000000) : 10000000);
				if (ihtmltxtRange.FindText(searchString, num2, num))
				{
					ihtmltxtRange.SetText(replaceString);
				}
			}
			return this.Find(searchString, matchCase, wholeWord, searchUp);
		}
		public bool CanInsertHyperlink
		{
			get
			{
				bool flag;
				if ((this.Selection.Type == HtmlSelectionType.TextSelection || this.Selection.Type == HtmlSelectionType.Empty) && this.Selection.Length == 0)
				{
					flag = this.CanInsertHtml;
				}
				else
				{
					flag = this.IsEnabled(2124);
				}
				return flag;
			}
		}
		public void InsertHyperlink(string url, string description)
		{
			this.Selection.SynchronizeSelection();
			if (url == null)
			{
				try
				{
					this.Execute(2124);
				}
				catch
				{
				}
			}
			else if ((this.Selection.Type == HtmlSelectionType.TextSelection || this.Selection.Type == HtmlSelectionType.Empty) && this.Selection.Length == 0)
			{
				this.InsertHtml(string.Concat(new string[] { "<a href=\"", url, "\">", description, "</a>" }));
			}
			else
			{
				this.ExecuteWithUserInterface(2124, new object[] { url });
			}
		}
		public bool CanInsertHtml
		{
			get
			{
				bool flag;
				if (this.Selection.Type == HtmlSelectionType.ElementSelection)
				{
					NativeMethods.IHtmlControlRange htmlControlRange = (NativeMethods.IHtmlControlRange)this.Selection.Selection;
					int length = htmlControlRange.GetLength();
					if (length == 1)
					{
						NativeMethods.IHTMLElement ihtmlelement = htmlControlRange.Item(0);
						if (string.Compare(ihtmlelement.GetTagName(), "div", true) == 0 || string.Compare(ihtmlelement.GetTagName(), "td", true) == 0)
						{
							return true;
						}
					}
					flag = false;
				}
				else
				{
					flag = true;
				}
				return flag;
			}
		}
		public void InsertImage(string url)
		{
			this.Selection.SynchronizeSelection();
			if ((this.Selection.Type == HtmlSelectionType.TextSelection || this.Selection.Type == HtmlSelectionType.Empty) && this.Selection.Length == 0)
			{
				this.InsertHtml("<img src=\"" + url + "\"/>");
			}
			else
			{
				this.Execute(2168, new object[] { url });
			}
		}
		public void InsertImage()
		{
			this.ExecuteWithUserInterface(2168, null);
		}
		public void InsertHtml(string html)
		{
			this.Selection.SynchronizeSelection();
			if (this.Selection.Type == HtmlSelectionType.ElementSelection)
			{
				NativeMethods.IHtmlControlRange htmlControlRange = (NativeMethods.IHtmlControlRange)this.Selection.Selection;
				int length = htmlControlRange.GetLength();
				if (length == 1)
				{
					NativeMethods.IHTMLElement ihtmlelement = htmlControlRange.Item(0);
					if (string.Compare(ihtmlelement.GetTagName(), "div", true) == 0 || string.Compare(ihtmlelement.GetTagName(), "td", true) == 0)
					{
						ihtmlelement.InsertAdjacentHTML("beforeEnd", html);
					}
				}
			}
			else
			{
				NativeMethods.IHTMLTxtRange ihtmltxtRange = (NativeMethods.IHTMLTxtRange)this.Selection.Selection;
				ihtmltxtRange.PasteHTML(html);
			}
		}
		/// <summary>
		/// Gets the current MSHTML selection.
		/// </summary>
		public HtmlSelection Selection
		{
			get
			{
				if (this.selection == null)
				{
					this.selection = new HtmlSelection(this);
				}
				return this.selection;
			}
		}
		/// <summary>
		/// Gets the formatting facade for the current selection.
		/// </summary>
		public HtmlTextFormatting TextFormatting
		{
			get
			{
				if (this.textFormatting == null)
				{
					this.textFormatting = new HtmlTextFormatting(this);
				}
				return this.textFormatting;
			}
		}
		private static readonly object readyStateCompleteEvent = new object();
		private bool scriptEnabled;
		private bool firstActivation;
		private bool isReady;
		private bool isCreated;
		private bool desiredLoad;
		private string desiredContent;
		private string desiredUrl;
		private bool absolutePositioningEnabled;
		private bool absolutePositioningDesired;
		private bool multipleSelectionEnabled;
		private bool multipleSelectionDesired;
		private bool focusDesired;
		private string url;
		private object scriptObject;
		private HtmlSite site;
		private static IDictionary urlMap;
		private bool isDesignMode;
		private bool designModeDesired;
		private HtmlSelection selection;
		private HtmlTextFormatting textFormatting;
		private NativeMethods.IPersistStreamInit persistStream;
		private NativeMethods.IOleUndoManager undoManager;
		private NativeMethods.IHTMLEditServices editServices;
	}
}
