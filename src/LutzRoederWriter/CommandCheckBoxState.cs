using System;
using Writer.Forms;

namespace Writer
{
	internal class CommandCheckBoxState : CommandState
	{
		public CommandCheckBoxState(string commandName)
			: base(commandName)
		{
		}
		public bool IsChecked
		{
			get
			{
				return (base.ActiveItem as CommandBarCheckBox).IsChecked;
			}
			set
			{
				foreach (object obj in base.Items)
				{
					CommandBarCheckBox commandBarCheckBox = (CommandBarCheckBox)obj;
					commandBarCheckBox.IsChecked = value;
				}
			}
		}
	}
}
