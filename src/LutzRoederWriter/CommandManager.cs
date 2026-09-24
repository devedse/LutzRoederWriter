using System;
using System.Collections;
using System.Diagnostics;
using Writer.Forms;

namespace Writer
{
	/// <summary>
	/// Connects named commands to command-bar controls and dispatches command execution
	/// and status queries to registered <see cref="ICommandTarget"/> instances.
	/// </summary>
	/// <remarks>
	/// Targets are queried in reverse registration order. The most recently added target
	/// therefore gets the first opportunity to handle a command.
	/// </remarks>
	public class CommandManager
	{
		/// <summary>
		/// Associates a command name with a command-bar control.
		/// </summary>
		/// <param name="commandName">The stable command identifier used during dispatch.</param>
		/// <param name="control">The control that raises the command.</param>
		public void Add(string commandName, CommandBarControl control)
		{
			control.Click += this.CommandBarControl_Click;
			CommandState commandState = this.GetCommandState(commandName, control);
			commandState.AddItem(control);
			this.itemTable[control] = commandName;
		}
		/// <summary>
		/// Adds a command target at the front of the dispatch chain.
		/// </summary>
		/// <param name="target">The target to register.</param>
		public void AddTarget(ICommandTarget target)
		{
			this.targets.Insert(0, target);
		}
		/// <summary>
		/// Removes a command target from the dispatch chain.
		/// </summary>
		/// <param name="target">The target to remove.</param>
		public void RemoveTarget(ICommandTarget target)
		{
			this.targets.Remove(target);
		}
		/// <summary>
		/// Refreshes the visible and enabled state of every registered command.
		/// </summary>
		public void QueryStatus()
		{
			foreach (object obj in this.states)
			{
				CommandState commandState = (CommandState)obj;
				this.QueryStatus(commandState);
			}
		}
		private CommandState GetCommandState(string commandName, CommandBarControl control)
		{
			foreach (object obj in this.states)
			{
				CommandState commandState = (CommandState)obj;
				if (commandName == commandState.CommandName)
				{
					return commandState;
				}
			}
			CommandBarButton commandBarButton = control as CommandBarButton;
			CommandState commandState2;
			if (commandBarButton != null)
			{
				CommandButtonState commandButtonState = new CommandButtonState(commandName);
				this.states.Add(commandButtonState);
				commandState2 = commandButtonState;
			}
			else
			{
				CommandBarCheckBox commandBarCheckBox = control as CommandBarCheckBox;
				if (commandBarCheckBox != null)
				{
					CommandCheckBoxState commandCheckBoxState = new CommandCheckBoxState(commandName);
					this.states.Add(commandCheckBoxState);
					commandState2 = commandCheckBoxState;
				}
				else
				{
					CommandBarComboBox commandBarComboBox = control as CommandBarComboBox;
					if (commandBarComboBox == null)
					{
						throw new NotSupportedException();
					}
					CommandComboBoxState commandComboBoxState = new CommandComboBoxState(commandName);
					this.states.Add(commandComboBoxState);
					commandState2 = commandComboBoxState;
				}
			}
			return commandState2;
		}
		private void Execute(CommandState commandState)
		{
			Debug.WriteLine("Execute '" + commandState.CommandName + "'.");
			if (commandState.IsEnabled)
			{
				foreach (object obj in this.targets)
				{
					ICommandTarget commandTarget = (ICommandTarget)obj;
					if (commandTarget.Execute(commandState))
					{
						this.QueryStatus();
						break;
					}
				}
			}
		}
		private void QueryStatus(CommandState commandState)
		{
			foreach (object obj in this.targets)
			{
				ICommandTarget commandTarget = (ICommandTarget)obj;
				if (commandTarget.QueryStatus(commandState))
				{
					return;
				}
			}
			commandState.IsEnabled = false;
		}
		private void CommandBarControl_Click(object sender, EventArgs e)
		{
			CommandBarControl commandBarControl = sender as CommandBarControl;
			if (commandBarControl != null)
			{
				string text = (string)this.itemTable[commandBarControl];
				CommandState commandState = this.GetCommandState(text, commandBarControl);
				commandState.ActiveItem = commandBarControl;
				this.Execute(commandState);
			}
		}
		private Hashtable itemTable = new Hashtable();
		private ArrayList targets = new ArrayList();
		private ArrayList states = new ArrayList();
	}
}
