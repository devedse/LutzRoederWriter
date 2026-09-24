using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace Writer.Html
{
	/// <summary>
	/// Wraps an MSHTML element and provides access to its markup, attributes, and relatives.
	/// </summary>
	[DesignOnly(true)]
	public class HtmlElement
	{
		internal HtmlElement(NativeMethods.IHTMLElement peer, HtmlControl owner)
		{
			Debug.Assert(peer != null);
			this.peer = peer;
			this.owner = owner;
		}
		/// <summary>Gets or sets the markup contained by the element.</summary>
		[Browsable(false)]
		public string InnerHtml
		{
			get
			{
				string text;
				try
				{
					text = this.peer.GetInnerHTML();
				}
				catch (Exception ex)
				{
					Debug.Fail(ex.ToString(), "Could not get Element InnerHTML");
					text = string.Empty;
				}
				return text;
			}
			set
			{
				try
				{
					this.peer.SetInnerHTML(value);
				}
				catch (Exception ex)
				{
					Debug.Fail(ex.ToString(), "Could not set Element InnerHTML");
				}
			}
		}
		/// <summary>Gets or sets the markup for the element and its contents.</summary>
		[Browsable(false)]
		public string OuterHtml
		{
			get
			{
				string text;
				try
				{
					text = this.peer.GetOuterHTML();
				}
				catch (Exception ex)
				{
					Debug.Fail(ex.ToString(), "Could not get Element OuterHTML");
					text = string.Empty;
				}
				return text;
			}
			set
			{
				try
				{
					this.peer.SetOuterHTML(value);
				}
				catch (Exception ex)
				{
					Debug.Fail(ex.ToString(), "Could not set Element OuterHTML");
				}
			}
		}
		/// <summary>Gets the element tag name.</summary>
		[Browsable(false)]
		public string Name
		{
			get
			{
				string text;
				try
				{
					text = this.peer.GetTagName();
				}
				catch (Exception ex)
				{
					Debug.Fail(ex.ToString(), "Could not get Element TagName" + ex.ToString());
					text = string.Empty;
				}
				return text;
			}
		}
		internal NativeMethods.IHTMLElement Peer
		{
			get
			{
				return this.peer;
			}
		}
		/// <summary>Gets an attribute value from the element.</summary>
		/// <param name="attribute">The attribute name.</param>
		/// <returns>The attribute value, or <see langword="null"/> when absent.</returns>
		public object GetAttribute(string attribute)
		{
			object obj2;
			try
			{
				object[] array = new object[1];
				this.peer.GetAttribute(attribute, 0, array);
				object obj = array[0];
				if (obj is DBNull)
				{
					obj = null;
				}
				obj2 = obj;
			}
			catch (Exception ex)
			{
				Debug.Fail(ex.ToString(), "Call to IHTMLElement::GetAttribute failed in Element");
				obj2 = null;
			}
			return obj2;
		}
		/// <summary>Gets a child element by zero-based index.</summary>
		/// <param name="index">The child index.</param>
		/// <returns>The selected child element.</returns>
		public HtmlElement GetChild(int index)
		{
			NativeMethods.IHTMLElementCollection ihtmlelementCollection = (NativeMethods.IHTMLElementCollection)this.peer.GetChildren();
			NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)ihtmlelementCollection.Item(null, index);
			return new HtmlElement(ihtmlelement, this.owner);
		}
		/// <summary>Gets a named child element.</summary>
		/// <param name="name">The child name or identifier understood by MSHTML.</param>
		/// <returns>The selected child element.</returns>
		public HtmlElement GetChild(string name)
		{
			NativeMethods.IHTMLElementCollection ihtmlelementCollection = (NativeMethods.IHTMLElementCollection)this.peer.GetChildren();
			NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)ihtmlelementCollection.Item(name, null);
			return new HtmlElement(ihtmlelement, this.owner);
		}
		/// <summary>Gets the parent element.</summary>
		public HtmlElement Parent
		{
			get
			{
				NativeMethods.IHTMLElement parentElement = this.peer.GetParentElement();
				return new HtmlElement(parentElement, this.owner);
			}
		}
		protected string GetRelativeUrl(string absoluteUrl)
		{
			string text;
			if (absoluteUrl == null || absoluteUrl.Length == 0)
			{
				text = string.Empty;
			}
			else
			{
				string text2 = absoluteUrl;
				if (this.owner != null)
				{
					string url = this.owner.Url;
					if (url.Length != 0)
					{
						try
						{
							Uri uri = new Uri(url);
							Uri uri2 = new Uri(text2);
							text2 = uri.MakeRelativeUri(uri2).AbsolutePath;
						}
						catch
						{
						}
					}
				}
				text = text2;
			}
			return text;
		}
		protected internal string GetStringAttribute(string attribute)
		{
			return this.GetStringAttribute(attribute, string.Empty);
		}
		protected internal string GetStringAttribute(string attribute, string defaultValue)
		{
			object attribute2 = this.GetAttribute(attribute);
			string text;
			if (attribute2 == null)
			{
				text = defaultValue;
			}
			else if (attribute2 is string)
			{
				text = (string)attribute2;
			}
			else
			{
				text = defaultValue;
			}
			return text;
		}
		/// <summary>Removes an attribute from the element.</summary>
		/// <param name="attribute">The attribute name.</param>
		public void RemoveAttribute(string attribute)
		{
			try
			{
				this.peer.RemoveAttribute(attribute, 0);
			}
			catch (Exception ex)
			{
				Debug.Fail(ex.ToString(), "Call to IHTMLElement::RemoveAttribute failed in Element");
			}
		}
		/// <summary>Sets an attribute on the element.</summary>
		/// <param name="attribute">The attribute name.</param>
		/// <param name="value">The attribute value.</param>
		public void SetAttribute(string attribute, object value)
		{
			try
			{
				this.peer.SetAttribute(attribute, value, 0);
			}
			catch (Exception ex)
			{
				Debug.Fail(ex.ToString(), "Call to IHTMLElement::SetAttribute failed in Element");
			}
		}
		protected internal void SetBooleanAttribute(string attribute, bool value)
		{
			if (value)
			{
				this.SetAttribute(attribute, true);
			}
			else
			{
				this.RemoveAttribute(attribute);
			}
		}
		protected internal void SetColorAttribute(string attribute, Color value)
		{
			if (value.IsEmpty)
			{
				this.RemoveAttribute(attribute);
			}
			else
			{
				this.SetAttribute(attribute, ColorTranslator.ToHtml(value));
			}
		}
		protected internal void SetEnumAttribute(string attribute, Enum value, Enum defaultValue)
		{
			Debug.Assert(value.GetType().Equals(defaultValue.GetType()));
			if (value.Equals(defaultValue))
			{
				this.RemoveAttribute(attribute);
			}
			else
			{
				this.SetAttribute(attribute, value.ToString());
			}
		}
		protected internal void SetIntegerAttribute(string attribute, int value, int defaultValue)
		{
			if (value == defaultValue)
			{
				this.RemoveAttribute(attribute);
			}
			else
			{
				this.SetAttribute(attribute, value);
			}
		}
		protected internal void SetStringAttribute(string attribute, string value)
		{
			this.SetStringAttribute(attribute, value, string.Empty);
		}
		protected internal void SetStringAttribute(string attribute, string value, string defaultValue)
		{
			if (value == null || value.Equals(defaultValue))
			{
				this.RemoveAttribute(attribute);
			}
			else
			{
				this.SetAttribute(attribute, value);
			}
		}
		public override string ToString()
		{
			if (this.peer != null)
			{
				try
				{
					return "<" + this.peer.GetTagName() + ">";
				}
				catch
				{
				}
			}
			return string.Empty;
		}
		private NativeMethods.IHTMLElement peer;
		private HtmlControl owner;
	}
}
