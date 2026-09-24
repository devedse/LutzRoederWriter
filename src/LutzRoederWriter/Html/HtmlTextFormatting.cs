using System;
using System.Diagnostics;
using System.Drawing;

namespace Writer.Html
{
	/// <summary>
	/// Reads and changes text and paragraph formatting for the current selection.
	/// </summary>
	/// <remarks>
	/// Check the corresponding <c>Can...</c> property before applying a formatting
	/// operation because MSHTML enables commands according to the active selection.
	/// </remarks>
	public class HtmlTextFormatting
	{
		/// <summary>Initializes a formatting facade for an HTML control.</summary>
		/// <param name="control">The owning control.</param>
		public HtmlTextFormatting(HtmlControl control)
		{
			this.control = control;
		}
		/// <summary>Gets or sets the selection background color.</summary>
		public Color BackColor
		{
			get
			{
				return this.ConvertColorFromHtml(this.control.Execute(51));
			}
			set
			{
				this.control.Execute(51, new object[] { ColorTranslator.ToHtml(value) });
			}
		}
		public bool CanIndent
		{
			get
			{
				return this.control.IsEnabled(2186);
			}
		}
		public bool CanSetBackColor
		{
			get
			{
				return this.control.IsEnabled(51);
			}
		}
		public bool CanSetFontName
		{
			get
			{
				return this.control.IsEnabled(18);
			}
		}
		public bool CanSetFontSize
		{
			get
			{
				return this.control.IsEnabled(19);
			}
		}
		public bool CanSetHtmlFormat
		{
			get
			{
				return this.control.IsEnabled(2234);
			}
		}
		public bool CanUnindent
		{
			get
			{
				return this.control.IsEnabled(2187);
			}
		}
		/// <summary>Gets or sets the selected font family.</summary>
		public string FontName
		{
			get
			{
				return this.control.Execute(18) as string;
			}
			set
			{
				this.control.Execute(18, new object[] { value });
			}
		}
		/// <summary>Gets or sets the legacy HTML font size.</summary>
		public HtmlFontSize FontSize
		{
			get
			{
				object obj = this.control.Execute(19);
				HtmlFontSize htmlFontSize;
				if (obj == null)
				{
					htmlFontSize = HtmlFontSize.Medium;
				}
				else
				{
					try
					{
						htmlFontSize = (HtmlFontSize)obj;
					}
					catch (InvalidCastException)
					{
						htmlFontSize = HtmlFontSize.Medium;
					}
				}
				return htmlFontSize;
			}
			set
			{
				this.control.Execute(19, new object[] { (int)value });
			}
		}
		public bool CanSetForeColor
		{
			get
			{
				return this.control.IsEnabled(55);
			}
		}
		/// <summary>Gets or sets the selection foreground color.</summary>
		public Color ForeColor
		{
			get
			{
				return this.ConvertColorFromHtml(this.control.Execute(55));
			}
			set
			{
				string text = ColorTranslator.ToHtml(value);
				this.control.Execute(55, new object[] { text });
			}
		}
		private Color ConvertColorFromHtml(object colorValue)
		{
			if (colorValue != null)
			{
				Type type = colorValue.GetType();
				if (type == typeof(int))
				{
					return ColorTranslator.FromWin32((int)colorValue);
				}
				if (type == typeof(string))
				{
					return ColorTranslator.FromHtml((string)colorValue);
				}
				Debug.Fail("Unexpected color type : " + type.FullName);
			}
			return Color.Empty;
		}
		/// <summary>Gets or sets the block format of the selection.</summary>
		public HtmlFormat HtmlFormat
		{
			get
			{
				string text = this.control.Execute(2234, null) as string;
				if (text != null)
				{
					for (int i = 0; i < HtmlTextFormatting.formats.Length; i++)
					{
						if (text.Equals(HtmlTextFormatting.formats[i]))
						{
							return (HtmlFormat)i;
						}
					}
				}
				return HtmlFormat.Normal;
			}
			set
			{
				this.control.Execute(2234, new object[] { HtmlTextFormatting.formats[(int)value] });
			}
		}
		/// <summary>Increases the indentation of the current block.</summary>
		public void Indent()
		{
			this.control.Execute(2186);
		}
		public bool CanToggleBold
		{
			get
			{
				return this.control.IsEnabled(52);
			}
		}
		public bool IsBold
		{
			get
			{
				return this.control.IsChecked(52);
			}
		}
		/// <summary>Toggles bold formatting.</summary>
		public void ToggleBold()
		{
			this.control.Execute(52);
		}
		public bool CanToggleItalic
		{
			get
			{
				return this.control.IsEnabled(56);
			}
		}
		public bool IsItalic
		{
			get
			{
				return this.control.IsChecked(56);
			}
		}
		/// <summary>Toggles italic formatting.</summary>
		public void ToggleItalics()
		{
			this.control.Execute(56);
		}
		public bool CanToggleStrikethrough
		{
			get
			{
				return this.control.IsEnabled(91);
			}
		}
		public bool IsStrikethrough
		{
			get
			{
				return this.control.IsChecked(91);
			}
		}
		/// <summary>Toggles strikethrough formatting.</summary>
		public void ToggleStrikethrough()
		{
			this.control.Execute(91);
		}
		public bool CanToggleSubscript
		{
			get
			{
				return this.control.IsEnabled(2247);
			}
		}
		public bool IsSubscript
		{
			get
			{
				return this.control.IsChecked(2247);
			}
		}
		/// <summary>Toggles subscript formatting.</summary>
		public void ToggleSubscript()
		{
			this.control.Execute(2247);
		}
		public bool CanToggleSuperscript
		{
			get
			{
				return this.control.IsEnabled(2248);
			}
		}
		public bool IsSuperscript
		{
			get
			{
				return this.control.IsChecked(2248);
			}
		}
		/// <summary>Toggles superscript formatting.</summary>
		public void ToggleSuperscript()
		{
			this.control.Execute(2248);
		}
		public bool CanToggleUnderline
		{
			get
			{
				return this.control.IsEnabled(63);
			}
		}
		public bool IsUnderline
		{
			get
			{
				return this.control.IsChecked(63);
			}
		}
		/// <summary>Toggles underline formatting.</summary>
		public void ToggleUnderline()
		{
			this.control.Execute(63);
		}
		/// <summary>Decreases the indentation of the current block.</summary>
		public void Unindent()
		{
			this.control.Execute(2187);
		}
		/// <summary>Gets or sets paragraph alignment for the selection.</summary>
		public HtmlAlignment Alignment
		{
			get
			{
				HtmlAlignment htmlAlignment;
				if (this.control.IsChecked(this.MapAlignment(HtmlAlignment.Left)))
				{
					htmlAlignment = HtmlAlignment.Left;
				}
				else if (this.control.IsChecked(this.MapAlignment(HtmlAlignment.Right)))
				{
					htmlAlignment = HtmlAlignment.Right;
				}
				else if (this.control.IsChecked(this.MapAlignment(HtmlAlignment.Center)))
				{
					htmlAlignment = HtmlAlignment.Center;
				}
				else
				{
					htmlAlignment = HtmlAlignment.Full;
				}
				return htmlAlignment;
			}
			set
			{
				this.control.Execute(this.MapAlignment(value));
			}
		}
		/// <summary>Determines whether the requested alignment can be applied.</summary>
		/// <param name="alignment">The alignment to query.</param>
		/// <returns><see langword="true"/> when MSHTML enables the alignment command.</returns>
		public bool CanAlign(HtmlAlignment alignment)
		{
			return this.control.IsEnabled(this.MapAlignment(alignment));
		}
		private int MapAlignment(HtmlAlignment alignment)
		{
			int num;
			switch (alignment)
			{
			case HtmlAlignment.Left:
				num = 59;
				break;
			case HtmlAlignment.Center:
				num = 57;
				break;
			case HtmlAlignment.Right:
				num = 60;
				break;
			default:
				num = -1;
				break;
			}
			return num;
		}
		private static readonly string[] formats = new string[]
		{
			"Normal", "Formatted", "Heading 1", "Heading 2", "Heading 3", "Heading 4", "Heading 5", "Heading 6", "Paragraph", "Numbered List",
			"Bulleted List"
		};
		private HtmlControl control;
	}
}
