using System;
using System.Drawing;
using System.Windows.Forms;
using Writer.Forms;

namespace Writer
{
	/// <summary>
	/// Provides the complete legacy Writer editing surface, including menu bar, toolbar,
	/// status bar, document view, and command routing.
	/// </summary>
	/// <remarks>
	/// Applications that only need an embeddable editor can use
	/// <see cref="Html.HtmlControl"/> directly.
	/// </remarks>
	public partial class htmlwriter : UserControl, ICommandTarget
	{
		public htmlwriter()
		{
			this.commandManager = new CommandManager();
			this.configurationManager = new ConfigurationManager();
			this.configurationManager.Load(base.GetType().Module.FullyQualifiedName);
			this.fileHistory = new FileHistory();
			this.fileHistory.Configuration = this.configurationManager["FileHistory"];
			this.ftpConfiguration = new FtpConfiguration();
			this.ftpConfiguration.Configuration = this.configurationManager["FtpConfiguration"];
			this.commandManager.AddTarget(this);
			this.FileNew();
			this.Font = new Font("Tahoma", 8.25f);
			base.Size = new Size(800, 800);
			this.view.Dock = DockStyle.Fill;
			this.view.TabStop = false;
			base.Controls.Add(this.view);
			this.menuBar = new CommandBar(this.commandBarManager, CommandBarStyle.Menu);
			this.toolBar = new CommandBar(this.commandBarManager, CommandBarStyle.ToolBar);
			this.commandBarManager.CommandBars.Add(this.menuBar);
			this.commandBarManager.CommandBars.Add(this.toolBar);
			base.Controls.Add(this.commandBarManager);
			base.Controls.Add(this.statusBar);
			CommandBar commandBar = this.ToolBar;
			this.commandManager.Add("File.New", commandBar.Items.AddButton(CommandBarImageResource.New, "&New", null, (Keys)131150));
			this.commandManager.Add("File.Open", commandBar.Items.AddButton(CommandBarImageResource.Open, "&Open...", null, (Keys)131151));
			this.commandManager.Add("File.Save", commandBar.Items.AddButton(CommandBarImageResource.Save, "&Save", null, (Keys)131155));
			commandBar.Items.AddSeparator();
			this.commandManager.Add("Edit.Cut", commandBar.Items.AddButton(CommandBarImageResource.Cut, "Cu&t", null, (Keys)131160));
			this.commandManager.Add("Edit.Copy", commandBar.Items.AddButton(CommandBarImageResource.Copy, "&Copy", null, (Keys)131139));
			this.commandManager.Add("Edit.Paste", commandBar.Items.AddButton(CommandBarImageResource.Paste, "&Paste", null, (Keys)131158));
			this.commandManager.Add("Edit.Delete", commandBar.Items.AddButton(CommandBarImageResource.Delete, "&Delete", null, Keys.Delete));
			commandBar.Items.AddSeparator();
			this.commandManager.Add("Edit.Undo", commandBar.Items.AddButton(CommandBarImageResource.Undo, "&Undo", null, (Keys)131162));
			this.commandManager.Add("Edit.Redo", commandBar.Items.AddButton(CommandBarImageResource.Redo, "&Redo", null, (Keys)131161));
			commandBar.Items.AddSeparator();
			this.commandManager.Add("Format.Font", commandBar.Items.AddComboBox("Font", new FontComboBox()));
			this.commandManager.Add("Format.FontSize", commandBar.Items.AddComboBox("Font Size", new FontSizeComboBox()));
			commandBar.Items.AddSeparator();
			this.commandManager.Add("Format.Bold", commandBar.Items.AddCheckBox(FormatResource.Bold, "&Bold", (Keys)131138));
			this.commandManager.Add("Format.Italic", commandBar.Items.AddCheckBox(FormatResource.Italic, "&Italic", (Keys)131145));
			this.commandManager.Add("Format.Underline", commandBar.Items.AddCheckBox(FormatResource.Underline, "U&nderline", (Keys)131157));
			commandBar.Items.AddSeparator();
			this.commandManager.Add("Format.UnorderedList", commandBar.Items.AddCheckBox(FormatResource.UnorderedList, "&Bullets"));
			this.commandManager.Add("Format.OrderedList", commandBar.Items.AddCheckBox(FormatResource.OrderedList, "&Numbering"));
			this.commandManager.Add("Format.Unindent", commandBar.Items.AddButton(FormatResource.Unindent, "Unind&ent", null, Keys.LButton | Keys.Back | Keys.Shift));
			this.commandManager.Add("Format.Indent", commandBar.Items.AddButton(FormatResource.Indent, "In&dent", null, Keys.Tab));
			commandBar.Items.AddSeparator();
			CommandBarMenu commandBarMenu = commandBar.Items.AddMenu(FormatResource.ForeColor, "Foreground Color");
			this.commandManager.Add("Format.ForeColor.Black", commandBarMenu.Items.AddButton(FormatResource.Black, "Blac&k", null));
			this.commandManager.Add("Format.ForeColor.Yellow", commandBarMenu.Items.AddButton(FormatResource.Yellow, "&Yellow", null));
			this.commandManager.Add("Format.ForeColor.Red", commandBarMenu.Items.AddButton(FormatResource.Red, "&Red", null));
			this.commandManager.Add("Format.ForeColor.Green", commandBarMenu.Items.AddButton(FormatResource.Green, "&Green", null));
			this.commandManager.Add("Format.ForeColor.Blue", commandBarMenu.Items.AddButton(FormatResource.Blue, "&Blue", null));
			this.commandManager.Add("Format.ForeColor.White", commandBarMenu.Items.AddButton(FormatResource.White, "&White", null));
			CommandBarMenu commandBarMenu2 = commandBar.Items.AddMenu(FormatResource.BackColor, "Background Color");
			this.commandManager.Add("Format.BackColor.Black", commandBarMenu2.Items.AddButton(FormatResource.Black, "Blac&k", null));
			this.commandManager.Add("Format.BackColor.Yellow", commandBarMenu2.Items.AddButton(FormatResource.Yellow, "&Yellow", null));
			this.commandManager.Add("Format.BackColor.Red", commandBarMenu2.Items.AddButton(FormatResource.Red, "&Red", null));
			this.commandManager.Add("Format.BackColor.Green", commandBarMenu2.Items.AddButton(FormatResource.Green, "&Green", null));
			this.commandManager.Add("Format.BackColor.Blue", commandBarMenu2.Items.AddButton(FormatResource.Blue, "&Blue", null));
			this.commandManager.Add("Format.BackColor.White", commandBarMenu2.Items.AddButton(FormatResource.White, "&White", null));
			CommandBar commandBar2 = this.MenuBar;
			CommandBarMenu commandBarMenu3 = commandBar2.Items.AddMenu("&File");
			this.commandManager.Add("File.New", commandBarMenu3.Items.AddButton(CommandBarImageResource.New, "&New", null, (Keys)131150));
			this.commandManager.Add("File.Open", commandBarMenu3.Items.AddButton(CommandBarImageResource.Open, "&Open...", null, (Keys)131151));
			this.commandManager.Add("File.Save", commandBarMenu3.Items.AddButton(CommandBarImageResource.Save, "&Save", null, (Keys)131155));
			this.commandManager.Add("File.SaveAs", commandBarMenu3.Items.AddButton("Save &As...", null));
			commandBarMenu3.Items.AddSeparator();
			this.commandManager.Add("File.PrintPreview", commandBarMenu3.Items.AddButton(CommandBarImageResource.Preview, "Print Pre&view", null));
			this.commandManager.Add("File.Print", commandBarMenu3.Items.AddButton(CommandBarImageResource.Print, "&Print", null, (Keys)131152));
			commandBarMenu3.Items.AddSeparator();
			CommandBarMenu commandBarMenu4 = commandBarMenu3.Items.AddMenu("Recent &Files");
			this.commandManager.Add("File.History.0", commandBarMenu4.Items.AddButton("&1", null));
			this.commandManager.Add("File.History.1", commandBarMenu4.Items.AddButton("&2", null));
			this.commandManager.Add("File.History.2", commandBarMenu4.Items.AddButton("&3", null));
			this.commandManager.Add("File.History.3", commandBarMenu4.Items.AddButton("&4", null));
			CommandBarMenu commandBarMenu5 = commandBar2.Items.AddMenu("&Edit");
			this.commandManager.Add("Edit.Undo", commandBarMenu5.Items.AddButton(CommandBarImageResource.Undo, "&Undo", null, (Keys)131162));
			this.commandManager.Add("Edit.Redo", commandBarMenu5.Items.AddButton(CommandBarImageResource.Redo, "&Redo", null, (Keys)131161));
			commandBarMenu5.Items.AddSeparator();
			this.commandManager.Add("Edit.Cut", commandBarMenu5.Items.AddButton(CommandBarImageResource.Cut, "Cu&t", null, (Keys)131160));
			this.commandManager.Add("Edit.Copy", commandBarMenu5.Items.AddButton(CommandBarImageResource.Copy, "&Copy", null, (Keys)131139));
			this.commandManager.Add("Edit.Paste", commandBarMenu5.Items.AddButton(CommandBarImageResource.Paste, "&Paste", null, (Keys)131158));
			this.commandManager.Add("Edit.Delete", commandBarMenu5.Items.AddButton(CommandBarImageResource.Delete, "&Delete", null, Keys.Delete));
			commandBarMenu5.Items.AddSeparator();
			this.commandManager.Add("Edit.SelectAll", commandBarMenu5.Items.AddButton("Select &All", null, (Keys)131137));
			commandBarMenu5.Items.AddSeparator();
			this.commandManager.Add("Edit.Find", commandBarMenu5.Items.AddButton(CommandBarImageResource.Search, "&Find...", null, (Keys)131142));
			this.commandManager.Add("Edit.FindNext", commandBarMenu5.Items.AddButton("Find &Next", null, Keys.F3));
			this.commandManager.Add("Edit.Replace", commandBarMenu5.Items.AddButton("&Replace...", null, (Keys)131144));
			CommandBarMenu commandBarMenu6 = commandBar2.Items.AddMenu("&Insert");
			this.commandManager.Add("Edit.InsertHyperlink", commandBarMenu6.Items.AddButton(FormatResource.Hyperlink, "Insert &Hyperlink", null, (Keys)131147));
			this.commandManager.Add("Edit.InsertPicture", commandBarMenu6.Items.AddButton(FormatResource.Picture, "Insert &Picture...", null));
			this.commandManager.Add("Edit.InsertDateTime", commandBarMenu6.Items.AddButton("Insert &Date and Time...", null));
			CommandBarMenu commandBarMenu7 = commandBar2.Items.AddMenu("F&ormat");
			this.commandManager.Add("Format.Bold", commandBarMenu7.Items.AddCheckBox(FormatResource.Bold, "&Bold", (Keys)131138));
			this.commandManager.Add("Format.Italic", commandBarMenu7.Items.AddCheckBox(FormatResource.Italic, "&Italic", (Keys)131145));
			this.commandManager.Add("Format.Underline", commandBarMenu7.Items.AddCheckBox(FormatResource.Underline, "U&nderline", (Keys)131157));
			this.commandManager.Add("Format.Superscript", commandBarMenu7.Items.AddCheckBox(FormatResource.Superscript, "&Superscript"));
			this.commandManager.Add("Format.Subscript", commandBarMenu7.Items.AddCheckBox(FormatResource.Subscript, "Subscri&pt"));
			this.commandManager.Add("Format.Strikethrough", commandBarMenu7.Items.AddCheckBox(FormatResource.Strikethrough, "Stri&ke"));
			commandBarMenu7.Items.AddSeparator();
			this.commandManager.Add("Format.ForeColor", commandBarMenu7.Items.AddButton(FormatResource.ForeColor, "&Fore Color", null));
			this.commandManager.Add("Format.BackColor", commandBarMenu7.Items.AddButton(FormatResource.BackColor, "B&ack Color", null));
			commandBarMenu7.Items.AddSeparator();
			this.commandManager.Add("Format.AlignLeft", commandBarMenu7.Items.AddCheckBox(FormatResource.AlignLeft, "Align &Left"));
			this.commandManager.Add("Format.AlignCenter", commandBarMenu7.Items.AddCheckBox(FormatResource.AlignCenter, "Align &Center"));
			this.commandManager.Add("Format.AlignRight", commandBarMenu7.Items.AddCheckBox(FormatResource.AlignRight, "Align &Right"));
			commandBarMenu7.Items.AddSeparator();
			this.commandManager.Add("Format.OrderedList", commandBarMenu7.Items.AddCheckBox(FormatResource.OrderedList, "&Numbering"));
			this.commandManager.Add("Format.UnorderedList", commandBarMenu7.Items.AddCheckBox(FormatResource.UnorderedList, "B&ullets"));
			this.commandManager.Add("Format.Unindent", commandBarMenu7.Items.AddButton(FormatResource.Unindent, "Unind&ent", null, Keys.LButton | Keys.Back | Keys.Shift));
			this.commandManager.Add("Format.Indent", commandBarMenu7.Items.AddButton(FormatResource.Indent, "In&dent", null, Keys.Tab));
			this.commandManager.AddTarget(this);
			this.FileNew();
			this.Font = new Font("Tahoma", 8.25f);
			base.Size = new Size(800, 800);
			this.view.Dock = DockStyle.Fill;
			this.view.TabStop = false;
			base.Controls.Add(this.view);
			this.menuBar = new CommandBar(this.commandBarManager, CommandBarStyle.Menu);
			this.toolBar = new CommandBar(this.commandBarManager, CommandBarStyle.ToolBar);
			this.commandBarManager.CommandBars.Add(this.menuBar);
			this.commandBarManager.CommandBars.Add(this.toolBar);
			base.Controls.Add(this.commandBarManager);
			base.Controls.Add(this.statusBar);
		}
		public CommandBar MenuBar
		{
			get
			{
				return this.menuBar;
			}
		}
		public CommandBar ToolBar
		{
			get
			{
				return this.toolBar;
			}
		}
		public StatusBar StatusBar
		{
			get
			{
				return this.statusBar;
			}
		}
		public Control View
		{
			get
			{
				return this.view;
			}
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return this.commandBarManager.PreProcessMessage(ref msg) || base.ProcessCmdKey(ref msg, keyData);
		}
		public bool Execute(CommandState commandState)
		{
			string commandName = commandState.CommandName;
			switch (commandName)
			{
			case "File.New":
				this.FileNew();
				return true;
			case "File.Open":
				this.FileOpen();
				return true;
			case "File.Save":
				this.document.Save();
				return true;
			case "File.SaveAs":
				this.document.SaveAs();
				this.fileHistory.AddFile(this.document.Url);
				return true;
			case "File.FtpSetting":
				this.PublishSetting();
				return true;
			case "File.Publish":
				this.Publish();
				return true;
			case "Application.About":
				this.About();
				return true;
			case "File.History.0":
				this.LoadFile(this.fileHistory[0]);
				return true;
			case "File.History.1":
				this.LoadFile(this.fileHistory[1]);
				return true;
			case "File.History.2":
				this.LoadFile(this.fileHistory[2]);
				return true;
			case "File.History.3":
				this.LoadFile(this.fileHistory[3]);
				return true;
			}
			return false;
		}
		public bool QueryStatus(CommandState commandState)
		{
			string commandName = commandState.CommandName;
			switch (commandName)
			{
			case "File.New":
			case "File.Open":
			case "Application.Exit":
			case "Application.Feedback":
			case "Application.CheckForUpdates":
			case "Application.About":
				commandState.IsEnabled = true;
				return true;
			case "File.Save":
				commandState.IsEnabled = this.document != null && this.document.IsDirty;
				return true;
			case "File.SaveAs":
				commandState.IsEnabled = this.document != null;
				return true;
			case "File.FtpSetting":
				commandState.IsEnabled = true;
				return true;
			case "File.Publish":
				commandState.IsEnabled = !this.ftpConfiguration.IsEmpty;
				return true;
			case "File.History.0":
				commandState.IsVisible = this.fileHistory.Count > 0;
				commandState.Text = "&1 " + this.fileHistory[0];
				return true;
			case "File.History.1":
				commandState.IsVisible = this.fileHistory.Count > 1;
				commandState.Text = "&2 " + this.fileHistory[1];
				return true;
			case "File.History.2":
				commandState.IsVisible = this.fileHistory.Count > 2;
				commandState.Text = "&3 " + this.fileHistory[2];
				return true;
			case "File.History.3":
				commandState.IsVisible = this.fileHistory.Count > 3;
				commandState.Text = "&4 " + this.fileHistory[3];
				return true;
			}
			return false;
		}
		private void FileNew()
		{
			if (this.FileClose())
			{
				this.LoadFile(null);
			}
		}
		private void FileOpen()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = false;
			openFileDialog.Filter = Resource.GetString("HtmlFilter");
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.FileClose();
				string fileName = openFileDialog.FileName;
				this.LoadFile(fileName);
			}
		}
		private bool FileClose()
		{
			if (this.document != null)
			{
				if (this.document.IsDirty)
				{
					string text = "Do you want to save the changes to '" + this.document.Url + "'.";
					DialogResult dialogResult = MessageBox.Show(base.Parent, text, Resource.GetString("ApplicationName"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
					DialogResult dialogResult2 = dialogResult;
					if (dialogResult2 == DialogResult.Cancel)
					{
						return false;
					}
					switch (dialogResult2)
					{
					case DialogResult.Yes:
						this.document.Save();
						break;
					}
				}
				this.document.Dispose();
				this.document = null;
			}
			if (this.View.Controls.Count > 0)
			{
				Control control = this.View.Controls[0];
				control.Parent = null;
				control.Dispose();
			}
			return true;
		}
		private bool LoadFile(string url)
		{
			bool flag;
			if (this.FileClose())
			{
				this.document = new Document(this.commandManager, url);
				this.View.Controls.Add(this.document.HtmlControl);
				this.commandManager.AddTarget(this.document);
				this.fileHistory.AddFile(url);
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}
		private void About()
		{
			AboutDialog aboutDialog = new AboutDialog();
			aboutDialog.Run();
		}
		private void PublishSetting()
		{
			FtpConfigurationDialog ftpConfigurationDialog = new FtpConfigurationDialog();
			if (!this.ftpConfiguration.IsEmpty)
			{
				ftpConfigurationDialog.FtpServer = this.ftpConfiguration.Host;
				ftpConfigurationDialog.Directory = this.ftpConfiguration.Directory;
				ftpConfigurationDialog.Port = this.ftpConfiguration.Port;
				ftpConfigurationDialog.Username = this.ftpConfiguration.UserName;
				if (this.ftpConfiguration.Password != null || this.ftpConfiguration.Password.Length != 0)
				{
					ftpConfigurationDialog.StorePassword = true;
					ftpConfigurationDialog.Password = this.ftpConfiguration.Password;
				}
			}
			if (ftpConfigurationDialog.Run())
			{
				this.ftpConfiguration.Host = ftpConfigurationDialog.FtpServer;
				this.ftpConfiguration.UserName = ftpConfigurationDialog.Username;
				this.ftpConfiguration.Password = ftpConfigurationDialog.Password;
				this.ftpConfiguration.Directory = ftpConfigurationDialog.Directory;
				this.ftpConfiguration.Port = ftpConfigurationDialog.Port;
			}
		}
		private void Publish()
		{
			string text = this.ftpConfiguration.Password;
			if (text == null || text.Length == 0)
			{
				FtpPasswordDialog ftpPasswordDialog = new FtpPasswordDialog();
				if (!ftpPasswordDialog.Run())
				{
					return;
				}
				text = ftpPasswordDialog.Password;
			}
			try
			{
				this.document.Save();
				Cursor cursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				FtpConnection ftpConnection = new FtpConnection(this.ftpConfiguration.Host, this.ftpConfiguration.UserName, text, int.Parse(this.ftpConfiguration.Port));
				ftpConnection.ChangeDirectory(this.ftpConfiguration.Directory);
				ftpConnection.Upload(this.document.Url);
				ftpConnection.Disconnect();
				Cursor.Current = cursor;
				MessageBox.Show(base.Parent, "File Publised Successfully.", Resource.GetString("ApplicationName"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception ex)
			{
				MessageBox.Show(base.Parent, ex.Message, Resource.GetString("ApplicationName"));
			}
		}
		private CommandBarManager commandBarManager = new CommandBarManager();
		private CommandBar menuBar;
		private CommandBar toolBar;
		private Panel view = new Panel();
		private StatusBar statusBar = new StatusBar();
		private FileHistory fileHistory;
		private FtpConfiguration ftpConfiguration;
		private CommandManager commandManager;
		private ConfigurationManager configurationManager;
		public Document document = null;
	}
}
