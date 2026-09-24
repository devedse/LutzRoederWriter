using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Writer.Html
{
	/// <summary>
	/// Represents and manipulates the current text or element selection in an
	/// <see cref="HtmlControl"/>.
	/// </summary>
	public class HtmlSelection
	{
		/// <summary>Occurs when the hosted document selection changes.</summary>
		public event EventHandler SelectionChanged;

		/// <summary>Initializes a selection facade for an HTML control.</summary>
		/// <param name="control">The owning control.</param>
		public HtmlSelection(HtmlControl control)
		{
			this.control = control;
			this.maxZIndex = 99;
		}
		public bool CanAlign
		{
			get
			{
				bool flag;
				if (this.items.Count < 2)
				{
					flag = false;
				}
				else if (this.type == HtmlSelectionType.ElementSelection)
				{
					foreach (object obj in this.items)
					{
						NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)obj;
						if (!this.IsElement2DPositioned(ihtmlelement))
						{
							return false;
						}
						if (this.IsElementLocked(ihtmlelement))
						{
							return false;
						}
					}
					flag = this.SameParent;
				}
				else
				{
					flag = false;
				}
				return flag;
			}
		}
		public bool CanMatchSize
		{
			get
			{
				bool flag;
				if (this.items.Count < 2)
				{
					flag = false;
				}
				else if (this.type == HtmlSelectionType.ElementSelection)
				{
					foreach (object obj in this.items)
					{
						NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)obj;
						if (this.IsElementLocked(ihtmlelement))
						{
							return false;
						}
					}
					flag = this.SameParent;
				}
				else
				{
					flag = false;
				}
				return flag;
			}
		}
		public bool CanChangeZIndex
		{
			get
			{
				bool flag;
				if (this.items.Count == 0)
				{
					flag = false;
				}
				else if (this.type == HtmlSelectionType.ElementSelection)
				{
					foreach (object obj in this.items)
					{
						NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)obj;
						if (!this.IsElement2DPositioned(ihtmlelement))
						{
							return false;
						}
					}
					flag = this.SameParent;
				}
				else
				{
					flag = false;
				}
				return flag;
			}
		}
		public bool CanWrapSelection
		{
			get
			{
				return this.selectionLength != 0 && this.Type == HtmlSelectionType.TextSelection;
			}
		}
		protected HtmlControl Control
		{
			get
			{
				return this.control;
			}
		}
		/// <summary>Gets the selected elements.</summary>
		public ICollection Elements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new ArrayList();
					foreach (object obj in this.items)
					{
						NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)obj;
						object obj2 = this.CreateElementWrapper(ihtmlelement);
						if (obj2 != null)
						{
							this.elements.Add(obj2);
						}
					}
				}
				return this.elements;
			}
		}
		internal ICollection Items
		{
			get
			{
				return this.items;
			}
		}
		/// <summary>Gets the number of selected elements.</summary>
		public int Length
		{
			get
			{
				return this.selectionLength;
			}
		}
		private bool SameParent
		{
			get
			{
				if (!this.sameParentValid)
				{
					IntPtr intPtr = NativeMethods.NullIntPtr;
					foreach (object obj in this.items)
					{
						NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)obj;
						NativeMethods.IHTMLElement parentElement = ihtmlelement.GetParentElement();
						IntPtr iunknownForObject = Marshal.GetIUnknownForObject(parentElement);
						if (intPtr == NativeMethods.NullIntPtr)
						{
							intPtr = iunknownForObject;
						}
						else
						{
							if (intPtr != iunknownForObject)
							{
								Marshal.Release(iunknownForObject);
								if (intPtr != NativeMethods.NullIntPtr)
								{
									Marshal.Release(intPtr);
								}
								this.sameParentValid = false;
								return this.sameParentValid;
							}
							Marshal.Release(iunknownForObject);
						}
					}
					if (intPtr != NativeMethods.NullIntPtr)
					{
						Marshal.Release(intPtr);
					}
					this.sameParentValid = true;
				}
				return this.sameParentValid;
			}
		}
		protected internal object Selection
		{
			get
			{
				return this.selection;
			}
		}
		/// <summary>Gets or sets the text in the current selection.</summary>
		public string Text
		{
			get
			{
				string text;
				if (this.type == HtmlSelectionType.TextSelection)
				{
					text = this.text;
				}
				else
				{
					text = null;
				}
				return text;
			}
		}
		/// <summary>Gets the kind of the current selection.</summary>
		public HtmlSelectionType Type
		{
			get
			{
				return this.type;
			}
		}
		/// <summary>Clears the current selection.</summary>
		public void ClearSelection()
		{
			this.control.Execute(2007);
		}
		internal object CreateElementWrapper(NativeMethods.IHTMLElement element)
		{
			return new HtmlElement(element, this.control);
		}
		/// <summary>Gets the outer HTML of the single selected element.</summary>
		/// <returns>The selected element markup.</returns>
		public string GetOuterHtml()
		{
			Debug.Assert(this.Items.Count == 1, "Can't get OuterHtml of more than one element");
			string text = string.Empty;
			try
			{
				text = ((NativeMethods.IHTMLElement)this.items[0]).GetOuterHTML();
				text = ((NativeMethods.IHTMLElement)this.items[0]).GetOuterHTML();
			}
			catch
			{
			}
			return text;
		}
		public ArrayList GetParentHierarchy(object o)
		{
			NativeMethods.IHTMLElement ihtmlelement = this.GetHtmlElement(o);
			ArrayList arrayList;
			if (ihtmlelement == null)
			{
				arrayList = null;
			}
			else
			{
				string text = ihtmlelement.GetTagName().ToLower();
				if (text.Equals("body"))
				{
					arrayList = null;
				}
				else
				{
					ArrayList arrayList2 = new ArrayList();
					ihtmlelement = ihtmlelement.GetParentElement();
					while (ihtmlelement != null && !ihtmlelement.GetTagName().ToLower().Equals("body"))
					{
						HtmlElement htmlElement = new HtmlElement(ihtmlelement, this.control);
						if (this.IsSelectableElement(htmlElement))
						{
							arrayList2.Add(htmlElement);
						}
						ihtmlelement = ihtmlelement.GetParentElement();
					}
					if (ihtmlelement != null)
					{
						HtmlElement htmlElement = new HtmlElement(ihtmlelement, this.control);
						if (this.IsSelectableElement(htmlElement))
						{
							arrayList2.Add(htmlElement);
						}
					}
					arrayList = arrayList2;
				}
			}
			return arrayList;
		}
		internal NativeMethods.IHTMLElement GetHtmlElement(object o)
		{
			NativeMethods.IHTMLElement ihtmlelement;
			if (o is HtmlElement)
			{
				ihtmlelement = ((HtmlElement)o).Peer;
			}
			else
			{
				ihtmlelement = null;
			}
			return ihtmlelement;
		}
		private bool IsElement2DPositioned(NativeMethods.IHTMLElement element)
		{
			NativeMethods.IHTMLElement2 ihtmlelement = (NativeMethods.IHTMLElement2)element;
			NativeMethods.IHTMLCurrentStyle currentStyle = ihtmlelement.GetCurrentStyle();
			string position = currentStyle.GetPosition();
			return position != null && position.ToLower() == "absolute";
		}
		private bool IsElementLocked(NativeMethods.IHTMLElement element)
		{
			object[] array = new object[1];
			element.GetAttribute(HtmlSelection.DesignTimeLockAttribute, 0, array);
			if (array[0] == null)
			{
				NativeMethods.IHTMLStyle style = element.GetStyle();
				array[0] = style.GetAttribute(HtmlSelection.DesignTimeLockAttribute, 0);
			}
			return array[0] != null && array[0] is string;
		}
		protected virtual bool IsSelectableElement(HtmlElement element)
		{
			return true;
		}
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this, e);
			}
		}
		/// <summary>Selects a single MSHTML element or <see cref="HtmlElement"/>.</summary>
		/// <param name="o">The element to select.</param>
		/// <returns><see langword="true"/> when the selection was applied.</returns>
		public bool SelectElement(object o)
		{
			return this.SelectElements(new ArrayList(1) { o });
		}
		/// <summary>Selects multiple MSHTML elements or <see cref="HtmlElement"/> instances.</summary>
		/// <param name="elements">The elements to select.</param>
		/// <returns><see langword="true"/> when the selection was applied.</returns>
		public bool SelectElements(ICollection elements)
		{
			NativeMethods.IHTMLElement body = this.control.HtmlDocument.GetBody();
			NativeMethods.IHTMLTextContainer ihtmltextContainer = body as NativeMethods.IHTMLTextContainer;
			Debug.Assert(ihtmltextContainer != null);
			object obj = ihtmltextContainer.createControlRange();
			NativeMethods.IHtmlControlRange htmlControlRange = obj as NativeMethods.IHtmlControlRange;
			Debug.Assert(htmlControlRange != null);
			bool flag;
			if (htmlControlRange == null)
			{
				flag = false;
			}
			else
			{
				NativeMethods.IHtmlControlRange2 htmlControlRange2 = obj as NativeMethods.IHtmlControlRange2;
				Debug.Assert(htmlControlRange2 != null);
				if (htmlControlRange2 == null)
				{
					flag = false;
				}
				else
				{
					int num = 0;
					foreach (object obj2 in elements)
					{
						NativeMethods.IHTMLElement ihtmlelement = this.GetHtmlElement(obj2);
						if (ihtmlelement == null)
						{
							return false;
						}
						num = htmlControlRange2.addElement(ihtmlelement);
						if (num != 0)
						{
							break;
						}
					}
					if (num == 0)
					{
						htmlControlRange.Select();
					}
					else
					{
						NativeMethods.IHtmlBodyElement htmlBodyElement = (NativeMethods.IHtmlBodyElement)body;
						NativeMethods.IHTMLTxtRange ihtmltxtRange = htmlBodyElement.createTextRange();
						if (ihtmltxtRange != null)
						{
							foreach (object obj2 in elements)
							{
								try
								{
									NativeMethods.IHTMLElement ihtmlelement = this.GetHtmlElement(obj2);
									if (ihtmlelement == null)
									{
										return false;
									}
									ihtmltxtRange.MoveToElementText(ihtmlelement);
								}
								catch
								{
								}
							}
							ihtmltxtRange.Select();
						}
					}
					flag = true;
				}
			}
			return flag;
		}
		/// <summary>Replaces the outer HTML of the single selected element.</summary>
		/// <param name="outerHtml">The replacement markup.</param>
		public void SetOuterHtml(string outerHtml)
		{
			Debug.Assert(this.Items.Count == 1, "Can't get OuterHtml of more than one element");
			NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)this.items[0];
			ihtmlelement.SetOuterHTML(outerHtml);
		}
		public bool SynchronizeSelection()
		{
			if (this.document == null)
			{
				this.document = this.control.HtmlDocument;
			}
			NativeMethods.IHTMLSelectionObject ihtmlselectionObject = this.document.GetSelection();
			object obj = null;
			try
			{
				obj = ihtmlselectionObject.CreateRange();
			}
			catch
			{
			}
			ArrayList arrayList = this.items;
			HtmlSelectionType htmlSelectionType = this.type;
			int num = this.selectionLength;
			this.type = HtmlSelectionType.Empty;
			this.selectionLength = 0;
			if (obj != null)
			{
				this.selection = obj;
				this.items = new ArrayList();
				if (obj is NativeMethods.IHTMLTxtRange)
				{
					NativeMethods.IHTMLTxtRange ihtmltxtRange = (NativeMethods.IHTMLTxtRange)obj;
					NativeMethods.IHTMLElement ihtmlelement = ihtmltxtRange.ParentElement();
					if (this.IsSelectableElement(new HtmlElement(ihtmlelement, this.control)))
					{
						if (ihtmlelement != null)
						{
							this.text = ihtmltxtRange.GetText();
							if (this.text != null)
							{
								this.selectionLength = this.text.Length;
							}
							else
							{
								this.selectionLength = 0;
							}
							this.type = HtmlSelectionType.TextSelection;
							this.items.Add(ihtmlelement);
						}
					}
				}
				else if (obj is NativeMethods.IHtmlControlRange)
				{
					NativeMethods.IHtmlControlRange htmlControlRange = (NativeMethods.IHtmlControlRange)obj;
					int length = htmlControlRange.GetLength();
					if (length > 0)
					{
						this.type = HtmlSelectionType.ElementSelection;
						for (int i = 0; i < length; i++)
						{
							NativeMethods.IHTMLElement ihtmlelement2 = htmlControlRange.Item(i);
							this.items.Add(ihtmlelement2);
						}
						this.selectionLength = length;
					}
				}
			}
			this.sameParentValid = false;
			bool flag = false;
			if (this.type != htmlSelectionType)
			{
				flag = true;
			}
			else if (this.selectionLength != num)
			{
				flag = true;
			}
			else if (this.items != null)
			{
				for (int i = 0; i < this.items.Count; i++)
				{
					if (this.items[i] != arrayList[i])
					{
						flag = true;
						break;
					}
				}
			}
			bool flag2;
			if (flag)
			{
				this.elements = null;
				this.OnSelectionChanged(EventArgs.Empty);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}
		public void ToggleAbsolutePosition()
		{
			this.control.Execute(2397, new object[] { !this.control.IsChecked(2397) });
			this.SynchronizeSelection();
			if (this.type == HtmlSelectionType.ElementSelection)
			{
				foreach (object obj in this.items)
				{
					NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)obj;
					ihtmlelement.GetStyle().SetZIndex(++this.maxZIndex);
				}
			}
		}
		public void ToggleLock()
		{
			foreach (object obj in this.items)
			{
				NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)obj;
				NativeMethods.IHTMLStyle style = ihtmlelement.GetStyle();
				if (this.IsElementLocked(ihtmlelement))
				{
					ihtmlelement.RemoveAttribute(HtmlSelection.DesignTimeLockAttribute, 0);
					style.RemoveAttribute(HtmlSelection.DesignTimeLockAttribute, 0);
				}
				else
				{
					ihtmlelement.SetAttribute(HtmlSelection.DesignTimeLockAttribute, "true", 0);
					style.SetAttribute(HtmlSelection.DesignTimeLockAttribute, "true", 0);
				}
			}
		}
		/// <summary>Wraps the selection in the specified element.</summary>
		/// <param name="tag">The element name.</param>
		public void WrapSelection(string tag)
		{
			this.WrapSelection(tag, null);
		}
		/// <summary>Wraps the selection in an element with optional attributes.</summary>
		/// <param name="elementName">The element name.</param>
		/// <param name="attributes">Attribute names and values, or <see langword="null"/>.</param>
		public void WrapSelection(string elementName, IDictionary attributes)
		{
			string text = string.Empty;
			if (attributes != null)
			{
				foreach (object obj in attributes.Keys)
				{
					string text2 = (string)obj;
					object obj2 = text;
					text = string.Concat(new object[]
					{
						obj2,
						text2,
						"=\"",
						attributes[text2],
						"\" "
					});
				}
			}
			this.SynchronizeSelection();
			if (this.type == HtmlSelectionType.TextSelection)
			{
				NativeMethods.IHTMLTxtRange ihtmltxtRange = (NativeMethods.IHTMLTxtRange)this.Selection;
				string text3 = ihtmltxtRange.GetHtmlText();
				if (text3 == null)
				{
					text3 = string.Empty;
				}
				string text4 = string.Concat(new string[] { "<", elementName, " ", text, ">", text3, "</", elementName, ">" });
				ihtmltxtRange.PasteHTML(text4);
			}
		}
		public void WrapSelectionInDiv()
		{
			this.WrapSelection("div");
		}
		public void WrapSelectionInSpan()
		{
			this.WrapSelection("span");
		}
		public void WrapSelectionInBlockQuote()
		{
			this.WrapSelection("blockquote");
		}
		/// <summary>Wraps the selection in a hyperlink.</summary>
		/// <param name="url">The hyperlink target.</param>
		public void WrapSelectionInHyperlink(string url)
		{
			this.control.Execute(2124, new object[] { url });
		}
		public bool CanRemoveHyperlink
		{
			get
			{
				return this.control.IsEnabled(2125);
			}
		}
		/// <summary>Removes hyperlink formatting from the current selection.</summary>
		public void RemoveHyperlink()
		{
			this.control.Execute(2125);
		}
		private static readonly string DesignTimeLockAttribute = "Design_Time_Lock";
		private HtmlControl control;
		private NativeMethods.IHTMLDocument2 document;
		private HtmlSelectionType type;
		private int selectionLength;
		private string text;
		private object selection;
		private ArrayList items;
		private ArrayList elements;
		private bool sameParentValid;
		private int maxZIndex;
	}
}
