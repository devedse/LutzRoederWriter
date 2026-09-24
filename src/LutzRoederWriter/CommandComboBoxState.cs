using System;
using Writer.Forms;

namespace Writer
{
	internal class CommandComboBoxState : CommandState
	{
		public CommandComboBoxState(string commandName)
			: base(commandName)
		{
		}
		public string Value
		{
			get
			{
				return (base.ActiveItem as CommandBarComboBox).Value;
			}
			set
			{
				foreach (object obj in base.Items)
				{
					CommandBarComboBox commandBarComboBox = (CommandBarComboBox)obj;
					commandBarComboBox.Value = value;
				}
			}
		}
	}
}
