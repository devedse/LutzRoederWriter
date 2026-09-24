using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Writer.Forms
{
	public abstract class CommandBarButtonBase : CommandBarControl
	{
		protected CommandBarButtonBase(string text)
			: base(text)
		{
		}
		protected CommandBarButtonBase(Image image)
			: base(image)
		{
		}
		protected CommandBarButtonBase(Image image, string text)
			: base(image, text)
		{
		}
		public Keys Shortcut
		{
			get
			{
				return this.shortcut;
			}
			set
			{
				if (value != this.shortcut)
				{
					this.shortcut = value;
					this.OnPropertyChanged(new PropertyChangedEventArgs("Shortcut"));
				}
			}
		}
		private Keys shortcut = Keys.None;
	}
}
