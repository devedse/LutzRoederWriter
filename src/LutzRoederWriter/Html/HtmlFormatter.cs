using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Writer.Html
{
	/// <summary>
	/// Formats legacy HTML into a consistently indented, line-wrapped representation.
	/// </summary>
	/// <remarks>
	/// The formatter preserves the original Writer behavior, including handling for
	/// legacy HTML elements, server-side blocks, comments, and XML-style elements.
	/// It formats text only and does not require a Windows Forms message loop.
	/// </remarks>
	public class HtmlFormatter
	{
		static HtmlFormatter()
		{
			HtmlFormatter.tagTable["a"] = new HtmlFormatter.TagInfo("a", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["acronym"] = new HtmlFormatter.TagInfo("acronym", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["address"] = new HtmlFormatter.TagInfo("address", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["applet"] = new HtmlFormatter.TagInfo("applet", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["area"] = new HtmlFormatter.TagInfo("area", HtmlFormatter.FormattingFlags.NoEndTag);
			HtmlFormatter.tagTable["b"] = new HtmlFormatter.TagInfo("b", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["base"] = new HtmlFormatter.TagInfo("base", HtmlFormatter.FormattingFlags.NoEndTag);
			HtmlFormatter.tagTable["basefont"] = new HtmlFormatter.TagInfo("basefont", HtmlFormatter.FormattingFlags.NoEndTag, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["bdo"] = new HtmlFormatter.TagInfo("bdo", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["bgsound"] = new HtmlFormatter.TagInfo("bgsound", HtmlFormatter.FormattingFlags.NoEndTag);
			HtmlFormatter.tagTable["big"] = new HtmlFormatter.TagInfo("big", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["blink"] = new HtmlFormatter.TagInfo("blink", HtmlFormatter.FormattingFlags.Inline);
			HtmlFormatter.tagTable["blockquote"] = new HtmlFormatter.TagInfo("blockquote", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["body"] = new HtmlFormatter.TagInfo("body", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["br"] = new HtmlFormatter.TagInfo("br", HtmlFormatter.FormattingFlags.NoEndTag, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["button"] = new HtmlFormatter.TagInfo("button", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["caption"] = new HtmlFormatter.TagInfo("caption", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["cite"] = new HtmlFormatter.TagInfo("cite", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["center"] = new HtmlFormatter.TagInfo("center", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["code"] = new HtmlFormatter.TagInfo("code", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["col"] = new HtmlFormatter.TagInfo("col", HtmlFormatter.FormattingFlags.NoEndTag);
			HtmlFormatter.tagTable["colgroup"] = new HtmlFormatter.TagInfo("colgroup", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["dd"] = new HtmlFormatter.TagInfo("dd", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["del"] = new HtmlFormatter.TagInfo("del", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["dfn"] = new HtmlFormatter.TagInfo("dfn", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["dir"] = new HtmlFormatter.TagInfo("dir", HtmlFormatter.FormattingFlags.None, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["div"] = new HtmlFormatter.TagInfo("div", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["dl"] = new HtmlFormatter.TagInfo("dl", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["dt"] = new HtmlFormatter.TagInfo("dt", HtmlFormatter.FormattingFlags.Inline);
			HtmlFormatter.tagTable["em"] = new HtmlFormatter.TagInfo("em", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["embed"] = new HtmlFormatter.TagInfo("embed", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["fieldset"] = new HtmlFormatter.TagInfo("fieldset", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["font"] = new HtmlFormatter.TagInfo("font", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["form"] = new HtmlFormatter.TagInfo("form", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["frame"] = new HtmlFormatter.TagInfo("frame", HtmlFormatter.FormattingFlags.NoEndTag);
			HtmlFormatter.tagTable["frameset"] = new HtmlFormatter.TagInfo("frameset", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["head"] = new HtmlFormatter.TagInfo("head", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant);
			HtmlFormatter.tagTable["h1"] = new HtmlFormatter.TagInfo("h1", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["h2"] = new HtmlFormatter.TagInfo("h2", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["h3"] = new HtmlFormatter.TagInfo("h3", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["h4"] = new HtmlFormatter.TagInfo("h4", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["h5"] = new HtmlFormatter.TagInfo("h5", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["h6"] = new HtmlFormatter.TagInfo("h6", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["hr"] = new HtmlFormatter.TagInfo("hr", HtmlFormatter.FormattingFlags.NoEndTag, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["html"] = new HtmlFormatter.TagInfo("html", HtmlFormatter.FormattingFlags.NoIndent, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant);
			HtmlFormatter.tagTable["i"] = new HtmlFormatter.TagInfo("i", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["iframe"] = new HtmlFormatter.TagInfo("iframe", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["img"] = new HtmlFormatter.TagInfo("img", HtmlFormatter.FormattingFlags.Inline | HtmlFormatter.FormattingFlags.NoEndTag, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["input"] = new HtmlFormatter.TagInfo("input", HtmlFormatter.FormattingFlags.NoEndTag, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["ins"] = new HtmlFormatter.TagInfo("ins", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["isindex"] = new HtmlFormatter.TagInfo("isindex", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["kbd"] = new HtmlFormatter.TagInfo("kbd", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["label"] = new HtmlFormatter.TagInfo("label", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["legend"] = new HtmlFormatter.TagInfo("legend", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["li"] = new HtmlFormatter.LITagInfo();
			HtmlFormatter.tagTable["link"] = new HtmlFormatter.TagInfo("link", HtmlFormatter.FormattingFlags.NoEndTag);
			HtmlFormatter.tagTable["listing"] = new HtmlFormatter.TagInfo("listing", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["map"] = new HtmlFormatter.TagInfo("map", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["marquee"] = new HtmlFormatter.TagInfo("marquee", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["menu"] = new HtmlFormatter.TagInfo("menu", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["meta"] = new HtmlFormatter.TagInfo("meta", HtmlFormatter.FormattingFlags.NoEndTag);
			HtmlFormatter.tagTable["nobr"] = new HtmlFormatter.TagInfo("nobr", HtmlFormatter.FormattingFlags.Inline | HtmlFormatter.FormattingFlags.NoEndTag, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["noembed"] = new HtmlFormatter.TagInfo("noembed", HtmlFormatter.FormattingFlags.None, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["noframes"] = new HtmlFormatter.TagInfo("noframes", HtmlFormatter.FormattingFlags.None, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["noscript"] = new HtmlFormatter.TagInfo("noscript", HtmlFormatter.FormattingFlags.None, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["object"] = new HtmlFormatter.TagInfo("object", HtmlFormatter.FormattingFlags.None, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["ol"] = new HtmlFormatter.OLTagInfo();
			HtmlFormatter.tagTable["option"] = new HtmlFormatter.TagInfo("option", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.CarryThrough);
			HtmlFormatter.tagTable["p"] = new HtmlFormatter.PTagInfo();
			HtmlFormatter.tagTable["param"] = new HtmlFormatter.TagInfo("param", HtmlFormatter.FormattingFlags.NoEndTag);
			HtmlFormatter.tagTable["pre"] = new HtmlFormatter.TagInfo("pre", HtmlFormatter.FormattingFlags.PreserveContent, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["q"] = new HtmlFormatter.TagInfo("q", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["rt"] = new HtmlFormatter.TagInfo("rt", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["ruby"] = new HtmlFormatter.TagInfo("ruby", HtmlFormatter.FormattingFlags.None, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["s"] = new HtmlFormatter.TagInfo("s", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["samp"] = new HtmlFormatter.TagInfo("samp", HtmlFormatter.FormattingFlags.None, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["script"] = new HtmlFormatter.TagInfo("script", HtmlFormatter.FormattingFlags.PreserveContent, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["select"] = new HtmlFormatter.TagInfo("select", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["small"] = new HtmlFormatter.TagInfo("small", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["span"] = new HtmlFormatter.TagInfo("span", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["strike"] = new HtmlFormatter.TagInfo("strike", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["strong"] = new HtmlFormatter.TagInfo("strong", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["style"] = new HtmlFormatter.TagInfo("style", HtmlFormatter.FormattingFlags.PreserveContent, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Any);
			HtmlFormatter.tagTable["sub"] = new HtmlFormatter.TagInfo("sub", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["sup"] = new HtmlFormatter.TagInfo("sup", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["table"] = new HtmlFormatter.TagInfo("table", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["tbody"] = new HtmlFormatter.TagInfo("tbody", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["td"] = new HtmlFormatter.TDTagInfo();
			HtmlFormatter.tagTable["textarea"] = new HtmlFormatter.TagInfo("textarea", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["tfoot"] = new HtmlFormatter.TagInfo("tfoot", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["th"] = new HtmlFormatter.TagInfo("th", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["thead"] = new HtmlFormatter.TagInfo("thead", HtmlFormatter.FormattingFlags.None);
			HtmlFormatter.tagTable["title"] = new HtmlFormatter.TagInfo("title", HtmlFormatter.FormattingFlags.Inline);
			HtmlFormatter.tagTable["tr"] = new HtmlFormatter.TRTagInfo();
			HtmlFormatter.tagTable["tt"] = new HtmlFormatter.TagInfo("tt", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["u"] = new HtmlFormatter.TagInfo("u", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["ul"] = new HtmlFormatter.TagInfo("ul", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["xml"] = new HtmlFormatter.TagInfo("xml", HtmlFormatter.FormattingFlags.Xml, HtmlFormatter.WhiteSpaceType.Significant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["xmp"] = new HtmlFormatter.TagInfo("xmp", HtmlFormatter.FormattingFlags.PreserveContent, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block);
			HtmlFormatter.tagTable["var"] = new HtmlFormatter.TagInfo("var", HtmlFormatter.FormattingFlags.Inline, HtmlFormatter.ElementType.Inline);
			HtmlFormatter.tagTable["wbr"] = new HtmlFormatter.TagInfo("wbr", HtmlFormatter.FormattingFlags.Inline | HtmlFormatter.FormattingFlags.NoEndTag, HtmlFormatter.ElementType.Inline);
		}
		/// <summary>
		/// Gets or sets the character used for each indentation position.
		/// </summary>
		public char IndentChar
		{
			get
			{
				return this._indentChar;
			}
			set
			{
				this._indentChar = value;
			}
		}
		/// <summary>
		/// Gets or sets the number of <see cref="IndentChar"/> characters per indentation level.
		/// </summary>
		public int IndentSize
		{
			get
			{
				return this._indentSize;
			}
			set
			{
				this._indentSize = value;
			}
		}
		/// <summary>
		/// Gets or sets the preferred maximum output line length.
		/// </summary>
		public int MaxLineLength
		{
			get
			{
				return this._maxLineLength;
			}
			set
			{
				this._maxLineLength = value;
			}
		}
		/// <summary>
		/// Formats an HTML string and writes the result to a text writer.
		/// </summary>
		/// <param name="input">The HTML to format.</param>
		/// <param name="output">The destination for the formatted HTML.</param>
		public void Format(string input, TextWriter output)
		{
			bool flag = true;
			int maxLineLength = this._maxLineLength;
			string text = new string(this._indentChar, this._indentSize);
			char[] array = input.ToCharArray();
			Stack stack = new Stack();
			Stack stack2 = new Stack();
			HtmlFormatter.FormatInfo formatInfo = null;
			string text2 = string.Empty;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			HtmlFormatter.HtmlWriter htmlWriter = new HtmlFormatter.HtmlWriter(output, text, maxLineLength);
			stack2.Push(htmlWriter);
			HtmlFormatter.Token token = HtmlFormatter.HtmlTokenizer.GetFirstToken(array);
			HtmlFormatter.Token token2 = token;
			while (token != null)
			{
				htmlWriter = (HtmlFormatter.HtmlWriter)stack2.Peek();
				switch (token.Type)
				{
				case 0:
					if (token.Text.Length > 0)
					{
						htmlWriter.Write(' ');
					}
					break;
				case 1:
				case 7:
				case 22:
				{
					flag3 = false;
					string text3;
					HtmlFormatter.TagInfo tagInfo;
					if (token.Type == 7)
					{
						text3 = token.Text;
						tagInfo = new HtmlFormatter.TagInfo(token.Text, HtmlFormatter.commentTag);
					}
					else if (token.Type == 22)
					{
						string text4 = token.Text.Trim();
						text4 = text4.Substring(1);
						text3 = text4;
						if (text4.StartsWith("%@"))
						{
							tagInfo = new HtmlFormatter.TagInfo(text4, HtmlFormatter.directiveTag);
						}
						else
						{
							tagInfo = new HtmlFormatter.TagInfo(text4, HtmlFormatter.otherServerSideScriptTag);
						}
					}
					else
					{
						text3 = token.Text;
						tagInfo = HtmlFormatter.tagTable[text3] as HtmlFormatter.TagInfo;
						if (tagInfo == null)
						{
							if (text3.IndexOf(':') > -1)
							{
								tagInfo = new HtmlFormatter.TagInfo(text3, HtmlFormatter.unknownXmlTag);
							}
							else if (htmlWriter is HtmlFormatter.XmlWriter)
							{
								tagInfo = new HtmlFormatter.TagInfo(text3, HtmlFormatter.nestedXmlTag);
							}
							else
							{
								tagInfo = new HtmlFormatter.TagInfo(text3, HtmlFormatter.unknownHtmlTag);
							}
						}
						else if (this._elementCasing == HtmlFormatter.HtmlFormatterCase.LowerCase || flag)
						{
							text3 = tagInfo.TagName;
						}
						else if (this._elementCasing == HtmlFormatter.HtmlFormatterCase.UpperCase)
						{
							text3 = tagInfo.TagName.ToUpper();
						}
					}
					if (formatInfo == null)
					{
						formatInfo = new HtmlFormatter.FormatInfo(tagInfo, false);
						formatInfo.indent = 0;
						stack.Push(formatInfo);
						htmlWriter.Write(text2);
						if (tagInfo.IsXml)
						{
							HtmlFormatter.HtmlWriter htmlWriter2 = new HtmlFormatter.XmlWriter(htmlWriter.Indent, tagInfo.TagName, text, maxLineLength);
							stack2.Push(htmlWriter2);
							htmlWriter = htmlWriter2;
						}
						if (token2.Type == 12)
						{
							htmlWriter.Write("</");
						}
						else
						{
							htmlWriter.Write('<');
						}
						htmlWriter.Write(text3);
						text2 = string.Empty;
					}
					else
					{
						HtmlFormatter.FormatInfo formatInfo2 = new HtmlFormatter.FormatInfo(tagInfo, token2.Type == 12);
						HtmlFormatter.WhiteSpaceType whiteSpaceType;
						if (formatInfo.isEndTag)
						{
							whiteSpaceType = formatInfo.tagInfo.FollowingWhiteSpaceType;
						}
						else
						{
							whiteSpaceType = formatInfo.tagInfo.InnerWhiteSpaceType;
						}
						bool flag5 = formatInfo.tagInfo.IsInline;
						bool flag6 = false;
						bool flag7 = false;
						if (htmlWriter is HtmlFormatter.XmlWriter)
						{
							HtmlFormatter.XmlWriter xmlWriter = (HtmlFormatter.XmlWriter)htmlWriter;
							if (xmlWriter.IsUnknownXml)
							{
								flag7 = ((formatInfo.isBeginTag && formatInfo.tagInfo.TagName.ToLower() == xmlWriter.TagName.ToLower()) || (formatInfo2.isEndTag && formatInfo2.tagInfo.TagName.ToLower() == xmlWriter.TagName.ToLower())) && !HtmlFormatter.FormattedTextWriter.IsWhiteSpace(text2);
							}
							if (formatInfo.isBeginTag)
							{
								if (HtmlFormatter.FormattedTextWriter.IsWhiteSpace(text2))
								{
									if (xmlWriter.IsUnknownXml && formatInfo2.isEndTag && formatInfo.tagInfo.TagName.ToLower() == formatInfo2.tagInfo.TagName.ToLower())
									{
										flag5 = true;
										flag6 = true;
										text2 = "";
									}
								}
								else if (!xmlWriter.IsUnknownXml)
								{
									xmlWriter.ContainsText = true;
								}
							}
						}
						bool flag8 = true;
						if (formatInfo.isBeginTag && formatInfo.tagInfo.PreserveContent)
						{
							htmlWriter.Write(text2);
						}
						else
						{
							if (whiteSpaceType == HtmlFormatter.WhiteSpaceType.NotSignificant)
							{
								if (!flag5 && !flag7)
								{
									htmlWriter.WriteLineIfNotOnNewLine();
									flag8 = false;
								}
							}
							else if (whiteSpaceType == HtmlFormatter.WhiteSpaceType.Significant)
							{
								if (HtmlFormatter.FormattedTextWriter.HasFrontWhiteSpace(text2))
								{
									if (!flag5 && !flag7)
									{
										htmlWriter.WriteLineIfNotOnNewLine();
										flag8 = false;
									}
								}
							}
							else if (whiteSpaceType == HtmlFormatter.WhiteSpaceType.CarryThrough)
							{
								if (flag2 || HtmlFormatter.FormattedTextWriter.HasFrontWhiteSpace(text2))
								{
									if (!flag5 && !flag7)
									{
										htmlWriter.WriteLineIfNotOnNewLine();
										flag8 = false;
									}
								}
							}
							if (formatInfo.isBeginTag)
							{
								if (!formatInfo.tagInfo.NoIndent && !flag5)
								{
									htmlWriter.Indent++;
								}
							}
							if (flag7)
							{
								htmlWriter.Write(text2);
							}
							else
							{
								htmlWriter.WriteLiteral(text2, flag8);
							}
						}
						if (formatInfo2.isEndTag)
						{
							if (!formatInfo2.tagInfo.NoEndTag)
							{
								ArrayList arrayList = new ArrayList();
								bool flag9 = false;
								bool flag10 = false;
								if ((formatInfo2.tagInfo.Flags & HtmlFormatter.FormattingFlags.AllowPartialTags) != HtmlFormatter.FormattingFlags.None)
								{
									flag10 = true;
								}
								if (stack.Count > 0)
								{
									HtmlFormatter.FormatInfo formatInfo3 = (HtmlFormatter.FormatInfo)stack.Pop();
									arrayList.Add(formatInfo3);
									while (stack.Count > 0 && formatInfo3.tagInfo.TagName.ToLower() != formatInfo2.tagInfo.TagName.ToLower())
									{
										if ((formatInfo3.tagInfo.Flags & HtmlFormatter.FormattingFlags.AllowPartialTags) != HtmlFormatter.FormattingFlags.None)
										{
											flag10 = true;
											break;
										}
										formatInfo3 = (HtmlFormatter.FormatInfo)stack.Pop();
										arrayList.Add(formatInfo3);
									}
									if (formatInfo3.tagInfo.TagName.ToLower() != formatInfo2.tagInfo.TagName.ToLower())
									{
										for (int i = arrayList.Count - 1; i >= 0; i--)
										{
											stack.Push(arrayList[i]);
										}
									}
									else
									{
										flag9 = true;
										for (int i = 0; i < arrayList.Count - 1; i++)
										{
											HtmlFormatter.FormatInfo formatInfo4 = (HtmlFormatter.FormatInfo)arrayList[i];
											if (formatInfo4.tagInfo.IsXml)
											{
												if (stack2.Count > 1)
												{
													HtmlFormatter.HtmlWriter htmlWriter3 = (HtmlFormatter.HtmlWriter)stack2.Pop();
													htmlWriter = (HtmlFormatter.HtmlWriter)stack2.Peek();
													htmlWriter.Write(htmlWriter3.Content);
												}
											}
											if (!formatInfo4.tagInfo.NoEndTag)
											{
												htmlWriter.WriteLineIfNotOnNewLine();
												htmlWriter.Indent = formatInfo4.indent;
												if (flag && !flag10)
												{
													htmlWriter.Write("</" + formatInfo4.tagInfo.TagName + ">");
												}
											}
										}
										htmlWriter.Indent = formatInfo3.indent;
									}
								}
								if (flag9 || flag10)
								{
									if (!flag6 && !flag7 && !formatInfo2.tagInfo.IsInline && !formatInfo2.tagInfo.PreserveContent && (HtmlFormatter.FormattedTextWriter.IsWhiteSpace(text2) || HtmlFormatter.FormattedTextWriter.HasBackWhiteSpace(text2) || formatInfo2.tagInfo.FollowingWhiteSpaceType == HtmlFormatter.WhiteSpaceType.NotSignificant) && (!(formatInfo2.tagInfo is HtmlFormatter.TDTagInfo) || HtmlFormatter.FormattedTextWriter.HasBackWhiteSpace(text2)))
									{
										htmlWriter.WriteLineIfNotOnNewLine();
									}
									htmlWriter.Write("</");
									htmlWriter.Write(text3);
								}
								else
								{
									flag4 = true;
								}
								if (formatInfo2.tagInfo.IsXml)
								{
									if (stack2.Count > 1)
									{
										HtmlFormatter.HtmlWriter htmlWriter3 = (HtmlFormatter.HtmlWriter)stack2.Pop();
										htmlWriter = (HtmlFormatter.HtmlWriter)stack2.Peek();
										htmlWriter.Write(htmlWriter3.Content);
									}
								}
							}
							else
							{
								flag4 = true;
							}
						}
						else
						{
							bool flag11 = false;
							while (!flag11 && stack.Count > 0)
							{
								HtmlFormatter.FormatInfo formatInfo4 = (HtmlFormatter.FormatInfo)stack.Peek();
								flag11 = formatInfo4.tagInfo.CanContainTag(formatInfo2.tagInfo);
								if (!flag11)
								{
									stack.Pop();
									htmlWriter.Indent = formatInfo4.indent;
									if (flag)
									{
										if (!formatInfo4.tagInfo.IsInline)
										{
											htmlWriter.WriteLineIfNotOnNewLine();
										}
										htmlWriter.Write("</" + formatInfo4.tagInfo.TagName + ">");
									}
								}
							}
							formatInfo2.indent = htmlWriter.Indent;
							if (!flag7 && !formatInfo2.tagInfo.IsInline && !formatInfo2.tagInfo.PreserveContent && (HtmlFormatter.FormattedTextWriter.IsWhiteSpace(text2) || HtmlFormatter.FormattedTextWriter.HasBackWhiteSpace(text2) || (text2.Length == 0 && ((formatInfo.isBeginTag && formatInfo.tagInfo.InnerWhiteSpaceType == HtmlFormatter.WhiteSpaceType.NotSignificant) || (formatInfo.isEndTag && formatInfo.tagInfo.FollowingWhiteSpaceType == HtmlFormatter.WhiteSpaceType.NotSignificant)))))
							{
								htmlWriter.WriteLineIfNotOnNewLine();
							}
							if (!formatInfo2.tagInfo.NoEndTag)
							{
								stack.Push(formatInfo2);
							}
							else
							{
								flag3 = true;
							}
							if (formatInfo2.tagInfo.IsXml)
							{
								HtmlFormatter.HtmlWriter htmlWriter2 = new HtmlFormatter.XmlWriter(htmlWriter.Indent, formatInfo2.tagInfo.TagName, text, maxLineLength);
								stack2.Push(htmlWriter2);
								htmlWriter = htmlWriter2;
							}
							htmlWriter.Write('<');
							htmlWriter.Write(text3);
						}
						flag2 = HtmlFormatter.FormattedTextWriter.HasBackWhiteSpace(text2);
						text2 = string.Empty;
						formatInfo = formatInfo2;
					}
					break;
				}
				case 2:
					if (flag)
					{
						string text5 = string.Empty;
						if (!formatInfo.tagInfo.IsXml)
						{
							text5 = token.Text.ToLower();
						}
						else
						{
							text5 = token.Text;
						}
						htmlWriter.Write(text5);
						HtmlFormatter.Token nextToken = HtmlFormatter.HtmlTokenizer.GetNextToken(token);
						if (nextToken.Type != 15)
						{
							htmlWriter.Write("=\"" + text5 + "\"");
						}
					}
					else if (!formatInfo.tagInfo.IsXml)
					{
						if (this._attributeCasing == HtmlFormatter.HtmlFormatterCase.UpperCase)
						{
							htmlWriter.Write(token.Text.ToUpper());
						}
						else if (this._attributeCasing == HtmlFormatter.HtmlFormatterCase.LowerCase)
						{
							htmlWriter.Write(token.Text.ToLower());
						}
						else
						{
							htmlWriter.Write(token.Text);
						}
					}
					else
					{
						htmlWriter.Write(token.Text);
					}
					break;
				case 3:
					if (flag && token2.Type != 13 && token2.Type != 14)
					{
						htmlWriter.Write('"');
						htmlWriter.Write(token.Text.Replace("\"", "&quot;"));
						htmlWriter.Write('"');
					}
					else
					{
						htmlWriter.Write(token.Text);
					}
					break;
				case 4:
				case 20:
				case 21:
				case 23:
					if (flag)
					{
						text2 += token.Text.Replace("&nbsp;", "&#160;");
					}
					else
					{
						text2 += token.Text;
					}
					break;
				case 5:
					formatInfo.isEndTag = true;
					if (!formatInfo.tagInfo.NoEndTag)
					{
						stack.Pop();
						if (formatInfo.tagInfo.IsXml)
						{
							HtmlFormatter.HtmlWriter htmlWriter3 = (HtmlFormatter.HtmlWriter)stack2.Pop();
							htmlWriter = (HtmlFormatter.HtmlWriter)stack2.Peek();
							htmlWriter.Write(htmlWriter3.Content);
						}
					}
					if (token2.Type == 0 && token2.Text.Length > 0)
					{
						htmlWriter.Write("/>");
					}
					else
					{
						htmlWriter.Write(" />");
					}
					break;
				case 6:
					break;
				case 8:
					if (token2.Type == 10)
					{
						htmlWriter.Write('<');
					}
					htmlWriter.Write(token.Text);
					break;
				case 9:
				case 16:
				case 17:
				case 18:
				case 19:
					goto IL_0F03;
				case 10:
				case 12:
					break;
				case 11:
					if (flag)
					{
						if (flag4)
						{
							flag4 = false;
						}
						else if (flag3 && !formatInfo.tagInfo.IsComment)
						{
							htmlWriter.Write(" />");
						}
						else
						{
							htmlWriter.Write('>');
						}
					}
					else
					{
						htmlWriter.Write('>');
					}
					break;
				case 13:
					htmlWriter.Write('"');
					break;
				case 14:
					htmlWriter.Write('\'');
					break;
				case 15:
					htmlWriter.Write('=');
					break;
				case 24:
					htmlWriter.WriteLineIfNotOnNewLine();
					htmlWriter.Write('<');
					htmlWriter.Write(token.Text);
					htmlWriter.Write('>');
					htmlWriter.WriteLineIfNotOnNewLine();
					flag4 = true;
					break;
				default:
					goto IL_0F03;
				}
				IL_0F10:
				token2 = token;
				token = HtmlFormatter.HtmlTokenizer.GetNextToken(token);
				continue;
				IL_0F03:
				Debug.Fail("Invalid token type!");
				goto IL_0F10;
			}
			if (text2.Length > 0)
			{
				htmlWriter.Write(text2);
			}
			while (stack2.Count > 1)
			{
				HtmlFormatter.HtmlWriter htmlWriter3 = (HtmlFormatter.HtmlWriter)stack2.Pop();
				htmlWriter = (HtmlFormatter.HtmlWriter)stack2.Peek();
				htmlWriter.Write(htmlWriter3.Content);
			}
			htmlWriter.Flush();
		}
		private static IDictionary tagTable = new HybridDictionary(true);
		private static HtmlFormatter.TagInfo commentTag = new HtmlFormatter.TagInfo("", HtmlFormatter.FormattingFlags.NoEndTag | HtmlFormatter.FormattingFlags.Comment, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.ElementType.Any);
		private static HtmlFormatter.TagInfo directiveTag = new HtmlFormatter.TagInfo("", HtmlFormatter.FormattingFlags.NoEndTag, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Any);
		private static HtmlFormatter.TagInfo otherServerSideScriptTag = new HtmlFormatter.TagInfo("", HtmlFormatter.FormattingFlags.Inline | HtmlFormatter.FormattingFlags.NoEndTag, HtmlFormatter.ElementType.Any);
		private static HtmlFormatter.TagInfo nestedXmlTag = new HtmlFormatter.TagInfo("", HtmlFormatter.FormattingFlags.AllowPartialTags, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Any);
		private static HtmlFormatter.TagInfo unknownXmlTag = new HtmlFormatter.TagInfo("", HtmlFormatter.FormattingFlags.Xml, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Any);
		private static HtmlFormatter.TagInfo unknownHtmlTag = new HtmlFormatter.TagInfo("", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Any);
		private HtmlFormatter.HtmlFormatterCase _elementCasing = HtmlFormatter.HtmlFormatterCase.LowerCase;
		private HtmlFormatter.HtmlFormatterCase _attributeCasing = HtmlFormatter.HtmlFormatterCase.LowerCase;
		private char _indentChar = '\t';
		private int _indentSize = 1;
		private int _maxLineLength = 80;
		private delegate bool CanContainTag(HtmlFormatter.TagInfo info);
		private class FormatInfo
		{
			public FormatInfo(HtmlFormatter.TagInfo info, bool isEnd)
			{
				this.tagInfo = info;
				this.isEndTag = isEnd;
			}
			public bool isBeginTag
			{
				get
				{
					return !this.isEndTag;
				}
			}
			public HtmlFormatter.TagInfo tagInfo;
			public bool isEndTag;
			public int indent;
		}
		private class Token
		{
			public Token(int type, int endState, int startIndex, int endIndex, char[] chars, int charsLength)
			{
				this._type = type;
				this._chars = chars;
				this._charsLength = charsLength;
				this._startIndex = startIndex;
				this._endIndex = endIndex;
				this._endState = endState;
			}
			internal char[] Chars
			{
				get
				{
					return this._chars;
				}
			}
			internal int CharsLength
			{
				get
				{
					return this._charsLength;
				}
			}
			public int EndIndex
			{
				get
				{
					return this._endIndex;
				}
			}
			public int EndState
			{
				get
				{
					return this._endState;
				}
			}
			public int Length
			{
				get
				{
					return this._endIndex - this._startIndex;
				}
			}
			public int StartIndex
			{
				get
				{
					return this._startIndex;
				}
			}
			public string Text
			{
				get
				{
					if (this._text == null)
					{
						this._text = new string(this._chars, this.StartIndex, this.EndIndex - this.StartIndex);
					}
					return this._text;
				}
			}
			public int Type
			{
				get
				{
					return this._type;
				}
			}
			public override string ToString()
			{
				string text = "'" + this.Text + "'";
				switch (this.Type)
				{
				case 0:
					text += "(Whitespace)";
					break;
				case 1:
					text += "(Tag)";
					break;
				case 2:
					text += "(AttrName)";
					break;
				case 3:
					text += "(AttrVal)";
					break;
				case 4:
					text += "(Text)";
					break;
				case 5:
					text += "(SelfTerm)";
					break;
				case 6:
					text += "(Empty)";
					break;
				case 7:
					text += "(Comment)";
					break;
				case 8:
					text += "(Error)";
					break;
				case 10:
					text += "(OpenBracket)";
					break;
				case 11:
					text += "(CloseBracket)";
					break;
				case 12:
					text += "(ForwardSlash)";
					break;
				case 13:
					text += "(DoubleQuote)";
					break;
				case 14:
					text += "(SingleQuote)";
					break;
				case 15:
					text += "(Equals)";
					break;
				case 20:
					text += "(ClientScriptBlock)";
					break;
				case 21:
					text += "(Style)";
					break;
				case 22:
					text += "(InlineServerScript)";
					break;
				case 23:
					text += "(ServerScriptBlock)";
					break;
				}
				return text;
			}
			public const int Whitespace = 0;
			public const int TagName = 1;
			public const int AttrName = 2;
			public const int AttrVal = 3;
			public const int TextToken = 4;
			public const int SelfTerminating = 5;
			public const int Empty = 6;
			public const int Comment = 7;
			public const int Error = 8;
			public const int OpenBracket = 10;
			public const int CloseBracket = 11;
			public const int ForwardSlash = 12;
			public const int DoubleQuote = 13;
			public const int SingleQuote = 14;
			public const int EqualsChar = 15;
			public const int ClientScriptBlock = 20;
			public const int Style = 21;
			public const int InlineServerScript = 22;
			public const int ServerScriptBlock = 23;
			public const int XmlDirective = 24;
			private int _type;
			private char[] _chars;
			private int _charsLength;
			private string _text;
			private int _startIndex;
			private int _endIndex;
			private int _endState;
		}
		private class HtmlTokenizer
		{
			public static HtmlFormatter.Token GetFirstToken(char[] chars)
			{
				if (chars == null)
				{
					throw new ArgumentNullException("chars");
				}
				return HtmlFormatter.HtmlTokenizer.GetNextToken(chars, chars.Length, 0, 0);
			}
			public static HtmlFormatter.Token GetFirstToken(char[] chars, int length, int initialState)
			{
				return HtmlFormatter.HtmlTokenizer.GetNextToken(chars, length, 0, initialState);
			}
			public static HtmlFormatter.Token GetNextToken(HtmlFormatter.Token token)
			{
				if (token == null)
				{
					throw new ArgumentNullException("token");
				}
				return HtmlFormatter.HtmlTokenizer.GetNextToken(token.Chars, token.CharsLength, token.EndIndex, token.EndState);
			}
			public static HtmlFormatter.Token GetNextToken(char[] chars, int length, int startIndex, int startState)
			{
				if (chars == null)
				{
					throw new ArgumentNullException("chars");
				}
				HtmlFormatter.Token token;
				if (startIndex >= length)
				{
					token = null;
				}
				else
				{
					int num = startState;
					bool flag = (startState & 256) != 0;
					int num2 = (flag ? 256 : 0);
					bool flag2 = (startState & 512) != 0;
					int num3 = (flag2 ? 512 : 0);
					bool flag3 = (startState & 1024) != 0;
					int num4 = (flag3 ? 1024 : 0);
					bool flag4 = (startState & 2048) != 0;
					int num5 = (flag4 ? 2048 : 0);
					int num6 = startIndex;
					int num7 = startIndex;
					HtmlFormatter.Token token2 = null;
					while (token2 == null && num6 < length)
					{
						char c = chars[num6];
						int num8 = num & 255;
						if (num8 <= 50)
						{
							switch (num8)
							{
							case 0:
								if (c == '<')
								{
									num = 1;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(4, num, num7, num9, chars, length);
								}
								break;
							case 1:
								if (c == '<')
								{
									if (num6 + 1 < length && chars[num6 + 1] == '%')
									{
										num = 30 | num2 | num3;
										num7 = num6;
									}
									else
									{
										num = 2 | num2 | num3;
										int num9 = num6 + 1;
										token2 = new HtmlFormatter.Token(10, num, num7, num9, chars, length);
									}
								}
								else
								{
									num = 16;
								}
								break;
							case 2:
								if (c == '/')
								{
									num = 3 | num2 | num3;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(6, num, num7, num9, chars, length);
								}
								else if (c == '!')
								{
									num = 100 | num2 | num3;
									num7 = num6;
								}
								else if (c == '%')
								{
									num = 30;
									num7 = num6;
								}
								else if (HtmlFormatter.HtmlTokenizer.IsWordChar(c))
								{
									num = 5 | num2 | num3;
									num7 = num6;
								}
								else
								{
									num = 16;
								}
								break;
							case 3:
								if (c == '/')
								{
									num = 4 | num2 | num3;
									int num9 = num6 + 1;
									token2 = new HtmlFormatter.Token(12, num, num7, num9, chars, length);
								}
								else
								{
									num = 16;
								}
								break;
							case 4:
								if (HtmlFormatter.HtmlTokenizer.IsWordChar(c))
								{
									num = 5 | num2 | num3;
									num7 = num6;
								}
								else
								{
									num = 16;
								}
								break;
							case 5:
								if (HtmlFormatter.HtmlTokenizer.IsWhitespace(c))
								{
									num = 6;
									int num9 = num6;
									string text = new string(chars, num7, num9 - num7);
									if (text.ToLower().Equals("script"))
									{
										if (!flag)
										{
											num |= 256;
										}
									}
									else if (text.ToLower().Equals("style"))
									{
										if (!flag2)
										{
											num |= 512;
										}
									}
									token2 = new HtmlFormatter.Token(1, num, num7, num9, chars, length);
								}
								else if (c == '>')
								{
									num = 17;
									int num9 = num6;
									string text = new string(chars, num7, num9 - num7);
									if (text.ToLower().Equals("script"))
									{
										if (!flag)
										{
											num |= 256;
										}
									}
									else if (text.ToLower().Equals("style"))
									{
										if (!flag2)
										{
											num |= 512;
										}
									}
									token2 = new HtmlFormatter.Token(1, num, num7, num9, chars, length);
								}
								else if (!HtmlFormatter.HtmlTokenizer.IsWordChar(c))
								{
									if (c == '/')
									{
										num = 15;
										int num9 = num6;
										string text = new string(chars, num7, num9 - num7);
										if (text.ToLower().Equals("script"))
										{
											if (!flag)
											{
												num |= 256;
											}
										}
										else if (text.ToLower().Equals("style"))
										{
											if (!flag2)
											{
												num |= 512;
											}
										}
										token2 = new HtmlFormatter.Token(1, num, num7, num9, chars, length);
									}
									else
									{
										num = 16;
									}
								}
								break;
							case 6:
								if (HtmlFormatter.HtmlTokenizer.IsWordChar(c))
								{
									num = 7 | num2 | num3 | num5;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(0, num, num7, num9, chars, length);
								}
								else if (c == '>')
								{
									num = 17 | num2 | num3 | num5;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(0, num, num7, num9, chars, length);
								}
								else if (c == '/')
								{
									num = 15 | num2 | num3;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(0, num, num7, num9, chars, length);
								}
								else if (!HtmlFormatter.HtmlTokenizer.IsWhitespace(c))
								{
									num = 16;
								}
								break;
							case 7:
								if (HtmlFormatter.HtmlTokenizer.IsWhitespace(c))
								{
									num = 8 | num2 | num3 | num5;
									int num9 = num6;
									if (flag)
									{
										if (new string(chars, num7, num9 - num7).ToLower() == "runat")
										{
											num |= 1024;
										}
									}
									token2 = new HtmlFormatter.Token(2, num, num7, num9, chars, length);
								}
								else if (c == '=')
								{
									num = 8 | num2 | num3 | num5;
									int num9 = num6;
									if (flag)
									{
										if (new string(chars, num7, num9 - num7).ToLower() == "runat")
										{
											num |= 1024;
										}
									}
									token2 = new HtmlFormatter.Token(2, num, num7, num9, chars, length);
								}
								else if (c == '>')
								{
									num = 17 | num2 | num3 | num5;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(2, num, num7, num9, chars, length);
								}
								else if (c == '/')
								{
									num = 15 | num2 | num3;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(2, num, num7, num9, chars, length);
								}
								else if (!HtmlFormatter.HtmlTokenizer.IsWordChar(c))
								{
									num = 16;
								}
								break;
							case 8:
								if (c == '=')
								{
									num = 9 | num2 | num3 | num4 | num5;
									num7 = num6;
									int num9 = num6 + 1;
									token2 = new HtmlFormatter.Token(15, num, num7, num9, chars, length);
								}
								else if (c == '>')
								{
									num = 17 | num2 | num3 | num5;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(0, num, num7, num9, chars, length);
								}
								else if (c == '/')
								{
									num = 15;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(0, num, num7, num9, chars, length);
								}
								else if (HtmlFormatter.HtmlTokenizer.IsWordChar(c))
								{
									num = 7 | num2 | num3 | num5;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(0, num, num7, num9, chars, length);
								}
								else if (!HtmlFormatter.HtmlTokenizer.IsWhitespace(c))
								{
									num = 16;
								}
								break;
							case 9:
								if (c == '\'')
								{
									num = 20 | num2 | num3 | num4 | num5;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(0, num, num7, num9, chars, length);
								}
								else if (c == '"')
								{
									num = 19 | num2 | num3 | num4 | num5;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(0, num, num7, num9, chars, length);
								}
								else if (HtmlFormatter.HtmlTokenizer.IsWordChar(c))
								{
									num = 14 | num2 | num3 | num4 | num5;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(0, num, num7, num9, chars, length);
								}
								else if (!HtmlFormatter.HtmlTokenizer.IsWhitespace(c))
								{
									num = 16;
								}
								break;
							case 10:
								if (c == '"')
								{
									num = 11 | num2 | num3 | num5;
									int num9 = num6;
									if (flag3 && new string(chars, num7, num9 - num7).ToLower() == "server")
									{
										num |= 2048;
									}
									token2 = new HtmlFormatter.Token(3, num, num7, num9, chars, length);
								}
								break;
							case 11:
								if (c == '"')
								{
									num = 6 | num2 | num3 | num5;
									int num9 = num6 + 1;
									token2 = new HtmlFormatter.Token(13, num, num7, num9, chars, length);
								}
								else
								{
									num = 16;
								}
								break;
							case 12:
								if (c == '\'')
								{
									num = 13 | num2 | num3 | num5;
									int num9 = num6;
									if (flag3 && new string(chars, num7, num9 - num7).ToLower() == "server")
									{
										num |= 2048;
									}
									token2 = new HtmlFormatter.Token(3, num, num7, num9, chars, length);
								}
								break;
							case 13:
								if (c == '\'')
								{
									num = 6 | num2 | num3 | num5;
									int num9 = num6 + 1;
									token2 = new HtmlFormatter.Token(14, num, num7, num9, chars, length);
								}
								else
								{
									num = 16;
								}
								break;
							case 14:
								if (HtmlFormatter.HtmlTokenizer.IsWhitespace(c))
								{
									num = 6 | num2 | num3 | num5;
									int num9 = num6;
									if (flag3 && new string(chars, num7, num9 - num7).ToLower() == "server")
									{
										num |= 2048;
									}
									token2 = new HtmlFormatter.Token(3, num, num7, num9, chars, length);
								}
								else if (c == '>')
								{
									num = 17 | num2 | num3 | num5;
									int num9 = num6;
									if (flag3 && new string(chars, num7, num9 - num7).ToLower() == "server")
									{
										num |= 2048;
									}
									token2 = new HtmlFormatter.Token(3, num, num7, num9, chars, length);
								}
								else if (c == '/')
								{
									if (num6 + 1 < length && chars[num6 + 1] == '>')
									{
										num = 15 | num2 | num3 | num5;
										int num9 = num6;
										if (flag3 && new string(chars, num7, num9 - num7).ToLower() == "server")
										{
											num |= 2048;
										}
										token2 = new HtmlFormatter.Token(3, num, num7, num9, chars, length);
									}
								}
								break;
							case 15:
								if (c == '/' && num6 + 1 < length && chars[num6 + 1] == '>')
								{
									num = 0;
									int num9 = num6 + 2;
									token2 = new HtmlFormatter.Token(5, num, num7, num9, chars, length);
								}
								else
								{
									num = 16;
								}
								break;
							case 16:
								if (c == '>')
								{
									num = 17;
									int num9 = num6;
									token2 = new HtmlFormatter.Token(8, num, num7, num9, chars, length);
								}
								break;
							case 17:
								if (c == '>')
								{
									if (flag)
									{
										num = 40 | num2 | num3 | num5;
									}
									else if (flag2)
									{
										num = 50 | num2 | num3;
									}
									else
									{
										num = 0;
									}
									int num9 = num6 + 1;
									token2 = new HtmlFormatter.Token(11, num, num7, num9, chars, length);
								}
								else
								{
									num = 16;
								}
								break;
							case 18:
								if (c == '=')
								{
									num = 9 | num2 | num3 | num4 | num5;
									int num9 = num6 + 1;
									token2 = new HtmlFormatter.Token(15, num, num7, num9, chars, length);
								}
								else
								{
									num = 16;
								}
								break;
							case 19:
								if (c == '"')
								{
									num = 10 | num2 | num3 | num4 | num5;
									int num9 = num6 + 1;
									token2 = new HtmlFormatter.Token(13, num, num7, num9, chars, length);
								}
								else
								{
									num = 16;
								}
								break;
							case 20:
								if (c == '\'')
								{
									num = 12 | num2 | num3 | num4 | num5;
									int num9 = num6 + 1;
									token2 = new HtmlFormatter.Token(14, num, num7, num9, chars, length);
								}
								else
								{
									num = 16;
								}
								break;
							case 21:
							case 22:
							case 23:
							case 24:
							case 25:
							case 26:
							case 27:
							case 28:
							case 29:
							case 31:
							case 32:
							case 33:
							case 34:
							case 35:
							case 36:
							case 37:
							case 38:
							case 39:
								break;
							case 30:
							{
								int num10 = HtmlFormatter.HtmlTokenizer.IndexOf(chars, num6, length, "%>");
								if (num10 > -1)
								{
									num = 0;
									int num9 = num10 + 2;
									token2 = new HtmlFormatter.Token(22, num, num7, num9, chars, length);
								}
								else
								{
									num6 = length;
								}
								break;
							}
							case 40:
							{
								int num11 = HtmlFormatter.HtmlTokenizer.IndexOf(chars, num6, length, "</script>");
								if (num11 > -1)
								{
									num = 1 | num2 | num3 | num5;
									int num9 = num11;
									if (flag4)
									{
										token2 = new HtmlFormatter.Token(23, num, num7, num9, chars, length);
									}
									else
									{
										token2 = new HtmlFormatter.Token(20, num, num7, num9, chars, length);
									}
								}
								else
								{
									num6 = length - 1;
								}
								break;
							}
							default:
								if (num8 == 50)
								{
									int num12 = HtmlFormatter.HtmlTokenizer.IndexOf(chars, num6, length, "</style>");
									if (num12 > -1)
									{
										num = 1 | num2 | num3;
										int num9 = num12;
										token2 = new HtmlFormatter.Token(21, num, num7, num9, chars, length);
									}
									else
									{
										num6 = length - 1;
									}
								}
								break;
							}
						}
						else if (num8 != 60)
						{
							switch (num8)
							{
							case 100:
								if (c == '-')
								{
									num = 101;
								}
								else if (HtmlFormatter.HtmlTokenizer.IsWordChar(c))
								{
									num = 60;
								}
								else
								{
									num = 16;
								}
								break;
							case 101:
								if (c == '-')
								{
									num = 102;
								}
								else
								{
									num = 16;
								}
								break;
							case 102:
								if (c == '-')
								{
									num = 103;
								}
								break;
							case 103:
								if (c == '-')
								{
									num = 104;
								}
								else
								{
									num = 102;
								}
								break;
							case 104:
								if (!char.IsWhiteSpace(c))
								{
									if (c == '>')
									{
										num = 17;
										int num9 = num6;
										token2 = new HtmlFormatter.Token(7, num, num7, num9, chars, length);
									}
									else
									{
										num = 102;
									}
								}
								break;
							}
						}
						else if (c == '>')
						{
							num = 17;
							int num9 = num6;
							token2 = new HtmlFormatter.Token(24, num, num7, num9, chars, length);
						}
						num6++;
					}
					if (num6 >= length && token2 == null)
					{
						int num8 = num & 255;
						int num13;
						if (num8 <= 30)
						{
							if (num8 == 0)
							{
								num13 = 4;
								goto IL_0FC9;
							}
							if (num8 == 30)
							{
								num13 = 22;
								goto IL_0FC9;
							}
						}
						else
						{
							if (num8 == 40)
							{
								if (flag4)
								{
									num13 = 23;
								}
								else
								{
									num13 = 20;
								}
								goto IL_0FC9;
							}
							if (num8 == 50)
							{
								num13 = 21;
								goto IL_0FC9;
							}
							switch (num8)
							{
							case 100:
							case 101:
							case 102:
							case 103:
							case 104:
								num13 = 7;
								goto IL_0FC9;
							}
						}
						num13 = 8;
						num = 16;
						IL_0FC9:
						int num9 = num6;
						token2 = new HtmlFormatter.Token(num13, num, num7, num9, chars, length);
					}
					token = token2;
				}
				return token;
			}
			private static bool IsWhitespace(char c)
			{
				return char.IsWhiteSpace(c);
			}
			private static bool IsWordChar(char c)
			{
				return char.IsLetterOrDigit(c) || c == '_' || c == ':' || c == '#' || c == '-' || c == '.';
			}
			private static int IndexOf(char[] chars, int startIndex, int endColumnNumber, string s)
			{
				int length = s.Length;
				int num = endColumnNumber - length + 1;
				for (int i = startIndex; i < num; i++)
				{
					bool flag = true;
					for (int j = 0; j < length; j++)
					{
						if (char.ToUpper(chars[i + j]) != char.ToUpper(s[j]))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return i;
					}
				}
				return -1;
			}
		}
		private class HtmlTokenizerStates
		{
			public const int Text = 0;
			public const int StartTag = 1;
			public const int ExpTag = 2;
			public const int ForwardSlash = 3;
			public const int ExpTagAfterSlash = 4;
			public const int InTagName = 5;
			public const int ExpAttr = 6;
			public const int InAttr = 7;
			public const int ExpEquals = 8;
			public const int ExpAttrVal = 9;
			public const int InDoubleQuoteAttrVal = 10;
			public const int EndDoubleQuote = 11;
			public const int InSingleQuoteAttrVal = 12;
			public const int EndSingleQuote = 13;
			public const int InAttrVal = 14;
			public const int SelfTerminating = 15;
			public const int Error = 16;
			public const int EndTag = 17;
			public const int EqualsChar = 18;
			public const int BeginDoubleQuote = 19;
			public const int BeginSingleQuote = 20;
			public const int ServerSideScript = 30;
			public const int Script = 40;
			public const int Style = 50;
			public const int XmlDirective = 60;
			public const int BeginCommentTag1 = 100;
			public const int BeginCommentTag2 = 101;
			public const int InCommentTag = 102;
			public const int EndCommentTag1 = 103;
			public const int EndCommentTag2 = 104;
			public const int ScriptState = 256;
			public const int StyleState = 512;
			public const int RunAtState = 1024;
			public const int RunAtServerState = 2048;
		}
		internal enum ElementType
		{
			Other,
			Block,
			Inline,
			Any
		}
		internal class FormattedTextWriter : TextWriter
		{
			public FormattedTextWriter(TextWriter writer, string indentString)
			{
				this.baseWriter = writer;
				this.indentString = indentString;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override Encoding Encoding
			{
				get
				{
					return this.baseWriter.Encoding;
				}
			}
			public override string NewLine
			{
				get
				{
					return this.baseWriter.NewLine;
				}
				set
				{
					this.baseWriter.NewLine = value;
				}
			}
			public int Indent
			{
				get
				{
					return this.indentLevel;
				}
				set
				{
					if (value < 0)
					{
						value = 0;
					}
					this.indentLevel = value;
					Debug.Assert(value >= 0, "Invalid IndentLevel");
				}
			}
			public override void Close()
			{
				this.baseWriter.Close();
			}
			public override void Flush()
			{
				this.baseWriter.Flush();
			}
			public static bool HasBackWhiteSpace(string s)
			{
				return s != null && s.Length != 0 && char.IsWhiteSpace(s[s.Length - 1]);
			}
			public static bool HasFrontWhiteSpace(string s)
			{
				return s != null && s.Length != 0 && char.IsWhiteSpace(s[0]);
			}
			public static bool IsWhiteSpace(string s)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (!char.IsWhiteSpace(s[i]))
					{
						return false;
					}
				}
				return true;
			}
			private string MakeSingleLine(string s)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int i = 0;
				while (i < s.Length)
				{
					char c = s[i];
					if (char.IsWhiteSpace(c))
					{
						stringBuilder.Append(' ');
						while (i < s.Length && char.IsWhiteSpace(s[i]))
						{
							i++;
						}
					}
					else
					{
						stringBuilder.Append(c);
						i++;
					}
				}
				return stringBuilder.ToString();
			}
			public static string Trim(string text, bool frontWhiteSpace)
			{
				string text2;
				if (text.Length == 0)
				{
					text2 = string.Empty;
				}
				else if (HtmlFormatter.FormattedTextWriter.IsWhiteSpace(text))
				{
					if (frontWhiteSpace)
					{
						text2 = " ";
					}
					else
					{
						text2 = string.Empty;
					}
				}
				else
				{
					string text3 = text.Trim();
					if (frontWhiteSpace && HtmlFormatter.FormattedTextWriter.HasFrontWhiteSpace(text))
					{
						text3 = ' ' + text3;
					}
					if (HtmlFormatter.FormattedTextWriter.HasBackWhiteSpace(text))
					{
						text3 += ' ';
					}
					text2 = text3;
				}
				return text2;
			}
			private void OutputIndent()
			{
				if (this.indentPending)
				{
					for (int i = 0; i < this.indentLevel; i++)
					{
						this.baseWriter.Write(this.indentString);
					}
					this.indentPending = false;
				}
			}
			public void WriteLiteral(string s)
			{
				if (s.Length != 0)
				{
					StringReader stringReader = new StringReader(s);
					string text = stringReader.ReadLine();
					string text2 = stringReader.ReadLine();
					while (text != null)
					{
						this.Write(text);
						text = text2;
						text2 = stringReader.ReadLine();
						if (text != null)
						{
							this.WriteLine();
						}
						if (text2 != null)
						{
							text = text.Trim();
						}
						else if (text != null)
						{
							text = HtmlFormatter.FormattedTextWriter.Trim(text, false);
						}
					}
				}
			}
			public void WriteLiteralWrapped(string s, int maxLength)
			{
				if (s.Length != 0)
				{
					string[] array = this.MakeSingleLine(s).Split(null);
					if (HtmlFormatter.FormattedTextWriter.HasFrontWhiteSpace(s))
					{
						this.Write(' ');
					}
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].Length > 0)
						{
							this.Write(array[i]);
							if (i < array.Length - 1 && array[i + 1].Length > 0)
							{
								if (this.currentColumn > maxLength)
								{
									this.WriteLine();
								}
								else
								{
									this.Write(' ');
								}
							}
						}
					}
					if (HtmlFormatter.FormattedTextWriter.HasBackWhiteSpace(s) && !HtmlFormatter.FormattedTextWriter.IsWhiteSpace(s))
					{
						this.Write(' ');
					}
				}
			}
			public void WriteLineIfNotOnNewLine()
			{
				if (!this.onNewLine)
				{
					this.baseWriter.WriteLine();
					this.onNewLine = true;
					this.currentColumn = 0;
					this.indentPending = true;
				}
			}
			public override void Write(string s)
			{
				this.OutputIndent();
				this.baseWriter.Write(s);
				this.onNewLine = false;
				this.currentColumn += s.Length;
			}
			public override void Write(bool value)
			{
				this.OutputIndent();
				this.baseWriter.Write(value);
				this.onNewLine = false;
				this.currentColumn += value.ToString().Length;
			}
			public override void Write(char value)
			{
				this.OutputIndent();
				this.baseWriter.Write(value);
				this.onNewLine = false;
				this.currentColumn++;
			}
			public override void Write(char[] buffer)
			{
				this.OutputIndent();
				this.baseWriter.Write(buffer);
				this.onNewLine = false;
				this.currentColumn += buffer.Length;
			}
			public override void Write(char[] buffer, int index, int count)
			{
				this.OutputIndent();
				this.baseWriter.Write(buffer, index, count);
				this.onNewLine = false;
				this.currentColumn += count;
			}
			public override void Write(double value)
			{
				this.OutputIndent();
				this.baseWriter.Write(value);
				this.onNewLine = false;
				this.currentColumn += value.ToString().Length;
			}
			public override void Write(float value)
			{
				this.OutputIndent();
				this.baseWriter.Write(value);
				this.onNewLine = false;
				this.currentColumn += value.ToString().Length;
			}
			public override void Write(int value)
			{
				this.OutputIndent();
				this.baseWriter.Write(value);
				this.onNewLine = false;
				this.currentColumn += value.ToString().Length;
			}
			public override void Write(long value)
			{
				this.OutputIndent();
				this.baseWriter.Write(value);
				this.onNewLine = false;
				this.currentColumn += value.ToString().Length;
			}
			public override void Write(object value)
			{
				this.OutputIndent();
				this.baseWriter.Write(value);
				this.onNewLine = false;
				this.currentColumn += value.ToString().Length;
			}
			public override void Write(string format, object arg0)
			{
				this.OutputIndent();
				string text = string.Format(format, arg0);
				this.baseWriter.Write(text);
				this.onNewLine = false;
				this.currentColumn += text.Length;
			}
			public override void Write(string format, object arg0, object arg1)
			{
				this.OutputIndent();
				string text = string.Format(format, arg0, arg1);
				this.baseWriter.Write(text);
				this.onNewLine = false;
				this.currentColumn += text.Length;
			}
			public override void Write(string format, params object[] arg)
			{
				this.OutputIndent();
				string text = string.Format(format, arg);
				this.baseWriter.Write(text);
				this.onNewLine = false;
				this.currentColumn += text.Length;
			}
			public override void WriteLine(string s)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(s);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine()
			{
				this.OutputIndent();
				this.baseWriter.WriteLine();
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(bool value)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(value);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(char value)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(value);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(char[] buffer)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(buffer);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(char[] buffer, int index, int count)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(buffer, index, count);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(double value)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(value);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(float value)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(value);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(int value)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(value);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(long value)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(value);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(object value)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(value);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(string format, object arg0)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(format, arg0);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(string format, object arg0, object arg1)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(format, arg0, arg1);
				this.indentPending = true;
				this.onNewLine = true;
				this.currentColumn = 0;
			}
			public override void WriteLine(string format, params object[] arg)
			{
				this.OutputIndent();
				this.baseWriter.WriteLine(format, arg);
				this.indentPending = true;
				this.currentColumn = 0;
				this.onNewLine = true;
			}
			private TextWriter baseWriter;
			private string indentString;
			private int currentColumn;
			private int indentLevel;
			private bool indentPending;
			private bool onNewLine;
		}
		[Flags]
		internal enum FormattingFlags
		{
			None = 0,
			Inline = 1,
			NoIndent = 2,
			NoEndTag = 4,
			PreserveContent = 8,
			Xml = 16,
			Comment = 32,
			AllowPartialTags = 64
		}
		internal class HtmlWriter
		{
			public HtmlWriter(TextWriter writer, string indentString, int maxLineLength)
			{
				this._baseWriter = writer;
				this._maxLineLength = maxLineLength;
				this._writer = new HtmlFormatter.FormattedTextWriter(this._baseWriter, indentString);
			}
			protected TextWriter BaseWriter
			{
				get
				{
					return this._baseWriter;
				}
			}
			public virtual string Content
			{
				get
				{
					this._writer.Flush();
					return this._baseWriter.ToString();
				}
			}
			public int Indent
			{
				get
				{
					return this._writer.Indent;
				}
				set
				{
					this._writer.Indent = value;
				}
			}
			public void Flush()
			{
				this._writer.Flush();
			}
			public HtmlFormatter.FormattedTextWriter Writer
			{
				get
				{
					return this._writer;
				}
			}
			public virtual void Write(char c)
			{
				this._writer.Write(c);
			}
			public virtual void Write(string s)
			{
				this._writer.Write(s);
			}
			public virtual void WriteLiteral(string s, bool frontWhiteSpace)
			{
				this._writer.WriteLiteralWrapped(HtmlFormatter.FormattedTextWriter.Trim(s, frontWhiteSpace), this._maxLineLength);
			}
			public virtual void WriteLineIfNotOnNewLine()
			{
				this._writer.WriteLineIfNotOnNewLine();
			}
			private HtmlFormatter.FormattedTextWriter _writer;
			private TextWriter _baseWriter;
			private int _maxLineLength;
		}
		internal class TagInfo
		{
			public TagInfo(string tagName, HtmlFormatter.FormattingFlags flags)
				: this(tagName, flags, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.ElementType.Other)
			{
			}
			public TagInfo(string tagName, HtmlFormatter.FormattingFlags flags, HtmlFormatter.ElementType type)
				: this(tagName, flags, HtmlFormatter.WhiteSpaceType.CarryThrough, HtmlFormatter.WhiteSpaceType.CarryThrough, type)
			{
			}
			public TagInfo(string tagName, HtmlFormatter.FormattingFlags flags, HtmlFormatter.WhiteSpaceType innerWhiteSpace, HtmlFormatter.WhiteSpaceType followingWhiteSpace)
				: this(tagName, flags, innerWhiteSpace, followingWhiteSpace, HtmlFormatter.ElementType.Other)
			{
			}
			public TagInfo(string tagName, HtmlFormatter.FormattingFlags flags, HtmlFormatter.WhiteSpaceType innerWhiteSpace, HtmlFormatter.WhiteSpaceType followingWhiteSpace, HtmlFormatter.ElementType type)
			{
				Debug.Assert(innerWhiteSpace == HtmlFormatter.WhiteSpaceType.NotSignificant || innerWhiteSpace == HtmlFormatter.WhiteSpaceType.Significant || innerWhiteSpace == HtmlFormatter.WhiteSpaceType.CarryThrough, "Invalid whitespace type");
				Debug.Assert(followingWhiteSpace == HtmlFormatter.WhiteSpaceType.NotSignificant || followingWhiteSpace == HtmlFormatter.WhiteSpaceType.Significant || followingWhiteSpace == HtmlFormatter.WhiteSpaceType.CarryThrough, "Invalid whitespace type");
				this._tagName = tagName;
				this._inner = innerWhiteSpace;
				this._following = followingWhiteSpace;
				this._flags = flags;
				this._type = type;
			}
			public TagInfo(string newTagName, HtmlFormatter.TagInfo info)
			{
				this._tagName = newTagName;
				this._inner = info.InnerWhiteSpaceType;
				this._following = info.FollowingWhiteSpaceType;
				this._flags = info.Flags;
				this._type = info.Type;
			}
			public HtmlFormatter.ElementType Type
			{
				get
				{
					return this._type;
				}
			}
			public HtmlFormatter.FormattingFlags Flags
			{
				get
				{
					return this._flags;
				}
			}
			public HtmlFormatter.WhiteSpaceType FollowingWhiteSpaceType
			{
				get
				{
					return this._following;
				}
			}
			public HtmlFormatter.WhiteSpaceType InnerWhiteSpaceType
			{
				get
				{
					return this._inner;
				}
			}
			public bool IsComment
			{
				get
				{
					return (this._flags & HtmlFormatter.FormattingFlags.Comment) != HtmlFormatter.FormattingFlags.None;
				}
			}
			public bool IsInline
			{
				get
				{
					return (this._flags & HtmlFormatter.FormattingFlags.Inline) != HtmlFormatter.FormattingFlags.None;
				}
			}
			public bool IsXml
			{
				get
				{
					return (this._flags & HtmlFormatter.FormattingFlags.Xml) != HtmlFormatter.FormattingFlags.None;
				}
			}
			public bool NoEndTag
			{
				get
				{
					return (this._flags & HtmlFormatter.FormattingFlags.NoEndTag) != HtmlFormatter.FormattingFlags.None;
				}
			}
			public bool NoIndent
			{
				get
				{
					return (this._flags & HtmlFormatter.FormattingFlags.NoIndent) != HtmlFormatter.FormattingFlags.None || this.NoEndTag;
				}
			}
			public bool PreserveContent
			{
				get
				{
					return (this._flags & HtmlFormatter.FormattingFlags.PreserveContent) != HtmlFormatter.FormattingFlags.None;
				}
			}
			public string TagName
			{
				get
				{
					return this._tagName;
				}
			}
			public virtual bool CanContainTag(HtmlFormatter.TagInfo info)
			{
				return true;
			}
			private string _tagName;
			private HtmlFormatter.WhiteSpaceType _inner;
			private HtmlFormatter.WhiteSpaceType _following;
			private HtmlFormatter.FormattingFlags _flags;
			private HtmlFormatter.ElementType _type;
		}
		internal class LITagInfo : HtmlFormatter.TagInfo
		{
			public LITagInfo()
				: base("li", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.CarryThrough)
			{
			}
			public override bool CanContainTag(HtmlFormatter.TagInfo info)
			{
				return info.Type == HtmlFormatter.ElementType.Any || ((info.Type == HtmlFormatter.ElementType.Inline) | (info.Type == HtmlFormatter.ElementType.Block));
			}
		}
		internal class OLTagInfo : HtmlFormatter.TagInfo
		{
			public OLTagInfo()
				: base("ol", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block)
			{
			}
			public override bool CanContainTag(HtmlFormatter.TagInfo info)
			{
				return info.Type == HtmlFormatter.ElementType.Any || info.TagName.ToLower().Equals("li");
			}
		}
		internal class PTagInfo : HtmlFormatter.TagInfo
		{
			public PTagInfo()
				: base("p", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Block)
			{
			}
			public override bool CanContainTag(HtmlFormatter.TagInfo info)
			{
				return info.Type == HtmlFormatter.ElementType.Any || ((info.Type == HtmlFormatter.ElementType.Inline) | info.TagName.ToLower().Equals("table") | info.TagName.ToLower().Equals("hr"));
			}
		}
		internal class TDTagInfo : HtmlFormatter.TagInfo
		{
			public TDTagInfo()
				: base("td", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Other)
			{
			}
			public override bool CanContainTag(HtmlFormatter.TagInfo info)
			{
				return info.Type == HtmlFormatter.ElementType.Any || ((info.Type == HtmlFormatter.ElementType.Inline) | (info.Type == HtmlFormatter.ElementType.Block));
			}
		}
		internal class TRTagInfo : HtmlFormatter.TagInfo
		{
			public TRTagInfo()
				: base("tr", HtmlFormatter.FormattingFlags.None, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.WhiteSpaceType.NotSignificant, HtmlFormatter.ElementType.Other)
			{
			}
			public override bool CanContainTag(HtmlFormatter.TagInfo info)
			{
				return info.Type == HtmlFormatter.ElementType.Any || (info.TagName.ToLower().Equals("th") | info.TagName.ToLower().Equals("td"));
			}
		}
		internal enum WhiteSpaceType
		{
			Significant,
			NotSignificant,
			CarryThrough
		}
		internal class XmlWriter : HtmlFormatter.HtmlWriter
		{
			public XmlWriter(int initialIndent, string tagName, string indentString, int maxLineLength)
				: base(new StringWriter(), indentString, maxLineLength)
			{
				base.Writer.Indent = initialIndent;
				this._unformatted = new StringBuilder();
				this._tagName = tagName;
				this._isUnknownXml = this._tagName.IndexOf(':') > -1;
			}
			public bool ContainsText
			{
				get
				{
					return this._containsText;
				}
				set
				{
					this._containsText = value;
				}
			}
			public override string Content
			{
				get
				{
					string text;
					if (this.ContainsText)
					{
						text = this._unformatted.ToString();
					}
					else
					{
						text = base.Content;
					}
					return text;
				}
			}
			public string TagName
			{
				get
				{
					return this._tagName;
				}
			}
			public bool IsUnknownXml
			{
				get
				{
					return this._isUnknownXml;
				}
			}
			public override void Write(char c)
			{
				base.Write(c);
				this._unformatted.Append(c);
			}
			public override void Write(string s)
			{
				base.Write(s);
				this._unformatted.Append(s);
			}
			public override void WriteLiteral(string s, bool frontWhiteSpace)
			{
				base.WriteLiteral(s, frontWhiteSpace);
				this._unformatted.Append(s);
			}
			private bool _containsText;
			private StringBuilder _unformatted;
			private string _tagName;
			private bool _isUnknownXml;
		}
		private enum HtmlFormatterCase
		{
			PreserveCase,
			UpperCase,
			LowerCase
		}
	}
}
