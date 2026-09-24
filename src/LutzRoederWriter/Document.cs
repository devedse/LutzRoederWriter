using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Writer.Forms;
using Writer.Html;

namespace Writer
{
	/// <summary>
	/// Represents an editable HTML document hosted in an <see cref="Html.HtmlControl"/>
	/// and participates in Writer command routing.
	/// </summary>
	public class Document : ICommandTarget, IDisposable
	{
		public Document(CommandManager commandManager, string url)
		{
			this.url = url;
			this.permanentUrl = url;
			this.commandManager = commandManager;
			this.timer = new Timer();
			this.timer.Interval = 500;
			this.timer.Tick += this.Timer_Tick;
			this.timer.Start();
			this.htmlControl = new HtmlControl();
			this.htmlControl.Dock = DockStyle.Fill;
			this.htmlControl.TabIndex = 0;
			this.htmlControl.IsDesignMode = true;
			if (this.url == null)
			{
				string tempFileName = Path.GetTempFileName();
				if (File.Exists(tempFileName))
				{
					File.Delete(tempFileName);
				}
				Directory.CreateDirectory(tempFileName);
				string text = Path.ChangeExtension("Document" + Document.documentIndex, ".htm");
				Document.documentIndex++;
				this.url = Path.Combine(tempFileName, text);
				StreamWriter streamWriter = new StreamWriter(this.url);
				streamWriter.Write("<html><body></body></html>");
				streamWriter.Close();
			}
			StreamReader streamReader = File.OpenText(this.url);
			this.htmlControl.LoadHtml(streamReader.ReadToEnd(), this.url);
			streamReader.Close();
			CommandBarContextMenu commandBarContextMenu = new CommandBarContextMenu();
			this.commandManager.Add("Edit.Undo", commandBarContextMenu.Items.AddButton(CommandBarImageResource.Undo, "&Undo", null, (Keys)131162));
			this.commandManager.Add("Edit.Redo", commandBarContextMenu.Items.AddButton(CommandBarImageResource.Redo, "&Redo", null, (Keys)131161));
			commandBarContextMenu.Items.AddSeparator();
			this.commandManager.Add("Edit.Cut", commandBarContextMenu.Items.AddButton(CommandBarImageResource.Cut, "Cu&t", null, (Keys)131160));
			this.commandManager.Add("Edit.Copy", commandBarContextMenu.Items.AddButton(CommandBarImageResource.Copy, "&Copy", null, (Keys)131139));
			this.commandManager.Add("Edit.Paste", commandBarContextMenu.Items.AddButton(CommandBarImageResource.Paste, "&Paste", null, (Keys)131158));
			this.commandManager.Add("Edit.Delete", commandBarContextMenu.Items.AddButton(CommandBarImageResource.Delete, "&Delete", null, Keys.Delete));
			this.htmlControl.ContextMenu = commandBarContextMenu;
		}
		public void Dispose()
		{
			this.commandManager.RemoveTarget(this);
			this.timer.Stop();
			this.timer.Tick -= this.Timer_Tick;
			this.htmlControl.Dispose();
			this.htmlControl = null;
			if (this.permanentUrl == null)
			{
				string directoryName = Path.GetDirectoryName(this.url);
				if (Directory.Exists(directoryName))
				{
					Directory.Delete(directoryName, true);
				}
			}
		}
		public HtmlControl HtmlControl
		{
			get
			{
				return this.htmlControl;
			}
		}
		public bool IsDirty
		{
			get
			{
				return this.htmlControl.IsDirty;
			}
		}
		public string Url
		{
			get
			{
				return this.url;
			}
		}
		public void Save()
		{
			if (this.permanentUrl != null)
			{
				this.SaveAs(this.permanentUrl);
			}
			else
			{
				this.SaveAs();
			}
		}
		public void SaveAs()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = Resource.GetString("HtmlFilter");
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = saveFileDialog.FileName;
				this.SaveAs(fileName);
			}
		}
		private void SaveAs(string url)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			using (StreamWriter streamWriter = new StreamWriter(url))
			{
				HtmlFormatter htmlFormatter = new HtmlFormatter();
				htmlFormatter.Format(this.htmlControl.SaveHtml(), streamWriter);
			}
			FileStream fileStream = new FileStream(url, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, (int)fileStream.Length);
			fileStream.Close();
			try
			{
				int num = 3;
				fileStream = new FileStream(url, FileMode.OpenOrCreate, FileAccess.ReadWrite);
				fileStream.SetLength((long)(array.Length + num));
				byte[] array2 = new byte[]
				{
					Convert.ToByte("EF", 16),
					Convert.ToByte("BB", 16),
					Convert.ToByte("BF", 16)
				};
				fileStream.Write(array2, 0, num);
				fileStream.Write(array, 0, array.Length);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			this.permanentUrl = url;
			this.url = url;
			Cursor.Current = cursor;
		}
		public bool Execute(CommandState commandState)
		{
			if (this.htmlControl.IsHandleCreated && this.htmlControl.IsReady)
			{
				string commandName = commandState.CommandName;
				switch (commandName)
				{
				case "File.Print":
					this.htmlControl.Print();
					return true;
				case "File.PrintPreview":
					this.htmlControl.PrintPreview();
					return true;
				case "Edit.Undo":
					this.htmlControl.Undo();
					return true;
				case "Edit.Redo":
					this.htmlControl.Redo();
					return true;
				case "Edit.Cut":
					this.htmlControl.Cut();
					return true;
				case "Edit.Copy":
					this.htmlControl.Copy();
					return true;
				case "Edit.Paste":
					this.htmlControl.Paste();
					return true;
				case "Edit.Delete":
					this.htmlControl.Delete();
					return true;
				case "Edit.SelectAll":
					this.htmlControl.SelectAll();
					return true;
				case "Format.ForeColor.White":
					this.htmlControl.TextFormatting.ForeColor = Color.White;
					return true;
				case "Format.ForeColor.Red":
					this.htmlControl.TextFormatting.ForeColor = Color.Red;
					return true;
				case "Format.ForeColor.Green":
					this.htmlControl.TextFormatting.ForeColor = Color.FromArgb(0, 255, 0);
					return true;
				case "Format.ForeColor.Blue":
					this.htmlControl.TextFormatting.ForeColor = Color.Blue;
					return true;
				case "Format.ForeColor.Yellow":
					this.htmlControl.TextFormatting.ForeColor = Color.Yellow;
					return true;
				case "Format.ForeColor.Black":
					this.htmlControl.TextFormatting.ForeColor = Color.Black;
					return true;
				case "Format.ForeColor":
				{
					ColorDialog colorDialog = new ColorDialog();
					colorDialog.Color = this.htmlControl.TextFormatting.ForeColor;
					if (colorDialog.ShowDialog() == DialogResult.OK)
					{
						this.htmlControl.TextFormatting.ForeColor = colorDialog.Color;
					}
					return true;
				}
				case "Format.BackColor.Black":
					this.htmlControl.TextFormatting.BackColor = Color.Black;
					return true;
				case "Format.BackColor.Red":
					this.htmlControl.TextFormatting.BackColor = Color.Red;
					return true;
				case "Format.BackColor.Green":
					this.htmlControl.TextFormatting.BackColor = Color.FromArgb(0, 255, 0);
					return true;
				case "Format.BackColor.Blue":
					this.htmlControl.TextFormatting.BackColor = Color.Blue;
					return true;
				case "Format.BackColor.Yellow":
					this.htmlControl.TextFormatting.BackColor = Color.Yellow;
					return true;
				case "Format.BackColor.White":
					this.htmlControl.TextFormatting.BackColor = Color.White;
					return true;
				case "Format.BackColor":
				{
					ColorDialog colorDialog = new ColorDialog();
					colorDialog.Color = this.htmlControl.TextFormatting.BackColor;
					if (colorDialog.ShowDialog() == DialogResult.OK)
					{
						this.htmlControl.TextFormatting.BackColor = colorDialog.Color;
					}
					return true;
				}
				case "Format.Font":
					this.htmlControl.TextFormatting.FontName = (commandState as CommandComboBoxState).Value;
					return true;
				case "Format.FontSize":
					this.htmlControl.TextFormatting.FontSize = (HtmlFontSize)int.Parse((commandState as CommandComboBoxState).Value);
					return true;
				case "Format.Bold":
					this.htmlControl.TextFormatting.ToggleBold();
					return true;
				case "Format.Italic":
					this.htmlControl.TextFormatting.ToggleItalics();
					return true;
				case "Format.Underline":
					this.htmlControl.TextFormatting.ToggleUnderline();
					return true;
				case "Format.Strikethrough":
					this.htmlControl.TextFormatting.ToggleStrikethrough();
					return true;
				case "Format.Superscript":
					this.htmlControl.TextFormatting.ToggleSuperscript();
					return true;
				case "Format.Subscript":
					this.htmlControl.TextFormatting.ToggleSubscript();
					return true;
				case "Format.AlignLeft":
					this.htmlControl.TextFormatting.Alignment = HtmlAlignment.Left;
					return true;
				case "Format.AlignCenter":
					this.htmlControl.TextFormatting.Alignment = HtmlAlignment.Center;
					return true;
				case "Format.AlignRight":
					this.htmlControl.TextFormatting.Alignment = HtmlAlignment.Right;
					return true;
				case "Format.OrderedList":
					this.htmlControl.TextFormatting.HtmlFormat = HtmlFormat.OrderedList;
					return true;
				case "Format.UnorderedList":
					this.htmlControl.TextFormatting.HtmlFormat = HtmlFormat.UnorderedList;
					return true;
				case "Format.Indent":
					this.htmlControl.TextFormatting.Indent();
					return true;
				case "Format.Unindent":
					this.htmlControl.TextFormatting.Unindent();
					return true;
				case "Edit.InsertHyperlink":
					this.InsertHyperlink();
					return true;
				case "Edit.InsertPicture":
					this.InsertPicture();
					return true;
				case "Edit.InsertDateTime":
					this.InsertDateTime();
					return true;
				case "Edit.Find":
					this.Find();
					return true;
				case "Edit.FindNext":
					this.FindNext();
					return true;
				case "Edit.Replace":
					this.Replace();
					return true;
				}
			}
			return false;
		}
		public bool QueryStatus(CommandState commandState)
		{
			if (this.htmlControl.IsHandleCreated && this.htmlControl.IsReady)
			{
				CommandCheckBoxState commandCheckBoxState = commandState as CommandCheckBoxState;
				string commandName = commandState.CommandName;
				switch (commandName)
				{
				case "File.Print":
					commandState.IsEnabled = this.htmlControl.CanPrint;
					return true;
				case "File.PrintPreview":
					commandState.IsEnabled = this.htmlControl.CanPrintPreview;
					return true;
				case "Edit.Undo":
					commandState.IsEnabled = this.htmlControl.CanUndo;
					commandState.Text = "&Undo " + this.htmlControl.UndoDescription;
					return true;
				case "Edit.Redo":
					commandState.IsEnabled = this.htmlControl.CanRedo;
					commandState.Text = "&Redo " + this.htmlControl.RedoDescription;
					return true;
				case "Edit.Copy":
					commandState.IsEnabled = this.htmlControl.CanCopy;
					return true;
				case "Edit.Cut":
					commandState.IsEnabled = this.htmlControl.CanCut;
					return true;
				case "Edit.Paste":
					commandState.IsEnabled = this.htmlControl.CanPaste;
					return true;
				case "Edit.Delete":
					commandState.IsEnabled = this.htmlControl.CanDelete;
					return true;
				case "Edit.SelectAll":
					commandState.IsEnabled = this.htmlControl.CanSelectAll;
					return true;
				case "Edit.Find":
				case "Edit.FindNext":
				case "Edit.Replace":
					commandState.IsEnabled = true;
					return true;
				case "Edit.InsertHyperlink":
				case "Edit.InsertPicture":
				case "Edit.InsertDateTime":
					commandState.IsEnabled = this.htmlControl.CanInsertHtml;
					return true;
				case "Format.Font":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanSetFontName;
					(commandState as CommandComboBoxState).Value = this.htmlControl.TextFormatting.FontName;
					return true;
				case "Format.FontSize":
				{
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanSetFontSize;
					CommandComboBoxState commandComboBoxState = commandState as CommandComboBoxState;
					int fontSize = (int)this.htmlControl.TextFormatting.FontSize;
					commandComboBoxState.Value = fontSize.ToString();
					return true;
				}
				case "Format.Bold":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanToggleBold;
					commandCheckBoxState.IsChecked = this.htmlControl.TextFormatting.IsBold;
					return true;
				case "Format.Italic":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanToggleItalic;
					commandCheckBoxState.IsChecked = this.htmlControl.TextFormatting.IsItalic;
					return true;
				case "Format.Underline":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanToggleUnderline;
					commandCheckBoxState.IsChecked = this.htmlControl.TextFormatting.IsUnderline;
					return true;
				case "Format.Strikethrough":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanToggleStrikethrough;
					commandCheckBoxState.IsChecked = this.htmlControl.TextFormatting.IsStrikethrough;
					return true;
				case "Format.Subscript":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanToggleSubscript;
					commandCheckBoxState.IsChecked = this.htmlControl.TextFormatting.IsSubscript;
					return true;
				case "Format.Superscript":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanToggleSuperscript;
					commandCheckBoxState.IsChecked = this.htmlControl.TextFormatting.IsSuperscript;
					return true;
				case "Format.AlignLeft":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanAlign(HtmlAlignment.Left);
					commandCheckBoxState.IsChecked = this.htmlControl.TextFormatting.Alignment == HtmlAlignment.Left;
					return true;
				case "Format.AlignRight":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanAlign(HtmlAlignment.Right);
					commandCheckBoxState.IsChecked = this.htmlControl.TextFormatting.Alignment == HtmlAlignment.Right;
					return true;
				case "Format.AlignCenter":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanAlign(HtmlAlignment.Center);
					commandCheckBoxState.IsChecked = this.htmlControl.TextFormatting.Alignment == HtmlAlignment.Center;
					return true;
				case "Format.OrderedList":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanSetHtmlFormat;
					commandCheckBoxState.IsChecked = this.htmlControl.TextFormatting.HtmlFormat == HtmlFormat.OrderedList;
					return true;
				case "Format.UnorderedList":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanSetHtmlFormat;
					commandCheckBoxState.IsChecked = this.htmlControl.TextFormatting.HtmlFormat == HtmlFormat.UnorderedList;
					return true;
				case "Format.Indent":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanIndent;
					return true;
				case "Format.Unindent":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanUnindent;
					return true;
				case "Format.ForeColor":
				case "Format.ForeColor.Black":
				case "Format.ForeColor.Yellow":
				case "Format.ForeColor.Red":
				case "Format.ForeColor.Green":
				case "Format.ForeColor.Blue":
				case "Format.ForeColor.White":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanSetForeColor;
					return true;
				case "Format.BackColor":
				case "Format.BackColor.Black":
				case "Format.BackColor.Yellow":
				case "Format.BackColor.Red":
				case "Format.BackColor.Green":
				case "Format.BackColor.Blue":
				case "Format.BackColor.White":
					commandState.IsEnabled = this.htmlControl.TextFormatting.CanSetBackColor;
					return true;
				}
			}
			return false;
		}
		private void Timer_Tick(object sender, EventArgs e)
		{
			this.commandManager.QueryStatus();
		}
		private void InsertHyperlink()
		{
			this.htmlControl.InsertHyperlink(null, null);
		}
		private void InsertPicture()
		{
			this.htmlControl.InsertImage();
		}
		private void InsertDateTime()
		{
			InsertDateTimeDialog insertDateTimeDialog = new InsertDateTimeDialog();
			if (insertDateTimeDialog.Run())
			{
				this.htmlControl.InsertHtml(insertDateTimeDialog.Format);
			}
		}
		private void Find()
		{
			FindDialog findDialog = new FindDialog();
			if (this.findSearchText != string.Empty)
			{
				findDialog.SearchText = this.findSearchText;
				findDialog.IsCaseChecked = this.findSearchCase;
				findDialog.IsWholeChecked = this.findSearchWhole;
				findDialog.IsUp = this.findSearchDirection;
			}
			if (findDialog.Run())
			{
				this.findSearchText = findDialog.SearchText;
				this.findSearchCase = findDialog.IsCaseChecked;
				this.findSearchWhole = findDialog.IsWholeChecked;
				this.findSearchDirection = findDialog.IsUp;
				if (!this.htmlControl.Find(this.findSearchText, this.findSearchCase, this.findSearchWhole, this.findSearchDirection))
				{
					MessageBox.Show("Finished searching the document.", Resource.GetString("ApplicationName"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}
		private void Replace()
		{
			ReplaceDialog replaceDialog = new ReplaceDialog(this.htmlControl);
			replaceDialog.ShowDialog();
		}
		private void FindNext()
		{
			if (this.findSearchText != string.Empty)
			{
				this.htmlControl.Find(this.findSearchText, this.findSearchCase, this.findSearchWhole, this.findSearchDirection);
			}
		}
		private static int documentIndex = 1;
		private CommandManager commandManager;
		private string url;
		private string permanentUrl;
		private HtmlControl htmlControl;
		private Timer timer;
		private string findSearchText = string.Empty;
		private bool findSearchCase = false;
		private bool findSearchWhole = false;
		private bool findSearchDirection = false;
	}
}
