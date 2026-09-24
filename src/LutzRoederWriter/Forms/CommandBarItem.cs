using System;
using System.ComponentModel;
using System.Drawing;

namespace Writer.Forms
{
	/// <summary>
	/// Base component for an item displayed by a <see cref="CommandBar"/>.
	/// </summary>
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class CommandBarItem : Component
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private CommandBarItem()
		{
		}
		public CommandBarItem(string text)
		{
			this.text = text;
		}
		public CommandBarItem(Image image)
		{
			this.image = image;
		}
		public CommandBarItem(Image image, string text)
		{
			this.text = text;
			this.image = image;
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.image = null;
				this.text = null;
			}
			base.Dispose(disposing);
		}
		public virtual object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}
		public virtual bool Visible
		{
			get
			{
				return this.isVisible;
			}
			set
			{
				if (value != this.isVisible)
				{
					this.isVisible = value;
					this.OnPropertyChanged(new PropertyChangedEventArgs("IsVisible"));
				}
			}
		}
		public virtual bool Enabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				if (this.isEnabled != value)
				{
					this.isEnabled = value;
					this.OnPropertyChanged(new PropertyChangedEventArgs("IsEnabled"));
				}
			}
		}
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				if (value != this.image)
				{
					this.image = value;
					this.OnPropertyChanged(new PropertyChangedEventArgs("Image"));
				}
			}
		}
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value != this.text)
				{
					this.text = value;
					this.OnPropertyChanged(new PropertyChangedEventArgs("Text"));
				}
			}
		}
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, e);
			}
		}
		private Image image = null;
		private string text = null;
		private bool isEnabled = true;
		private bool isVisible = true;
		private object tag;
	}
}
